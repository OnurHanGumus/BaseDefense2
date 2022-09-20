using Enums;
using Keys;
using System.Collections.Generic;

namespace Commands
{
    public class SaveGameCommand
    {

        public void OnSaveData(SaveLoadStates states, int newValue, string fileName = "SaveFile")
        {
            //if (states.Equals(SaveLoadStates.Level))
            //{
            //    ES3.Save("Level", newValue);
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.Money))
            //{
            //    ES3.Save("Money", newValue);
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.Gem))
            //{
            //    ES3.Save("Gem", newValue);
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.CurrentBossHealth))
            //{
            //    ES3.Save("CurrentBossHealth", newValue); 
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.GunId))
            //{
            //    ES3.Save("GunId", newValue, "Guns.es3");
            //    return;
            //}
            ES3.Save(states.ToString(), newValue, fileName+ ".es3");
        }

        public void OnSaveList(SaveLoadStates states, int newValue, string fileName = "SaveFile")
        {


            List<int> tempList = ES3.Load(states.ToString(), fileName + ".es3", new List<int>());
            if (tempList.Contains(newValue))
            {
                return;
            }
            tempList.Add(newValue);
            ES3.Save(states.ToString(), tempList, fileName + ".es3");



            //if (states.Equals(SaveLoadStates.CurrentLevelOpenedAreas))
            //{
            //    List<int> tempList = ES3.Load("CurrentLevelOpenedAreas", new List<int>());
            //    if (tempList.Contains(newValue))
            //    {
            //        return;
            //    }
            //    tempList.Add(newValue);
            //    ES3.Save("CurrentLevelOpenedAreas", tempList);
            //}
            //if (states.Equals(SaveLoadStates.OpenedTurrets))
            //{
            //    List<int> tempList = ES3.Load("CurrentLevelOpenedTurrets", "WorkerCurrentCounts.es3", new List<int>());
            //    if (tempList.Contains(newValue))
            //    {
            //        return;
            //    }
            //    tempList.Add(newValue);
            //    ES3.Save("CurrentLevelOpenedTurrets", tempList, "WorkerCurrentCounts.es3");
            //}
            //if (states.Equals(SaveLoadStates.OpenedTurretOwners))
            //{
            //    List<int> tempList = ES3.Load("CurrentLevelOpenedTurretOwners", "WorkerCurrentCounts.es3", new List<int>());
            //    if (tempList.Contains(newValue))
            //    {
            //        return;
            //    }
            //    tempList.Add(newValue);
            //    ES3.Save("CurrentLevelOpenedTurretOwners", tempList, "WorkerCurrentCounts.es3");
            //}
            //if (states.Equals(SaveLoadStates.OpenedEnemyAreas))
            //{
            //    List<int> tempList = ES3.Load("CurrentLevelOpenedEnemyAreas", "WorkerCurrentCounts.es3", new List<int>());
            //    if (tempList.Contains(newValue))
            //    {
            //        return;
            //    }
            //    tempList.Add(newValue);
            //    ES3.Save("CurrentLevelOpenedEnemyAreas", tempList, "WorkerCurrentCounts.es3");
            //}
        }

        public void OnSaveArray(SaveLoadStates states, int[] array, string fileName = "SaveFile")
        {
            ES3.Save(states.ToString(), array, fileName + ".es3");

            //if (states.Equals(SaveLoadStates.OpenedAreasCounts))
            //{
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.OpenedTurretsCounts))
            //{
            //    ES3.Save("CurrentLevelOpenedTurretsCounts", array, "WorkerCurrentCounts.es3");
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.OpenedTurretOwnersCounts))
            //{
            //    ES3.Save("CurrentLevelOpenedTurretOwnersCounts", array, "WorkerCurrentCounts.es3");
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.OpenedEnemyAreaCounts))
            //{
            //    ES3.Save("CurrentLevelOpenedEnemyAreaCounts", array, "WorkerCurrentCounts.es3");
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.GunLevels))
            //{
            //    ES3.Save("GunLevels", array, "Guns.es3");
            //    return;
            //}
        }

        public void OnResetList(SaveLoadStates states, string fileName = "SaveFile")
        {
            List<int> tempList = new List<int>();
            ES3.Save(states.ToString(), tempList, fileName + ".es3");

            //if (states.Equals(SaveLoadStates.CurrentLevelOpenedAreas))
            //{
            //    List<int> tempList = new List<int>();
            //    ES3.Save("CurrentLevelOpenedAreas", tempList);
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.OpenedTurrets))
            //{
            //    List<int> tempList = new List<int>();
            //    ES3.Save("CurrentLevelOpenedTurrets", tempList, "WorkerCurrentCounts.es3");
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.OpenedTurretOwners))
            //{
            //    List<int> tempList = new List<int>();
            //    ES3.Save("CurrentLevelOpenedTurretOwners", tempList, "WorkerCurrentCounts.es3");
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.OpenedEnemyAreas))
            //{
            //    List<int> tempList = new List<int>();
            //    ES3.Save("CurrentLevelOpenedEnemyAreas", tempList, "WorkerCurrentCounts.es3");
            //    return;
            //}
        }

        public void OnResetArray(SaveLoadStates states, string fileName = "SaveFile")
        {
            int[] tempArray = new int[2] { -1, -1 };
            ES3.Save(states.ToString(), tempArray, fileName+".es3");

            //if (states.Equals(SaveLoadStates.OpenedAreasCounts))
            //{
            //    int[] tempArray = new int[2] { -1, -1 };
            //    ES3.Save("CurrentLevelOpenedAreaCounts", tempArray, "WorkerCurrentCounts.es3");
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.OpenedTurretsCounts))
            //{
            //    int[] tempArray = new int[2] { -1, -1 };
            //    ES3.Save("CurrentLevelOpenedTurretsCounts", tempArray, "WorkerCurrentCounts.es3");
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.OpenedTurretOwnersCounts))
            //{
            //    int[] tempArray = new int[2] { -1, -1 };
            //    ES3.Save("CurrentLevelOpenedTurretOwnersCounts", tempArray, "WorkerCurrentCounts.es3");
            //    return;
            //}
            //if (states.Equals(SaveLoadStates.OpenedEnemyAreaCounts))
            //{
            //    int[] tempArray = new int[2] { -1, -1 };
            //    ES3.Save("CurrentLevelOpenedEnemyAreaCounts", tempArray, "WorkerCurrentCounts.es3");
            //    return;
            //}
        }


    }
}
