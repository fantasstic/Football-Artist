using System.Linq;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Ball[] _balls;

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
