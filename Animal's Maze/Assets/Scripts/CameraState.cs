using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class CameraState : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private List<Vector2> _positions;
    public List<Vector3> Sizes;
    private MazeSpawner _mazeSpawner;
    [SerializeField] private TMP_Text _levelText;
    private WinsController _winsController;
    [HideInInspector] public int LevelForList;
    
    void Start()
    {
        _mazeSpawner = FindObjectOfType<MazeSpawner>();
        _winsController = FindObjectOfType<WinsController>();
        if(PlayerPrefs.HasKey("Level") == false) PlayerPrefs.SetInt("Level", 1);
        _level = PlayerPrefs.GetInt("Level");
        
        if (_level >= 1 && _level <= 9)
        {
            LevelForList = 0;
        }
        else if (_level >= 10 && _level <= 29)
        {
            LevelForList = 1;
        }
        else if (_level >= 30 && _level <= 59)
        {
            LevelForList = 2;
        }
        else if (_level >= 60 && _level <= 89)
        {
            LevelForList = 3;
        }
        else if (_level >= 90 && _level <= 119)
        {
            LevelForList = 4;
            Camera.main.orthographicSize = 9;
        }
        else if (_level >= 120 && _level <= 149)
        {
            LevelForList = 5;
            Camera.main.orthographicSize = 9;
        }
        else if (_level >= 150 && _level <= 199)
        {
            LevelForList = 6;
            Camera.main.orthographicSize = 10;
        }
        else if (_level >= 200 && _level <= 299)
        {
            LevelForList = 7;
            Camera.main.orthographicSize = 11;
        }
        else if (_level >= 300 && _level <= 399)
        {
            LevelForList = 8;
            Camera.main.orthographicSize = 12;
        }
        else if (_level >= 400 && _level <= 499)
        {
            LevelForList = 9;
            Camera.main.orthographicSize = 13;
        }
        else if (_level >= 500)
        {
            LevelForList = 10;
            Camera.main.orthographicSize = 15;
        }
        
        transform.position = _positions[LevelForList];
        _mazeSpawner.MazeZoneOffset = _positions[LevelForList];

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        _levelText.text = $"Уровень: {_level}";
    }

    void Update()
    {
        if (_winsController.Win) StartCoroutine(NextLevel());
    }
    
    private bool _nextLevelComplete;

    private IEnumerator NextLevel()
    {
        if (_nextLevelComplete == false) _level++;
        _nextLevelComplete = true;
        PlayerPrefs.SetInt("Level", _level);
        yield return new WaitForSeconds(3);
        SceneController.LoadScene(0);
    } 
    
}
