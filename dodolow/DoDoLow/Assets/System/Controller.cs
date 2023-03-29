using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Controller
{
    List<SlotController> slotControllers = new List<SlotController>();
    TimerController timerController;
    View view;
    SlotController currentSlotController;
    bool canClick = true;
    int slotOpened = 0;
    public void Initialize(View v){
        view = v;
        timerController = new TimerController();
        timerController.Initialize(this);
        for(int i = 0; i < 40; i++){
            SlotController s = new SlotController();
            slotControllers.Add(s);
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
    public void StartGameButtonPressed(){
        view.HideMainMenu();
        StartGame();
    }
    public void ExitButtonPressed(){
        Application.Quit();
    }
    public void RestartButtonPressed(){
        StartGame();
    }
    public void MainMenuButtonPressed(){
        view.ShowMainMenu();
        timerController.StopTimer();
    }
    public async void SlotClicked(SlotController slot){
        if(currentSlotController == null){
            currentSlotController = slot;
        }
        else{
            canClick = false;
            await Task.Delay(1000);
            if(currentSlotController.GetSlotId() == slot.GetSlotId()){
                currentSlotController.Disable();
                slot.Disable();
                slotOpened += 2;
                if(slotOpened == slotControllers.Count){
                    EndGame();
                }
            }
            else{
                currentSlotController.Close();
                slot.Close();
            }
            canClick = true;
            currentSlotController = null;
        }
    }
    async void StartGame(){
        timerController.ResetTimer();
        view.HideMenuButton();
        view.HideRestartButton();
        slotControllers = Shuffle(slotControllers);
        for(int i = 0; i < 20; i++){
            slotControllers[2 * i].SetSlotId(i);
            slotControllers[2 * i + 1].SetSlotId(i);
        }
        for(int i = 0; i < slotControllers.Count; i++){
            slotControllers[i].Open();
        }
        await Task.Delay(5000);
        timerController.StartTimer();
        view.ShowMenuButton();
        for(int i = 0; i < slotControllers.Count; i++){
            slotControllers[i].Close();
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
        return !canClick;
    }
    List<T> Shuffle<T>(List<T> list){
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0 , n+1);
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }
        return list;
    }
}
