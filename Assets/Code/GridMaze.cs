

using System.Collections.Generic;
using UnityEngine;

public class GridMaze 
{
    public Vector2Int _size;
    public bool[,] _wallCells;

    enum Direction {up,right,down, left }

    public GridMaze(Vector2Int size)
    {
        if (size.x < 0) size.x = 0;
        if (size.y < 0) size.y = 0;

        _size = size;
        _wallCells = new bool[_size.x, _size.y];
        GenerateMaze();
    }

    public GridMaze(int size)
    {
        if (size < 0) size = 0;

        _size = new Vector2Int(size,size);
        _wallCells = new bool[_size.x, _size.y];
        GenerateMaze();
    }

    private void GenerateMaze()
    {
    }
}
