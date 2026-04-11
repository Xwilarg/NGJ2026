using NGJ2026.SO;
using Sketch.Common;
using Sketch.Translation;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NGJ2026.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        [SerializeField]
        private GameInfo _info;
        public GameInfo Info => _info;

        [Header("Leaderboard")]
        [SerializeField]
        private TMP_Text _leaderboardText;

        [Header("Score submission")]
        [SerializeField]
        private TMP_Text _statDisplay;

        [SerializeField]
        private TMP_Text _scoreText;

        [SerializeField]
        private GameObject _submitPanel;

        [SerializeField]
        private TMP_InputField _inputField;

        private Timer _gameTimer;

        private int _levelIndex;

        public Level CurrentLevel => _levelIndex >= Info.Levels.Length ? Info.Levels.Last() : Info.Levels[_levelIndex];

        public UnityEvent OnGameStart { get; } = new();
        public UnityEvent OnGameReset { get; } = new();

        public void ProgressLevel()
        {
            if (_levelIndex == 0)
            {
                _gameTimer.Start(Info.GameDuration);
                OnGameStart.Invoke();
            }

            _levelIndex++;
        }

        private void Awake()
        {
            Instance = this;

            _submitPanel.SetActive(false);

            _gameTimer = new();
            _gameTimer.OnDone.AddListener(() =>
            {
                OnGameReset.Invoke();
            });

            OnGameReset.AddListener(() =>
            {
                _submitPanel.SetActive(true);
                _scoreText.text = Translate.Instance.Tr("score_title", InsectManager.Instance.ButterflyCaught.ToString());
            });

            if (_statDisplay == null)
            {
                Debug.LogWarning("Stat display not set");
            }
        }

        private void Start()
        {
            Translate.Instance.OnLanguageChanged.AddListener(UpdateLeaderboard);
            UpdateLeaderboard();
        }

        public void SubmitScore()
        {
            if (_inputField.text.Length == 0)
            {
                Debug.LogWarning("Trying to submit an empty text, ignoring...");
                return;
            }
            // TODO: register score

            _submitPanel.SetActive(false);
        }

        public void SkipScore()
        {
            _submitPanel.SetActive(false);
        }

        private void Update()
        {
            _gameTimer.Update(Time.deltaTime);

            UpdateUI();
        }

        public void UpdateLeaderboard()
        {
            _leaderboardText.text = Translate.Instance.Tr("leaderboard_title");
        }

        private void UpdateUI()
        {
            if (_statDisplay == null) return;

            var remaining = Info.GameDuration - _gameTimer.TimerClamped;
            _statDisplay.text = $"{Translate.Instance.Tr("stat_timer", Mathf.FloorToInt(remaining / 60f).ToString("00"), (remaining % 60).ToString("00"))}\n{Translate.Instance.Tr("stat_score", InsectManager.Instance.ButterflyCaught.ToString())}";
        }
    }
}
