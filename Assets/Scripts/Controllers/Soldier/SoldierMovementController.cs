using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Keys;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class SoldierMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables


        #endregion

        #region Private Variables
        private Rigidbody _rig;
        private SoldierManager _manager;
        private SoldierData _data;
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
            _manager = GetComponent<SoldierManager>();
            _data = _manager.GetSoldierData();
            _speed = _data.Speed;
        }

        private void Start()
        {
            

        }

        public void MoveToTarget(Vector3 direction, Transform lookAtObject)
        {
            direction = new Vector3(direction.x, 0, direction.z);

            Vector3 tarpos = new Vector3(lookAtObject.position.x, 0, lookAtObject.position.z);
            _rig.velocity = direction * _speed;
            if (_rig.velocity != Vector3.zero)
            {
                transform.LookAt(tarpos);
            }
        }

        public void Idle()
        {
            _rig.velocity = Vector3.zero;
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

        public void Aim(Transform lookAtObject)
        {
            _rig.velocity = Vector3.zero;
            transform.LookAt(lookAtObject);

        }
    }
}