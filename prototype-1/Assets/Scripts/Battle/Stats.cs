using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;

public class Stat
{
    public int value;
    
    public Stat(int statValue)
    {
        value = statValue;
    }

    public void ChangeStat(int change)
    {
        value += change;
    }

    public int getValue()
    {
        return value;
    }
}
