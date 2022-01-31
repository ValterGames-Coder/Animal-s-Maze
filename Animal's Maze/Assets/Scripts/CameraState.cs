using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CameraState : MonoBehaviour
{
    public int Level;
    [SerializeField] private List<Vector2> _positions;
    public List<Vector3> Sizes;
    private MazeSpawner _mazeSpawner;
    [SerializeField] private TMP_Text _levelText;
    private WinsController _winsController;
    public int LevelForList;
    
    void Start()
    {
        _mazeSpawner = FindObjectOfType<MazeSpawner>();
        _winsController = FindObjectOfType<WinsController>();
        if(PlayerPrefs.HasKey("Level") == false) PlayerPrefs.SetInt("Level", 1);
        Level = PlayerPrefs.GetInt("Level");
        
        if (Level >= 1 && Level <= 19)
        {
            LevelForList = 0;
        }
        else if (Level >= 20 && Level <= 39)
        {
            LevelForList = 1;
        }
        else if (Level >= 40 && Level <= 69)
        {
            LevelForList = 2;
        }
        else if (Level >= 70 && Level <= 99)
        {
            LevelForList = 3;
        }
        else if (Level >= 100 && Level <= 149)
        {
            LevelForList = 4;
        }
        else if (Level >= 150 && Level <= 199)
        {
            LevelForList = 5;
        }
        else if (Level >= 200)
        {
            LevelForList = 6;
            Camera.main.orthographicSize = 9;
        }
        
        transform.position = _positions[LevelForList];
        _mazeSpawner.MazeZoneOffset = _positions[LevelForList];

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        _levelText.text = $"Уровень: {Level}";
    }

    void Update()
    {
        if (_winsController.Win) StartCoroutine(NextLevel());
    }
    
    private bool _nextLevelComplete;

    private IEnumerator NextLevel()
    {
        if (_nextLevelComplete == false) Level++;
        _nextLevelComplete = true;
        PlayerPrefs.SetInt("Level", Level);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    } 
}
