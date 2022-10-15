using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using DG.Tweening;
using Data.ValueObject;
using Managers;
using Sirenix.OdinInspector;
using Signals;
using TMPro;
using Data.UnityObject;

public class BossPhysicsController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private BossManager manager;
    [SerializeField] private TextMeshPro healthTxt;

    #endregion
    #region Private Variables
    private BossData _data;
    [ShowInInspector] private int _health = 100;
    private AllGunsData _gunData;
    private int _damage = 25;


    public int Health
    {
        get { return _health; }
        set
        {

            _health = value;
            if (_health <= 0)
            {
                _health = -1;
                return;
            }
            SaveSignals.Instance.onBossTakedDamage?.Invoke(Health);
            healthTxt.text = _health.ToString();
        }
    }

    #endregion
    #endregion

    private void Start()
    {
        _data = manager.GetData();
        _gunData = GetData();
        _damage = GetDamageNumber();

        SetHealth();
    }
    private AllGunsData GetData() => Resources.Load<CD_Gun>("Data/CD_Gun").Data;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Health -= _damage;


        }

        if (_health <= 0)
        {
            LevelSignals.Instance.onBossDefeated?.Invoke();
            Destroy(transform.parent.gameObject, 0.5f);
        }
    }

    public void ResetData()
    {
        Health = _data.Health;

    }
    private int GetDamageNumber()
    {
        return _gunData.guns[SaveSignals.Instance.onGetSelectedGun()].StartDamage;
    }

    private void SetHealth()
    {
        int savedHealth = SaveSignals.Instance.onGetBossHealth();

        if (savedHealth == 0)
        {
            Health = _data.Health;
        }
        if (savedHealth == -1)
        {
            LevelSignals.Instance.onBossDefeated?.Invoke();
            Destroy(transform.parent.gameObject, 0.2f);
        }
        else if (savedHealth > 0)
        {
            Health = savedHealth;
        }
    }
}
