using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(IUnit))]
public class PlayableUnitController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotSpeed;

    private IUnit _unit;
    private Rigidbody2D _rb;

    private float CurrentRotation { 
        get { return transform.rotation.z; }
        set { transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, value)); }
    }

    private void Awake()
    {
        _unit = GetComponent<IUnit>();
        _rb = GetComponent<Rigidbody2D>();

        if (!_unit.isNpc)
        {
            _rb.bodyType = RigidbodyType2D.Static;
        }
    }


    public void MoveTowards(Vector2 dir)
    {
        if (!_unit.active) return;

        Vector3 dirV3 = new Vector3(dir.x, dir.y, transform.position.z);
        transform.Translate(dirV3 * _moveSpeed * Time.deltaTime);
    }


    private void RotateTowardsTargetRotation(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
