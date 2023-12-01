using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMonitor : MonoBehaviour
{
    public Image goodHeart;
    public Image evilHeart;

    private float scaleMin = 0.7f;
    private float scaleMax = 0.85f;

    // Start is called before the first frame update
    void Start()
    {
        goodHeart.fillAmount = 0.4f;
        evilHeart.fillAmount = 0.4f;
    }

    private void Update()
    {
        float newSize = Mathf.Lerp(scaleMin, scaleMax, Mathf.InverseLerp(0, 1, Mathf.Pow(Mathf.Sin(Time.time), 63) * 8 * Mathf.Sin(Time.time + 1.5f)));
        transform.localScale = new Vector3(newSize, newSize, 1);
    }

    public void AddFill(bool isSoulConsumed)
    {
        StartCoroutine(ChangeFill(isSoulConsumed));
    } 

    public Image GetHeart(bool isSoulConsumed)
    {
        if (isSoulConsumed) return evilHeart;
        else return goodHeart;
    }

/*    public float FillRatio()
    {
        // print(goodHeart.fillAmount / evilHeart.fillAmount);
        return goodHeart.fillAmount / evilHeart.fillAmount;
    }

    public float NormalizedFillRatio()
    {
        return (FillRatio() - 0.4f/2.1f) / 2.1f;
    }*/

    IEnumerator ChangeFill(bool isSoulConsumed)
    {
        float startTime = Time.time;
        float startAngle = transform.eulerAngles.z;
        float finalAngle;
        Image ventricle = GetHeart(isSoulConsumed);
        float startFillAmount = ventricle.fillAmount;
        float duration = 2f;

        if (isSoulConsumed) finalAngle = transform.eulerAngles.z - 15f; 
        else finalAngle = transform.eulerAngles.z + 15f;

        while (Time.time - startTime < duration)
        {
            ventricle.fillAmount = Mathf.Lerp(startFillAmount, startFillAmount + 0.2f, ((Time.time - startTime) / duration));
            float angle = Mathf.LerpAngle(startAngle, finalAngle, ((Time.time - startTime) / duration));
            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
    }
}
