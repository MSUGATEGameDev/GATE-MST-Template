using UnityEngine;
using System.Collections.Generic;

public class AudioPlayer : GameAction
{
    [Tooltip("List of sounds this player will play. It will cycle through these and play the next on the list each time its activated.")]public List<AudioClip> sounds = new();
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
        if (randomize)
        {
            clipToPlay = sounds[Random.Range(0, sounds.Count - 1)];
        }
        else
        {
            clipToPlay = sounds[iterator];
            iterator = (iterator + 1) % sounds.Count;
        }

        if (sounds.Count != 0)
        {
            switch (soundType)
            {
                case SoundType.Local:
                    audioSource.clip = clipToPlay;
                    audioSource.Play();
                    break;
                case SoundType.Soundtrack:
                    SoundManager.PlayMusic(clipToPlay);
                    break;
                case SoundType.Ambience:
                    SoundManager.PlayAmbience(clipToPlay);
                    break;
                case SoundType.SFX:
                    SoundManager.PlaySFX(clipToPlay);
                    break;
            }
        }
    }

    public override void Deactivate()
    {
        audioSource.Stop();
    }
}
