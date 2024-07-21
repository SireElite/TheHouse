using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    [SerializeField] private Image _image;

    public bool HasItem { get; private set; }

    private Item _currentItem;
    private Rigidbody _currentItemRB;
    private GameObject _currentItemGO;
    private bool _isSelected;
    private IUsable _usable;

    public void Select()
    {
        _highlight.SetActive(true);
        _isSelected = true;

        if(HasItem)
        {
            _currentItemGO.SetActive(true);
            PlayerInteractions.Instance.DisablePickUpInputAction();
        }
    }

    public void TryToUseItem()
    {
        if(_usable != null)
        {
            _usable.Use();
        }
    }

    public void Deselect()
    {
        _highlight.SetActive(false);
        _isSelected = false;
        PlayerInteractions.Instance.EnablePickUpInputAction();

        if(_currentItem != null)
            _currentItemGO.SetActive(value: false);
    }

    public void AddItem(Item item)
    {
        _currentItem = item;
        HasItem = true;
        _image.enabled = true;
        _image.sprite = _currentItem.ItemSO.Icon;

        _currentItemRB = _currentItem.GetComponent<Rigidbody>();
        _currentItemRB.isKinematic = true;

        _currentItemGO = _currentItem.gameObject;
        _usable = _currentItem.GetComponent<IUsable>();

        if(_isSelected == true)
        {
            _currentItemGO.SetActive(true);
            PlayerInteractions.Instance.DisablePickUpInputAction();
        }
        else
        {
            _currentItemGO.SetActive(false);
        }

        Transform playerHand = Player.Instance.GetHandTransform();
        Transform itemTransform = _currentItem.transform;
        itemTransform.parent = playerHand;
        itemTransform.position = playerHand.position;
        itemTransform.rotation = playerHand.rotation;
    }

    public void DropItem()
    {
        _usable = null;
        _currentItem.Drop();
        _currentItem = null;
        _image.sprite = null;
        _image.enabled = false;
        HasItem = false;
    }
}
