using UnityEngine;

public class UIStateSound : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _state;

    private bool _isActive = true;

    public void OnChangeState()
    {
        _isActive = !_isActive;
        _state.text = _isActive == true ? "ON" : "OFF"; 
    }
}