using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RpcSpawner : MonoBehaviour, IUnitsSpawner
{
    [SerializeField,Range(0,1)] private float spaceBetween;

    [Header("Positions")]
    [SerializeField] private Transform rockSpawn;
    [SerializeField] private Transform paperSpawn;
    [SerializeField] private Transform scissorSpawn;

    [Header("Prefabs")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject paperPrefab;
    [SerializeField] private GameObject scissorPrefab;

    public void SpawnUnits(int ammount)
    {
        SpawnRock(ammount);
        SpawnPaper(ammount);
        SpawnScissor(ammount);
    }

    private void SpawnPaper(int ammount)
    {
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
    private void SpawnRock(int ammount)
    {
        List<Vector3> posts = new List<Vector3>();
        posts.Add(rockSpawn.position);
        int dir = 0;
        int dist = 1;
        for (int i = 0; i < ammount; i++)
        {
            var unitClone = Instantiate(rockPrefab, posts.Last(), Quaternion.identity);
            posts.Add(NewPos(rockSpawn.position, ref dir, ref dist));

            UnitsManager.Singleton.AddRockToList(unitClone.GetComponent<IUnit>());
        }
    }
    private void SpawnScissor(int ammount)
    {
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

    // get a new pos around the startpos that is walkable.
    private Vector3 NewPos(Vector3 startPos, ref int nextPosDir, ref int distFromStartPos)
    {
        Vector3 newPos = startPos;
        float distWithOffset = distFromStartPos + spaceBetween;

        bool walkable;
        do
        {
            walkable = true;
            switch (nextPosDir)
            {
                case 0:
                    newPos += new Vector3(0, distWithOffset, 0);
                    break;
                case 1:
                    newPos += new Vector3(distWithOffset, distWithOffset, 0);
                    break;
                case 2:
                    newPos += new Vector3(distWithOffset, 0, 0);
                    break;
                case 3:
                    newPos += new Vector3(distWithOffset, -distWithOffset, 0);
                    break;
                case 4:
                    newPos += new Vector3(0, -distWithOffset, 0);
                    break;
                case 5:
                    newPos += new Vector3(-distWithOffset, -distWithOffset, 0);
                    break;
                case 6:
                    newPos += new Vector3(-distWithOffset, 0, 0);
                    break;
            }
            nextPosDir++;

            if (nextPosDir == 7)
            {
                nextPosDir = 0;
                distFromStartPos++;
            }

            // check if the new pos is walkable.
            var graph = AstarPath.active.graphs.First();
            if (!graph.GetNearest(newPos).node.Walkable)
            {
                walkable = false;
            }


        } while (walkable == false);

        return newPos;
    }
}
