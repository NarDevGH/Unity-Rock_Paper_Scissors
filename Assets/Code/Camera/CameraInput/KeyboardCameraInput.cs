using UnityEngine;

public class KeyboardCameraInput : MonoBehaviour
{
    private ICameraController _cameraController;

    private void Awake()
    {
        _cameraController = GetComponent<ICameraController>();
        if (_cameraController is null)
        {
            Debug.LogWarning("Missing ICameraController component !!!");
        }
    }

    private void Update()
    {
        if (_cameraController is null) return;

        HandleZoom();
        HandleMovement();
    }

    private void HandleZoom()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            _cameraController.ZoomIn();
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            _cameraController.ZoomOut();
        }
    }

    private void HandleMovement()
    {
        _cameraController.MoveTowards(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}
