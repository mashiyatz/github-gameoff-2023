using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneOnTimer : MonoBehaviour
{
    public float changeTime;
    public string sceneName;

    void Start()
    {
        AkSoundEngine.PostEvent("Play_Opening_Demon_Voice", gameObject);        
    }

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0)
        {
            AkSoundEngine.StopAll();
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
