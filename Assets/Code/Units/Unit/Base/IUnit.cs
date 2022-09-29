using UnitHelper;
using UnityEngine;
using UnityEngine.Events;

public interface IUnit 
{
    public bool active { get; }
    public bool isNpc { get; }
    public UnitType unitType { get;}
    public GameObject gameObject { get; }

    public UnityEvent onStartUnit { get;}
    public UnityEvent onStopUnit { get;}
    public UnityEvent onTypeChange { get; }

    public void ChangeType(UnitType type, int layer, Sprite sprite);

    public void StartUnit();
    public void StopUnit();
}
