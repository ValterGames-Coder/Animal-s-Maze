using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static void LoadScene(int scene_id)
    {
        SceneManager.LoadScene(scene_id);
    }
    
}
