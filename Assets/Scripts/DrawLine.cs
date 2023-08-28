using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _minDistance = 0.1f;

    private Vector3 _lastPosition;
    private bool _isDrawing = false;

    public LineRenderer Line => _lineRenderer;

    private void Start()
    {
        _lastPosition = transform.position;
        _lineRenderer.positionCount = 0;
    }

    public void StartDrawing(Vector3 startPosition)
    {
        _isDrawing = true;
        _lastPosition = startPosition;
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, startPosition);
    }

    public void Draw(Vector3 newPosition)
    {
        if (!_isDrawing)
            return;

        if (Vector3.Distance(newPosition, _lastPosition) > _minDistance)
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newPosition);
            _lastPosition = newPosition;
        }
    }

    public void StopDrawing()
    {
        _isDrawing = false;
    }
}
