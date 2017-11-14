using UnityEngine;

public class CurrentFigureMoveDownSystem : IExecuteSystem
{
    private readonly IControl _control;
    private readonly Config _config;

    public CurrentFigureMoveDownSystem(IControl control, Config config)
    {
        _control = control;
        _config = config;
    }

    private float timeFromLastMove = 0;
    
    public void Tick()
    {
        timeFromLastMove += Time.deltaTime;
        if (timeFromLastMove > _config.AutoMoveTime)
        {
            timeFromLastMove = 0;
            _control.MoveDown();
        }
    }
}