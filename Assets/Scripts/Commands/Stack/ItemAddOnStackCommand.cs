using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject;
using Signals;
using DG.Tweening;

namespace Commands
{
    public class ItemAddOnStackCommand
    {
        #region Self Variables
        #region Private Variables
        private List<GameObject> _collectableStack;
        private Transform _transform;
        private StackData _stackData;
        #endregion
        #endregion
        
        public ItemAddOnStackCommand(ref List<GameObject> collectableStack, Transform transform, StackData stackData)
        {
            _collectableStack = collectableStack;
            _transform = transform;
            _stackData = stackData;
        }
        
        public void Execute(GameObject _collectableGameObject)
        {
            _collectableGameObject.transform.SetParent(_transform);
            Vector3 newPos = new Vector3(0, _stackData.OffsetY * _collectableStack.Count, _stackData.OffsetZ);

            _collectableGameObject.transform.DOLocalMove(newPos, 1f).OnComplete(() => SetPosition(_collectableGameObject, newPos)).SetEase(Ease.InOutBack);
            _collectableGameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _collectableStack.Add(_collectableGameObject);
        }

        private void SetPosition(GameObject _collectableGameObject, Vector3 newPos)
        {
            _collectableGameObject.transform.localPosition = newPos;

        }
    }
}