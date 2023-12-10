using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayMonitor : MonoBehaviour
{
    public enum TIME { DAY, NOON, DUSK, NIGHT }
    public static TIME currentTime;

    Dictionary<TIME, string> timeToAkState = new();

    [SerializeField]
    private Color[] skyColors;

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

        while (Time.time - startTime < 5) {
            Color newSkyColor = Color.Lerp(skyColors[(int)timeOfDay - 1], skyColors[(int)timeOfDay], Time.time - startTime / 5);
            Camera.main.backgroundColor = newSkyColor;
            yield return null;
        } 
    }
         
}
