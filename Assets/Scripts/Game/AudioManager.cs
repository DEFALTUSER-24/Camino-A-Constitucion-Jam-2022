using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]    private     AudioClip[]     _music;
                        private     AudioSource     _src;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _src = GetComponent<AudioSource>();
        if (_music.Length > 0)
            PlayMusic(_music[Random.Range(0, _music.Length)]);
    }

    public void PlayMusic(AudioClip clip)
    {
        _src.Stop();
        _src.clip = clip;
        _src.Play();
    }

    public void PlaySound(AudioClip sound)
    {
        _src.PlayOneShot(sound);
    }
}
