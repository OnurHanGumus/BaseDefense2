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
        yield return new WaitForSeconds(0.7f);
        Instantiate(particle, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), transform.rotation).transform.parent = transform;

    }
}
