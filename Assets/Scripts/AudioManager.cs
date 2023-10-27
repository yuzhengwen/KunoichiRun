using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource jumpSfx, landSfx, runSfx, deathSfx, damageSfx;

    public static AudioManager Instance { get; private set; }
    public enum AudioType
    {
        Jump, Land, Run, Death, Damage
    }

    private void Awake()
    {
        Instance = this;
        runSfx.enabled = false;
    }
    public void playAudio(AudioType type)
    {
        switch (type)
        {
            case AudioType.Jump:
                jumpSfx.Play();
                break;
            case AudioType.Land:
                landSfx.Play();
                break;
            case AudioType.Run:
                runSfx.enabled = true;
                break;
            case AudioType.Death:
                deathSfx.Play();
                break;
            case AudioType.Damage:
                damageSfx.Play();
                break;
        }
    }
    public void stopAudio(AudioType type)
    {
        switch (type)
        {
            case AudioType.Run:
                runSfx.enabled = false;
                break;
        }
    }
}
