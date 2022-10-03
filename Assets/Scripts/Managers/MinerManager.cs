using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Controllers;

public class MinerManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables
    [SerializeField] private GameObject[] minesOnScene;
    [SerializeField] private Transform selectedMine;
    [SerializeField] private Transform gemArea;
    #endregion

    #region Private Variables

    private MinerAnimationController _animationController;
    #endregion

    #endregion

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        minesOnScene = GameObject.FindGameObjectsWithTag("CollectAmmoArea");
        gemArea = GameObject.FindGameObjectWithTag("GemArea").transform;

        SelectRandomMine();
    }

    private void Start()
    {


    }

    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    { 

    }

    private void UnsubscribeEvents()
    {


    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void SelectRandomMine()
    {
        selectedMine = minesOnScene[Random.Range(0, minesOnScene.Length)].transform;
        MoveToSelectedMine();
    }

    private void MoveToSelectedMine()
    {
        transform.DOMove(selectedMine.position, 5f).SetSpeedBased(true);
    }

    public void Work()// animation type as parameter
    {
        //_animationController.SetAnimState();
        StartCoroutine(WorkCoroutine());
    }

    private IEnumerator WorkCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        ReturnGemToArea();
    }

    private void ReturnGemToArea()
    {
        transform.DOMove(gemArea.position, 5f).SetSpeedBased(true);

    }

}
