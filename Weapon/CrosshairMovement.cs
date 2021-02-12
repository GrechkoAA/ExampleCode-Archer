using UnityEngine;

public class CrosshairMovement : MonoBehaviour
{
    public void SetPosition(RaycastHit hit)
    {
        transform.position = hit.point + hit.normal * 0.01f;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);
    }
}