using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    public static PlayerInteractions Instance;

    [SerializeField] private Transform _grabPoint;
    [SerializeField] private Transform _dropPoint;
    [SerializeField] private float _interactRadius;
    [SerializeField] private float _grabDistance;
    [SerializeField] private float _pickUpDistance;
    [SerializeField] private float _clickDistance;

    public static Action<int> OnCoinFound;
    public static Action OnCoinNotFound;

    private const int IgnorePlayerBitmask = ~(1 << 7);

    private Item _grabbedItem;
    private Camera _camera;
    private Transform _cameraTransform;
    private Transform _cachedTransform;
    private PlayerInputActions _playerInputActions;
    private Collider _nearestInteractableCollider;
    private InteractableWithUIBehavior _nearestInteractable;
    private float _distanceToNearestInteractableCollider = float.MaxValue;

    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"Have more than 1 {name} Instance");
        }
        #endregion

        _camera = Camera.main;
        _cameraTransform = _camera.GetComponent<Transform>();
        _cachedTransform = GetComponent<Transform>();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _playerInputActions.Player.Interact.started += TryToInteract;
        _playerInputActions.Player.GrabPickUp.started += TryToGrab;
        _playerInputActions.Player.Click.started += TryToClickOnObject;
        Inventory.OnItemAdded += DiscardGrabbedItem;
        Inventory.OnDontHaveSpace += DiscardGrabbedItem;
        StoreItem.OnPurchased += HandlePurchasing;

        InvokeRepeating(nameof(TryToFindCoins), 0f, 0.1f);
        InvokeRepeating(nameof(FindInteracteableObjects), 0f, 0.1f);
        InvokeRepeating(nameof(MoveGrabPoint), 0f, 0.05f);
    }
    
    public Transform GetDropPoint()
    {
        return _dropPoint;
    }

    public Transform GetGrabPoint()
    {
        return _grabPoint;
    }

    public void DropGrabbedItem()
    {
        _grabbedItem.Drop();
        _grabbedItem = null;
    }

    public void DisablePickUpInputAction()
    {
        _playerInputActions.Player.GrabPickUp.Disable();
    }

    public void EnablePickUpInputAction()
    {
        _playerInputActions.Player.GrabPickUp.Enable();
    }

    private void HandlePurchasing(Item item)
    {
        if(_grabbedItem == item)
            DropGrabbedItem();
    }

    private void MoveGrabPoint()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);

        RaycastHit hit;

        int IgnorePlayerAndGrabbableBitmask = ~((1 << 10) | (1 << 7));

        if(Physics.Raycast(ray, out hit, _grabDistance, IgnorePlayerAndGrabbableBitmask, QueryTriggerInteraction.Ignore))
        {
            float offset = 0.3f;
            _grabPoint.position = hit.point + hit.normal * offset;
        }
        else
        {
            _grabPoint.position = _cameraTransform.position + _cameraTransform.forward * _grabDistance;
        }
    }

    private void DiscardGrabbedItem()
    {
        _grabbedItem = null;
    }

    private void TryToFindCoins()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, _pickUpDistance, IgnorePlayerBitmask, QueryTriggerInteraction.Ignore))
        {
            Coin coin;

            if(hit.collider.TryGetComponent<Coin>(out coin))
            {
                OnCoinFound?.Invoke(coin.Value);
                return;
            }

            OnCoinNotFound?.Invoke();
        }
        else
        {
            OnCoinNotFound?.Invoke();
        }
    }

    private void TryToGrab(InputAction.CallbackContext context)
    {
        if(_grabbedItem == null)
        {
            RaycastHit hitInfo;

            if(Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hitInfo, _pickUpDistance, IgnorePlayerBitmask, QueryTriggerInteraction.Ignore))
            {
                if(hitInfo.collider.TryGetComponent<Item>(out _grabbedItem))
                {
                    _grabbedItem.Take();
                }
            }
        }
        else
        {
            DropGrabbedItem();
        }
    }

    private void TryToClickOnObject(InputAction.CallbackContext context)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo; 

        IClickable clickable;

        if(Physics.Raycast(ray, out hitInfo, _clickDistance, IgnorePlayerBitmask, QueryTriggerInteraction.Ignore))
        {
            if(hitInfo.collider.TryGetComponent<IClickable>(out clickable))
            {
                clickable.HandleClick();
            }
        }
    }

    private void TryToInteract(InputAction.CallbackContext context)
    {
        if (_nearestInteractable != null)
            _nearestInteractable.Interact();
    }

    private void FindInteracteableObjects()
    {
        int InteractableBitmask = 1 << 11;

        Collider[] colliders = Physics.OverlapSphere(_cachedTransform.position, _interactRadius, InteractableBitmask);

        if(colliders.Length == 0 && _nearestInteractable != null)
        {
            _nearestInteractable.DisableInteractUI();
            _nearestInteractable = null;
            _nearestInteractableCollider = null;
            return;
        }

        foreach(Collider collider in colliders)
        {
            float distance = Vector3.Distance(_cachedTransform.position, collider.transform.position);

            if(_nearestInteractableCollider != null)
                _distanceToNearestInteractableCollider = Vector3.Distance(_cachedTransform.position, _nearestInteractableCollider.transform.position);

            InteractableWithUIBehavior previousInteractable;

            if(distance < _distanceToNearestInteractableCollider)
            {
                if(_nearestInteractableCollider != null)
                {
                    previousInteractable = _nearestInteractableCollider.GetComponent<InteractableWithUIBehavior>();
                    previousInteractable.DisableInteractUI();
                }

                _distanceToNearestInteractableCollider = distance;
                _nearestInteractableCollider = collider;
                _nearestInteractable = _nearestInteractableCollider.GetComponent<InteractableWithUIBehavior>();
                _nearestInteractable.EnableInteractUI();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(_cameraTransform == null)
            _cameraTransform = Camera.main.transform;

        if(_cachedTransform == null)
            _cachedTransform = GetComponent<Transform>();

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_cachedTransform.position, _interactRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_cameraTransform.position, _cameraTransform.forward * _pickUpDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_cameraTransform.position, _cameraTransform.forward * _grabDistance);
    }
}
