using UnityEngine;

[RequireComponent(typeof(PlayableUnitController))]
public class MouseUnitInput : MonoBehaviour
{
    [SerializeField, Range(0.1f, 0.9f)] float screenEdgeRange = 0.1f;

    private PlayableUnitController unitController;

    private void Awake()
    {
        unitController = GetComponent<PlayableUnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unitController is null) return;

        unitController.MoveTowards(MouseDir());
    }


    private Vector2 MouseDir()
    {
        Vector2 mouseDir = Vector2.zero;

        if (Input.mousePosition.y > Screen.height * ( 1 - screenEdgeRange))
        {
            mouseDir += Vector2.up;
        }
        else if (Input.mousePosition.y < Screen.height * ( 0 + screenEdgeRange))
        {
            mouseDir += Vector2.down;
        }

        if (Input.mousePosition.x > Screen.width * ( 1 - screenEdgeRange))
        {
            mouseDir += Vector2.right;
        }
        else if (Input.mousePosition.x < Screen.width * ( 0 + screenEdgeRange))
        {
            mouseDir += Vector2.left;
        }

        return mouseDir;
    }
}
