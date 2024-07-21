using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SceneVisibleTrigger : MonoBehaviour
{
    [SerializeField] protected Color _triggerColor;

    private BoxCollider _boxCollider;

    private void OnValidate()
    {
        if(_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider>();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = _triggerColor;
        Bounds colliderBounds = _boxCollider.bounds;
        Gizmos.DrawCube(colliderBounds.center, colliderBounds.size);
    }
}
