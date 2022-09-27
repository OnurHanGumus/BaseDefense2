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
    public class PlayerPhysicsController1 : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager2 manager;

        [SerializeField] private BoxCollider boxCollider;


        #endregion
        #region Private Variables
        private PlayerData _data;
        private int _health = 100;
        private int _healtLevel = 1;
        #endregion
        #endregion


        private void Start()
        {
            _data = manager.GetPlayerData();
            SetHealth();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                PlayerSignals.Instance.onInteractionCollectable?.Invoke(other.gameObject);
                return;
            }
            if (other.CompareTag("BaseTrigger"))
            {
                PlayerSignals.Instance.onPlayerReachBase?.Invoke();
                manager.SetAnimBool(PlayerAnimStates.Base, true);
                manager.IsOnBase = true;
                ChangeColliderActiveness(false);
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

                ChangeColliderActiveness(true);

                return;
            }
            if (other.CompareTag("Damage"))
            {
                if (manager.IsPlayerDead)
                {
                    return;
                }
                _health -= 10;
                Debug.Log(_health);
                if (_health <= 0)
                {
                    manager.IsPlayerDead = true;
                    manager.SetAnimState(PlayerAnimStates.Die);
                    PlayerSignals.Instance.onPlayerDie?.Invoke();
                    StartCoroutine(PlayerRespawn());
                }
                return;
            }
        }

        public void OnGetHealthData(List<int> upgradeList)
        {
            if (upgradeList.Count < 3)
            {
                upgradeList = new List<int>() { 0, 0, 0 };
            }
            _healtLevel = upgradeList[0] + 1;
        }

        private void SetHealth()
        {
            _health = _data.Health + (10 * _healtLevel);

        }

        private IEnumerator PlayerRespawn()
        {
            yield return new WaitForSeconds(1.5f);
            manager.IsPlayerDead = false;
            PlayerSignals.Instance.onPlayerSpawned?.Invoke();
            SetHealth();
            transform.parent.position = new Vector3(0, 0.4f, 0);
            manager.SetAnimState(PlayerAnimStates.Idle);
        }

        private void ChangeColliderActiveness(bool state)
        {
            boxCollider.enabled = state;
        }
    }
}