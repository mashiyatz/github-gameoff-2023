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

    void Start()
    {
        if (MainManager.Instance)
        {
            if (MainManager.Instance.toyChoice) { evilCheck += 1; };
            if (MainManager.Instance.knifeChoice) { evilCheck += 1; };
            if (MainManager.Instance.locketChoice) { evilCheck += 1; };
        } else
        {
            evilCheck = Random.Range(0, 3);
        }

        if(evilCheck > 1)
        {
            isEvil = true;
        }
    }

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
