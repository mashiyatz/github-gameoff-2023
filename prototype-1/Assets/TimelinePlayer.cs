using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector director;

    public GameObject controlP;
    private bool hasPlayed = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += Director_Stopped;
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        if (hasPlayed)
        {
            SceneManager.LoadScene("Scenes/MainMenuScene");
        }
    }
}
