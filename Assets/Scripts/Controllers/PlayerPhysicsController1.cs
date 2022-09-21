using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using Data.ValueObject;
using Sirenix.OdinInspector;

namespace Controllers
{
    public class PlayerPhysicsController1 : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager2 manager;



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
                return;
            }
            if (other.CompareTag("OutTrigger"))
            {
                manager.SetAnimBool(PlayerAnimStates.Base, false);
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

        public void OnGetHealthData(int healthLevel)
        {
            _healtLevel = healthLevel + 1;
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
    }
}