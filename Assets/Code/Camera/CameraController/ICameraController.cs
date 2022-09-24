using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraController
{
    public void MoveTowards(Vector2 dir);
    public void ZoomIn();
    public void ZoomOut();
}
