using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Signals;

public class MoneyController : MonoBehaviour
{
    #region Self Variables

    #region Public Variables


    #endregion

    #region Serialized Variables


    #endregion

    #region Private Variables
    private Rigidbody _rig;
    private BoxCollider _col;

    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _rig = GetComponent<Rigidbody>();
        _col = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        Jump();
        _col.enabled = false;
        transform.tag = "Collectable";

    }

    private void OnDisable()
    {
        _col.enabled = false;
    }



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

}
