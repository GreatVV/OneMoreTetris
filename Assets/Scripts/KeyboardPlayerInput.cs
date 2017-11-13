using UnityEngine;

public class KeyboardPlayerInput : IUpdateable 
{
    private readonly IControl _control;

    public KeyboardPlayerInput(IControl control)
    {
        _control = control;
    }
    
    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _control.RotateClockWise();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            _control.RotateCounterClockWise();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _control.MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _control.MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _control.MoveRight();
        }
    }
}