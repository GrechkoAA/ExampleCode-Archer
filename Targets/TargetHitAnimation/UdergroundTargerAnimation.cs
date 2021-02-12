using DG.Tweening;
using UnityEngine;

public class UdergroundTargerAnimation : MonoBehaviour
{
    public void OnPlay()
    {
        transform.DOLocalRotate(new Vector3(0, 180, 0), 1, RotateMode.LocalAxisAdd);
    }
}