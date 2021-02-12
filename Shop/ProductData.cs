using UnityEngine;

[CreateAssetMenu(fileName = "NewGood", menuName = "Shop/Good")]
public class ProductData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private GameObject[] _model;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private bool _isThrowingWeapon;

    public int ID => _id;

    public Sprite Sprite => _sprite;

    public int Price => _price;

    public bool IsThrowingWeapon => _isThrowingWeapon;

    public GameObject GetModelWeapon()
    {
        return _model[0];
    }

    public GameObject GetModelBullet()
    {
        return _model[_model.Length - 1];
    }
}