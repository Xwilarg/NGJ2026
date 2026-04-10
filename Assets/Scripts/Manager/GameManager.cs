using NGJ2026.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NGJ2026.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        [SerializeField]
        private GameInfo _info;
        public GameInfo Info => _info;

        private void Awake()
        {
            Instance = this;

            if (!HasScene("level_01"))
            {
                SceneManager.LoadScene("level_01", LoadSceneMode.Additive);
            }
        }

        private bool HasScene(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (string.Compare(sceneName, SceneManager.GetSceneAt(i).name, true) == 0)
                    return true;
            }
            return false;
        }
    }
}
