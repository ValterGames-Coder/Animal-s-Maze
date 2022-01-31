using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private int Width, Height, HowMuchToSpawn;
    
    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        Width = generator.Width;
        Height = generator.Height;

        for (int i = 0; i < HowMuchToSpawn; i++)
        {
            int item = Random.Range(0, HowMuchToSpawn);
            Vector2 position = new Vector2(Random.Range(0, Width), Random.Range(0, Height));
            Instantiate(_items[item], position, Quaternion.identity);
        }
    }
}
