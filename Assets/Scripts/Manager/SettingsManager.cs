using Sketch.Translation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NGJ2026.Manager
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _settingsMenu;

        private void Awake()
        {
            Translate.Instance.SetLanguages(new string[] { "english", "french" });

            _settingsMenu.SetActive(false);
        }

        public void ToggleMenu()
        {
            _settingsMenu.SetActive(!_settingsMenu.activeInHierarchy);
        }

        public void SetEnglishLanguage() => Translate.Instance.CurrentLanguage = "english";
        public void SetFrenchLanguage() => Translate.Instance.CurrentLanguage = "french";
        public void SetDutchLanguage() => Translate.Instance.CurrentLanguage = "dutch";

        public void ResetGame()
        {
            SceneManager.LoadScene("Main");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
