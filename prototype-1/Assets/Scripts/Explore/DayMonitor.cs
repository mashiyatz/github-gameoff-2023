using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayMonitor : MonoBehaviour
{
    public enum TIME { DAY, NOON, DUSK, NIGHT }
    public static TIME currentTime;

    Dictionary<TIME, string> timeToAkState = new();

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
        StartCoroutine(PlayMusic());
    }

    public IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(15);
        AkSoundEngine.PostEvent("Play_Music", gameObject);
    }
}
