using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase, RequireComponent(typeof(AudioSource))]
public class Store : MonoBehaviour
{
    [SerializeField] private List<GameObject> _woodenLogs;
    [SerializeField] private GameObject _entranceLog;
    [SerializeField] private BrokeMan _brokeMan;
    [SerializeField] private float _logBreakingDelay;
    [SerializeField] private float _storeOpeningDelay;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _brokeMan.OnMoneyCollected += OpenStore;
    }

    private void OpenStore()
    {
        StartCoroutine(OpenStoreCoroutine());
    }

    private IEnumerator OpenStoreCoroutine()
    {
        yield return new WaitForSeconds(_storeOpeningDelay);

        foreach(var log in _woodenLogs)
        {
            Destroy(log);
            _audioSource.Play();
            yield return new WaitForSeconds(_logBreakingDelay);
        }

        Destroy(_entranceLog);
        _audioSource.Play();
    }
}
