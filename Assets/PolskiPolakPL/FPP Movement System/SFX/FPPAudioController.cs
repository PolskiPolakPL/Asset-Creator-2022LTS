using UnityEngine;

public class FPPAudioController : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] float velocityThreshold = 0.1f;

    [SerializeField] AudioClip[] jumpSounds;
    [SerializeField] AudioClip[] landingSounds;

    // Motor & Modules
    [Header("Modules")]
    [SerializeField] Motor motor;
    [SerializeField] MovementModule movementMod;
    [SerializeField] JumpModule jumpMod;
    [SerializeField] CrouchProneModule crouchProneMod;

    [Header("Stance Sounds")]
    [SerializeField] AudioClip[] crouchingSounds;
    [SerializeField] AudioClip[] layingDownSounds;
    [SerializeField] AudioClip[] standingUpSounds;

    [Header("Footsteps")]
    [SerializeField] AudioSource footstepSpource;
    [SerializeField] AudioClip footstepsAudioClip;
    [SerializeField] AudioSrcSettings sprintSrcSettings;
    [SerializeField] AudioSrcSettings runSrcSettings;

    private void Awake()
    {

        SubscribeAudio();
    }

    private void Update()
    {

        PlayFootstepsAudio();

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

    public void PlayFootstepsAudio()
    {

        // Play moving audio if the character is moving and on the ground.
        float currentSpeed = motor.GetHorizontalSpeed();
        if (currentSpeed < velocityThreshold || !motor.isGrounded)
        {
            footstepSpource.Stop();
            return;
        }

        switch (movementMod.moveType)
        {

            case MoveType.RUN:
                {
                    SetAudioSrcSettings(footstepSpource, runSrcSettings);
                }
                break;

            case MoveType.SPRINT:
                {
                    SetAudioSrcSettings(footstepSpource, sprintSrcSettings);
                }
                break;

            default:
                {
                    footstepSpource.Stop();
                    return;
                }
        }
        PlayAudioClip(footstepSpource, footstepsAudioClip);
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

    void SetAudioSrcSettings(AudioSource source, AudioSrcSettings audioSrcSettingsSO)
    {
        source.priority = audioSrcSettingsSO.Priority;
        source.volume = audioSrcSettingsSO.Volume;
        source.pitch = audioSrcSettingsSO.Pitch;
        source.panStereo = audioSrcSettingsSO.StereoPan;
        source.spatialBlend = audioSrcSettingsSO.SpatialBlend;
        source.reverbZoneMix = audioSrcSettingsSO.ReverbZoneMix;
    }

    void PlayAudioClip(AudioSource source, AudioClip audioClip)
    {
        if (!source)
            return;
        if (source.isPlaying)
            return;
        source.PlayOneShot(audioClip);
    }
}
