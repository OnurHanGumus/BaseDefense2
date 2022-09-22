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
        private int _carryLevel = 2; //save'e 0 olarak kaydediliyor. Buraya gelince hemen 1 arttırılıyor.
        private int _maxHeight = 10;
        private int _currentTowerCount = 1;
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
            if (_carryLevel < _currentTowerCount)
            {
                return;
            }
            _collectableGameObject.tag = "Collected";

            _collectableGameObject.transform.SetParent(_transform);
            Vector3 newPos;

            newPos = new Vector3(0, _stackData.OffsetY * (_collectableStack.Count % _maxHeight), _stackData.OffsetZ * _currentTowerCount);



            _collectableGameObject.transform.DOLocalMove(newPos, 1f).OnComplete(() => SetPosition(_collectableGameObject, newPos)).SetEase(Ease.InOutBack);
            _collectableGameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _collectableStack.Add(_collectableGameObject);
            _currentTowerCount = (_collectableStack.Count / _maxHeight) + 1;
        }

        private void SetPosition(GameObject _collectableGameObject, Vector3 newPos)
        {
            _collectableGameObject.transform.localPosition = newPos;

        }
        public void ResetTowerCount()
        {
            _currentTowerCount = 1;
        }

        public void OnGetCarryLevel(List<int> upgradeList)
        {
            if (upgradeList.Count < 3)
            {
                upgradeList = new List<int>() { 0, 0, 0 };
            }
            _carryLevel = upgradeList[0] + 1;
        }
    }
}