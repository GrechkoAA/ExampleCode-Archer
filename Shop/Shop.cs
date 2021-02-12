using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GoodsList _characterList;
    [SerializeField] private GoodsList _weaponsList;
    [SerializeField] private ProductButton _productTempate;
    [SerializeField] private Transform[] _parentPosition;
    [SerializeField] private ProductIconSelection _productIconSelection;
    [SerializeField] private ProductSelection _productSelection;
    [SerializeField] private BuyProduct _buyProduct;
    [SerializeField] private TransactionData _transactionData;
    [SerializeField] private Sprite _unidentifiedProduct;
    [SerializeField] private SaveSystem _saveSystem;

    private void Start()
    {
        Fill(_characterList, _parentPosition[(int)TypeProduct.Character], TypeProduct.Character);
        Fill(_weaponsList, _parentPosition[(int)TypeProduct.Weapon], TypeProduct.Weapon);
    }

    private void Fill(GoodsList list, Transform parentPosition, TypeProduct typeProduct)
    {
        var productButtons = new ProductButton[list.Length];
        var productData = new ProductData[list.Length];
        var purchaseData = _saveSystem.GetPurchasedGoods(typeProduct);

        for (int i = 0; i < list.Length; i++)
        {
            ProductButton newProduct = Instantiate(_productTempate);
            newProduct.transform.SetParent(parentPosition);
            newProduct.transform.localScale = Vector3.one;
            newProduct.Init(list[i], _productIconSelection, typeProduct, _productSelection, _buyProduct, _transactionData.IsItemPurchased(i, typeProduct), _unidentifiedProduct, purchaseData[i]);

            productButtons[i] = newProduct;
            productData[i] = list[i];
        }

        _productIconSelection.HighlightActiveProduct(productButtons[_transactionData.HighlightPromotionalProduct(typeProduct)].Selection, 
                                                     typeProduct, 
                                                     productData[_transactionData.HighlightPromotionalProduct(typeProduct)]);

        _productSelection.Chose(productData[_transactionData.HighlightPromotionalProduct(typeProduct)], typeProduct);
    }
}