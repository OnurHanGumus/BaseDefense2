using System.Collections.Generic;
using Managers;
using Signals;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

namespace Commands
{
    public class ItemRemoveOnStackCommand: MonoBehaviour
    {
        #region Self Variables
        #region Private Variables
        private List<GameObject> _collectableStack;
        private GameObject _levelHolder;
        #endregion
        #endregion
        
        public ItemRemoveOnStackCommand(ref List<GameObject> CollectableStack,ref GameObject levelHolder)
        {
            _collectableStack = CollectableStack;
            _levelHolder = levelHolder;
        }
        public void Execute(GameObject collectableGameObject)
        {
            int index = _collectableStack.IndexOf(collectableGameObject);
            collectableGameObject.transform.SetParent(_levelHolder.transform.GetChild(0));
            collectableGameObject.SetActive(false);
            _collectableStack.RemoveAt(index);
            _collectableStack.TrimExcess();
     
            if (index==0)
            {
                //ScoreSignals.Instance.onSetLeadPosition?.Invoke(_collectableStack[0]);
            }
        }

        //public void ExecuteAllItems()
        //{
        //    for (int i = 0; i < _collectableStack.Count; i++)
        //    {
        //        int index = _collectableStack.Count - 1 - i;
        //        //_collectableStack[index].transform.DOLocalMove(new Vector3(0, _collectableStack[index].transform.position.y + 5, 10), 1f).SetD.OnComplete(() => RemoveItem(_collectableStack[index].gameObject)).SetEase(Ease.InOutBack);
        //        RemoveItem(_collectableStack[index].gameObject);
        //    }
        //}

        //private void RemoveItem(GameObject collectable)
        //{
        //    Destroy(collectable.gameObject);
        //    _collectableStack.Remove(collectable);

        //}
    }
}