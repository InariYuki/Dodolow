using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerModel
{
    bool timerstart = false;
    int time = 0;
    public bool GetTimerStart(){
        return timerstart;
    }
    public int GetTime(){
        return time;
    }
    public void SetTimerStart(bool stat){
        timerstart = stat;
    }
    public void SetTime(int t){
        time = t;
    }
}
