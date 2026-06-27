using UnityEngine;

/// <summary>
/// Game System -- Managest the dedicated game sound channels.
/// </summary>
public class SoundManager : MonoBehaviour
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Game System --\n" +
        "Manages the dedicated game sound channels.";
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

    /// <summary>
    /// Plays music in a loop on the dedicated music channel.
    /// </summary>
    /// <param name="clip">The song to be played.</param>
    /// /// <param name="volume">Volume of clip.</param>
    public static void PlayMusic(AudioClip clip, float volume)
    {
        current.musicPlayer.clip = clip;
        current.musicPlayer.volume = volume;
        current.musicPlayer.Play();
    }
    /// <summary>
    /// Stops whatever is playing in the dedicated music channel.
    /// </summary>
    public static void StopMusic()
    {
        current.musicPlayer.Stop();
    }
    /// <summary>
    /// Plays background noise on a lopp in the dedicated ambience channel.
    /// </summary>
    /// <param name="clip">The noise to be played.</param>
    /// /// <param name="volume">Volume of clip.</param>
    public static void PlayAmbience(AudioClip clip, float volume)
    {
        current.ambiencePlayer.clip = clip;
        current.ambiencePlayer.Play();
    }
    /// <summary>
    /// Stops whatever is playing in the dedicated ambience channel.
    /// </summary>
    public static void StopAmbience()
    {
        current.ambiencePlayer.Stop();
    }
    /// <summary>
    /// Plays a one-off sound effect in the dedicated SFX channel.
    /// </summary>
    /// <param name="clip">Sound to be played.</param>
    /// <param name="volume">Volume of clip.</param>
    public static void PlaySFX(AudioClip clip, float volume)
    {
        current.sfxPlayer.clip = clip;
        current.sfxPlayer.Play();
    }
    /// <summary>
    /// Stops whatever is playing in the dedicated SFX channel. (Not needed if your clip ends at the time you want it to.)
    /// </summary>
    public static void StopSFX() {
        current.sfxPlayer.Stop();
    }
}
