using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayMonitor : MonoBehaviour
{
    public enum TIME { DAY, NOON, DUSK, NIGHT }
    public static TIME currentTime;

    Dictionary<TIME, string> timeToAkState = new();

    [SerializeField]
    private Color dayColor;
    [SerializeField]
    private Color nightColor;

    void Start()
    {
        timeToAkState.Add(TIME.DAY, "DayMix");
        timeToAkState.Add(TIME.DUSK, "DustMix");
        timeToAkState.Add(TIME.NIGHT, "NightMix");
        timeToAkState.Add(TIME.NOON, "NoonMix");
        
        currentTime = TIME.DAY;
        AkSoundEngine.SetState("TimeStateNew", "DayMix");
        StartCoroutine(PlayMusic());
    }

    public void ShiftTime()
    {
        currentTime += 1;
        AkSoundEngine.SetState("TimeStateNew", timeToAkState[currentTime]);
        StartCoroutine(ChangeSkyColor(currentTime));
    }

    public IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(5);
        AkSoundEngine.PostEvent("Play_Music", gameObject);
    }

    public IEnumerator ChangeSkyColor(TIME timeOfDay)
    {
        float startTime = Time.time;
        float ratio = ((int)timeOfDay + 1) / 4;

        Color newSkyColor = Color.Lerp(dayColor, nightColor, ratio);
        Color currentColor = Camera.main.backgroundColor;

        while (Time.time - startTime < 5) {
            Color updatedColor = Color.Lerp(currentColor, newSkyColor, (Time.time - startTime / 5));
            Camera.main.backgroundColor = updatedColor;
            yield return null;
        } 
    }
         
}
