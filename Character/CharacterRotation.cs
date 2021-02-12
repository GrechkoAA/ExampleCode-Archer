using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] private Transform _crosshair;

    private Transform _character;

    public void Init(Transform character)
    {
        _character = character;
    }

    private void Update()
    {
        var targetPosition = new Vector3(_crosshair.position.x, _character.position.y, _crosshair.position.z);
        _character.LookAt(targetPosition);
    }
}