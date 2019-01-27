using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBGM
{
    None,
    //Title,
    Stage,
}

public enum eSound
{
    None,
    DoorOpen,
    GunShoot,
    Hit,
    PlayerSound,
    ZonbieHit
}

public class MusicSystem : SingletonMono<MusicSystem>
{
    private AudioSource AudioSourceBGM = null;
    private AudioSource AudioSourceSound = null;

    private eBGM BGM_Now = eBGM.None;

    private void Awake()
    {
        if (AudioSourceBGM == null)
        {
            AudioSourceBGM = this.gameObject.AddComponent<AudioSource>();
            AudioSourceBGM.volume = 0.5f;
        }
        if (AudioSourceSound == null)
        {
            AudioSourceSound = this.gameObject.AddComponent<AudioSource>();
        }
    }

    private AudioClip AudioClipBGM = null;
    public void PlayBGM(eBGM bgmName, bool isLoop = true)
    {
        if (bgmName == BGM_Now)
        {
            return;
        }

        AudioClipBGM = null;
        AudioClipBGM = Resources.Load<AudioClip>("Music/BGM/" + bgmName.ToString());
        if (AudioClipBGM != null)
        {
            BGM_Now = bgmName;

            AudioSourceBGM.clip = AudioClipBGM;
            AudioSourceBGM.loop = isLoop;
            AudioSourceBGM.Play();
        }
    }

    private AudioClip AudioClipSound = null;
    public void PlaySound(eSound soundName)
    {
        AudioClipSound = null;
        AudioClipSound = Resources.Load<AudioClip>("Music/Sound/" + soundName.ToString());
        if (AudioClipSound != null)
        {
            AudioSourceSound.PlayOneShot(AudioClipSound);
        }
    }
}