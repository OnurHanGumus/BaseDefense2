using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private Transform rotatedSpine;
        [SerializeField] private PlayerAimingController aimController;

        #endregion

        #region Private Variables
        private Rigidbody _rig;
        private PlayerManager _manager;
        private float _xValue, _zValue;
        private PlayerData _data;
        private bool _isPlayerUseTurret = false;


        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
            _manager = GetComponent<PlayerManager>();
            _data = _manager.GetPlayerData();
        }


        private void FixedUpdate()
        {
            if (_isPlayerUseTurret)
            {
                return;
            }
            _rig.velocity = new Vector3(_xValue * _data.Speed, 0, _zValue * _data.Speed);

            Quaternion quat = rotatedSpine.localRotation;

            if (quat.y <= -0.6f)
            {
                transform.Rotate(Vector3.up, -2f);

                //rotatedSpine.Rotate(Vector3.up, 2f);
                return;
            }
            else if (quat.y >= 0.6f)
            {
                transform.Rotate(Vector3.up, 2f);
                //rotatedSpine.Rotate(Vector3.up, -2f);
                return;
            }

            if (_rig.velocity != Vector3.zero && aimController.TargetList.Count == 0)
            {
                Quaternion toRotation = Quaternion.LookRotation(_rig.velocity, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,
                                _data.RotationSpeed);
            }
        }

        public void OnInputDragged(InputParams param)
        {
            _xValue = param.XValue;
            _zValue = param.ZValue;
        }

        public void OnPlayerUseTurret(bool value)
        {
            _isPlayerUseTurret = value;
            if (value.Equals(true))
            {
                _rig.velocity = Vector3.zero;

            }

        }
        public void OnPlayerLeaveTurret()
        {
            _isPlayerUseTurret = false;
        }

        public float OnGetPlayerSpeed()
        {
            return _data.Speed;
        }
    }
}