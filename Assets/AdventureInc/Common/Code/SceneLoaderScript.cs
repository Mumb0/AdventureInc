using UnityEngine;
using UnityEngine.SceneManagement;

namespace GMTK2023
{
    public class SceneLoaderScript : MonoBehaviour
    {
        public void Load(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}