using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Controllers;
using Enums;

public class MinerManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables
    public Transform SelectedMine;

    #endregion

    #region Serialized Variables
    [SerializeField] private GameObject[] minesOnScene;
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
        _animationController = GetComponent<MinerAnimationController>();

        minesOnScene = GameObject.FindGameObjectsWithTag("Mine");
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
        SelectedMine = minesOnScene[Random.Range(0, minesOnScene.Length)].transform;
        MoveToSelectedMine();
    }

    private void MoveToSelectedMine()
    {
        transform.DOMove(SelectedMine.position, 20f).SetSpeedBased(true).SetEase(Ease.Linear);
        transform.DOLookAt(SelectedMine.position, 0.5f);

    }

    public void Work(MinerAnimStates state)// animation type as parameter
    {
        _animationController.SetAnimState(state);
        StartCoroutine(WorkCoroutine());
    }

    private IEnumerator WorkCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _animationController.SetAnimState(MinerAnimStates.Run);
        ReturnGemToArea();
    }

    private void ReturnGemToArea()
    {
        transform.DOMove(gemArea.position, 20f).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(SelectRandomMine);
        transform.DOLookAt(gemArea.position, 0.5f);

    }

}
