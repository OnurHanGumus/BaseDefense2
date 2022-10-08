using Cinemachine;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public CameraStates CameraStateController
        {
            get => _cameraStateValue;
            set
            {
                _cameraStateValue = value;
                SetCameraStates();
            }
        }
        
        #endregion
        #region Serialized Variables
        
        [SerializeField]private CinemachineVirtualCamera turretCamera;
        [SerializeField]private CinemachineVirtualCamera idleCamera;

        #endregion

        #region Private Variables
        
        private Vector3 _initialPosition;
        private CameraStates _cameraStateValue = CameraStates.IdleCam;
        private Animator _camAnimator;
        
        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            GetInitialPosition();
        }

        private void GetReferences()
        {
            idleCamera = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
            turretCamera = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
            _camAnimator = GetComponent<Animator>();
        }
        
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerUseTurret += OnPlayerUseTurret;
            LevelSignals.Instance.onBossDefeated += OnBossDefeated;
            UISignals.Instance.onMoveOnAfterSuccessfulPanel += OnAfterBossDefeated;

        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerUseTurret -= OnPlayerUseTurret;
            LevelSignals.Instance.onBossDefeated -= OnBossDefeated;
            UISignals.Instance.onMoveOnAfterSuccessfulPanel -= OnAfterBossDefeated;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void Start()
        {
            
        }
        private void SetCameraStates()
        {
            if (CameraStateController == CameraStates.IdleCam)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
            else if (CameraStateController == CameraStates.TurretCam)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
            else if (CameraStateController == CameraStates.LevelComplatedCam)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
        }
        
        private void GetInitialPosition()
        {
            _initialPosition = turretCamera.transform.localPosition;
        }

        private void OnMoveToInitialPosition()
        {
            turretCamera.transform.localPosition = _initialPosition;
        }

        private void ChangeGameState(CameraStates cameraState)
        {
            CameraStateController = cameraState;
            SetCameraStates();
        }
        private void OnLevelSuccessful()
        {
            CameraStateController = CameraStates.LevelComplatedCam;
        }

        private void OnPlayerUseTurret(bool value)
        {
            if (value.Equals(true))
            {
                ChangeGameState(CameraStates.TurretCam);

            }
            else
            {
                ChangeGameState(CameraStates.IdleCam);

            }
        }

        private void OnBossDefeated()
        {
            ChangeGameState(CameraStates.LevelComplatedCam);

        }

        private void OnAfterBossDefeated()
        {
            ChangeGameState(CameraStates.IdleCam);

        }


    }
}