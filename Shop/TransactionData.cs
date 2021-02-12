using UnityEngine;

public class TransactionData : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;

    private bool[] _purchasedGoodsCharacter;
    private bool[] _purchasedGoodsWeapon;
    private int _highlightedCharacterId;
    private int _highlightedWeaponId;

    private void Awake()
    {
        _highlightedCharacterId = _saveSystem.GetSelectedCharacter();
        _highlightedWeaponId = _saveSystem.GetSelectedWeapon();
        _purchasedGoodsCharacter = _saveSystem.GetPurchasedGoods(TypeProduct.Character);
        _purchasedGoodsWeapon = _saveSystem.GetPurchasedGoods(TypeProduct.Weapon);
    }

    public bool this[int index, TypeProduct typeProduct]
    {
        get
        {
            switch (typeProduct)
            {
                case TypeProduct.Character:
                    return _purchasedGoodsCharacter[index];
                case TypeProduct.Weapon:
                    return _purchasedGoodsWeapon[index];
            }

            throw new System.Exception("Wrong product type specified");
        }
    }

    public void SetPurchasedItem(int id, TypeProduct typeProduct)
    {
        switch (typeProduct)
        {
            case TypeProduct.Character:
                _purchasedGoodsCharacter[id] = true;
                _saveSystem.SetPurchasedGoods(id, _purchasedGoodsCharacter[id], TypeProduct.Character);
                break;
            case TypeProduct.Weapon:
                _purchasedGoodsWeapon[id] = true;
                _saveSystem.SetPurchasedGoods(id, _purchasedGoodsWeapon[id], TypeProduct.Weapon);
                break;
        }
    }

    public int HighlightPromotionalProduct(TypeProduct typeProduct)
    {
        switch (typeProduct)
        {
            case TypeProduct.Character:
                return _highlightedCharacterId;

            case TypeProduct.Weapon:
                return _highlightedWeaponId;
        }

        throw new System.Exception("Wrong product type specified");
    }

    public void SetSelectionProduct(int id, TypeProduct typeProduct)
    {
        switch (typeProduct)
        {
            case TypeProduct.Character:
                _highlightedCharacterId = id;
                _saveSystem.SetSelectedCharacter(_highlightedCharacterId);
                break;
            case TypeProduct.Weapon:
                _highlightedWeaponId = id;
                _saveSystem.SetSelectedWeapon(_highlightedWeaponId);
                break;
        }
    }

    public bool IsItemPurchased(int id, TypeProduct typeProduct)
    {
        if (typeProduct == TypeProduct.Character)
        {
            return _purchasedGoodsCharacter[id];
        }
        else if (typeProduct == TypeProduct.Weapon)
        {
            return _purchasedGoodsWeapon[id];
        }

        throw new System.Exception("Wrong product type specified");
    }
}