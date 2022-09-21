using Controllers;
using Enums;
using Signals;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;

namespace Controllers
{
    public class UIGunStoreController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<TextMeshProUGUI> levelTxt;
        [SerializeField] private List<int> gunLevels;
        [SerializeField] private int currentSelectedGun;


        #endregion
        private GunData _data;
        #endregion





    }
}