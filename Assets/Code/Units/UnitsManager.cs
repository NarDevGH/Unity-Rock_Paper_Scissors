using System.Collections.Generic;
using System.Linq;
using UnitHelper;
using UnityEngine;
using UnityEngine.Events;

public class UnitsManager : MonoBehaviour
{
    public const int PAPER_LAYER = 7;
    public const int ROCK_LAYER = 6;
    public const int SCISSOR_LAYER = 8;


    [SerializeField] private Sprite rockSprite;
    [SerializeField] private Sprite paperSprite;
    [SerializeField] private Sprite scissorSprite;

    public List<IUnit> paperUnitsList { get; private set; }
    public List<IUnit> rockUnitsList { get; private set; }
    public List<IUnit> scissorUnitsList { get; private set; }

    public UnityEvent onAllScissorUnitsEliminated { get; set; }
    public UnityEvent onAllRockUnitsEliminated { get; set; }
    public UnityEvent onAllPaperUnitsEliminated { get; set; }

    public static UnitsManager Singleton {get; set;}

    private List<IUnit> chasedUnits;

    private void Awake()
    {
        if (Singleton is null)
        {
            Singleton = this;
        }
        else
        {
            Debug.LogError("Warning: There is more than one AiManager instance.");
            enabled = false;
            return;
        }

        paperUnitsList = new List<IUnit>();
        rockUnitsList = new List<IUnit>();
        scissorUnitsList = new List<IUnit>();
        chasedUnits = new List<IUnit>();

        onAllRockUnitsEliminated = new UnityEvent();
        onAllPaperUnitsEliminated = new UnityEvent();
        onAllScissorUnitsEliminated = new UnityEvent();
        
    }

    public void AddPaperToList(IUnit paperUnit)
    {
        paperUnitsList.Add(paperUnit);
    }
    public void AddRockToList(IUnit rockUnit)
    {
            rockUnitsList.Add(rockUnit);
    }
    public void AddScissorToList(IUnit scissorUnit)
    {
        scissorUnitsList.Add(scissorUnit);
    }
    public void RemovePaperFromList(IUnit paperUnit)
    {
        paperUnitsList.Remove(paperUnitsList.First(x => x == paperUnit));
    }
    public void RemoveRockFromList(IUnit rockUnit)
    {
        rockUnitsList.Remove(rockUnitsList.First(x => x == rockUnit));
    }
    public void RemoveScissorfromList(IUnit scissorUnit)
    {
        scissorUnitsList.Remove(scissorUnitsList.First(x => x == scissorUnit));
    }


    public void ChangeRockToPaper(IUnit unit)
    {
        RemoveRockFromList(unit);
        AddPaperToList(unit);

        unit.ChangeType(UnitType.Paper, PAPER_LAYER, paperSprite);

        if (rockUnitsList.Count == 0)
        {
            onAllRockUnitsEliminated.Invoke();
            return;
        }
    }
    public void ChangeScissorToRock(IUnit unit)
    {
        unit.ChangeType(UnitType.Rock, ROCK_LAYER, rockSprite);

        RemoveScissorfromList(unit);
        AddRockToList(unit);

        if (scissorUnitsList.Count == 0)
        {
            onAllScissorUnitsEliminated.Invoke();
            return;
        }
    }
    public void ChangePaperToScissor(IUnit unit)
    {
        unit.ChangeType(UnitType.Scissor, SCISSOR_LAYER, scissorSprite);

        RemovePaperFromList(unit);
        AddScissorToList(unit);

        if (paperUnitsList.Count == 0)
        {
            onAllPaperUnitsEliminated.Invoke();
            return;
        }
    }

    public void StartAllUnits()
    {
        rockUnitsList.ForEach(x => x.StartUnit());
        paperUnitsList.ForEach(x => x.StartUnit());
        scissorUnitsList.ForEach(x => x.StartUnit());
    }
    public void StopAllUnits()
    {
        rockUnitsList.ForEach(x => x.StopUnit());
        paperUnitsList.ForEach(x => x.StopUnit());
        scissorUnitsList.ForEach(x => x.StopUnit());
    }

    public Transform GetTargetFromList(IEnumerable<IUnit> targetsList)
    {
        if (targetsList.Count() == 0) return null;

        var targetUnit = targetsList.FirstOrDefault(x => !chasedUnits.Contains(x));
        if (!chasedUnits.Contains(targetUnit))
        {
            chasedUnits.Add(targetUnit);
        }

        return targetUnit.gameObject.transform;
    }
}
