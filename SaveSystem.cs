using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private Save _save = new Save();

    public string Path { get; private set; }

    public string LevelName { get => _save.NameCurrentLevel; set => _save.NameCurrentLevel = value; }

    public int LevelNumber { get => _save.LevelNumber; set => _save.LevelNumber = value; }

    public int MusicVolume { get => _save.MusicVolume; set => _save.MusicVolume = value; }

    public int EffectVolume { get => _save.EffectVolume; set => _save.EffectVolume = value; }

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
              Path = System.IO.Path.Combine(Application.persistentDataPath, "Save.json");
#else
        Path = System.IO.Path.Combine(Application.dataPath, "Save.json");
#endif
        if (File.Exists(Path))
        {
            _save = JsonUtility.FromJson<Save>(File.ReadAllText(Path));
        }
    }

    public void SetGamePoints(int score)
    {
        _save.Points = score;
    }

    public int GetGamePoints()
    {
        return _save.Points;
    }

    public int GetSelectedCharacter()
    {
        return _save.CurrentCharacter;
    }

    public void SetSelectedCharacter(int _highlightedCharacterId)
    {
        _save.CurrentCharacter = _highlightedCharacterId;
    }

    public int GetSelectedWeapon()
    {
        return _save.CurrentWeapon;
    }

    public void SetSelectedWeapon(int _highlightedWeaponId)
    {
        _save.CurrentWeapon = _highlightedWeaponId;
    }

    public void InitializeProducts(bool[] productList, TypeProduct typeProduct)
    {
        switch (typeProduct)
        {
            case TypeProduct.Character:
                _save.PurchasedGoodsCharacter = productList;
                break;
            case TypeProduct.Weapon:
                _save.PurchasedGoodsWapon = productList;
                break;
        }
    }

    public void SetPurchasedGoods(int id, bool productList, TypeProduct typeProduct)
    {
        switch (typeProduct)
        {
            case TypeProduct.Character:
                _save.PurchasedGoodsCharacter[id] = productList;
                break;
            case TypeProduct.Weapon:
                _save.PurchasedGoodsWapon[id] = productList;
                break;
        }
    }

    public bool[] GetPurchasedGoods(TypeProduct typeProduct)
    {
        switch (typeProduct)
        {
            case TypeProduct.Character:
                return _save.PurchasedGoodsCharacter;
            case TypeProduct.Weapon:
                return _save.PurchasedGoodsWapon;
        }

        throw new System.Exception("Wrong product type specified");
    }

    public void OnSave()
    {
        File.WriteAllText(Path, JsonUtility.ToJson(_save));
    }

    private void OnApplicationQuit()
    {
        OnSave();
    }

    private void OnApplicationPause(bool pause)
    {
        OnSave();
    }
}

[System.Serializable]
public class Save
{
    public int Points;

    public bool[] PurchasedGoodsCharacter;
    public int CurrentCharacter;

    public bool[] PurchasedGoodsWapon;
    public int CurrentWeapon;

    public int LevelNumber = 1;
    public string NameCurrentLevel;

    public int MusicVolume;
    public int EffectVolume;
}