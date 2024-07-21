using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public static event Action OnItemAdded;
    public static event Action OnDontHaveSpace;

    public List<InventorySlot> InventorySlots = new List<InventorySlot>();

    private InventorySlot _selectedSlot;
    private PlayerInputActions _playerInputActions;

    void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"Already have a {name} Instance");
        }
        #endregion

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Click.started += TryToUseItemInSelectedSlot;
        _playerInputActions.Inventory.KeyboardNubmers.started += SelectSlot;
        _playerInputActions.Inventory.DropItem.started += TryToDropItemInSelectedSlot;
        Player.OnLogicBlocked += DisableInputActions;
        Player.OnLogicUnblocked += EnableInputActions;
        PickupableBehaviour.OnItemShouldBeAddedToInventory += AddItem;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Click.started -= TryToUseItemInSelectedSlot;
        _playerInputActions.Inventory.KeyboardNubmers.started -= SelectSlot;
        _playerInputActions.Inventory.DropItem.started -= TryToDropItemInSelectedSlot;
        Player.OnLogicBlocked -= DisableInputActions;
        Player.OnLogicUnblocked -= EnableInputActions;
        PickupableBehaviour.OnItemShouldBeAddedToInventory -= AddItem;
    }

    public void AddItem(Item item)
    {
        foreach(var slot in InventorySlots)
        {
            if(slot.HasItem == false)
            {
                slot.AddItem(item);

                if(_selectedSlot == slot)
                    item.gameObject.SetActive(true);
                
                OnItemAdded?.Invoke();
                break;
            }
            else if(slot == InventorySlots[InventorySlots.Count - 1] && slot.HasItem)
            {
                OnDontHaveSpace?.Invoke();
            }
        }
    }

    public bool CanAddItem()
    {
        foreach(var slot in InventorySlots)
        {
            if(slot.HasItem == false)
            {
                return true;
            }
        }

        return false;
    }

    private void SelectSlot(InputAction.CallbackContext context)
    {
     //Конвертирует имя нажатого бинда из строки в int (их имена "1", "2", "3" ...)
        int slotIndex = int.Parse(context.control.name) - 1;

        if(slotIndex >= InventorySlots.Count || slotIndex < 0)
            return;

        InventorySlot slot;
        slot = InventorySlots[slotIndex];

        if(_selectedSlot == null)
        {
            slot.Select();
            _selectedSlot = slot;
        }
        else if(_selectedSlot == slot)
        {
            _selectedSlot.Deselect();
            _selectedSlot = null;
        }
        else
        {
            _selectedSlot.Deselect();
            slot.Select();
            _selectedSlot = slot;
        }
    }

    private void TryToUseItemInSelectedSlot(InputAction.CallbackContext context)
    {
        _selectedSlot?.TryToUseItem();
    }

    public void TryToDropItemInSelectedSlot(InputAction.CallbackContext context)
    {
        if(_selectedSlot == null || _selectedSlot.HasItem == false)
            return;

        _selectedSlot.DropItem();
        _selectedSlot.Deselect();
        _selectedSlot = null;
    }

    private void DisableInputActions()
    {
        _playerInputActions.Disable();
    }

    private void EnableInputActions()
    {
        _playerInputActions.Enable();
    }
}
