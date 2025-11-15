using UnityEngine;

public class AudioManagerLevel1 : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip shoot;
    public AudioClip damage;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
            SFXSource.PlayOneShot(clip);
    }

}
