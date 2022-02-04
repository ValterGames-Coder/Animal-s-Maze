using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class MazeSpawner : MonoBehaviour
{
    [Header("Generator Maze")]
    public CellVariable CellPrefab;
    public Vector3 CellSize = new Vector3(1,1,0);

    [HideInInspector] public Maze maze;
    
    [SerializeField] private HintRenderer _hintRenderer;
    [HideInInspector] public List<int> DistanceForStart;
    private List<Vector2> _isGoals = new List<Vector2>();
    private int _howMuchToSpawn;
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

        _howMuchToSpawn = (int)_cameraState.Sizes[_cameraState.LevelForList].z;
        maze = generator.GenerateMaze();
        
        GenerateMazeZone(generator, MazeZoneOffset);
        
        GameObject Maze = new GameObject("Maze");

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                CellVariable c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);

                c.gameObject.transform.parent = Maze.transform;
                
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
                                    if (maze.cells[x, y] != maze.cells[0, 0]) _isGoals.Add(new Vector2(x, y));
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
                                if (maze.cells[x, y] != maze.cells[0, 0]) _isGoals.Add(new Vector2(x, y));
                            }
                        }
                        
                        
                        if (maze.cells[x, y].WallLeft)
                        {
                            if (maze.cells[x + 1, y].WallLeft == false)
                            {
                                if (maze.cells[x, y + 1].WallBottom)
                                {
                                    if (maze.cells[x, y] != maze.cells[0, 0]) _isGoals.Add(new Vector2(x, y));
                                }
                            }
                        }
                        else if (maze.cells[x, y].WallLeft == false)
                        {
                            if (maze.cells[x + 1, y].WallLeft)
                            {
                                if (maze.cells[x, y + 1].WallBottom)
                                {
                                    if (maze.cells[x, y] != maze.cells[0, 0]) _isGoals.Add(new Vector2(x, y));
                                }
                            }
                        }
                    }
                }
            }
        }

        DistanceToStart = DistanceForStart.Max() + 3;

        try
        {
            _hintRenderer.DrawPath();
        }
        catch
        {
            // ignored
        }
        
        SpawnItem();
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

    private void SpawnItem()
    {
        try
        {
            for (int i = 0; i < _howMuchToSpawn; i++)
            {
                int item = Random.Range(0, _items.Count);
                int isGoal = Random.Range(0, _isGoals.Count);
                Vector2 position = new Vector2(_isGoals[isGoal].x, _isGoals[isGoal].y);
                Instantiate(_items[item], position, Quaternion.identity);
                _isGoals.Remove(new Vector2(_isGoals[isGoal].x, _isGoals[isGoal].y));
            }
        }
        catch
        {
           //ingored
        }

    }
}
