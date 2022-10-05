using UnitHelper;
using UnityEngine;
using UnityEngine.Events;

public class RpcUnit : MonoBehaviour, IUnit
{
    public bool active { get; private set; }
    public bool isNpc { get; set; }
    public UnitType unitType { get; private set; }

    public UnityEvent onTypeChange { get; private set; }
    public UnityEvent onStartUnit { get; private set; }
    public UnityEvent onStopUnit { get; private set; }

    [SerializeField, Range(0, 2)] public int npcValue = 0;
    [SerializeField] private bool _isNpc;
    [SerializeField] private bool _startActive;

    private void Awake()
    {
        onStartUnit = new UnityEvent();
        onStopUnit = new UnityEvent();
        onTypeChange = new UnityEvent();

        isNpc = _isNpc;
        active = _startActive;
        unitType = (UnitType)npcValue;
    }

    public void StartUnit()
    {
        active = true;
        onStartUnit.Invoke();
    }

    public void StopUnit()
    {
        active = false;
        onStopUnit.Invoke();
    }

    public void ChangeType(UnitType type, int layer, Sprite sprite)
    {
        gameObject.layer = layer;
        unitType = type;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        onTypeChange.Invoke();
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
