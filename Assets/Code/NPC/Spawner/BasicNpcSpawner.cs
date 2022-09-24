using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicNpcSpawner : AbsNPCsSpawner
{
    [Header("Positions")]
    [SerializeField] private Transform paperSpawn;
    [SerializeField] private Transform rockSpawn;
    [SerializeField] private Transform scissorSpawn;

    [Header("Prefabs")]
    [SerializeField] private GameObject paperPrefab;
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject scissorPrefab;

    public override void SpawnNPCs(int ammount)
    {
        SpawnPaper(ammount);
        SpawnRock(ammount);
        SpawnScissor(ammount);
    }

    private void SpawnPaper(int ammount) {
        List<Vector3> posts = new List<Vector3>();
        posts.Add(paperSpawn.position);
        int dir = 0;
        int dist = 1;
        for (int i = 0; i < ammount; i++)
        {
            NpcManager.Singleton.AddPaperToList(Instantiate(paperPrefab, posts.Last(), Quaternion.identity));
            posts.Add(NewPos(paperSpawn.position, ref dir, ref dist));
        }
    }
    private void SpawnRock(int ammount) {
        List<Vector3> posts = new List<Vector3>();
        posts.Add(rockSpawn.position);
        int dir = 0;
        int dist = 1;
        for (int i = 0; i < ammount; i++)
        {
            NpcManager.Singleton.AddRockToList(Instantiate(rockPrefab, posts.Last(), Quaternion.identity));
            posts.Add(NewPos(rockSpawn.position, ref dir, ref dist));
        }
    }
    private void SpawnScissor(int ammount) {
        List<Vector3> posts = new List<Vector3>();
        posts.Add(scissorSpawn.position);
        int dir = 0;
        int dist = 1;
        for (int i = 0; i < ammount; i++)
        {
            NpcManager.Singleton.AddScissorToList(Instantiate(scissorPrefab, posts.Last(), Quaternion.identity));
            posts.Add(NewPos(scissorSpawn.position, ref dir, ref dist));
        }
    }

    private Vector3 NewPos(Vector3 startPos,ref int dir,ref int dist)
    {
        Vector3 newPos = startPos;
        switch (dir)
        {
            case 0:
                newPos += new Vector3(0, dist, 0);
                break;
            case 1:
                newPos += new Vector3(dist, dist, 0);
                break;
            case 2:
                newPos += new Vector3(dist, 0, 0);
                break;
            case 3:
                newPos += new Vector3(dist, -dist, 0);
                break;
            case 4:
                newPos += new Vector3(0, -dist, 0);
                break;
            case 5:
                newPos += new Vector3(-dist, -dist, 0);
                break;
            case 6:
                newPos += new Vector3(-dist, 0, 0);
                break;
        }
        dir++;
        if (dir == 7)
        {
            dir = 0;
            dist++;
        }
        return newPos;
    }
}
