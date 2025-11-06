using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    public void PlayOnce(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
