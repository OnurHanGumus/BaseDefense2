using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using DG.Tweening;
public class BossAimController : MonoBehaviour
{
    #region Self Variables

    #region Public Variables
    public Transform Target;

    #endregion

    #region Serialized Variables
    [SerializeField] private SpriteRenderer targetSprite;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform bombInstantiateTransform;
    [SerializeField] private Transform playerTransform;
    #endregion

    #region Private Variables

    #endregion

    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerOutOfBase"))
        {
            playerTransform = other.transform;
            ThrowBomb();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerOutOfBase"))
        {
            playerTransform = null;
            Target = null;
        }
    }

    public void ThrowBomb()
    {
        if (playerTransform == null)
        {
            return;
        }
        targetSprite.transform.position = playerTransform.position;
        Target = targetSprite.transform;
        StartCoroutine(ThrowBombCoroutine());

    }

    private IEnumerator ThrowBombCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        targetSprite.transform.DOScale(3, 0.5f).SetEase(Ease.InOutBack);
        targetSprite.DOFade(0.5f, 0.5f);
        targetSprite.transform.position = playerTransform.position;
        Target = targetSprite.transform;
        Transform bomb = Instantiate(bombPrefab, bombInstantiateTransform.transform.position, bombPrefab.transform.rotation).transform;
        bomb.DOPath(new Vector3[] { new Vector3((targetSprite.transform.position.x + transform.position.x)/2, 40, (targetSprite.transform.position.z + transform.position.z) / 2) , targetSprite.transform.position },1f);
        yield return new WaitForSeconds(1f);
        targetSprite.transform.DOScale(0, 0.5f);
        targetSprite.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);

        ThrowBomb();
    }
}
