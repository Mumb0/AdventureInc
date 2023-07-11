using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdventureInc
{
    public class SceneLoaderScript : MonoBehaviour
    {
        public void Load(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}