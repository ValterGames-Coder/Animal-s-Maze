using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class CameraState : MonoBehaviour
{
    private Camera _camera;
    public int Level;
    [SerializeField] private List<Vector3> _positions;
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
        
        if (Level >= 1 && Level <= 9)
        {
            LevelForList = 0;
        }
        else if (Level >= 10 && Level <= 29)
        {
            LevelForList = 1;
        }
        else if (Level >= 30 && Level <= 59)
        {
            LevelForList = 2;
        }
        else if (Level >= 60 && Level <= 89)
        {
            LevelForList = 3;
        }
        else if (Level >= 90 && Level <= 119)
        {
            LevelForList = 4;
        }
        else if (Level >= 120 && Level <= 149)
        {
            LevelForList = 5;
        }
        else if (Level >= 150 && Level <= 199)
        {
            LevelForList = 6;
        }
        else if (Level >= 200 && Level <= 299)
        {
            LevelForList = 7;
        }
        else if (Level >= 300 && Level <= 399)
        {
            LevelForList = 8;
        }
        else if (Level >= 400 && Level <= 499)
        {
            LevelForList = 9;
        }
        else if (Level >= 500)
        {
            LevelForList = 10;
        }
        
        transform.position = _positions[LevelForList];
        _camera.orthographicSize = _positions[LevelForList].z;
        _camera.backgroundColor = _colors[LevelForList];

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        _levelText.text = $"Level: {Level}";
    }

}
