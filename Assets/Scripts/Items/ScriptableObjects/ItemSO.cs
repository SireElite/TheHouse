using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Sprite _icon;

    public GameObject Prefab => _prefab;
    public Sprite Icon => _icon;
}
