using UnityEngine;

[CreateAssetMenu(fileName = "NewGoodsList", menuName = "Shop/GoodsList")]
public class GoodsList : ScriptableObject
{
    [SerializeField] private ProductData[] _goods;

    public ProductData this [int index] => _goods[index];

    public int Length => _goods.Length;
}