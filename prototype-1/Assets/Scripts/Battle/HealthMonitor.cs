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
        currentHealth = charac.HP; // Set initial health
        newHealth = charac.HP; // Initialize newHealth

        // Update UI elements
        UpdateHealthFill();
    }

    // Update is called once per frame
    void Update()
    {
        float newSize = Mathf.Lerp(scaleMin, scaleMax, Mathf.InverseLerp(0, 1, Mathf.Pow(Mathf.Sin(Time.time), 63) * 8 * Mathf.Sin(Time.time + 1.5f)));
        transform.localScale = new Vector3(newSize, newSize, 1);
    }
    
    private void UpdateHealthFill()
    {
        // Assuming max health is 100 (adjust as needed)
        healthAmount.fillAmount = (float)currentHealth / 20f;
    }

    public void ChangeHealth()
    {
        newHealth = charac.Hp.value;
        print("nh:" + newHealth);/* Set initial health */;
        //UpdateHealthFill();
        // Trigger the coroutine when health changes
        StartCoroutine(ChangeFillHealth());

        // Update the UI or perform other actions as needed
        //UpdateHealthFill();
    }
    
    IEnumerator ChangeFillHealth()
    {
        float startTime = Time.time;
        float startFillAmount = healthAmount.fillAmount;
        float duration = 2f;

        while (Time.time - startTime < duration)
        {
            healthAmount.fillAmount = Mathf.Lerp(startFillAmount, (float)newHealth / charac.HP, ((Time.time - startTime) / duration));
            yield return null;
        }

        currentHealth = newHealth; // Update currentHealth after the coroutine finishes
    }
}
