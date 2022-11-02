using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using Data.ValueObject;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Controllers
{
    public class PlayerPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;

        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private HealthBarManager healthBarManager;


        #endregion
        #region Private Variables
        private PlayerData _data;
        private int _health = 100;
        private int _healtLevel = 1;
        [ShowInInspector] private int _maxHealth = 100;
        private bool _isHealing = false;
        #endregion
        #endregion

        public int Health
        {
            get { return _health; }
            set { _health = value;
                
                if (_health > _maxHealth)
                {
                    _maxHealth = _health;
                }
                healthBarManager.HealthText.text = Health.ToString();
                healthBarManager.SetHealthBarScale(_health, _maxHealth);
            }
        }


        private void Start()
        {
            _data = manager.GetPlayerData();
            SetHealth();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") || other.CompareTag("Ammo"))
            {
                PlayerSignals.Instance.onInteractionCollectable?.Invoke(other.gameObject);
           
                return;
            }

            if (other.CompareTag("BaseTrigger"))
            {
                PlayerOnBase();
                if (_isHealing || Health >= _maxHealth)
                {
                    return;
                }
                StartCoroutine(StartHealing());
                return;
            }

            if (other.CompareTag("TurretAmmoArea"))
            {
                PlayerSignals.Instance.onPlayerReachTurretAmmoArea?.Invoke(other.gameObject);
                return;
            }

            if (other.CompareTag("OutTrigger"))
            {
                manager.SetAnimBool(PlayerAnimStates.Base, false);
                manager.IsOnBase = false;
                StopAllCoroutines();
                _isHealing = false;
                PlayerSignals.Instance.onPlayerLeaveBase?.Invoke();

                ChangeColliderActiveness(true);

                return;
            }

            if (other.CompareTag("Damage"))
            {
                if (manager.IsPlayerDead)
                {
                    return;
                }
                Health -= 10;
                Debug.Log(Health);
                if (Health <= 0)
                {
                    manager.IsPlayerDead = true;
                    manager.SetAnimState(PlayerAnimStates.Die);
                    //PlayerSignals.Instance.onPlayerDie?.Invoke();
                    StartCoroutine(PlayerRespawn());
                }
                return;
            }

            if (other.CompareTag("MineEnter"))
            {
                int mineRemainCapacity = LevelSignals.Instance.onGetMineRemainCapacity();
                if (mineRemainCapacity.Equals(0))
                {
                    return;
                }
                if (mineRemainCapacity >= manager.RescuePersonList.Count)
                {
                    PlayerSignals.Instance.onPlayerInMineArea?.Invoke(other.transform);
                    LevelSignals.Instance.onMinerCountIncreased?.Invoke(manager.RescuePersonList.Count);
                    manager.RescuePersonList.Clear();
                }
                else
                {
                    LevelSignals.Instance.onMinerCountIncreased?.Invoke(mineRemainCapacity);

                    for (int i = 0; i < mineRemainCapacity; i++)
                    {

                        PlayerSignals.Instance.onPlayerInMineAreaLowCapacity?.Invoke(manager.RescuePersonList[manager.RescuePersonList.Count - 1], other.transform);
                        manager.RescuePersonList.RemoveAt(manager.RescuePersonList.Count - 1);

                    }
                }
                return;
            }

            if (other.CompareTag("MilitaryEnter"))
            {
                int militaryRemainCapacity = LevelSignals.Instance.onGetMilitaryTotalCapacity();
                if (militaryRemainCapacity.Equals(0))
                {
                    return;
                }
                if (militaryRemainCapacity >= manager.RescuePersonList.Count)
                {
                    PlayerSignals.Instance.onPlayerInMilitaryArea?.Invoke();
                    LevelSignals.Instance.onMilitaryPopulationIncreased?.Invoke(manager.RescuePersonList.Count);
                    manager.RescuePersonList.Clear();

                }
                else
                {
                    //int remainPlace = manager.RescuePersonList.Count - mineRemainCapacity;
                    Debug.Log(militaryRemainCapacity);

                    LevelSignals.Instance.onMilitaryPopulationIncreased?.Invoke(militaryRemainCapacity);

                    for (int i = 0; i < militaryRemainCapacity; i++)
                    {

                        PlayerSignals.Instance.onPlayerInMilitaryAreaLowCapacity?.Invoke(manager.RescuePersonList[manager.RescuePersonList.Count - 1]);
                        manager.RescuePersonList.RemoveAt(manager.RescuePersonList.Count - 1);

                    }
                }


                return;
            }
            if (other.CompareTag("GemArea"))
            {
                int gemCount = PlayerSignals.Instance.onGetGems();
                ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Gem, gemCount);
                //Destroy(other.gameObject, 0.5f);
            }

            if (other.CompareTag("Portal"))
            {
                PlayerSignals.Instance.onPlayerReachNewBase?.Invoke();
                manager.SetAnimBool(PlayerAnimStates.Base, true);
                manager.IsOnBase = true;
                ChangeColliderActiveness(false);
                return;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Portal"))
            {
                CoreGameSignals.Instance.onNextLevel?.Invoke();
                LevelSignals.Instance.onPlayerReachedToNewBase?.Invoke();
                return;
            }
        }
        public void OnGetHealthData(List<int> upgradeList)
        {
            if (upgradeList.Count < 3)
            {
                upgradeList = new List<int>() { 0, 0, 0 };
            }
            _healtLevel = upgradeList[2];
        }

        private void SetHealth()
        {
            Health = _data.Health + (10 * _healtLevel);

        }

        private IEnumerator PlayerRespawn()
        {
            yield return new WaitForSeconds(0.5f);
            PlayerSignals.Instance.onPlayerDie?.Invoke();

            yield return new WaitForSeconds(1.0f);
            manager.IsPlayerDead = false;
            PlayerSignals.Instance.onPlayerSpawned?.Invoke();
            PlayerOnBase();
            SetHealth();
            transform.parent.position = new Vector3(0, 0.4f, 0);
            manager.SetAnimState(PlayerAnimStates.Idle);
        }

        private void ChangeColliderActiveness(bool state)
        {
            boxCollider.enabled = state;
        }

        public void OnGetHealthLevel(List<int> upgradeList)
        {
            if (upgradeList.Count < 3)
            {
                upgradeList = new List<int>() { 0, 0, 0 };
            }
            _healtLevel = upgradeList[2];
            SetHealth();
        }

        private IEnumerator StartHealing()
        {
            _isHealing = true;
            yield return new WaitForSeconds(0.3f);
            if (Health >= _maxHealth)
            {
                _isHealing = false;

                StopAllCoroutines();
            }
            else
            {
                Health += 1;
                StartCoroutine(StartHealing());
            }

        }

        private void PlayerOnBase()
        {
            PlayerSignals.Instance.onPlayerReachBase?.Invoke();

            manager.SetAnimBool(PlayerAnimStates.Base, true);
            manager.IsOnBase = true;
            ChangeColliderActiveness(false);
        }
    }
}