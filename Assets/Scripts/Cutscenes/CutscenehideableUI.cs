using UnityEngine;

public class CutscenehideableUI : MonoBehaviour
{
    private void Awake()
    {
        CutscenePlayer.Instance.OnPlayed += DisableSelf;
        CutscenePlayer.Instance.OnStopped += EnableSelf;
    }

    private void EnableSelf()
    {
        gameObject.SetActive(true);
    }

    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
