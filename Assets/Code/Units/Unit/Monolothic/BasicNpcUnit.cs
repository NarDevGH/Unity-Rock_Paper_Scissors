using Pathfinding;
using UnitHelper;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]
public class BasicNpcUnit : MonoBehaviour, IUnit
{
    public bool active { get; private set; }
    public bool isNpc { get;  private set; }
    public bool chased { get; private set; }
    public UnitType unitType { get; private set; }

    [SerializeField, Range(0, 2)] public int npcValue = 0;
    [SerializeField] private bool chaseOnStart = true;

    private AIPath pathfinding { get; set; }
    public UnityEvent onTypeChange { get; set; }
    public UnityEvent onStartUnit { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public UnityEvent onStopUnit { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        pathfinding = GetComponent<AIPath>();

        isNpc = true;
        unitType = (UnitType)npcValue;
    }

    private void Start()
    {
        if (chaseOnStart)
        {
            StartUnit();
        }
    }

    public void StartUnit()
    {
        active = true;
        pathfinding.enabled = true;
    }
    public void StopUnit()
    {
        active = false;
        pathfinding.enabled = false;
    }

    public void ChangeType(UnitType type, int layer, Sprite sprite)
    {
        throw new System.NotImplementedException();
    }

}
