using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public delegate void EventInt(int i);
public delegate void VoidEvent();
public delegate void EventBool(bool b);
public class View : MonoBehaviour
{
    public event EventInt SlotClickedEvent;
    public event VoidEvent ExitGameEvent , MainMenuEvent;
    public event EventBool StartGameEvent;
    [SerializeField] private List<SlotView> slotViews = new List<SlotView>();
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject restartButton , mainMenuButton;
    [SerializeField] private Toggle cheatBox;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    private void Awake(){
        Controller controller = new Controller();
        controller.Initialize(this);
        for(int i = 0; i < slotViews.Count; i++){
            slotViews[i].Initialize(i);
            slotViews[i].SlotClickedEvent += SlotClicked;
        }
    }
    public void SlotClicked(int index){
        SlotClickedEvent(index);
    }
    public void OnStartGameButtonPressed(){
        mainMenu.SetActive(false);
        StartGameEvent(cheatBox.isOn);
    }
    public void OnExitButtonPressed(){
        ExitGameEvent();
    }
    public void OnMainMenuButtonPressed(){
        MainMenuEvent();
    }
    public void OnRestartButtonPressed(){
        StartGameEvent(cheatBox.isOn);
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
    public void SetSlotViewOpen(int index , int imageId){
        slotViews[index].UpdateDisplay(Assets.Instance.IdCardDict[imageId].image , "");
    }
    public void SetSlotViewClose(int index , string slotId){
        slotViews[index].UpdateDisplay(Assets.Instance.blankSprite , slotId);
    }
    public void SetSlotViewDisabled(int index){
        slotViews[index].UpdateDisplay(Assets.Instance.disableSprite , "");
    }
    public void SetTimerTime(string time){
        timerDisplay.text = time;
    }
}
