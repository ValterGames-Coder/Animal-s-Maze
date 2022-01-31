using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private int Width, Height;
    [SerializeField] private int HowMuchToSpawn;
    [SerializeField] private CameraState _cameraState;
    [SerializeField] private HintRenderer _hintRenderer;

    void Start()
    {
        _cameraState = FindObjectOfType<CameraState>();
        Width = (int)_cameraState.Sizes[_cameraState.LevelForList].x;
        Height = (int)_cameraState.Sizes[_cameraState.LevelForList].y;
        HowMuchToSpawn = (int)_cameraState.Sizes[_cameraState.LevelForList].z;
        for (int i = 0; i < HowMuchToSpawn; i++)
        {
            int item = Random.Range(0, _items.Count);
            Vector2 position = new Vector2(Random.Range(1, Width - 1), Random.Range(1, Height - 1));
            Instantiate(_items[item], position, Quaternion.identity);
            // TODO: Сделать генерацию не настолько рандомной
        }
    }
}
