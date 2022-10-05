using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTest : MonoBehaviour
{
    public Tilemap tilemap;

    private void Awake()
    {
        Debug.Log(tilemap.origin);
        Debug.Log(tilemap.origin + tilemap.size);
    }
}
