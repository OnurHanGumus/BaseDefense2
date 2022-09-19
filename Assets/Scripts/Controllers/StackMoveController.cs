using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Controllers
{
    public class StackMoveController
    {
        #region Self Variables

        #region Private Veriables

        private StackData _stackData;
        #endregion
        #endregion

        public void InisializedController(StackData Stackdata)
        {
            _stackData = Stackdata;
        }

        public void StackItemsMoveOrigin(Vector3 direction,List<GameObject> _collectableStack)
        {
          
            



           //transform.localPosition = new Vector3(_collectableStack[0].transform.localPosition.x, _collectableStack[0].transform.localPosition.y, direction.z);
            //transform.LookAt(direction);

        }

    }
}