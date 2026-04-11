using NGJ2026.SO;
using Sketch.Common;
using Sketch.Translation;
using System.Linq;
using TMPro;
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

        [SerializeField]
        private TMP_Text _statDisplay;

        private Timer _gameTimer;

        private int _levelIndex;

        public Level CurrentLevel => _levelIndex >= Info.Levels.Length ? Info.Levels.Last() : Info.Levels[_levelIndex];

        public void ProgressLevel()
        {
            _levelIndex++;
        }

        private void Awake()
        {
            Instance = this;

            _gameTimer = new();
            _gameTimer.OnDone.AddListener(() =>
            {
                // TODO: Handle game over
            });
            _gameTimer.Start(Info.GameDuration);

            if (_statDisplay == null)
            {
                Debug.LogWarning("Stat display not set");
            }
        }

        private void Update()
        {
            _gameTimer.Update(Time.deltaTime);

            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_statDisplay == null) return;

            var remaining = Info.GameDuration - _gameTimer.TimerClamped;
            _statDisplay.text = $"{Translate.Instance.Tr("stat_timer", Mathf.FloorToInt(remaining / 60f).ToString("00"), (remaining % 60).ToString("00"))}\n{Translate.Instance.Tr("stat_score", InsectManager.Instance.ButterflyCaught.ToString())}";
        }
    }
}
