using UnityEngine;
using UnityEngine.UI;

public class ProductIconSelection : MonoBehaviour
{
    [SerializeField] private GamePointsModel _gamePointsModel;
    [SerializeField] private GameObject _buy;
    [SerializeField] private TransactionData _transactionData;

    private Image[] _previousPicture;
    private ProductData[] _previousproductData;

    public event System.Action<ProductData, TypeProduct> Selected;

    private void Awake()
    {
        _previousPicture = new Image[System.Enum.GetValues(typeof(TypeProduct)).Length];
        _previousproductData = new ProductData[System.Enum.GetValues(typeof(TypeProduct)).Length];
    }

    public void Choose(ProductData productData, Image picture, TypeProduct typeProduct, Image picturePrice)
    {
        SelectProduct(picture, typeProduct);
        ShowButton(productData, typeProduct, picturePrice);
        Selected?.Invoke(productData, typeProduct);
    }

    private void SelectProduct(Image picture, TypeProduct typeProduct)
    {
        if (_previousPicture[(int)typeProduct] != null)
        {
            _previousPicture[(int)typeProduct].enabled = false;
        }

        picture.enabled = true;

        _previousPicture[(int)typeProduct] = picture;

     
    }

    private void ShowButton(ProductData productData, TypeProduct typeProduct, Image picturePrice = null)
    {
        _previousproductData[(int)typeProduct] = productData;

        if (_transactionData[productData.ID, typeProduct] == false)
        {
            _buy.SetActive(productData.Price <= _gamePointsModel.Score);
        }
        else
        {
            if (picturePrice != null)
            {
                picturePrice.gameObject.SetActive(false);
            }

            _buy.SetActive(false);
            _transactionData.SetSelectionProduct(productData.ID, typeProduct);
        }
    }

    public void HighlightActiveProduct(Image picture, TypeProduct typeProduct, ProductData productData)
    {
        SelectProduct(picture, typeProduct);
        _previousproductData[(int)typeProduct] = productData;
    }

    public void OnSwitchProductPanel(int idTypeProduct)
    {
        if (_transactionData[_previousproductData[idTypeProduct].ID, (TypeProduct)idTypeProduct])
        {
            _buy.SetActive(false);
        }
        else
        {
            _buy.SetActive(_previousproductData[idTypeProduct].Price <= _gamePointsModel.Score);
        }
    }
}