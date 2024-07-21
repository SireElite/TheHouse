using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CoinPickUpCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Coin coin;

        if(collision.gameObject.TryGetComponent<Coin>(out coin))
        {
            coin.PickUp();
        }
    }
}
