using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    private static RabbitController _rabbit;

    void Start()
    {
        _rabbit = FindObjectOfType<RabbitController>();
    }
    public static void LoadScene(int sceneId) => SceneManager.LoadScene(sceneId);
    
    public static void CanSwipe(bool canSwipe) => _rabbit.CanSwipe = canSwipe;
    
    private bool _nextLevelComplete;

    public void NextLevel()
    {
        CameraState cameraState = FindObjectOfType<CameraState>();
        if (_nextLevelComplete == false) cameraState.Level++;
        _nextLevelComplete = true;
        PlayerPrefs.SetInt("Level", cameraState.Level);
        LoadScene(0);
    }

}
