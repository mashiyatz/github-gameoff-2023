using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayMonitor : MonoBehaviour
{
    public enum TIME { DAY, NOON, DUSK, NIGHT }
    public static TIME currentTime;

    void Start()
    {
        currentTime = TIME.DAY;    
    }

    public static void ShiftTime()
    {
        currentTime += 1;
    }
}
