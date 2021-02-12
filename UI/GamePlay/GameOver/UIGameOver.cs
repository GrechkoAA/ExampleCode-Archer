using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Level _level;
    [SerializeField] private UnityEngine.Events.UnityEvent Actived;

    public event System.Action<bool> Enabled;

    public void OnShow(bool isFinish)
    {
        Actived?.Invoke();
        Enabled?.Invoke(isFinish);
    }

    private void OnEnable()
    {
        _level.SceneFinished += OnShow;
    }
}