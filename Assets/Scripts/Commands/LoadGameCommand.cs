using Enums;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Commands
{
    public class LoadGameCommand
    {
        public int OnLoadGameData(SaveLoadStates saveLoadStates, string fileName = "SaveFile")
        {

            if (!ES3.FileExists(fileName + ".es3")/* || !ES3.KeyExists("Money")*/) {  return 0; }
            //Debug.Log(ES3.Load<int>(saveLoadStates.ToString(), fileName + ".es3", 0));
            return ES3.Load<int>(saveLoadStates.ToString(), fileName+ ".es3", 0);

            //if (saveLoadStates == SaveLoadStates.Level) return ES3.Load<int>("Level", 0);
            //else if (saveLoadStates == SaveLoadStates.Money) return ES3.Load<int>("Money", 0);
            //else if (saveLoadStates == SaveLoadStates.Gem) return ES3.Load<int>("Gem", 0);
            //else if (saveLoadStates == SaveLoadStates.CurrentBossHealth) return ES3.Load<int>("CurrentBossHealth");
            //else if (saveLoadStates == SaveLoadStates.GunId) return ES3.Load<int>("GunId", "Guns.es3", 0);
            //else return 0;
        }

        public List<int> OnLoadList(SaveLoadStates saveLoadStates, string fileName = "SaveFile")
        {
            if (!ES3.FileExists(fileName + ".es3")) {Debug.Log("çalışmadı"); return new List<int>(); }
            return ES3.Load(saveLoadStates.ToString(), fileName + ".es3", new List<int>());

            //if (saveLoadStates == SaveLoadStates.CurrentLevelOpenedAreas) return ES3.Load("CurrentLevelOpenedAreas", new List<int>());
            //else if (saveLoadStates == SaveLoadStates.OpenedTurrets) return ES3.Load("OpenedTurrets", "WorkerCurrentCounts.es3", new List<int>());
            //else if (saveLoadStates == SaveLoadStates.OpenedTurretOwners) return ES3.Load("OpenedTurretOwners", "WorkerCurrentCounts.es3", new List<int>());
            //else if (saveLoadStates == SaveLoadStates.OpenedEnemyAreas) return ES3.Load("OpenedEnemyAreas", "WorkerCurrentCounts.es3", new List<int>());
            //else
            //{
            //    Debug.Log("çalışmadı");
            //    return null;
            //}
        }

        public int[] OnLoadArray(SaveLoadStates saveLoadStates, string fileName = "SaveFile")
        {
            if (!ES3.FileExists(fileName + ".es3")) { Debug.Log("çalışmadı"); return new int[5] { -1, -1, -1, -1 , -1}; }
            return ES3.Load(saveLoadStates.ToString(), fileName + ".es3", new int[5] { -1, -1, -1, -1 , -1});

            //if (saveLoadStates == SaveLoadStates.OpenedAreasCounts) return ES3.Load(saveLoadStates.ToString(), "WorkerCurrentCounts.es3", new int[4] { -1,-1,-1,-1 });
            //else if (saveLoadStates == SaveLoadStates.OpenedTurretsCounts) return ES3.Load(saveLoadStates.ToString(), "WorkerCurrentCounts.es3", new int[4] { -1,-1,-1,-1 });
            //else if (saveLoadStates == SaveLoadStates.OpenedTurretOwnersCounts) return ES3.Load(saveLoadStates.ToString(), "WorkerCurrentCounts.es3", new int[4] { -1,-1,-1,-1 });
            //else if (saveLoadStates == SaveLoadStates.OpenedEnemyAreaCounts) return ES3.Load(saveLoadStates.ToString(), "WorkerCurrentCounts.es3", new int[4] { -1,-1,-1,-1 });
            //else if (saveLoadStates == SaveLoadStates.GunLevels) return ES3.Load(saveLoadStates.ToString(), "Guns.es3", new int[4] { -1,-1,-1,-1 });
            //else
            //{
            //    Debug.Log("çalışmadı");
            //    return new int[4] { -1, -1, -1, -1 };
            //}
        }
    }
}
