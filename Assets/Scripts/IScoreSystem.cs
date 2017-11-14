using UnityEngine.SocialPlatforms;

public interface IScoreSystem
{
    int Score { get; }
    void CountKilledLine();
    int TotalLineKilled { get;  }
}