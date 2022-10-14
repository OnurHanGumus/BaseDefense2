using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Signals;

public class MoneyManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables


    #endregion

    #region Serialized Variables


    #endregion

    #region Private Variables
    private BoxCollider _col;

    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _col = GetComponent<BoxCollider>();
    }
    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
        Jump();
        _col.enabled = false;
        transform.tag = "Collectable";
    }

    private void SubscribeEvents()
    {
        UISignals.Instance.onCloseSuccessfulPanel += OnCloseSuccessfulPanel;
    }

    private void UnsubscribeEvents()
    {
        UISignals.Instance.onCloseSuccessfulPanel -= OnCloseSuccessfulPanel;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
        _col.enabled = false;

    }

    #endregion





    private void Jump()
    {
        transform.position = new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y, transform.position.z + Random.Range(-5f, 5f));


       transform.DOJump(new Vector3(transform.position.x, 1, transform.position.z), 10, 1, 0.5f);
       transform.DORotateQuaternion(Quaternion.Euler(new Vector3(Random.Range(0f, 90f), Random.Range(0f, 90f), Random.Range(0f, 90f))), 0.5f).OnComplete(SetColliderActive);

    }

    private void SetColliderActive()
    {
        _col.enabled = true;
    }


    private void OnCloseSuccessfulPanel(bool isTrue)
    {
        if (isTrue)
        {
            ScoreSignals.Instance.onScoreIncrease?.Invoke(Enums.ScoreTypeEnums.Money, 1);
        }
        gameObject.SetActive(false);
    }
}
