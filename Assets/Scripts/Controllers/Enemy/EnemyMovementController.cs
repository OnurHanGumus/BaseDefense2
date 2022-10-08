using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class EnemyMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables


        #endregion

        #region Private Variables
        private Rigidbody _rig;
        private EnemyManager _manager;
        private EnemyData _data;
        private CapsuleCollider _col;
        private float _speed = 5f;

        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
            _col = GetComponent<CapsuleCollider>();
            _manager = GetComponent<EnemyManager>();
        }

        private void Start()
        {
            _data = _manager.GetEnemyData();
            _speed = _data.Speed;

        }

        public void ChasePlayer(Vector3 direction, Transform lookAtObject)
        {
            direction = new Vector3(direction.x, 0, direction.z);
            Vector3 tarpos = new Vector3(lookAtObject.position.x, 0, lookAtObject.position.z);
            _rig.velocity = direction * _speed;
            if (_rig.velocity != Vector3.zero)
            {
                transform.LookAt(tarpos);
            }
        }

        public void MoveToDefaultTarget(Vector3 direction, Transform lookAtObject)
        {
            direction = new Vector3(direction.x, 0, direction.z);
            Vector3 tarpos = new Vector3(lookAtObject.position.x, 0, lookAtObject.position.z);
            _rig.velocity = direction * _speed;
            if (_rig.velocity != Vector3.zero)
            {
                transform.LookAt(tarpos);
            }
        }

        public void DeathMove(Vector3 dieDirection)
        {
            _rig.velocity = Vector3.zero;
            _rig.AddForce(dieDirection * 500);
        }
    }
}