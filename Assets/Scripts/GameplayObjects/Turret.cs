using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource)), SelectionBase]
public class Turret : MonoBehaviour
{
    [SerializeField] private NPC _targetNPC;
    [SerializeField] private Transform _partToRotate;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private Light _shootLight;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _damage;

    private AudioSource _audioSource;
    private Renderer _targetNPCRenderer;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _targetNPCRenderer = _targetNPC.GFX.transform.GetComponent<Renderer>();
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while(true)
        {
            yield return new WaitForSeconds(_reloadTime);

            if (_targetNPC.IsAlive)
            {
                Vector3 targetGFXCenter = _targetNPCRenderer.bounds.center;

                _audioSource.Play();
                _partToRotate.LookAt(targetGFXCenter);

                Vector3 direction = _partToRotate.transform.position - targetGFXCenter;
                _targetNPC.SpawnBloodParticles(targetGFXCenter, Quaternion.LookRotation(direction));
                _targetNPC.TakeDamage(_damage);
                StartCoroutine(ToggleLight());
            }
        }
    }

    IEnumerator ToggleLight()
    {
        _shootLight.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _shootLight.enabled = false;
    }
}
