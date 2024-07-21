using TMPro;
using UnityEngine;

public class DeathNoteUI : SwitchableUI
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private AudioClip _deathNoteSound;

    public override void Initialize()
    {
        DeathNote.OnUse += HandleDeathNoteUsage;
        _inputField.onEndEdit.AddListener(FindObjectWithName);
    }

    private void OnDisable()
    {
        _inputField.text = string.Empty;
    }

    private void HandleDeathNoteUsage()
    {
        OnShouldBeOpened?.Invoke(this);
    }

    public void FindObjectWithName(string name)
    {
        GameObject foundObject = GameObject.Find(name);
        IDamagable damageable;

       if(foundObject != null)
        {
            if(foundObject.TryGetComponent(out damageable))
            {
                if(damageable.IsAlive)
                {
                    damageable.Die();
                    GlobalSoundsPlayer.Instance.PlayOneShot(_deathNoteSound);
                }
            }
        }

        OnShouldBeClosed?.Invoke(this);
    }
}
