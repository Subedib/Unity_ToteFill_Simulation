using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class SceneLoader : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
