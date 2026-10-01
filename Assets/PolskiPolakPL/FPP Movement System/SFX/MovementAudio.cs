using System.Collections.Generic;
using UnityEngine;

public class MovementAudio : MonoBehaviour
{
    [SerializeField] AudioSource source;

    [SerializeField] List<AudioClip> jumpSounds;
    [SerializeField] List<AudioClip> landingSounds;
    [SerializeField] List<AudioClip> crouchingSounds;
    [SerializeField] List<AudioClip> layingDownSounds;
    [SerializeField] List<AudioClip> standingUpSounds;


    public void PlayRandomJumpSound()
    {
        PlayRandomClip(jumpSounds);
    }
    public void PlayRandomLandingSound()
    {
        PlayRandomClip(landingSounds);
    }
    public void PlayStanceChangeSound(PlayerStance stance)
    {
        if(stance == PlayerStance.Standing)
        {
            PlayRandomClip(standingUpSounds);
            return;
        }

        if(stance == PlayerStance.Crouch)
        {
            PlayRandomClip(crouchingSounds);
            return;
        }

        PlayRandomClip(layingDownSounds);
    }

    void PlayRandomClip(List<AudioClip> soundList)
    {
        if(soundList==null || soundList.Count <= 0)
        {
            Debug.LogWarning($"{soundList} is NULL or empty.");
            return;
        }

        AudioClip randomClip = soundList[Random.Range(0, soundList.Count)];
        source.PlayOneShot(randomClip);
    }
}
