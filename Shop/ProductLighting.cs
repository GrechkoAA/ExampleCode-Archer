using UnityEngine;

[RequireComponent(typeof(ProductIconSelection))]

public class ProductLighting : MonoBehaviour
{
    [SerializeField] private BuyProduct _buyProduct;
    [SerializeField] private TransactionData _transactionData;
    [SerializeField] private ProductIconSelection _productIconSelection;
    [SerializeField] private Light[] _lights;
    [SerializeField] private LayerMask _character;
    [SerializeField] private LayerMask _weapon;

    private void RefreshLight(ProductData productData, TypeProduct typeProduct)
    {
        if (ItemIsPurchased(productData, typeProduct) == true)
        {
            Illuminate(typeProduct);
        }
        else
        {
            Shade(typeProduct);
        }
    }

    private void Shade(TypeProduct typeProduct)
    {
        foreach (var light in _lights)
        {
            light.cullingMask &= TypeProduct.Character == typeProduct ? ~_character : ~_weapon;
        }
    }

    private void Illuminate(TypeProduct typeProduct)
    {
        foreach (var light in _lights)
        {
            light.cullingMask |= TypeProduct.Character == typeProduct ? _character : _weapon;
        }
    }

    private bool ItemIsPurchased(ProductData productData, TypeProduct typeProduct)
    {
        return _transactionData[productData.ID, typeProduct];
    }

    private void OnEnable()
    {
        _productIconSelection.Selected += (productData, typeProduct) => RefreshLight(productData, typeProduct);
        _buyProduct.Buyed += (typeProduct) => Illuminate(typeProduct);
    }
}