using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel;
        public UnityAction<UIPanels> onClosePanel;
        public UnityAction<int> onUpdateStageData;
        public UnityAction<int> onSetLevelText;

        public UnityAction<ScoreTypeEnums,int> onSetChangedText;
    }
}