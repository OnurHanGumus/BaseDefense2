using UnityEngine;

namespace Commands
{
    public class TurretLoaderCommand : MonoBehaviour
    {
        public void InitializeTurret(int levelId, int turretId, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>("Turrets/"+levelId+"/"+ (turretId + 1)), levelHolder);
        }
    }
}