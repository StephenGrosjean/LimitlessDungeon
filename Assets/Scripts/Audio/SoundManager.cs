using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
/// <summary>
/// Singleton for playing sound effects
/// </summary>
public class SoundManager : MonoBehaviour
{

    //SINGLETON//
    public static SoundManager instance;

    private void Awake() {
        instance = this;
    }
    //END SINGLETON//

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerSnapshot normalSnapshot, deadSnapshot, menuSnapshot;
    [SerializeField] private AudioClip
        button_Click,
        player_Walk,
        player_Die,
        player_Hit,
        enemy_Punch,
        enemy_Growl,
        enemy_Die,
        portal_Open,
        portal_Enter,
        portal_Key_Place,
        chest_Sound,
        item_Pick,
        block_Mine,
        block_Hit,
        sword_Swing,
        sword_Hit;

    private AudioSource source;

    public enum sound {
        Button_Click,
        Player_Walk,
        Player_Die,
        Player_Hit,
        Enemy_Punch,
        Enemy_Growl,
        Enemy_Die,
        Portal_Open,
        Portal_Enter,
        Portal_Key_Place,
        Chest_Sound,
        Item_Pick,
        Block_Mine,
        Block_Hit,
        Sword_Swing,
        Sword_Hit
    }

    

    void Start()
    {
        source = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "Levels") {
            PlaySound(SoundManager.sound.Portal_Enter);
        }

    }

    //AUDIO SNAPSHOTS
    public void SwitchToNormal(float transitionTime = 0.5f) {
        normalSnapshot.TransitionTo(transitionTime);
    }

    public void SwitchToDead(float transitionTime = 0.5f) {
        deadSnapshot.TransitionTo(transitionTime);
    }

    public void SwitchToMenu(float transitionTime = 0.5f) {
        menuSnapshot.TransitionTo(transitionTime);
    }
    //

    //PLAY SOUNDS
    public void PlaySound(sound soundToPlay) {
        switch (soundToPlay) {
            case sound.Block_Hit:
                source.PlayOneShot(block_Hit);
                break;
            case sound.Block_Mine:
                source.PlayOneShot(block_Mine);
                break;
            case sound.Button_Click:
                source.PlayOneShot(button_Click);
                break;
            case sound.Chest_Sound:
                source.PlayOneShot(chest_Sound);
                break;
            case sound.Enemy_Die:
                source.PlayOneShot(enemy_Die);
                break;
            case sound.Enemy_Growl:
                source.PlayOneShot(enemy_Growl);
                break;
            case sound.Enemy_Punch:
                source.PlayOneShot(enemy_Punch);
                break;
            case sound.Item_Pick:
                source.PlayOneShot(item_Pick);
                break;
            case sound.Player_Die:
                source.PlayOneShot(player_Die);
                break;
            case sound.Player_Walk:
                source.PlayOneShot(player_Walk);
                break;
            case sound.Player_Hit:
                source.PlayOneShot(player_Hit);
                break;
            case sound.Portal_Key_Place:
                source.PlayOneShot(portal_Key_Place);
                break;
            case sound.Portal_Open:
                source.PlayOneShot(portal_Open);
                break;
            case sound.Portal_Enter:
                source.PlayOneShot(portal_Enter);
                break;
            case sound.Sword_Hit:
                source.PlayOneShot(sword_Hit);
                break;
            case sound.Sword_Swing:
                source.PlayOneShot(sword_Swing);
                break;
        }
    }
}
