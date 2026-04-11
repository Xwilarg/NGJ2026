using NGJ2026.Persistency;
using NGJ2026.SO;
using Sketch.Common;
using Sketch.Persistency;
using Sketch.Translation;
using System.Linq;
using System.Text;
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

        [SerializeField]
        private GameObject _gameStartHint;

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
        public UnityEvent OnGameSetup { get; } = new();

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
                if (PersistencyManager<SaveData>.Instance.SaveData.IsInLeaderboard(InsectManager.Instance.ButterflyCaught))
                {
                    _submitPanel.SetActive(true);
                }
                else
                {
                    _gameStartHint.SetActive(true);
                    _leaderboardText.gameObject.SetActive(false);
                }
                _scoreText.text = Translate.Instance.Tr("score_title", InsectManager.Instance.ButterflyCaught.ToString());
            });

            OnGameSetup.AddListener(() =>
            {
                _leaderboardText.gameObject.SetActive(false);
                _gameStartHint.SetActive(false);
                _submitPanel.SetActive(false);
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

            PersistencyManager<SaveData>.Instance.SaveData.AddScore(_inputField.text, InsectManager.Instance.ButterflyCaught);

            _submitPanel.SetActive(false);
            UpdateLeaderboard();
            _gameStartHint.SetActive(true);
            _leaderboardText.gameObject.SetActive(false);
        }

        public void SkipScore()
        {
            _submitPanel.SetActive(false);
            _gameStartHint.SetActive(true);
            _leaderboardText.gameObject.SetActive(false);
        }

        private void Update()
        {
            _gameTimer.Update(Time.deltaTime);

            UpdateUI();
        }

        public void UpdateLeaderboard()
        {
            StringBuilder str = new();
            str.AppendLine(Translate.Instance.Tr("leaderboard_title"));
            str.AppendLine();
            foreach (var score in PersistencyManager<SaveData>.Instance.SaveData.BestScores)
            {
                str.AppendLine(Translate.Instance.Tr("leaderboard_format", score.Name, score.Value.ToString()));
            }

            _leaderboardText.text = str.ToString();
        }

        private void UpdateUI()
        {
            if (_statDisplay == null) return;

            var remaining = Info.GameDuration - _gameTimer.TimerClamped;
            _statDisplay.text = $"{Translate.Instance.Tr("stat_timer", Mathf.FloorToInt(remaining / 60f).ToString("00"), (remaining % 60).ToString("00"))}\n{Translate.Instance.Tr("stat_score", InsectManager.Instance.ButterflyCaught.ToString())}";
        }
    }
}
