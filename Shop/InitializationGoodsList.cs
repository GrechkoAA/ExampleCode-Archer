using UnityEngine;

public class InitializationGoodsList : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private GoodsList _characterList;
    [SerializeField] private GoodsList _weaponsList;
    [SerializeField] private bool[] _purchasedItemCharacters;
    [SerializeField] private bool[] _purchasedItemWeapons;
    [SerializeField] private int _highlightedCharacterId;
    [SerializeField] private int _highlightedWeaponId;
    [SerializeField] private int _coins;

    private void Awake()
    {
        if (System.IO.File.Exists(_saveSystem.Path) == false)
        {
            if (IsSizeArraysMatchesForInitialization())
            {
                throw new System.Exception("The size of the GoodsList array does not match the initialized PurchaseItem array");
            }

            _saveSystem.InitializeProducts(_purchasedItemCharacters, TypeProduct.Character);
            _saveSystem.InitializeProducts(_purchasedItemWeapons, TypeProduct.Weapon);
            _saveSystem.SetSelectedCharacter(_highlightedCharacterId);
            _saveSystem.SetSelectedWeapon(_highlightedWeaponId);
            _saveSystem.SetGamePoints(_coins);
        }
    }

    private bool IsSizeArraysMatchesForInitialization()
    {
        return _purchasedItemCharacters.Length != _characterList.Length || _purchasedItemWeapons.Length != _weaponsList.Length;
    }
}