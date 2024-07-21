using System.Collections.Generic;
using TMPro;
using UnityEngine;

[SelectionBase, RequireComponent(typeof(AudioSource))]
public class ATM : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private ATMBasicButton _forwardButton;
    [SerializeField] private ATMBasicButton _spawnCoinButton;
    [SerializeField] private ATMBasicButton _backwardButton;
    [SerializeField] private List<ValuePerSpawnButton> _valuePerSpawnButtons;

    [Header("Sounds")]
    [SerializeField] private AudioClip _coinSpawnedSound;
    [SerializeField] private AudioClip _couldNotSpawnCoinSoound;

    [Header("Other")]
    [SerializeField] private TextMeshProUGUI _totalNominalValueTMP;
    [SerializeField] private Transform _coinSpawnPointTransform;

    private ValuePerSpawnButton _selectedValuePerClickButton;
    private AudioSource _audioSource;
    private int _valuePerClick;
    private int _totalValue;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        foreach(var button in _valuePerSpawnButtons)
        {
            button.OnPressed += SelectButton;
        }
    }
    private void OnEnable()
    {
        _forwardButton.OnPressed += AddValue;
        _backwardButton.OnPressed += SubtractValue;
        _spawnCoinButton.OnPressed += SpawnCoin;
    }

    private void OnDisable()
    {
        _forwardButton.OnPressed -= AddValue;
        _backwardButton.OnPressed -= SubtractValue;
        _spawnCoinButton.OnPressed -= SpawnCoin;
    }

    private void SelectButton(ValuePerSpawnButton button)
    {
        if(_selectedValuePerClickButton == button)
            return;
    
        if(_selectedValuePerClickButton != null)
            _selectedValuePerClickButton.Deselect();

        _selectedValuePerClickButton = button;
        _valuePerClick = button.Value;
        button.Select();
    }

    public void AddValue()
    {
        _totalValue += _valuePerClick;
        _totalValue = Mathf.Clamp(_totalValue, 0, 1000);
        _totalNominalValueTMP.text = $"${_totalValue}";
    }

    public void SubtractValue()
    {
        _totalValue -= _valuePerClick;
        _totalValue = Mathf.Clamp(_totalValue, 0, 1000);
        _totalNominalValueTMP.text = $"${_totalValue}";
    }

    private void SpawnCoin()
    {
        if(_totalValue <= PlayerStats.Money && _totalValue != 0)
        {
            PlayerStats.SubtractMoney(_totalValue);
            CoinSpawner.Instance.Spawn(_totalValue, _coinSpawnPointTransform.position, 1, false, false);
            _audioSource.clip = _coinSpawnedSound;
        }
        else
        {
            _audioSource.clip = _couldNotSpawnCoinSoound;
        }

        _audioSource.Play();
    }
}
