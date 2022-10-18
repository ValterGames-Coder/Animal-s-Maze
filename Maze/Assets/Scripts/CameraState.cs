using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class CameraState : MonoBehaviour
{
    private Camera _camera;
    public int Level;
    [SerializeField] private List<Vector3> _positions;
    [SerializeField] private List<Vector2> _valueLimits;
    public List<Vector3> Sizes;
    [SerializeField] private List<Color> _colors;
    private MazeSpawner _mazeSpawner;
    [SerializeField] private TMP_Text _levelText;
    private WinsController _winsController;
    [HideInInspector] public int LevelForList;
    [SerializeField] private AudioSource _winAudio;
    [SerializeField] private GameObject _allMapButton;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _curve;
    private bool _allMapSelected;
    
    void Start()
    {
        _camera = Camera.main;
        _mazeSpawner = FindObjectOfType<MazeSpawner>();
        _winsController = FindObjectOfType<WinsController>();
        if(PlayerPrefs.HasKey("Level") == false) PlayerPrefs.SetInt("Level", 1);
        Level = PlayerPrefs.GetInt("Level");
        
        Debug.Log($"{_valueLimits}");

        for (int i = 0; i < _valueLimits.Count + 1; i++)
        {
            if (Level >= _valueLimits[i].x && Level <= _valueLimits[i].y)
            {
                LevelForList = i;
                Debug.Log($"Value: {_valueLimits[i].x} ; {_valueLimits[i].y}");
            }
            else
            {
                LevelForList = _valueLimits.Count;
            }
        }

        transform.position = _positions[LevelForList];
        _camera.orthographicSize = _positions[LevelForList].z;
        _camera.backgroundColor = _colors[LevelForList];

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        _levelText.text = $"Level: {Level}";
    }

}
