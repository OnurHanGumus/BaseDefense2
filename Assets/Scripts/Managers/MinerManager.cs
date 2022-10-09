using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Controllers;
using Enums;
using Signals;

public class MinerManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables
    public Transform SelectedMine;
    public bool IsGemCollected = false;

    #endregion

    #region Serialized Variables
    [SerializeField] private GameObject[] minesOnScene;
    [SerializeField] private Transform gemArea;
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private Transform collectedGem;
    [SerializeField] private Transform gemParent;

    #endregion

    #region Private Variables

    private MinerAnimationController _animationController;
    private Tween tweenRef;
    #endregion
    private bool _mineFull = false;
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
        LevelSignals.Instance.onMineGemCapacityFull += OnGemCapacityFull;
        LevelSignals.Instance.onMineGemCapacityCleared += OnGemCapacityCleared;
    }

    private void UnsubscribeEvents()
    {
        LevelSignals.Instance.onMineGemCapacityFull -= OnGemCapacityFull;
        LevelSignals.Instance.onMineGemCapacityCleared -= OnGemCapacityCleared;


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


        tweenRef = transform.DOMove(SelectedMine.position, 20f).SetSpeedBased(true).SetEase(Ease.Linear);
        transform.DOLookAt(SelectedMine.position, 0.5f);

    }

    public void Work(MinerAnimStates state)// animation type as parameter
    {
        tweenRef.Kill();
        _animationController.SetAnimState(state);
        StartCoroutine(WorkCoroutine());
    }

    private IEnumerator WorkCoroutine()
    {
        yield return new WaitForSeconds(2f);
        CollectGem();
        _animationController.SetAnimState(MinerAnimStates.Run);
        ReturnGemToArea();
    }

    private void CollectGem()
    {
        IsGemCollected = true;
        collectedGem = Instantiate(gemPrefab, gemParent.position, Quaternion.Euler(180,0,0)).transform;
        collectedGem.parent = gemParent;
    }

    private void ReturnGemToArea()
    {
        if (_mineFull)
        {
            return;
        }
        transform.DOMove(gemArea.position, 20f).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(SelectRandomMine);
        transform.DOLookAt(gemArea.position, 0.5f);

    }

    public void ReleaseGemsToGemArea(GameObject releaseObject)
    {
        StartCoroutine(ReleaseGemToGemArea(releaseObject));
    }
    private IEnumerator ReleaseGemToGemArea(GameObject releaseObject)
    {
        yield return new WaitForSeconds(0.05f);

        collectedGem.transform.parent = releaseObject.transform;
        collectedGem.transform.position = new Vector3(releaseObject.transform.position.x, 0.75f, releaseObject.transform.position.z);
        collectedGem = null;
        IsGemCollected = false;

    }

    private void OnGemCapacityFull()
    {
        _mineFull = true;
    }

    private void OnGemCapacityCleared()
    {
        _mineFull = false;
        ReturnGemToArea();
    }

}
