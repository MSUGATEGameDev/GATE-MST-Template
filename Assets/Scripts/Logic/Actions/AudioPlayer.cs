using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// GameAction -- Plays a sound locally where this object is placed, or in the dedicated sound channel.
/// </summary>
public class AudioPlayer : GameAction
{
    #region Description for Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Plays a sound locally where this object is placed, or in the dedicated sound channel.";
#endregion
    #region Inspector-Editable Variables
    [Tooltip("Sound this player will play when activated.")]public AudioClip sound;
    [Range(0,1)][Tooltip("Volume of sound to be played.")]public float volume = 1;
    public enum SoundType { Local, Soundtrack, Ambience, SFX }
    [Tooltip("Play a local sound with spatial sound, or send sound to the game's dedicated global channels.")] public SoundType soundType = SoundType.Local;
    #endregion

    #region Internal Variables
    int iterator = 0;
    AudioSource audioSource;
    #endregion

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public override void Activate()
    {
            switch (soundType)
            {
                case SoundType.Local:
                    audioSource.clip = sound;
                    audioSource.volume = volume;
                    audioSource.Play();
                    break;
                case SoundType.Soundtrack:
                    SoundManager.PlayMusic(sound, volume);
                    break;
                case SoundType.Ambience:
                    SoundManager.PlayAmbience(sound, volume);
                    break;
                case SoundType.SFX:
                    SoundManager.PlaySFX(sound, volume);
                    break;
            }

    }

    public override void Deactivate()
    {
        switch (soundType)
        {
            case SoundType.Local:
                audioSource.Stop();
                break;
            case SoundType.Soundtrack:
                SoundManager.StopMusic();
                break;
            case SoundType.Ambience:
                SoundManager.StopAmbience();
                break;
            case SoundType.SFX:
                SoundManager.StopSFX();
                break;
        }
    }
}
