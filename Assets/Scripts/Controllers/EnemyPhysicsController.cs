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
    public class EnemyPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private EnemyManager manager;
        [SerializeField] private int pistolDamage = 25, shotgunDamage = 50, smgDamage = 20, assaultDamage = 40, rocketDamage = 100, minigunDamage = 80;


        #endregion
        #region Private Variables
        private EnemyData _data;
        [ShowInInspector] private int _health = 100, 
            _getDamageAmount = 50;
        #endregion
        #endregion

        private void Start()
        {
            _data = manager.GetEnemyData();
            _health = _data.Health;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PistolBullet"))
            {
                _health -= pistolDamage;

                if (_health <= 0)
                {
                    manager.DieState(other.attachedRigidbody.velocity);
                    PlayerSignals.Instance.onEnemyDie?.Invoke(manager.transform);
                }
            }
            else if (other.CompareTag("ShotgunBullet"))
            {
                _health -= shotgunDamage;

                if (_health <= 0)
                {
                    manager.DieState(other.attachedRigidbody.velocity);
                    PlayerSignals.Instance.onEnemyDie?.Invoke(manager.transform);
                }
            }
            else if (other.CompareTag("SMGBullet"))
            {
                _health -= smgDamage;

                if (_health <= 0)
                {
                    manager.DieState(other.attachedRigidbody.velocity);
                    PlayerSignals.Instance.onEnemyDie?.Invoke(manager.transform);
                }
            }
            else if (other.CompareTag("AssaultBullet"))
            {
                _health -= assaultDamage;

                if (_health <= 0)
                {
                    manager.DieState(other.attachedRigidbody.velocity);
                    PlayerSignals.Instance.onEnemyDie?.Invoke(manager.transform);
                }
            }
            else if (other.CompareTag("RocketBullet"))
            {
                _health -= rocketDamage;

                if (_health <= 0)
                {
                    manager.DieState(other.attachedRigidbody.velocity);
                    PlayerSignals.Instance.onEnemyDie?.Invoke(manager.transform);
                }
            }
            else if (other.CompareTag("MinigunBullet"))
            {
                _health -= minigunDamage;

                if (_health <= 0)
                {
                    manager.DieState(other.attachedRigidbody.velocity);
                    PlayerSignals.Instance.onEnemyDie?.Invoke(manager.transform);
                }
            }
        }

    }
}