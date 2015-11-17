using UnityEngine;
using System.Collections;

/*
Made by: Michael Bossink
Used for: Unity Countdown timers
Version: 1.3
Date: 17-11-2015
Last Change: Typo. And <para></para>
*/

public static class CountTimerScript {

    private static float _oldTime = 0;
    private static float _endTime = 0;
    /// <summary>
    /// Add the seconds to the current time. (Time when the project started in seconds)
    /// </summary>
    public static float AddSeconds(float pSeconds)
    {
        return Time.time + pSeconds;
    }
    /// <summary>
    /// Add the minutes to the current time. (Time when the project started in seconds)
    /// </summary>
    public static float AddMinutes(float pMinutes)
    {
        return Time.time + (pMinutes * 60);
    }
    /// <summary>
    /// Add the minutes and seconds to the current time. (Time when the project started in seconds)
    /// </summary>
    public static float AddMinutesAndSeconds(float pMinutes = 0, float pSeconds = 0)
    {
        return Time.time + (pMinutes * 60) + pSeconds;
    }
    /// <summary>
    /// <para>An easy to use timer. Work in progress</para>
    /// <para>pStartTime = Time.time/<para>
    /// <para>pEndTime is default Fill in 0.</para>
    /// <para>pMinutes is AddMinutes.</para>
    /// <para>pSeconds is AddSeconds./<para>
    /// </summary>
    public static bool IsTimerDown(float pStartTime = 0,float pEndTime = 0, float pMinutes = 0, float pSeconds = 0)
    {
        if (pEndTime == 0)
        {
            _endTime = pStartTime + (pMinutes * 60) + pSeconds;
        }
        if (_endTime < pStartTime)
        {
            return true;
        }
        else if(_endTime > pStartTime)
        {
            return false;
        }

        IsTimerDown(pStartTime, _endTime);
        return false;
    }

}
