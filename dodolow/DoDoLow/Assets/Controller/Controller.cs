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
        timer.TickEvent += UpdateTime;
    }
    private void StartGameButtonPressed(bool cheat){
        model.SetMouseLock(true);
        model.SetCheat(cheat);
        view.ToggleMainMenu(false);
        view.ToggleManuMenuButton(false);
        view.ToggleRestartButton(false);
        view.SetTimerTime(0.ToString());
        model.RandomizeGame();
        timer.ResetTimer();
        ShowAllCardsToPlayer();
    }
    private void ShowAllCardsToPlayer(){
        for(int i = 0; i < model.GetSlotSize(); i++){
            view.SetSlotViewOpen(i , model.GetSlotId(i));
        }
        timer.StopForSeconds(5 , CloseAllCards);
    }
    private void CloseAllCards(){
        model.SetMouseLock(false);
        view.ToggleManuMenuButton(true);
        for(int i = 0; i < model.GetSlotSize(); i++){
            view.SetSlotViewClose(i , model.GetSlotIdString(i));
            model.SetSlotClickable(i);
        }
        timer.StartTimer();
    }
    private void SlotClicked(int index){
        GameStat gameStat = model.SlotClicked(index);
        if(gameStat == null) return;
        view.SetSlotViewOpen(index , model.GetSlotId(index));
        if(gameStat.state == GameState.Match){
            SlotMatch(gameStat.clickedIndex1 , gameStat.clickedIndex2);
        }
        else if(gameStat.state == GameState.MisMatch){
            SlotMismatch(gameStat.clickedIndex1 , gameStat.clickedIndex2);
        }
        else if(gameStat.state == GameState.End){
            SlotMatch(gameStat.clickedIndex1 , gameStat.clickedIndex2);
            GameOver();
        }
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
        view.SetSlotViewDisabled(index1);
        view.SetSlotViewDisabled(index2);
        model.SetMouseLock(false);
    }
    private void CloseSlots(int index1 , int index2){
        view.SetSlotViewClose(index1 , model.GetSlotIdString(index1));
        view.SetSlotViewClose(index2 , model.GetSlotIdString(index2));
        model.SetSlotClickable(index1);
        model.SetSlotClickable(index2);
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
