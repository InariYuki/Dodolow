using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller
{
    View view;
    Model model;
    public void Initialize(View v){
        view = v;
        model = new Model();
        model.Initialize();
        view.StartGameEvent += StartGameButtonPressed;
        view.ExitGameEvent += ExitButtonPressed;
        view.MainMenuEvent += MainMenuButtonPressed;
        view.RestartEvent += RestartButtonPressed;
        view.SlotClickedEvent += SlotClicked;
        model.WinEvent += EndGame;
        model.SlotClickedEvent += SlotClickedCallBack;
        model.CloseSlotEvent += CloseSlotCallBack;
        model.DisableSlotEvent += DisableSlotCallBack;
    }
    public TimerView GetTimerView(){
        return view.GetTimerView();
    }
    async void StartGameButtonPressed(){
        view.HideMainMenu();
        view.HideMenuButton();
        view.HideRestartButton();
        
        await model.StartGame();
        view.ShowMenuButton();
    }
    void ExitButtonPressed(){
        Application.Quit();
    }
    async void RestartButtonPressed(){
        view.HideMenuButton();
        view.HideRestartButton();
        await model.StartGame();
        view.ShowMenuButton();
    }
    void MainMenuButtonPressed(){
        view.ShowMainMenu();
    }
    void EndGame(){
        view.ShowRestartButton();
    }
    void SlotClicked(int slotIndex){
        model.SlotClicked(slotIndex);
    }
    void SlotClickedCallBack(int slotIndex){
        
    }
    void DisableSlotCallBack(int slotIndex){
        
    }
    void CloseSlotCallBack(int slotIndex){
        
    }
}
