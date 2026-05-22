using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip plugIn;

    public void PlugIn() {
        sfx.PlayOneShot(plugIn);
    }
}
