using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// GameAction -- Plays a sound locally where this object is placed, or in the dedicated sound channel.
/// </summary>
public class AudioPlayer : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Plays a sound locally where this object is placed, or in the dedicated sound channel.";
    [Tooltip("List of sounds this player will play. It will cycle through these and play the next on the list each time its activated.")]public List<AudioClip> sounds = new();
    [Tooltip("List of volumes to be lpayed relative for each of the sounds listed above.")]public List<float> volumes = new();
    [Tooltip("Play the sounds in a random order.")]public bool randomize = false;
    int iterator = 0;
    AudioSource audioSource;
    public enum SoundType {Local,Soundtrack,Ambience,SFX}
    [Tooltip("Play a local sound with spatial sound, or send sound to the game's dedicated global channels.")] public SoundType soundType = SoundType.Local;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Activate()
    {
        AudioClip clipToPlay;
        float volumeOfClip;
        if (randomize)
        {
            int i = Random.Range(0, sounds.Count - 1);
            clipToPlay = sounds[i];
            volumeOfClip = volumes[i];
        }
        else
        {
            clipToPlay = sounds[iterator];
            volumeOfClip = volumes[iterator];
            iterator = (iterator + 1) % sounds.Count;
        }

        if (sounds.Count != 0)
        {
            switch (soundType)
            {
                case SoundType.Local:
                    audioSource.clip = clipToPlay;
                    audioSource.volume = volumeOfClip;
                    audioSource.Play();
                    break;
                case SoundType.Soundtrack:
                    SoundManager.PlayMusic(clipToPlay,volumeOfClip);
                    break;
                case SoundType.Ambience:
                    SoundManager.PlayAmbience(clipToPlay, volumeOfClip);
                    break;
                case SoundType.SFX:
                    SoundManager.PlaySFX(clipToPlay, volumeOfClip);
                    break;
            }
        }
    }

    public override void Deactivate()
    {
        audioSource.Stop();
    }
}
