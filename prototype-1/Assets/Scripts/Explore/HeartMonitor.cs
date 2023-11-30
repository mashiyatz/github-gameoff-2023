using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMonitor : MonoBehaviour
{
    public Image goodHeart;
    public Image evilHeart;



    // Start is called before the first frame update
    void Start()
    {
        goodHeart.fillAmount = 0.4f;
        evilHeart.fillAmount = 0.4f;
    }

    public void AddFill(bool isSoulConsumed)
    {
        if (isSoulConsumed) StartCoroutine(ChangeFill(evilHeart));
        else StartCoroutine(ChangeFill(goodHeart));
    } 

    public float FillRatio()
    {
        // print(goodHeart.fillAmount / evilHeart.fillAmount);
        return goodHeart.fillAmount / evilHeart.fillAmount;
    }

    public float NormalizedFillRatio()
    {
        return ((FillRatio() - 0.4f) / 2.1f);
    }

    IEnumerator ChangeFill(Image ventricle)
    {
        float startTime = Time.time;
        float startFillAmount = ventricle.fillAmount;
        float duration = 3f;

        while (Time.time - startTime < duration)
        {
            ventricle.fillAmount = Mathf.Lerp(startFillAmount, startFillAmount + 0.2f, ((Time.time - startTime) / duration));
            float angle = Mathf.Lerp(30, -30, NormalizedFillRatio());
            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
