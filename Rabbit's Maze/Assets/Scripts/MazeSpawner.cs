using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MazeSpawner : MonoBehaviour
{
    public CellVariable CellPrefab;
    public Vector3 CellSize = new Vector3(1,1,0);

    public Maze maze;
    
    public HintRenderer HintRenderer;
    public List<int> DistanceForStart;
    public int DistanceToStart;
    [SerializeField] private TMP_Text _distanceText;

    private void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                CellVariable c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);
                
                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
                
            }
        }

        DistanceToStart = DistanceForStart.Max();

        HintRenderer.DrawPath();
    }

    private void Update()
    {
        _distanceText.text = "Осталось ходов: " + DistanceToStart;
    }
}
