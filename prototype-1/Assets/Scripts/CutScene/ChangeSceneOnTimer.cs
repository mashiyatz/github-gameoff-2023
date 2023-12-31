using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneOnTimer : MonoBehaviour
{
    public float changeTime;
    public string sceneName;

    [SerializeField]
    AK.Wwise.Event openingVoice;

    void Start()
    {
        AkSoundEngine.PostEvent("Play_Opening_Demon_Voice", gameObject);        
    }

    void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0)
        {
            AkSoundEngine.StopPlayingID((uint)PlayerPrefs.GetInt("mainMenuMusicID"), 2000);
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
