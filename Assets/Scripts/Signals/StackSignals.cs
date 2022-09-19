using Enums;
using Extentions;
using System;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public Func<int> onGetStackCount = delegate { return 0; };
     
    }
}