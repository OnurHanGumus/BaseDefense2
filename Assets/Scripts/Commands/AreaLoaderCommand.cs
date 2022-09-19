using UnityEngine;

namespace Commands
{
    public class AreaLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(int levelId, int areaId, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>("Areas/"+levelId+"/"+ (areaId + 1)), levelHolder);
        }
    }
}