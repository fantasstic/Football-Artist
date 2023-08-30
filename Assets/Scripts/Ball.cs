using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private DrawLine _drawController;
    [SerializeField] private float _speed;
    [SerializeField] private string _gateTag;

    private Animator _animator;
    private BallController _ballController;
    private Vector3[] _positions;
    private int _movePositionIndex = 0;
    private bool _drawingStarted = false, _startMovement = false, _isMoved = true;
    private bool _isTouched;
    private bool _isFinishOnGate;

    public bool IsOnGate => _isFinishOnGate;

    public event Action PreparedToMove;
    public bool Prepared { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
       _ballController = FindObjectOfType<BallController>();   
    }

    private void Update()
    {
        if (Input.touchCount > 0 && !_startMovement && _isMoved)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;

            if (touch.phase == TouchPhase.Began)
            {
                if (CheckTouchPosition(touchPosition))
                {
                    _isTouched = true;
                    _drawController.StartDrawing(touchPosition);
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (_isTouched)
                    _drawController.Draw(touchPosition);
            }
            else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && _isTouched)
            {
                _drawController.StopDrawing();
                _positions = new Vector3[_drawController.Line.positionCount];
                _drawController.Line.GetPositions(_positions);
                _isTouched = false;
                _movePositionIndex = 0;
                _isMoved = false;
                Prepared = true;
                PreparedToMove?.Invoke();
            }
        }

        if (_startMovement)
        {
            if (_movePositionIndex < 0 || _movePositionIndex >= _positions?.Length)
                return;

            _animator.SetBool("IsMove", true);
            Vector2 currentPos = _positions[_movePositionIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentPos, _speed * Time.deltaTime);

            float distance = Vector2.Distance(currentPos, transform.position);

            if (distance <= 0.05f)
            {
                _movePositionIndex++;
            }

            if (_movePositionIndex > _positions.Length - 1)
            {
                _startMovement = false;
                enabled = false;
            }

            if(_movePositionIndex > _positions.Length - 1 && _isFinishOnGate)
            {
                _ballController.AddFinishBall();
                _animator.SetBool("IsMove", false);
            }
            else if(_movePositionIndex > _positions.Length - 1 && !_isFinishOnGate)
            {
                _ballController.CheckWin();
                _animator.SetBool("IsMove", false);
            }
        }
    }

    public void Move()
    {
        _startMovement = true;
    }

    private bool CheckTouchPosition(Vector3 touchPosition)
    {
        Collider2D collider = Physics2D.OverlapPoint(touchPosition);
        return collider != null && collider.gameObject == gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Star")
        {
            GameController gameController = FindObjectOfType<GameController>();
            gameController.AddScore(100);
            Destroy(collision.gameObject);
        }

        if(collision.transform.tag == "Log" || collision.transform.tag == "Ball")
        {
            _ballController.CheckWin();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == _gateTag)
        {
            _isFinishOnGate = true;
        }
        else
        {
            _ballController.CheckWin();
        }
    }
}

