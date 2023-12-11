using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour
{
    public Image healthAmount;
    public Sprite goodHeart;
    public Sprite badHeart;
    public Character charac;
    
    private int currentHealth;
    private int newHealth;

    public EndingCheck thoustEvil;
    
    // Start is called before the first frame update
    
    private float scaleMin = 1.55f;
    private float scaleMax = 1.85f;
    void Start()
    {
        if (thoustEvil.isEvil)
        {
            healthAmount.sprite = badHeart;
        }
        else
        {
            healthAmount.sprite = goodHeart;
        }
        currentHealth = charac.GetMaxHP(); // Set initial health
        newHealth = charac.GetMaxHP(); // Initialize newHealth

        // Update UI elements
        UpdateHealthFill();
    }

    // Update is called once per frame
    void Update()
    {
        float newSize;
        if (charac.currentStatus == Character.STATUS.BLEEDING)
        {
            newSize = Mathf.Lerp(scaleMin, scaleMax, Mathf.InverseLerp(0, 1, Mathf.Pow(Mathf.Sin(Time.time * 2), 63) * 8 * Mathf.Sin((Time.time + 1.5f) * 2)));
        }
        else
        {
            newSize = Mathf.Lerp(scaleMin, scaleMax, Mathf.InverseLerp(0, 1, Mathf.Pow(Mathf.Sin(Time.time), 63) * 8 * Mathf.Sin(Time.time + 1.5f)));
        }
        transform.localScale = new Vector3(newSize, newSize, 1);
    }
    
    private void UpdateHealthFill()
    {
        // Assuming max health is 100 (adjust as needed)
        healthAmount.fillAmount = (float)currentHealth / charac.GetMaxHP();
    }

    public void ChangeHealth()
    {
        newHealth = charac.GetCurrentHP();
        StartCoroutine(ChangeFillHealth());
    }
    
    IEnumerator ChangeFillHealth()
    {
        float startTime = Time.time;
        float startFillAmount = healthAmount.fillAmount;
        float duration = 2f;

        print((float)newHealth / charac.GetMaxHP());

        while (Time.time - startTime < duration)
        {
            healthAmount.fillAmount = Mathf.Lerp(startFillAmount, (float)newHealth / charac.GetMaxHP(), ((Time.time - startTime) / duration));
            yield return null;
        }

        currentHealth = newHealth; // Update currentHealth after the coroutine finishes
    }
}
