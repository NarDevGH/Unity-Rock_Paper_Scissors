using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdTest_GameManager : MonoBehaviour
{
    [SerializeField] private int unitsXSide = 1;
    [SerializeField] private bool spawnOnStart = false;

    [SerializeField] private GameObject spawnerGO;
    private IUnitsSpawner spawner;
    private void Awake()
    {
        if (spawnerGO)
        {
            spawner = spawnerGO.GetComponent<IUnitsSpawner>();
        }
    }

    private void Start()
    {
        UnitsManager.Singleton.onAllRockUnitsEliminated.AddListener(GameOver);
        UnitsManager.Singleton.onAllPaperUnitsEliminated.AddListener(GameOver);
        UnitsManager.Singleton.onAllScissorUnitsEliminated.AddListener(GameOver);

        if (spawnOnStart)
        {
            SpawnUnits();
            UnitsManager.Singleton.StartAllUnits();
        }
    }

    private void SpawnUnits()
    {
        if (spawner is null)
        {
            Debug.LogError("Error: Missing IUnitsSpawner component !!!");
            return;
        }

        spawner.SpawnUnits(unitsXSide);
    }

    private void GameOver()
    {
        UnitsManager.Singleton.StopAllUnits();
    }
}
