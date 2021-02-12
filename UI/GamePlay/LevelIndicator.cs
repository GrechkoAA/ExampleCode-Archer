using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Transform _indicatorParentPanel;
    [SerializeField] private Image _targetIndicatorTemplate;
    [SerializeField] private Level _level;
    [SerializeField] private ListTargetsLevel _listTargetsLevel;
    [SerializeField] private Image[] _targetsIcon;
    [SerializeField] private TMPro.TMP_Text _numberLevel;

    private int _countLvl;

    public TMPro.TMP_Text NumberLevel => _numberLevel;

    private void Start()
    {
        _targetsIcon = new Image[_listTargetsLevel.Count];
        SpawnTargetIndicatorIcon(_listTargetsLevel.Count);

        _rectTransform.DOAnchorPosY(40, 1).SetEase(Ease.OutCirc);
    }

    private void SpawnTargetIndicatorIcon(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _targetsIcon[i] = Instantiate(_targetIndicatorTemplate, _indicatorParentPanel);
        }
    }

    private void OnEnable()
    {
        _level.Completed += () => _targetsIcon[_countLvl++].transform.GetChild(0).GetComponentInChildren<Image>().enabled = true;
    }
}
