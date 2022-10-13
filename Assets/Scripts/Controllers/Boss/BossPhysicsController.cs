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

public class BossPhysicsController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private BossManager manager;
    [SerializeField] private int pistolDamage = 25, shotgunDamage = 50, smgDamage = 20, assaultDamage = 40, rocketDamage = 100, minigunDamage = 80, turretDamage = 60;
    [SerializeField] private TextMeshPro healthTxt;

    #endregion
    #region Private Variables
    private BossData _data;
    [ShowInInspector] private int _health = 100;

    public int Health
    {
        get { return _health; }
        set
        {

            _health = value;
            if (_health < 0)
            {
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
        int savedHealth = SaveSignals.Instance.onGetBossHealth();
        if (savedHealth > 0)
        {
            Health = savedHealth;
        }
        else
        {
            Health = _data.Health;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PistolBullet"))
        {
            Health -= pistolDamage;
            SaveSignals.Instance.onBossTakedDamage?.Invoke(Health);
            if (Health <= 0)
            {
                LevelSignals.Instance.onBossDefeated?.Invoke();
                Destroy(transform.parent.gameObject, 0.5f);
            }
        }
        else if (other.CompareTag("ShotgunBullet"))
        {
            Health -= shotgunDamage;
            SaveSignals.Instance.onBossTakedDamage?.Invoke(Health);

            if (Health <= 0)
            {
                LevelSignals.Instance.onBossDefeated?.Invoke();
                Destroy(transform.parent.gameObject, 0.5f);

            }
        }
        else if (other.CompareTag("SMGBullet"))
        {
            Health -= smgDamage;
            SaveSignals.Instance.onBossTakedDamage?.Invoke(Health);

            if (Health <= 0)
            {
                LevelSignals.Instance.onBossDefeated?.Invoke();
                Destroy(transform.parent.gameObject, 0.5f);

            }
        }
        else if (other.CompareTag("AssaultBullet"))
        {
            Health -= assaultDamage;
            SaveSignals.Instance.onBossTakedDamage?.Invoke(Health);

            if (Health <= 0)
            {
                LevelSignals.Instance.onBossDefeated?.Invoke();
                Destroy(transform.parent.gameObject, 0.5f);

            }
        }
        else if (other.CompareTag("RocketBullet"))
        {
            Health -= rocketDamage;
            SaveSignals.Instance.onBossTakedDamage?.Invoke(Health);

            if (Health <= 0)
            {
                LevelSignals.Instance.onBossDefeated?.Invoke();
                Destroy(transform.parent.gameObject, 0.5f);

            }
        }
        else if (other.CompareTag("MinigunBullet"))
        {
            Health -= minigunDamage;
            SaveSignals.Instance.onBossTakedDamage?.Invoke(Health);

            if (Health <= 0)
            {
                LevelSignals.Instance.onBossDefeated?.Invoke();
                Destroy(transform.parent.gameObject, 0.5f);

            }
        }
        else if (other.CompareTag("TurretBullet"))
        {
            Health -= turretDamage;
            SaveSignals.Instance.onBossTakedDamage?.Invoke(Health);

            if (Health <= 0)
            {
                LevelSignals.Instance.onBossDefeated?.Invoke();
                Destroy(transform.parent.gameObject, 0.5f);

            }
        }
    }

    public void ResetData()
    {
        Health = _data.Health;

    }
}
