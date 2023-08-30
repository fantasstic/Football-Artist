using System.Linq;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;

    private Ball[] _balls;
    private int _finishBalls;

    public void Init()
    {
        _balls = GetComponentsInChildren<Ball>(true);

        foreach (var ball in _balls)
        {
            ball.PreparedToMove += Ball_PreparedToMove;
        }
    }

    private void OnDestroy()
    {
        foreach (var ball in _balls)
        {
            ball.PreparedToMove -= Ball_PreparedToMove;
        }
    }

    public void AddFinishBall()
    {
        _finishBalls++;

        if (_finishBalls == _balls.Length)
            CheckWin();
    }    

    public void CheckWin()
    {
        if (_finishBalls == _balls.Length)
        {
            _gameController.CheckWin(true);
        }
        else
            _gameController.CheckWin(false);
    }

    private void Ball_PreparedToMove()
    {
        if (_balls.All(ball => ball.Prepared))
        {
            foreach (var ball in _balls)
            {
                ball.Move();
            }
        }
    }
}
