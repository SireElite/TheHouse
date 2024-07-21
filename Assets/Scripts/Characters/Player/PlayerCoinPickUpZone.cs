using UnityEngine;

public class PlayerCoinPickUpZone : MonoBehaviour
{
    [SerializeField] private Vector3 PickUpZoneSize;

    private const int GrabbableBitmask = 1 << 10;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        InvokeRepeating(nameof(CheckForCoins), 0, 0.1f);
    }

    private void OnValidate()
    {
        if(_characterController == null)
            _characterController = GetComponent<CharacterController>();
    }

    private void CheckForCoins()
    {
        Collider[] colliders = Physics.OverlapBox(_characterController.bounds.center, PickUpZoneSize / 2, Quaternion.identity, GrabbableBitmask);

        if(colliders != null)
        {
            foreach(var collider in colliders)
            {
                Coin coin;

                if(collider.TryGetComponent<Coin>(out coin))
                {
                    coin.PickUp();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_characterController.bounds.center, PickUpZoneSize);
    }
}
