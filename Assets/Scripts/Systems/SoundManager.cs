using UnityEngine;

/// <summary>
/// Game System -- Manages the dedicated game sound channels.
/// </summary>
public class SoundManager : MonoBehaviour
{
    #region Description for Unity Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Game System --\n" +
        "Manages the dedicated game sound channels.";
    #endregion

    #region Instatiating as a Singleton
    static SoundManager singleton;
    private void Awake()
    {
        if(singleton != null)
        {
            Destroy(this);
        }
        singleton = this;
    }
    #endregion
    #region Inspector-Editable Variables
    // Dedicated Audio Channels
    public AudioSource musicPlayer;
    public AudioSource ambiencePlayer;
    public AudioSource sfxPlayer;
    #endregion

    #region Functions
    /// <summary>
    /// Plays music in a loop on the dedicated music channel.
    /// </summary>
    /// <param name="clip">The song to be played.</param>
    /// /// <param name="volume">Volume of clip.</param>
    public static void PlayMusic(AudioClip clip, float volume)
    {
        singleton.musicPlayer.clip = clip;
        singleton.musicPlayer.volume = volume;
        singleton.musicPlayer.Play();
    }
    /// <summary>
    /// Stops whatever is playing in the dedicated music channel.
    /// </summary>
    public static void StopMusic()
    {
        singleton.musicPlayer.Stop();
    }
    /// <summary>
    /// Plays background noise on a lopp in the dedicated ambience channel.
    /// </summary>
    /// <param name="clip">The noise to be played.</param>
    /// /// <param name="volume">Volume of clip.</param>
    public static void PlayAmbience(AudioClip clip, float volume)
    {
        singleton.ambiencePlayer.clip = clip;
        singleton.ambiencePlayer.Play();
    }
    /// <summary>
    /// Stops whatever is playing in the dedicated ambience channel.
    /// </summary>
    public static void StopAmbience()
    {
        singleton.ambiencePlayer.Stop();
    }
    /// <summary>
    /// Plays a one-off sound effect in the dedicated SFX channel.
    /// </summary>
    /// <param name="clip">Sound to be played.</param>
    /// <param name="volume">Volume of clip.</param>
    public static void PlaySFX(AudioClip clip, float volume)
    {
        singleton.sfxPlayer.clip = clip;
        singleton.sfxPlayer.Play();
    }
    /// <summary>
    /// Stops whatever is playing in the dedicated SFX channel. (Not needed if your clip ends at the time you want it to.)
    /// </summary>
    public static void StopSFX() {
        singleton.sfxPlayer.Stop();
    }
    #endregion
}
