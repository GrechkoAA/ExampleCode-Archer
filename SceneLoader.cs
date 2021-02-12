using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] UnityEngine.Events.UnityEvent Loading;

    public void OnLoad(string scene)
    {
        Loading?.Invoke();
        SceneManager.LoadScene(scene);
    }

    public void OnRestart()
    {
        Loading?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}