using Pathfinding;
using UnitHelper;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]
public class NpcUnit : MonoBehaviour, IUnit
{
    public bool active { get; private set; }
    public bool isNpc { get; set; }
    public UnitType unitType { get; private set; }

    public UnityEvent onTypeChange { get; private set; }
    public UnityEvent onStartUnit { get; private set; }
    public UnityEvent onStopUnit { get; private set; }

    [SerializeField, Range(0, 2)] public int npcValue = 0;

    private AIPath pathfinding;
    private AIDestinationSetter destinationSetter;

    private void Awake()
    {
        pathfinding = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        onStartUnit = new UnityEvent();
        onStopUnit = new UnityEvent();
        onTypeChange = new UnityEvent();

        isNpc = true;
        unitType = (UnitType)npcValue;
    }

    public void StartUnit()
    {
        SetUnitTarget();
        pathfinding.enabled = true;
    }

    public void StopUnit()
    {
        active = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public void ChangeType(UnitType type, int layer, Sprite sprite)
    {
        gameObject.layer = layer;
        unitType = type;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        SetUnitTarget();
    }

    private void SetUnitTarget()
    {
        Transform target = TargetForUnit();
        if (target)
        {
            destinationSetter.target = target;
        }
        else
        {
            StopUnit();
        }
    }
    private Transform TargetForUnit()
    {
        switch (unitType)
        {
            case UnitType.Rock:
                return UnitsManager.Singleton.GetTargetFromList(UnitsManager.Singleton.scissorUnitsList);
            case UnitType.Paper:
                return UnitsManager.Singleton.GetTargetFromList(UnitsManager.Singleton.rockUnitsList);
            default:
                return UnitsManager.Singleton.GetTargetFromList(UnitsManager.Singleton.paperUnitsList);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!active) return;

        var collisionUnit = collision.gameObject.GetComponent<IUnit>();

        if (collisionUnit is null) return;

        switch (unitType)
        {
            case UnitType.Paper:
                if (collisionUnit.unitType == UnitType.Rock)
                {
                    UnitsManager.Singleton.ChangeRockToPaper(collisionUnit);
                }
                break;

            case UnitType.Rock:
                if (collisionUnit.unitType == UnitType.Scissor)
                {
                    UnitsManager.Singleton.ChangeScissorToRock(collisionUnit);
                }
                break;

            case UnitType.Scissor:
                if (collisionUnit.unitType == UnitType.Paper)
                {
                    UnitsManager.Singleton.ChangePaperToScissor(collisionUnit);
                }
                break;
        }
    }

}
