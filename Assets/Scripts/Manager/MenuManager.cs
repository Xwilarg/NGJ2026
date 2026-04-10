using UnityEngine;
using UnityEngine.SceneManagement;

namespace NGJ2026.Manager
{
    public class MenuManager : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.LoadScene("level_01", LoadSceneMode.Additive);
        }

        public void Play()
        {
            SceneManager.LoadScene("Main");
        }
    }
}
