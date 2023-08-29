using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private DrawLine _drawController;
    [SerializeField] private float _speed;

    private Vector3[] _positions;
    private int _movePositionIndex = 0;
    private bool _drawingStarted = false, _startMovement = false, _isMoved = true;
    private bool _isTouched;

    public event Action PreparedToMove;
    public bool Prepared { get; private set; }

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
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Gate")
        {
            Debug.Log("Win");
        }
    }
}

