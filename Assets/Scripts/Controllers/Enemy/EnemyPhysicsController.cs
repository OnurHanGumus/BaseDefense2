using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using Data.ValueObject;
using Sirenix.OdinInspector;
using Data.UnityObject;

namespace Controllers
{
    public class EnemyPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private EnemyManager manager;

        #endregion
        #region Private Variables
        private EnemyData _data;
        private AllGunsData _gunData;
        [ShowInInspector] private int _health = 100;
        private int _damage = 25;
        
        #endregion
        #endregion

        private void Start()
        {
            _data = manager.GetEnemyData();
            _health = _data.Health;
            _gunData = GetData();
            _damage = GetDamageNumber();
        }
        private AllGunsData GetData() => Resources.Load<CD_Gun>("Data/CD_Gun").Data;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                _health -= _damage;

                if (_health <= 0)
                {
                    manager.DieState(other.attachedRigidbody.velocity);
                    PlayerSignals.Instance.onEnemyDie?.Invoke(manager.transform);
                }
            }
            else if (other.CompareTag("TurretBullet"))
            {
                int damage = 60;
                _health -= damage;

                if (_health <= 0)
                {
                    manager.DieState(other.attachedRigidbody.velocity);
                    PlayerSignals.Instance.onEnemyDie?.Invoke(manager.transform);
                }
            }

            
        }

        public void ResetData()
        {
            _health = _data.Health;
        }

        private int GetDamageNumber()
        {
            return _gunData.guns[SaveSignals.Instance.onGetSelectedGun()].StartDamage;
        }
    }
}