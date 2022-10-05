using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicRpcSpawner : MonoBehaviour,IUnitsSpawner
{
    [SerializeField, Min(0.1f)] private float spaceBetween;

    [Header("Positions")]
    [SerializeField] private Transform rockSpawn;
    [SerializeField] private Transform paperSpawn;
    [SerializeField] private Transform scissorSpawn;

    [Header("Prefabs")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject paperPrefab;
    [SerializeField] private GameObject scissorPrefab;

    private void Awake()
    {
        if (!rockPrefab) Debug.LogError("Missing rockPrefab field.");
        if (!paperPrefab) Debug.LogError("Missing paperPrefab field.");
        if (!scissorPrefab) Debug.LogError("Missing scissorPrefab field.");
        if (!rockSpawn) Debug.LogError("Missing rockSpawn field.");
        if (!paperSpawn) Debug.LogError("Missing paperSpawn field.");
        if (!scissorSpawn) Debug.LogError("Missing scissorSpawn field.");
    }

    public void SpawnUnits(int ammount)
    {
        SpawnRock(ammount);
        SpawnPaper(ammount);
        SpawnScissor(ammount);
    }

    private void SpawnRock(int ammount) {
        if (!rockPrefab || !rockSpawn) return;

        List<Vector3> usedPositions = new List<Vector3>();
        usedPositions.Add(rockSpawn.position);
        int dir = 0;
        int dist = 1;
        for (int i = 0; i < ammount; i++)
        {
            var unitClone = Instantiate(rockPrefab, usedPositions.Last(), Quaternion.identity);
            var unitComp = unitClone.GetComponent<IUnit>();
            if (unitComp is not null)
            {
                UnitsManager.Singleton.AddRockToList(unitComp);
            }
            usedPositions.Add(NewPos(rockSpawn.position, ref dir, ref dist));

        }
    }
    private void SpawnPaper(int ammount) {
        if (!paperPrefab || !paperSpawn) return;

        List<Vector3> posts = new List<Vector3>();
        posts.Add(paperSpawn.position);
        int dir = 0;
        int dist = 1;
        for (int i = 0; i < ammount; i++)
        {
            var unitClone = Instantiate(paperPrefab, posts.Last(), Quaternion.identity);
            posts.Add(NewPos(paperSpawn.position, ref dir, ref dist));

            UnitsManager.Singleton.AddPaperToList(unitClone.GetComponent<IUnit>());
        }
    }
    private void SpawnScissor(int ammount) {
        if (!scissorPrefab || !scissorSpawn) return;

        List<Vector3> posts = new List<Vector3>();
        posts.Add(scissorSpawn.position);
        int dir = 0;
        int dist = 1;
        for (int i = 0; i < ammount; i++)
        {
            var unitClone = Instantiate(scissorPrefab, posts.Last(), Quaternion.identity);
            posts.Add(NewPos(scissorSpawn.position, ref dir, ref dist));

            UnitsManager.Singleton.AddScissorToList(unitClone.GetComponent<IUnit>());
        }
    }

    private Vector3 NewPos(Vector3 startPos, ref int nextPosDir, ref int distFromStartPos)
    {
        Vector3 newPos = startPos;
        float distWithOffset = distFromStartPos + spaceBetween;

        switch (nextPosDir)
        {
            case 0:
                newPos += new Vector3(0, distWithOffset, 0);
                break;
            case 1:
                newPos += new Vector3(distWithOffset, 0, 0);
                break;
            case 2:
                newPos += new Vector3(0, -distWithOffset, 0);
                break;
            case 3:
                newPos += new Vector3(-distWithOffset, 0, 0);
                break;
            case 4:
                newPos += new Vector3(distWithOffset, distWithOffset, 0);
                break;
            case 5:
                newPos += new Vector3(distWithOffset, -distWithOffset, 0);
                break;
            case 6:
                newPos += new Vector3(-distWithOffset, -distWithOffset, 0);
                break;
        }
        nextPosDir++;

        if (nextPosDir == 7)
        {
            nextPosDir = 0;
            distFromStartPos++;
        }

        return newPos;
    }
}
