using System;

public class ScoreSystem : IScoreSystem
{
    private readonly Config _config;
    private int _totalLineKilled;

    public ScoreSystem(Config config)
    {
        _config = config;
        TotalLineKilled = 0;
    }

    public int Score
    {
        get { return _config.PointsPerLine * TotalLineKilled; }
    }

    public void CountKilledLine()
    {
        TotalLineKilled++;
    }

    public int TotalLineKilled
    {
        get { return _totalLineKilled; }
        private set
        {
            _totalLineKilled = value;
            _config.ScoreLabel.text = string.Format("Score: {0}", Score);
        }
    }
}