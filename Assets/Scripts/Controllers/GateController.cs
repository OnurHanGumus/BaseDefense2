using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateController : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables
    [SerializeField] private Transform gate;
    [SerializeField] private int enteredCount = 0;


    #endregion

    #region Private Variables
    private Tween _closeTween;
    #endregion

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MoneyWorker"))
        {
            if (_closeTween!= null)
            {
                _closeTween.Kill();

            }
            enteredCount++;
            Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MoneyWorker"))
        {
            enteredCount--;

            if (enteredCount > 0)
            {
                return;
            }
            Close();
        }
    }

    private void Open()
    {
        gate.DORotate(new Vector3(0,0,-90), 0.4f).SetEase(Ease.InOutBack);
    }

    private void Close()
    {
        _closeTween = gate.DORotate(new Vector3(0, 0, 0), 0.4f).SetEase(Ease.InOutBack);

    }



}
