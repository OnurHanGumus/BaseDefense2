using Controllers;
using Enums;
using Signals;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController uiPanelController;
        [SerializeField] private TextMeshProUGUI moneyText, gemText;
        //[SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private GameObject holder;
        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onOpenStorePanel += OnOpenStorePanel;
            UISignals.Instance.onCloseStorePanel += OnCloseStorePanel;
            //UISignals.Instance.onUpdateStageData += OnUpdateStageData;
            //UISignals.Instance.onSetLevelText += OnSetLevelText;
            CoreGameSignals.Instance.onPlay += OnPlay;
            //CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;

            UISignals.Instance.onSetChangedText += OnChangeCollectableAmounts;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onOpenStorePanel -= OnOpenStorePanel;
            UISignals.Instance.onCloseStorePanel -= OnCloseStorePanel;
            //UISignals.Instance.onUpdateStageData -= OnUpdateStageData;
            //UISignals.Instance.onSetLevelText -= OnSetLevelText;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            //CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;

            UISignals.Instance.onSetChangedText -= OnChangeCollectableAmounts;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenPanel(panelParam);
        }

        

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.ClosePanel(panelParam);
        }
        private void OnOpenStorePanel(UIPanels panelParam)
        {
            uiPanelController.OpenStoreMenu(panelParam);
        }
        private void OnCloseStorePanel(UIPanels panelParam)
        {
            uiPanelController.CloseStoreMenu(panelParam);
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
        }

        //private void OnLevelFailed()
        //{
        //    UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
        //    UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        //}

        private void OnLevelSuccessful()
        {
            //UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            //UISignals.Instance.onOpenPanel?.Invoke(UIPanels.WinPanel);
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            //UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            //UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void BuyArea(int id)
        {
            LevelSignals.Instance.onBuyArea?.Invoke(id);
        }

        public void BuyTurret(int id)
        {
            LevelSignals.Instance.onBuyTurret?.Invoke(id);
        }

        public void BuyTurretOwner(int id)
        {
            LevelSignals.Instance.onBuyTurretOwners?.Invoke(id);
        }

        public void BuyEnemyArea(int id)
        {
            LevelSignals.Instance.onBuyEnemyArea?.Invoke(id);
        }

        public void OnChangeCollectableAmounts(ScoreTypeEnums type, int totalAmount)
        {

            if (type.Equals(ScoreTypeEnums.Money))
            {
                moneyText.text = totalAmount.ToString();
            }
            else
            {
                gemText.text = totalAmount.ToString();
            }
        }

        public void SelectGun(int id)
        {
            PlayerSignals.Instance.onPlayerSelectGun?.Invoke(id);
        }

        public void BossIsDead()
        {
            LevelSignals.Instance.onBossDefeated?.Invoke();
        }



    }
}