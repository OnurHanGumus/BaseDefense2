using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables
    public ParticleSystem particle;

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
        StartCoroutine(InstantiateParticle());
        Destroy(gameObject, 2f);
    }

    private IEnumerator InstantiateParticle()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(particle, transform.position, transform.rotation).transform.parent = transform;

    }
}
