using Sketch.Persistency;
using System.Collections.Generic;
using System.Linq;

namespace NGJ2026.Persistency
{
    public class SaveData : ISaveData
    {
        public List<Score> BestScores { set; get; } = new();

        public bool IsInLeaderboard(int value)
            => BestScores.Count < 10 || value > BestScores.Last().Value;

        public int AddScore(string name, int value)
        {
            for (int i = 0; i < BestScores.Count; i++)
            {
                if (value > BestScores[i].Value)
                {
                    BestScores.Insert(i, new() { Name = name, Value = value });
                    PersistencyManager<SaveData>.Instance.Save();
                    return i + 1;
                }
            }

            if (BestScores.Count == 10) return -1;

            BestScores.Add(new() { Name = name, Value = value });
            PersistencyManager<SaveData>.Instance.Save();
            return -1;
        }
    }

    [System.Serializable]
    public record Score
    {
        public int Value;
        public string Name;
    }
}
