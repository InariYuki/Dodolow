using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TimerController
{
    TimerView view;
    TimerModel model;
    Controller controller;
    public void Initialize(Controller c){
        controller = c;
        view = controller.GetTimerView();
        view.Initialize();
        model = new TimerModel();
    }
    public async void StartTimer(){
        model.SetTimerStart(true);
        while(model.GetTimerStart()){
            view.SetTimerText(model.GetTime().ToString());
            model.SetTime(model.GetTime() + 1);
            await Task.Delay(1000);
        }
    }
    public void StopTimer(){
        model.SetTimerStart(false);
    }
    public void ResetTimer(){
        model.SetTime(0);
        view.SetTimerText(model.GetTime().ToString());
    }
}
