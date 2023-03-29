using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Controller
{
    TimerController timerController;
    View view;
    Model model;
    public void Initialize(View v){
        view = v;
        model = new Model();
        timerController = new TimerController();
        timerController.Initialize(this);
        for(int i = 0; i < 40; i++){
            SlotController s = new SlotController();
            model.GetSlotControllers().Add(s);
            s.Initialize(this , i);
        }
    }
    public SlotView GetSlotViewByIndex(int index){
        List<SlotView> slotViews = view.GetAllSlotViews();
        return slotViews[index];
    }
    public TimerView GetTimerView(){
        return view.GetTimerView();
    }
    public async void StartGameButtonPressed(){
        view.HideMainMenu();
        timerController.ResetTimer();
        view.HideMenuButton();
        view.HideRestartButton();
        await model.StartGame();
        timerController.StartTimer();
        view.ShowMenuButton();
    }
    public void ExitButtonPressed(){
        Application.Quit();
    }
    public async void RestartButtonPressed(){
        timerController.ResetTimer();
        view.HideMenuButton();
        view.HideRestartButton();
        await model.StartGame();
        timerController.StartTimer();
        view.ShowMenuButton();
    }
    public void MainMenuButtonPressed(){
        view.ShowMainMenu();
        timerController.StopTimer();
    }
    public async void SlotClicked(SlotController slot){
        Task<int> task = model.SlotClicked(slot);
        int stat = await task;
        if(stat == 1){
            EndGame();
        }
    }
    void EndGame(){
        timerController.StopTimer();
        view.ShowRestartButton();
    }
    public bool IsCheating(){
        return view.IsCheating();
    }
    public bool MouseLocked(){
        return model.MouseLocked();
    }
}
