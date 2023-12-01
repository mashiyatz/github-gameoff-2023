using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayToBattle : MonoBehaviour
{
    public string sceneName;
    public GameObject expositionBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        expositionBox.SetActive(true);
    }

    public void EnterFight()
    {
        SceneManager.LoadScene(sceneName);
    }
}
