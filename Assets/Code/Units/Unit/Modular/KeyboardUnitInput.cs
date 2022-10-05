using UnityEngine;

[RequireComponent(typeof(PlayableUnitController))]
public class KeyboardUnitInput : MonoBehaviour
{
    private PlayableUnitController unitController;

    private void Awake()
    {
        unitController = GetComponent<PlayableUnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unitController is null) return;

        unitController.MoveTowards(KeyboradDir());
    }


    private Vector2 KeyboradDir()
    {
        Vector2 mouseDir = Vector2.zero;

        if (Input.GetAxis("Vertical") > 0)
        {
            mouseDir += Vector2.up;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            mouseDir += Vector2.down;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            mouseDir += Vector2.right;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            mouseDir += Vector2.left;
        }

        return mouseDir;
    }
}
