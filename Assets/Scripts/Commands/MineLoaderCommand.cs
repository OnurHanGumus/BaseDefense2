using UnityEngine;

namespace Commands
{
    public class MineLoaderCommand : MonoBehaviour
    {
        public void InitializeMine(int levelId, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>("Mines/"+levelId), levelHolder, true);

        }
    }
}