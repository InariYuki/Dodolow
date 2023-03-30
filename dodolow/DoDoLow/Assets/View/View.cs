using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public delegate void EventInt(int i);
public delegate void VoidEvent();
public class View : MonoBehaviour
{
    public event EventInt SlotClickedEvent;
    public event VoidEvent StartGameEvent , ExitGameEvent , RestartEvent , MainMenuEvent;
    [SerializeField] private List<SlotView> slotViews = new List<SlotView>();
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject restartButton , mainMenuButton;
    [SerializeField] private Toggle cheatBox;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    private void Awake(){
        Controller controller = new Controller();
        controller.Initialize(this);
        for(int i = 0; i < slotViews.Count; i++){
            slotViews[i].Initialize(this , i);
        }
    }
    public void SlotClicked(int index){
        SlotClickedEvent(index);
    }
    public void OnStartGameButtonPressed(){
        mainMenu.SetActive(false);
        StartGameEvent();
    }
    public void OnExitButtonPressed(){
        ExitGameEvent();
    }
    public void OnMainMenuButtonPressed(){
        MainMenuEvent();
    }
    public void OnRestartButtonPressed(){
        RestartEvent();
    }
    public bool CheatBoxChecked(){
        return cheatBox.isOn;
    }
    public void ToggleMainMenu(bool state){
        mainMenu.SetActive(state);
    }
    public void ToggleManuMenuButton(bool state){
        mainMenuButton.SetActive(state);
    }
    public void ToggleRestartButton(bool state){
        restartButton.SetActive(state);
    }
    public void SetSlotView(int index , int id , int state , bool cheat){
        Sprite sprite;
        string slotIdText;
        if(state == 0){ // open
            sprite = Assets.Instance.IdCardDict[id].image;
            slotIdText = "";
        }
        else if(state == 1){ // close
            sprite = Assets.Instance.blankSprite;
            if(cheat) slotIdText = id.ToString();
            else slotIdText = "";
        }
        else{ // disable
            sprite = Assets.Instance.disableSprite;
            slotIdText = "";
        }
        slotViews[index].UpdateDisplay(sprite , slotIdText);
    }
    public void SetTimerTime(string time){
        timerDisplay.text = time;
    }
}
