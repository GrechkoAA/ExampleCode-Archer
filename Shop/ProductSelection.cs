using UnityEngine;

public class ProductSelection : MonoBehaviour
{
    [SerializeField] private Transform _originPosition;
    [SerializeField] private Transform _handCharacter;

    private GameObject[] _previousProduct = new GameObject[System.Enum.GetValues(typeof(TypeProduct)).Length];
    private Transform _currentWeapon;
    private WeaponPosition _previousWeapon;

    public void Chose(ProductData product, TypeProduct typeProduct)
    {
        Destroy(_previousProduct[(int)typeProduct]);

        if (typeProduct == TypeProduct.Character && _currentWeapon != null)
        {
            _currentWeapon.transform.parent.DetachChildren();
        }

        CreateProduct(product, typeProduct);
    }

    private void CreateProduct(ProductData product, TypeProduct typeProduct)
    {
        GameObject newProduct = Instantiate(product.GetModelWeapon(), _originPosition);

        if (typeProduct == TypeProduct.Character)
        {
            _handCharacter = newProduct.GetComponent<CharacterHand>().RightHand;
            newProduct.transform.localScale = Vector3.one;
            newProduct.transform.rotation = Quaternion.Euler(15, 180, 0);

            if (_currentWeapon != null)
            {
                _currentWeapon.SetParent(_handCharacter);
                _previousWeapon.SetTransform();
            }
        }
        else if (typeProduct == TypeProduct.Weapon)
        {
            _currentWeapon = newProduct.transform;
            newProduct.transform.SetParent(_handCharacter);

            if (newProduct.TryGetComponent(out WeaponPosition weapon))
            {
                weapon.SetTransform();
                _previousWeapon = weapon;
            }
            else
            {
                throw new System.Exception("WeaponPosition not found");
            }
        }

        _previousProduct[(int)typeProduct] = newProduct;
    }
}