using UnityEngine;

namespace NGJ2026.Manager
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _settingsMenu;

        private void Awake()
        {
            _settingsMenu.SetActive(false);
        }

        public void ToggleMenu()
        {
            _settingsMenu.SetActive(!_settingsMenu.activeInHierarchy);
        }
    }
}
