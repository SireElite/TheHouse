using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 _openedRotation;

    public void Open()
    {
        transform.rotation = Quaternion.Euler(_openedRotation);
    }
}
