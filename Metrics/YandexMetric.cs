using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveSystem))]
public class YandexMetric : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;

    private Level _level;
    private IYandexAppMetrica _metric;

    private void Awake()
    {
        _level = FindObjectOfType<Level>();
        _metric = AppMetrica.Instance;
    }

    private void Start()
    {
        SetLevelStart();
    }

    private void SetLevelStart()
    {
        var parameters = new Dictionary<string, object>
        {
            {"level", _saveSystem.LevelNumber}
        };

        _metric.ReportEvent("level_start", parameters);
        _metric.SendEventsBuffer();
    }

    private void SetLevelFinish(bool isFinish)
    {
        var parameters = new Dictionary<string, object>
        {
           {"level", _saveSystem.LevelNumber},
           {"result", isFinish == true ? "win": "lose"},
           {"time", (int)Time.realtimeSinceStartup},
           {"progress", GetProgressPassingLevel(isFinish)}
        };

        _metric.ReportEvent("level_finish", parameters);
        _metric.SendEventsBuffer();
    }

    private int GetProgressPassingLevel(bool isFinish)
    {
        if (isFinish == false)
        {
            return 0;
        }
        else
        {
            float percentageMaximumAttempts = _level.MaximumAttempts / 100f;
            float result = _level.CurrentAttempts / percentageMaximumAttempts;

            return (int)result;
        }
    }

    private void OnEnable()
    {
        _level.SceneFinished += SetLevelFinish;
    }
}