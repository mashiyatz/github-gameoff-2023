using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCheck : MonoBehaviour
{

    public Character demon;

    public string goodEnd;
    public string badEnd;
    public bool isEvil;

    private int evilCheck;
    // Start is called before the first frame update
    void Start()
    {
        if (MainManager.Instance.toyChoice) {
            evilCheck += 1;
        };
        if(MainManager.Instance.knifeChoice) { evilCheck += 1; };
        if(MainManager.Instance.locketChoice) { evilCheck += 1; };
        
        if(evilCheck > 1)
        {
            isEvil = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (demon.currentStatus == Character.STATUS.DEAD)
        {
            if (isEvil == true)
            {
                SceneManager.LoadScene(badEnd);
            }
            if (isEvil == false)
            {
                SceneManager.LoadScene(goodEnd);
            }
        }
    }
}
