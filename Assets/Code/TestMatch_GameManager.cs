using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMatch_GameManager : MonoBehaviour
{
    [SerializeField] private int unitsXSide = 1;
    [SerializeField] private bool spawnOnStart = false;

    [SerializeField] private GameObject spawnerGO;

    private IUnitsSpawner spawner;
    private UnitsManager unitsManager;

    private void Awake()
    {
        if (spawnerGO)
        {
            spawner = spawnerGO.GetComponent<IUnitsSpawner>();
            if (spawner is null)
            {
                Debug.LogError("Error: Missing IUnitsSpawner component !!!");
            }
        }
        else
        {
            Debug.LogError("Error: Missing spawnerGO field !!!");
        }
    }

    private void Start()
    {
        if (!UnitsManager.Singleton) return;

        unitsManager = UnitsManager.Singleton;
        unitsManager.onAllRockUnitsEliminated.AddListener(GameOver);
        unitsManager.onAllPaperUnitsEliminated.AddListener(GameOver);
        unitsManager.onAllScissorUnitsEliminated.AddListener(GameOver);

        if (spawnOnStart)
        {
            SpawnUnits();
            unitsManager.StartAllUnits();
        }
    }

    private void SpawnUnits()
    {
        spawner.SpawnUnits(unitsXSide);
    }

    private void GameOver()
    {
        unitsManager.StopAllUnits();
    }
}
