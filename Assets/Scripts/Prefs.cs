using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs 
{
    public static int bestScore
    {
        set{
            if(PlayerPrefs.GetInt(Preconst.BEST_SCORE, 0) < value)
            {
                PlayerPrefs.SetInt(Preconst.BEST_SCORE, value);
            }    
        }
        get => PlayerPrefs.GetInt(Preconst.BEST_SCORE, 0);
    }
}
