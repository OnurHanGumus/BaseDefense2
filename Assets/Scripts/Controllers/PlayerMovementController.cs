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


        #endregion

        #region Private Variables
        private Rigidbody _rig;
        private PlayerManager2 _manager;
        private float _xValue, _zValue;
        private PlayerData _data;
        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
            _manager = GetComponent<PlayerManager2>();
            _data = _manager.GetPlayerData();
        }


        private void FixedUpdate()
        {
            _rig.velocity = new Vector3(_xValue * _data.Speed, 0, _zValue * _data.Speed);

            if (_rig.velocity != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(_rig.velocity, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,
                                _data.RotationSpeed * Time.fixedDeltaTime);
            }
        }

        public void OnInputDragged(InputParams param)
        {
            _xValue = param.XValue;
            _zValue = param.ZValue;
        }
    }
}