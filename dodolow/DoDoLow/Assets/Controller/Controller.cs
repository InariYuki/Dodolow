using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller
{
    private View view;
    private Model model;
    private Timer timer;
    public void Initialize(View v){
        view = v;
        timer = new Timer();
        model = new Model();
        model.Initialize();
        view.StartGameEvent += StartGameButtonPressed;
        view.SlotClickedEvent += SlotClicked;
        view.MainMenuEvent += MainMenuButtonPressed;
        view.RestartEvent += StartGameButtonPressed;
        model.RandomizeGameCompleteEvent += ShowAllCardsToPlayer;
        model.SlotClickedEvent += SlotClickedCallBack;
        model.MatchEvent += SlotMatch;
        model.MisMatchEvent += SlotMismatch;
        model.GameFinishedEvent += GameOver;
        timer.TickEvent += UpdateTime;
    }
    private void StartGameButtonPressed(){
        view.ToggleMainMenu(false);
        view.ToggleManuMenuButton(false);
        view.ToggleRestartButton(false);
        view.SetTimerTime(0.ToString());
        model.RandomizeGame();
        timer.ResetTimer();
    }
    private void ShowAllCardsToPlayer(){
        for(int i = 0; i < model.GetSlotSize(); i++){
            view.SetSlotView(i , model.GetSlotId(i) , 0 , view.CheatBoxChecked());
        }
        timer.StopForSeconds(5 , CloseAllCards);
    }
    private void CloseAllCards(){
        view.ToggleManuMenuButton(true);
        for(int i = 0; i < model.GetSlotSize(); i++){
            view.SetSlotView(i , model.GetSlotId(i) , 1 , view.CheatBoxChecked());
            model.SetSlotClickable(i , true);
        }
        timer.StartTimer();
    }
    private void SlotClicked(int index){
        model.SlotClicked(index);
    }
    private void SlotClickedCallBack(int index){
        view.SetSlotView(index , model.GetSlotId(index) , 0 , view.CheatBoxChecked());
    }
    private void SlotMatch(int index1 , int index2){
        model.SetMouseLock(true);
        timer.StopForSeconds(1 , () => {DisableSlots(index1 , index2);});
    }
    private void SlotMismatch(int index1 , int index2){
        model.SetMouseLock(true);
        timer.StopForSeconds(1 , () => {CloseSlots(index1 , index2);});
    }
    private void DisableSlots(int index1 , int index2){
        view.SetSlotView(index1 , model.GetSlotId(index1) , 2 , view.CheatBoxChecked());
        view.SetSlotView(index2 , model.GetSlotId(index2) , 2 , view.CheatBoxChecked());
        model.SetMouseLock(false);
    }
    private void CloseSlots(int index1 , int index2){
        view.SetSlotView(index1 , model.GetSlotId(index1) , 1 , view.CheatBoxChecked());
        view.SetSlotView(index2 , model.GetSlotId(index2) , 1 , view.CheatBoxChecked());
        model.SetSlotClickable(index1 , true);
        model.SetSlotClickable(index2 , true);
        model.SetMouseLock(false);
    }
    private void GameOver(){
        timer.StopTimer();
        timer.StopForSeconds(1 , () => {view.ToggleRestartButton(true);});
    }
    private void MainMenuButtonPressed(){
        timer.StopTimer();
        view.ToggleMainMenu(true);
    }
    private void UpdateTime(int time){
        view.SetTimerTime(time.ToString());
    }
}
