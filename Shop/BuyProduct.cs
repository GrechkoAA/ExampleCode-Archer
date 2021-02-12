using UnityEngine;

public class BuyProduct : MonoBehaviour
{
    [SerializeField] private GamePointsModel _gamePointsModel;
    [SerializeField] private TransactionData _transactionData;

    private ProductData[] _currentProductData;
    private UnityEngine.UI.Image[] _currentPicturePrice;
    private TypeProduct _currentTypeProduct;
    private ProductButton _currentProductButton;

    public event System.Action<TypeProduct> Buyed;

    private void Awake()
    {
        _currentProductData = new ProductData[System.Enum.GetValues(typeof(TypeProduct)).Length];
        _currentPicturePrice = new UnityEngine.UI.Image[System.Enum.GetValues(typeof(TypeProduct)).Length];
    }

    private void Start()
    {
        transform.gameObject.SetActive(false);
    }

    public void OnBuy()
    {
        _gamePointsModel.OnUpdateScore(-_currentProductData[(int)_currentTypeProduct].Price);
        _transactionData.SetPurchasedItem(_currentProductData[(int)_currentTypeProduct].ID, _currentTypeProduct);
        _transactionData.SetSelectionProduct(_currentProductData[(int)_currentTypeProduct].ID, _currentTypeProduct);

        transform.gameObject.SetActive(false);
        _currentPicturePrice[(int)_currentTypeProduct].gameObject.SetActive(false);

        Buyed?.Invoke(_currentTypeProduct);
        _currentProductButton.SetImageProduct();
    }

    public void SetProduct(ProductData productData, UnityEngine.UI.Image picturePrice, TypeProduct typeProduct, ProductButton productButton)
    {
        _currentProductData[(int)typeProduct] = productData;
        _currentPicturePrice[(int)typeProduct] = picturePrice;
        _currentTypeProduct = typeProduct;
        _currentProductButton = productButton;
    }

    public void OnChangeTypeProduct(int idProduct)
    {
        _currentTypeProduct = (TypeProduct)idProduct;
    }

    public void SetOriginProduct(ProductData productData, TypeProduct typeProduct)
    {
        if (typeProduct == TypeProduct.Character)
        {
            _currentProductData[(int)_currentTypeProduct] = productData;
        }
    }
}