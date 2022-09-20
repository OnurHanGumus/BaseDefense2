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
            if (other.CompareTag("Bullet"))
            {
                _health -= _getDamageAmount;

                if (_health <= 0)
                {
                    manager.DieState(other.attachedRigidbody.velocity);
                    PlayerSignals.Instance.onEnemyDie?.Invoke(manager.transform);
                }
            }
        }

    }
}