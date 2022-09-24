using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraInput : MonoBehaviour
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

        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector2 moveDir = Vector2.zero;

        if (Input.mousePosition.y > Screen.height * 0.9)
        {
            moveDir += Vector2.up;
        }
        else if (Input.mousePosition.y < Screen.height * 0.1)
        {
            moveDir += Vector2.down;
        }
    
        if (Input.mousePosition.x > Screen.width * 0.9)
        {
            moveDir += Vector2.right;
        }
        else if (Input.mousePosition.x < Screen.width * 0.1)
        {
            moveDir += Vector2.left;
        }

        _cameraController.MoveTowards(moveDir);
    }

    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                _cameraController.ZoomIn();

            }
            else
            {
                _cameraController.ZoomOut();
            }
        }
    }
}
