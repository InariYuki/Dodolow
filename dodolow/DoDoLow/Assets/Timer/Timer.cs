using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Timer
{
    public event EventInt TickEvent;
    private int time = 0 , secondToMilisecond = 1000;
    private bool stopped = true;
    public async void StopForSeconds(int second , VoidEvent PostStopBehavior){
        await Task.Delay(second * secondToMilisecond);
        PostStopBehavior();
    }
    public async void StartTimer(){
        stopped = false;
        while(!stopped){
            TickEvent(time);
            await Task.Delay(1 * secondToMilisecond);
            time++;
        }
    }
    public void StopTimer(){
        stopped = true;
    }
    public void ResetTimer(){
        time = 0;
        TickEvent(time);
    }
}
