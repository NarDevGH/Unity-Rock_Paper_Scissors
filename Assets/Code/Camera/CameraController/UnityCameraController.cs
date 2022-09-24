using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityCameraController : MonoBehaviour,ICameraController
{
    private const float MAX_ZOOM = 5f;

    [SerializeField, Min(0.1f)] private float moveSpeed = 10;
    [Header("Zoom")]
    [SerializeField, Min(0.1f)] private float _zoomAmmount = .1f;
    [SerializeField, Min(0.1f)] private float _zoomSpeed = 10;
    [SerializeField, Min(MAX_ZOOM)] private float _minZoom = 20;
    [SerializeField, Min(MAX_ZOOM)] private float _maxZoom = MAX_ZOOM;
    [Header("Bounds")]
    [SerializeField] private Transform _topBound;
    [SerializeField] private Transform _bottomBound;
    [SerializeField] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;

    private Coroutine _zoomRoutine = null;
    private float _targetViewSize;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
        _targetViewSize = _mainCamera.orthographicSize;
        _mainCamera.orthographicSize = _mainCamera.orthographicSize>_minZoom? _minZoom : _mainCamera.orthographicSize;
    }

    private void LateUpdate()
    {
        KeepCameraInsideArea();
    }

    private void KeepCameraInsideArea()
    {
        Vector3 fixPos = Vector3.zero;
        var distToTopBound = _topBound.position.y - (transform.position.y + _mainCamera.orthographicSize);
        if (distToTopBound < 0)
        {
            transform.Translate(Vector3.up * distToTopBound, Space.World);
        }

        var distToBottomBound = _bottomBound.position.y - (transform.position.y - _mainCamera.orthographicSize);
        if (distToBottomBound > 0)
        {
            transform.Translate(Vector3.up * distToBottomBound, Space.World);
        }
    
        var distToLeftBound = _leftBound.position.x - (transform.position.x - (_mainCamera.orthographicSize));
        if (distToLeftBound > 0)
        {
            transform.Translate(Vector3.right * distToLeftBound, Space.World);
        }
        var distToRighttBound = _rightBound.position.x - (transform.position.x + (_mainCamera.orthographicSize));
        if (distToRighttBound < 0)
        {
            transform.Translate(Vector3.right * distToRighttBound, Space.World);
        }
        transform.Translate(fixPos, Space.World);

    }

    #region Movement
    public void MoveTowards(Vector2 dir)
    {
        if (dir.y != 0) MoveVertically(dir.y);

        if (dir.x != 0) MoveHorizontaly(dir.x);
    }
    private void MoveHorizontaly(float dir)
    {
        if (dir < 0)
        {
            if (_leftBound.position.x > (transform.position.x - (_mainCamera.orthographicSize)))
            {
                return;
            }
        }
        else
        {
            if (_rightBound.position.x < (transform.position.x + (_mainCamera.orthographicSize)))
            {
                return;
            }
        }

        transform.Translate(Vector3.right * dir * moveSpeed * Time.deltaTime, Space.World);
    }
    private void MoveVertically(float dir)
    {
        if (dir > 0)
        {
            if (_topBound.position.y < (transform.position.y + _mainCamera.orthographicSize))
            {
                return;
            }
        }
        else
        {
            if (_bottomBound.position.y > (transform.position.y - _mainCamera.orthographicSize))
            {
                return;
            }
        }

        transform.Translate(Vector3.up * dir * moveSpeed * Time.deltaTime, Space.World);
    }
    #endregion

    #region ZOOM
    public void ZoomIn()
    {
        _targetViewSize -= _zoomAmmount;
        if (_targetViewSize < _maxZoom)
        {
            _targetViewSize = _maxZoom;
        }

        if (_zoomRoutine is null)
        {
            _zoomRoutine = StartCoroutine("ZoomRoutine");
        }
    }
    public void ZoomOut()
    {
        _targetViewSize += _zoomAmmount;
        if (_targetViewSize > _minZoom)
        {
            _targetViewSize = _minZoom;
        }

        if (_zoomRoutine is null)
        {
            _zoomRoutine = StartCoroutine("ZoomRoutine");
        }
    }

    private IEnumerator ZoomRoutine()
    {
        while (_mainCamera.orthographicSize != _targetViewSize)
        {
            _mainCamera.orthographicSize = Mathf.MoveTowards(_mainCamera.orthographicSize, _targetViewSize, _zoomSpeed*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        _zoomRoutine = null;
    }
    #endregion
}
