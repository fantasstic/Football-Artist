using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private DrawLine _drawController;
    [SerializeField] private float _speed;

    private Vector3[] _positions;
    private int _movePositionIndex = 0;
    private bool _drawingStarted = false, _startMovement = false, _isMoved = true;


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;

            if (touch.phase == TouchPhase.Began)
            {
                if (CheckTouchPosition(touchPosition))
                {
                    _drawController.StartDrawing(touchPosition);
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                _drawController.Draw(touchPosition);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _drawController.StopDrawing();
                _positions = new Vector3[_drawController.Line.positionCount];
                _drawController.Line.GetPositions(_positions);
                _startMovement = true;
                _movePositionIndex = 0;
            }
        }

        if (_startMovement && _isMoved)
        {
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
                _isMoved = false;
            }
        }
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

