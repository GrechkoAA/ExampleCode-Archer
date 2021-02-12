using UnityEngine;
using UnityEngine.UI;

public class ProductButton : MonoBehaviour
{
    [SerializeField] private Image _selection;
    [SerializeField] private TMPro.TMP_Text _price;
    [SerializeField] private Image _imageProduct;
    [SerializeField] private Image _picturePrice;

    public Image Selection => _selection;

    private ProductData _product;
    private ProductIconSelection _productIconSelection;
    private TypeProduct _typeProduct;
    private ProductSelection _productSelection;
    private BuyProduct _buyProduct;

    public void Init(ProductData product, ProductIconSelection productIconSelection, TypeProduct typeProduct, ProductSelection productSelection, BuyProduct buyProduct, bool isItemPurchased, Sprite unidentifiedProduct, bool ItemIsPurchased)
    {
        _product = product;
        _price.text = product.Price.ToString();
        _imageProduct.sprite = ItemIsPurchased == true ? product.Sprite : unidentifiedProduct;
        _productIconSelection = productIconSelection;
        _typeProduct = typeProduct;
        _productSelection = productSelection;
        _buyProduct = buyProduct;
        _picturePrice.gameObject.SetActive(!isItemPurchased);
    }

    public void OnSelect()
    {
        _productIconSelection.Choose(_product, _selection, _typeProduct, _picturePrice);
        _productSelection.Chose(_product, _typeProduct);
        _buyProduct.SetProduct(_product, _picturePrice, _typeProduct, this);
    }

    public void SetImageProduct()
    {
        _imageProduct.sprite = _product.Sprite;
    }
}