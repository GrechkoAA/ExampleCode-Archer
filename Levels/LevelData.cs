using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private Level _level;

    private SaveSystem _saveSystem;
    private UIGameOverButton _uiGameOverButton;
    private SceneLoader _sceneLoader;
    private LevelIndicator levelIndicator;

    private void Awake()
    {
        levelIndicator = FindObjectOfType<LevelIndicator>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _saveSystem = FindObjectOfType<SaveSystem>();
        _uiGameOverButton = FindObjectOfType<UIGameOverButton>();
    }

    private void Start()
    {
        if (levelIndicator != null)
        {
            levelIndicator.NumberLevel.text = $"LVL {_saveSystem.LevelNumber}"; 
        }
    }

    public void OnGetLevelName(string name)
    {
        if (string.IsNullOrEmpty(_saveSystem.LevelName))
        {
            _sceneLoader.OnLoad(name);
        }
        else
        {
            _sceneLoader.OnLoad(_saveSystem.LevelName);
        }
    }

    private void OnEnable()
    {
        if (_level != null)
        {
            _level.SceneFinished += (isFinish) =>
            {
                if (isFinish == true)
                {
                    _saveSystem.LevelName = _uiGameOverButton.NextScene;
                    _saveSystem.LevelNumber++;  
                }
            };
        }
    }
}