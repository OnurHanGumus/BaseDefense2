using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;

namespace Controllers
{
    public class StorePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private UIPanels releatedPanel;
        [SerializeField] private MeshRenderer meshRenderer;

        #endregion
        #region
        private Material _material;

        #endregion
        #endregion
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _material = GetMaterial();
            meshRenderer.material = _material;

        }
        public virtual Material GetMaterial() => Resources.Load<Material>("Materials/TurretFloor/" + (LevelSignals.Instance.onGetCurrentModdedLevel() + 1).ToString());

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //UISignals.Instance.onInitializeGunLevels?.Invoke(UISignals.Instance.onGetGunLevels());
                UISignals.Instance.onOpenStorePanel?.Invoke(releatedPanel);
                return;
            }
    
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //UISignals.Instance.onInitializeGunLevels?.Invoke(UISignals.Instance.onGetGunLevels());
                UISignals.Instance.onCloseStorePanel?.Invoke(releatedPanel);
                return;
            }

        }
    }
}