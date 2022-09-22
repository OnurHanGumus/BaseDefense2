using System.Collections.Generic;
using Enums;
using Managers;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> panels;

        #endregion

        #endregion

        public void OpenPanel(UIPanels panelParam)
        {
            //panels[(int) panelParam].SetActive(true);
        }

        public void ClosePanel(UIPanels panelParam)
        {
            //panels[(int) panelParam].SetActive(false);
        }

        public void OpenStoreMenu(UIPanels storeMenu)
        {
            Debug.Log((int)storeMenu);
            panels[(int)storeMenu].GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
            panels[(int)storeMenu].GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        public void CloseStoreMenu(UIPanels storeMenu)
        {
            panels[(int)storeMenu].GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
            panels[(int)storeMenu].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}