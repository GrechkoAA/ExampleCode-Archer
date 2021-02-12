using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private Animator _animator;
    private string _animationName;

    public void Init(Animator animator, string animationName)
    {
        _animator = animator;
        _animationName = animationName;
    }

    private void Aim()
    {
        _animator.SetTrigger("isShot");
        _animator.SetTrigger(_animationName);
    }

    private void Sthrow(bool isShoots)
    {
        _animator.SetTrigger(isShoots == true ? "throw" : "exitFromThrow");
    }

    private void OnEnable()
    {
        _weapon.Aimed += Aim;
        _weapon.Shooting += Sthrow;
    }
}