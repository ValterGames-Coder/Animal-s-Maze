using System.Collections.Generic;
using UnityEngine;

public class HintRenderer : MonoBehaviour
{
    public MazeSpawner MazeSpawner;
    [Header("Line point positions")]
    public List<Vector3> positions;

    private LineRenderer _componentLineRenderer;
    private void Start()
    {
        _componentLineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawPath()
    {
        Maze maze = MazeSpawner.maze;
        int x = maze.finishPosition.x;
        int y = maze.finishPosition.y;
        positions = new List<Vector3>();

        Vector2 startPosition = new Vector2(GameObject.Find("MazeZone").transform.position.x, 
          GameObject.Find("MazeZone").transform.position.y);
        positions.Add(startPosition);

        while ((x != 0 || y != 0) && positions.Count < 100000)
        {
            positions.Add(new Vector3(x * MazeSpawner.CellSize.x, y * MazeSpawner.CellSize.y));

            MazeGeneratorCell currentCell = maze.cells[x, y];

            if (x > 0 &&
                !currentCell.WallLeft &&
                maze.cells[x - 1, y].DistanceFromStart < currentCell.DistanceFromStart)
            {
                x--;
            }
            else if (y > 0 &&
                     !currentCell.WallBottom &&
                     maze.cells[x, y - 1].DistanceFromStart < currentCell.DistanceFromStart)
            {
                y--;
            }
            else if (x < maze.cells.GetLength(0) - 1 &&
                     !maze.cells[x + 1, y].WallLeft &&
                     maze.cells[x + 1, y].DistanceFromStart < currentCell.DistanceFromStart)
            {
                x++;
            }
            else if (y < maze.cells.GetLength(1) - 1 &&
                     !maze.cells[x, y + 1].WallBottom &&
                     maze.cells[x, y + 1].DistanceFromStart < currentCell.DistanceFromStart)
            {
                y++;
            }
        }

        if (positions[1].x > startPosition.x)
        {
            positions[0] = new Vector3(startPosition.x + .5f, startPosition.y);
        }
        else if (positions[1].x < startPosition.x)
        {
            positions[0] = new Vector3(startPosition.x - .5f, startPosition.y);
        }
        else if (positions[1].y > startPosition.y)
        {
            positions[0] = new Vector3(startPosition.x, startPosition.y  + .5f);
        }
        else
        {
            positions[0] = new Vector3(startPosition.x, startPosition.y - .5f);
        }

        positions.Add(Vector3.zero);
        _componentLineRenderer.positionCount = positions.Count;
        _componentLineRenderer.SetPositions(positions.ToArray());
        
    }
}