using System.Collections.Generic;
using UnityEngine;

public class FPPAudioController : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] float velocityThreshold = 0.1f;

    [SerializeField] AudioClip[] jumpSounds;
    [SerializeField] AudioClip[] landingSounds;
    [SerializeField] AudioClip[] crouchingSounds;
    [SerializeField] AudioClip[] layingDownSounds;
    [SerializeField] AudioClip[] standingUpSounds;

    // Motor & Modules
    Motor motor;
    MovementModule movementMod;
    JumpModule jumpMod;
    CrouchProneModule crouchProneMod;

    private void Awake()
    {
        motor = GetComponentInChildren<Motor>();
        movementMod = GetComponentInChildren<MovementModule>();
        jumpMod = GetComponentInChildren<JumpModule>();
        crouchProneMod = GetComponentInChildren<CrouchProneModule>();

        SubscribeAudio();
    }

    void SubscribeAudio()
    {
        motor.OnLanded += PlayLandAudio;

        if (jumpMod)
            jumpMod.OnJump += PlayJumpAudio;

        if (crouchProneMod)
            crouchProneMod.OnStanceChanged += PlayStanceAudio;
    }

    public void PlayJumpAudio()
    {
        PlayRandomClip(source, jumpSounds);
    }
    public void PlayLandAudio()
    {
        PlayRandomClip(source, landingSounds);
    }
    public void PlayStanceAudio(PlayerStance stance)
    {
        if(stance == PlayerStance.Standing)
        {
            PlayRandomClip(source, standingUpSounds);
            return;
        }

        if(stance == PlayerStance.Crouch)
        {
            PlayRandomClip(source, crouchingSounds);
            return;
        }

        PlayRandomClip(source, layingDownSounds);
    }

    void PlayRandomClip(AudioSource source, AudioClip[] soundList)
    {
        if(soundList==null || soundList.Length <= 0)
        {
            Debug.LogWarning($"{soundList} is NULL or empty.");
            return;
        }

        AudioClip randomClip = soundList[Random.Range(0, soundList.Length)];
        source.PlayOneShot(randomClip);
    }
}
