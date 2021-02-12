using UnityEngine;

public class SpawnProduct : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private GoodsList _characterList;
    [SerializeField] private GoodsList _weaponsList;
    [SerializeField] private Transform _parent;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Vector3 _positionCharacter;
    [SerializeField] private CharacterAnimation _weaponAnimation;
    [SerializeField] private CharacterRotation _characterRotation;

    private int _currentCharacter;
    private int _currentWeapon;

    private void Start()
    {   
        SetProduct();

        GameObject newCharacter = CreateCharacter();
        CreateWeapon(newCharacter.GetComponent<CharacterHand>());
        Init(newCharacter);
    }

    private void SetProduct()
    {
        _currentCharacter = _saveSystem.GetSelectedCharacter();
        _currentWeapon = _saveSystem.GetSelectedWeapon();
    }

    private GameObject CreateCharacter()
    {
        GameObject newCharacter = Instantiate(_characterList[_currentCharacter].GetModelWeapon());

        newCharacter.transform.SetParent(_parent);
        newCharacter.transform.localPosition = Vector3.zero + _positionCharacter;

        return newCharacter;
    }

    private void CreateWeapon(CharacterHand hand)
    {
        GameObject newWeapon = Instantiate(_weaponsList[_currentWeapon].GetModelWeapon());
        
        newWeapon.transform.SetParent(_weaponsList[_currentWeapon].IsThrowingWeapon == true ? hand.RightHand : hand.LeftHand);
        newWeapon.GetComponent<WeaponPosition>().SetTransform();
        newWeapon.transform.localScale /= 2;

        if (_weaponsList[_currentWeapon].IsThrowingWeapon == false)
        {
            CreateArrow(hand);
        }
        else
        {
            newWeapon.GetComponent<Collider>().enabled = false;
        }
    }

    private void CreateArrow(CharacterHand hand)
    {
        GameObject newArrow = Instantiate(_weaponsList[_currentWeapon].GetModelBullet());
        newArrow.GetComponent<Collider>().enabled = false;
        newArrow.transform.SetParent(hand.RightHand);
        newArrow.transform.localScale /= 2;
        newArrow.transform.localPosition = new Vector3(0, 0.009f, 0);
        newArrow.transform.localRotation = Quaternion.Euler(-90f, 0, 0);
    }

    private void Init(GameObject newCharacter)
    {
        var bulletAnimation = _weaponsList[_currentWeapon].GetModelBullet().GetComponent<BulletAnimation>();

        _weapon.Init(_weaponsList[_currentWeapon].GetModelBullet().GetComponent<WeaponMovement>(), bulletAnimation.ShotDelay);
        _weaponAnimation.Init(newCharacter.GetComponent<Animator>(), bulletAnimation.AnimationName);
        _characterRotation.Init(newCharacter.transform);
    }
}