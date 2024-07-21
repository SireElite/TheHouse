using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Collider))]
public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableAsset _cutscene;
    [SerializeField] private Color _triggerColor = Color.white;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() == true)
        {
            CutscenePlayer.Instance.PlayCutscene(_cutscene);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _triggerColor;
        Gizmos.DrawCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);
    }
}
