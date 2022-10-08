using UnityEngine;

namespace Commands
{
    public class WorkerManagersLoader : MonoBehaviour
    {
        public void InitializeWorkerManagers(Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>("WorkerAreas/AmmoWorkerManager"), levelHolder);
            Instantiate(Resources.Load<GameObject>("WorkerAreas/MoneyWorkerManager"), levelHolder);
        }
    }
}