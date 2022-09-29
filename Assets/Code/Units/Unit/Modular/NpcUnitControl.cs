using Pathfinding;
using System;
using UnitHelper;
using UnityEngine;

[RequireComponent(typeof(IUnit))]
[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]
public class NpcUnitControl : MonoBehaviour
{
    private IUnit unit;

    private AIPath pathfinding;
    private AIDestinationSetter destinationSetter;

    private void Awake()
    {
        unit = GetComponent<IUnit>();
        pathfinding = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        unit.onStartUnit.AddListener(OnStartUnit);
        unit.onStopUnit.AddListener(OnStopUnit);
        unit.onTypeChange.AddListener(OnTypeChange);
    }

    private void OnStartUnit()
    {
        if (unit.isNpc)
        {
            SetUnitTarget();
            pathfinding.enabled = true;
        }
    }
    private void OnStopUnit()
    {
        pathfinding.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }
    private void OnTypeChange()
    {
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
            unit.StopUnit();
        }
    }
    private Transform TargetForUnit()
    {
        switch (unit.unitType)
        {
            case UnitType.Rock:
                return UnitsManager.Singleton.GetTargetFromList(UnitsManager.Singleton.scissorUnitsList);
            case UnitType.Paper:
                return UnitsManager.Singleton.GetTargetFromList(UnitsManager.Singleton.rockUnitsList);
            default:
                return UnitsManager.Singleton.GetTargetFromList(UnitsManager.Singleton.paperUnitsList);
        }
    }
}
