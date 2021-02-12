using UnityEngine;

public class UIPause : MonoBehaviour
{
    [SerializeField] private GameObject _boardPause;

    public void OnShow()
    {
        _boardPause.SetActive(!_boardPause.activeSelf);
        Time.timeScale = _boardPause.activeSelf == true ? 0 : 1;
    }

    public void OnExit()
    {
        Time.timeScale = 1;
    }
}