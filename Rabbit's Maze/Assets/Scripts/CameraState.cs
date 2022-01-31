using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CameraState : MonoBehaviour
{
    public int Level;
    [SerializeField] private List<Vector2> _positions;
    public List<Vector2> Sizes;
    private MazeSpawner _mazeSpawner;
    [SerializeField] private TMP_Text _levelText;
    private WinsController _winsController;

    
    void Start()
    {
        _mazeSpawner = FindObjectOfType<MazeSpawner>();
        _winsController = FindObjectOfType<WinsController>();
        if(PlayerPrefs.HasKey("Level") == false) PlayerPrefs.SetInt("Level", 1);
        Level = PlayerPrefs.GetInt("Level");
        if (Level >= 1 && Level <= 19)
        {
            transform.position = _positions[0];
            _mazeSpawner.MazeZoneOffset = _positions[0];
        }
        else if (Level >= 20 && Level <= 39)
        {
            transform.position = _positions[1];
            _mazeSpawner.MazeZoneOffset = _positions[1];
        }
        else if (Level >= 40 && Level <= 69)
        {
            transform.position = _positions[2];
            _mazeSpawner.MazeZoneOffset = _positions[2];
        }
        else if (Level >= 70 && Level <= 99)
        {
            transform.position = _positions[3];
            _mazeSpawner.MazeZoneOffset = _positions[3];
        }
        else if (Level >= 100 && Level <= 149)
        {
            transform.position = _positions[4];
            _mazeSpawner.MazeZoneOffset = _positions[4];
        }
        else if (Level >= 150 && Level <= 199)
        {
            transform.position = _positions[5];
            _mazeSpawner.MazeZoneOffset = _positions[5];
        }
        else if (Level >= 200)
        {
            transform.position = _positions[6];
            Camera.main.orthographicSize = 9;
            _mazeSpawner.MazeZoneOffset = _positions[6];
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        _levelText.text = $"Уровень: {Level}";
    }

    void Update()
    {
        if (_winsController.Win) StartCoroutine(NextLevel());
    }
    
    private bool _nextLevelComplete = false;

    private IEnumerator NextLevel()
    {
        if (_nextLevelComplete == false) Level++;
        _nextLevelComplete = true;
        PlayerPrefs.SetInt("Level", Level);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    } 
}
