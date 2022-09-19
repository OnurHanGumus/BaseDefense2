using UnityEngine;

namespace Commands
{
    public class LevelLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(GameObject gameObject, Transform levelHolder)
        {
            Instantiate(gameObject, levelHolder);
        }
        public void InitializeLevel(GameObject gameObject, Transform levelHolder, Vector3 pos)
        {
            Instantiate(gameObject, pos, Quaternion.Euler(0, 180, 0), levelHolder);
        }
    }
}