using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MazeSpawner : MonoBehaviour
{
    [Header("Generator Maze")]
    public CellVariable CellPrefab;
    public Vector3 CellSize = new Vector3(1,1,0);

    [HideInInspector] public Maze maze;
    
    [SerializeField] private HintRenderer _hintRenderer;
    [HideInInspector] public List<int> DistanceForStart;
    [HideInInspector] public int DistanceToStart;
    [SerializeField] private TMP_Text _distanceText;
    [HideInInspector] public Vector2 MazeZoneOffset;
    
    [Header("Generator Items")]
    [SerializeField] private List<GameObject> _items;
    private CameraState _cameraState;

    private void Start()
    {
        _cameraState = FindObjectOfType<CameraState>();
        MazeGenerator generator = new MazeGenerator
        {
            Width = (int) _cameraState.Sizes[_cameraState.LevelForList].x,
            Height = (int) _cameraState.Sizes[_cameraState.LevelForList].y
        };
        
        maze = generator.GenerateMaze();
        
        GenerateMazeZone(generator, MazeZoneOffset);
        
        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                CellVariable c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);
                
                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
            }
        }
        
        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                if (maze.cells[x, y].Visited)
                {
                    if (maze.cells[x, y].WallBottom == false)
                    {
                        if (maze.cells[x, y + 1].WallBottom)
                        {
                            if (maze.cells[x, y].WallLeft)
                            {
                                if (maze.cells[x + 1, y].WallLeft)
                                {
                                    if(maze.cells[x, y] != maze.cells[0, 0]) SpawnItem(x, y);
                                }
                            }
                        }
                    }
                    else if (maze.cells[x, y].WallBottom)
                    {
                        if (maze.cells[x, y].WallLeft)
                        {
                            if (maze.cells[x + 1, y].WallLeft)
                            {
                                if(maze.cells[x, y] != maze.cells[0, 0]) SpawnItem(x, y);
                            }
                        }
                        
                        
                        if (maze.cells[x, y].WallLeft)
                        {
                            if (maze.cells[x + 1, y].WallLeft == false)
                            {
                                if (maze.cells[x, y + 1].WallBottom)
                                {
                                    if(maze.cells[x, y] != maze.cells[0, 0]) SpawnItem(x, y);
                                }
                            }
                        }
                        else if (maze.cells[x, y].WallLeft == false)
                        {
                            if (maze.cells[x + 1, y].WallLeft)
                            {
                                if (maze.cells[x, y + 1].WallBottom)
                                {
                                    if(maze.cells[x, y] != maze.cells[0, 0]) SpawnItem(x, y);
                                }
                            }
                        }
                    }
                }
            }
        }

        DistanceToStart = DistanceForStart.Max() + 3;
        
        if(_hintRenderer.gameObject.activeInHierarchy) _hintRenderer.DrawPath();
    }

    private void Update()
    {
        _distanceText.text = "Осталось ходов: " + DistanceToStart;
    }

    private void GenerateMazeZone(MazeGenerator generator, Vector2 offset)
    {
        GameObject mazeZone = new GameObject("MazeZone");
        mazeZone.AddComponent<BoxCollider2D>().size = new Vector2(generator.Width - 1, generator.Height - 1);
        mazeZone.GetComponent<BoxCollider2D>().isTrigger = true;
        mazeZone.GetComponent<BoxCollider2D>().offset = offset;
        mazeZone.transform.position = Vector2.zero;
        mazeZone.tag = "IsMaze";
    }

    private void SpawnItem(int X, int Y)
    {
        int item = Random.Range(0, _items.Count);
        Vector2 position = new Vector2(X, Y);
        Instantiate(_items[item], position, Quaternion.identity);
    }
}
