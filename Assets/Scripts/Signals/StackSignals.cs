using Enums;
using Extentions;
using System;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public Func<int> onGetStackRemainPlace = delegate { return 0; };
     
    }
}