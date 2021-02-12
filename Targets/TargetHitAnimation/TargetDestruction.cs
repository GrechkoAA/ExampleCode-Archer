using DG.Tweening;
using UnityEngine;

public class TargetDestruction : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _targets;
    [SerializeField] private Collider[] _colliders;

    public void OnPlayHitAnimation()
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            _targets[i].isKinematic = false;
            _targets[i].useGravity = true;
            _colliders[i].isTrigger = false;
        }

        DOTween.Sequence().AppendInterval(0.2f).AppendCallback(() =>
        {
            _targets[0].transform.SetParent(null);
            _targets[1].transform.SetParent(null);
        });

        _targets[0].AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
        _targets[1].AddForce(new Vector3(-1, 0, 0), ForceMode.Impulse);

        DOTween.Sequence()
               .AppendInterval(3)
               .AppendCallback(() => 
               {
                   for (int i = 0; i < _targets.Length; i++)
                   {
                       _targets[i].isKinematic = true;
                       _targets[i].useGravity = false;
                       _targets[i].detectCollisions = false;
                   }
               }
               );
    }
}