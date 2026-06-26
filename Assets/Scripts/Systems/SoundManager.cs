using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager current;
    private void Awake()
    {
        if(current != null)
        {
            Destroy(this);
        }
        current = this;
    }

    public AudioSource musicPlayer;
    public AudioSource ambiencePlayer;
    public AudioSource sfxPlayer;

    public static void PlayMusic(AudioClip clip)
    {
        current.musicPlayer.clip = clip;
        current.musicPlayer.Play();
    }
    public static void StopMusic()
    {
        current.musicPlayer.Stop();
    }
    public static void PlayAmbience(AudioClip clip)
    {
        current.ambiencePlayer.clip = clip;
        current.ambiencePlayer.Play();
    }
    public static void StopAmbience()
    {
        current.ambiencePlayer.Stop();
    }
    public static void PlaySFX(AudioClip clip)
    {
        current.sfxPlayer.clip = clip;
        current.sfxPlayer.Play();
    }
    public static void StopSFX() {
        current.sfxPlayer.Stop();
    }
}
