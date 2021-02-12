using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(UIGameOver))]
public class UIGameOverButton : MonoBehaviour
{
    [SerializeField] private UIGameOver _uiGameOver;
    [SerializeField] private RectTransform _panelButtons;
    [SerializeField] private float _delayAnchorPosY;
    [SerializeField] private Ease _ease;
    [SerializeField] private UnityEngine.UI.Button _play;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private string _nextScene;

    public string NextScene => _nextScene;

    private void Show(bool isFinish)
    {
        _panelButtons.gameObject.SetActive(true);

        if (isFinish == true)
        {
            _play.GetComponentInChildren<TMPro.TMP_Text>().text = isFinish == true ? "NEXT LEVEL" : "TRY NOW";
            GoNextScene();
        }

        _panelButtons.DOAnchorPosY(0, _delayAnchorPosY).SetEase(_ease);
    }

    private void GoNextScene()
    {
        _play.onClick.RemoveAllListeners();
        _play.onClick.AddListener(() => _sceneLoader.OnLoad(_nextScene));
    }

    private void OnEnable()
    {
        _uiGameOver.Enabled += Show;
    }
}