using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCameraController : MonoBehaviour,ICameraController
{
    private const float MAX_ZOOM = 5f;

    [SerializeField, Min(0.1f)] private float moveSpeed = 1;
    [Header("Zoom")]
    [SerializeField, Min(0.1f)] private float _zoomAmmount = .1f;
    [SerializeField, Min(0.1f)] private float _zoomSpeed = 10;
    [SerializeField, Min(MAX_ZOOM)] private float _minZoom = 20;
    [SerializeField, Min(MAX_ZOOM)] private float _maxZoom = MAX_ZOOM;

    private Coroutine _zoomRoutine = null;
    private float _targetViewSize;
    private CinemachineVirtualCamera _virtualCamera;
    private Transform _cameraTarget;
    private float CamViewSize {
        get {
            if (_virtualCamera)
            {
                return _virtualCamera.m_Lens.OrthographicSize;
            }
            else
            {
                return -1;
            }
        }
        set {
            if (_virtualCamera)
            {
                _virtualCamera.m_Lens.OrthographicSize = value;
            }
        }
    }

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (_virtualCamera is null)
        {
            Debug.LogWarning("Missing CinemachineVirtualCamera Component in the GameObject !!!");
        }
        else
        {
            if (_virtualCamera.Follow is null)
            {
                Debug.LogWarning("Missing Follow value on the CinemachineVirtualCamera Component !!!");
            }
            else
            {
                _cameraTarget = _virtualCamera.Follow.transform;
            }
        }

        _targetViewSize = CamViewSize;
        CamViewSize = CamViewSize > _minZoom ? _minZoom : CamViewSize;
    }

    #region Movement
    public void MoveTowards(Vector2 dir)
    {
        float posX = Camera.main.transform.position.x + dir.x * moveSpeed;
        float posY = Camera.main.transform.position.y + dir.y * moveSpeed;

        _cameraTarget.position = new Vector3(posX, posY, _cameraTarget.position.z);
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
        while (CamViewSize != _targetViewSize)
        {
            CamViewSize = Mathf.MoveTowards(CamViewSize, _targetViewSize, _zoomSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        _zoomRoutine = null;
    }
    #endregion
}
