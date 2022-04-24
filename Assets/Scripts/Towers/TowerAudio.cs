using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAudio : MonoBehaviour 
{
    public Tower tower;
    
    [Header("Audio")]
    public AudioClip shootSound;
    public AudioClip upgradeSound;


    public void PlayUpgradeSound()
    {
        tower.speaker.PlayOneShot(upgradeSound, 0.6f);
    }

    public void PlayAttackSound()
    {
        //Add some variety to the audio.
        tower.speaker.pitch = Random.Range(0.90f, 1.10f);
        tower.speaker.PlayOneShot(shootSound, Random.Range(0.65f, 0.75f));
    }

}
