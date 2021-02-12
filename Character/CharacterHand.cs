using DG.Tweening;
using System;
using UnityEngine;

public class CharacterHand : MonoBehaviour
{
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;

    public Transform RightHand => _rightHand;

    public Transform LeftHand => _leftHand;

    private void Start()
    {
        if (transform.root.GetComponent<Weapon>() is Weapon weapon)
        {
            weapon.Shooted += HideWeapon;
            weapon.CanShooted += ShowWeapon;
        }
    }

    private void ShowWeapon()
    {
        _rightHand.gameObject.SetActive(true);
    }

    private void HideWeapon(WeaponMovement _, bool isTarget)
    {
        _rightHand.gameObject.SetActive(false);
    }
}