using System.Collections;
using System.Collections.Generic;

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
}
