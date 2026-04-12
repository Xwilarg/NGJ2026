using Sketch.Translation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NGJ2026.Manager
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _settingsMenu;
        [SerializeField] private RawImage _languageButtonSprite;
        [SerializeField] private Texture2D _defaultLanguageSprite;
        [SerializeField] private Texture2D _closeLanguageSettingsSprite;
        
        private void Awake()
        {
            Translate.Instance.SetLanguages(new string[] { "english", "french", "dutch", "spanish", "german" });

            _settingsMenu.SetActive(false);
        }

        public void ToggleMenu()
        {
            _settingsMenu.SetActive(!_settingsMenu.activeInHierarchy);
            _languageButtonSprite.texture = _settingsMenu.activeInHierarchy ? _closeLanguageSettingsSprite : _defaultLanguageSprite;
        }

        public void SetEnglishLanguage() => Translate.Instance.CurrentLanguage = "english";
        public void SetFrenchLanguage() => Translate.Instance.CurrentLanguage = "french";
        public void SetDutchLanguage() => Translate.Instance.CurrentLanguage = "dutch";
        public void SetSpanishLanguage() => Translate.Instance.CurrentLanguage = "spanish";
        public void SetGermanLanguage() => Translate.Instance.CurrentLanguage = "german";

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
