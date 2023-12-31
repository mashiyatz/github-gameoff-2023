using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event mainMenuMusic;
    [SerializeField]
    private AK.Wwise.Event playButtonSound;

    void Start()
    {
        mainMenuMusic.Post(gameObject);
        PlayerPrefs.SetInt("mainMenuMusicID", (int)mainMenuMusic.PlayingId);
    }

    public void ClickSound()
    {
        playButtonSound.Post(gameObject);
    }
}
