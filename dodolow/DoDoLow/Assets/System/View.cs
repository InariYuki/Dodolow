using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] List<SlotView> slotViews = new List<SlotView>();
    [SerializeField] TimerView timerView;
    [SerializeField] GameObject mainMenu;
    [SerializeField] Toggle cheatToggle;
    [SerializeField] GameObject menuButton , restartButton;
    Controller controller;
    private void Awake() {
        controller = new Controller();
        controller.Initialize(this);
    }
    public void ShowMainMenu(){
        mainMenu.SetActive(true);
    }
    public void HideMainMenu(){
        mainMenu.SetActive(false);
    }
    public void HideMenuButton(){
        menuButton.SetActive(false);
    }
    public void HideRestartButton(){
        restartButton.SetActive(false);
    }
    public void ShowMenuButton(){
        menuButton.SetActive(true);
    }
    public void ShowRestartButton(){
        restartButton.SetActive(true);
    }
    public List<SlotView> GetAllSlotViews(){
        return slotViews;
    }
    public void OnStartGameButtonPressed(){
        controller.StartGameButtonPressed();
    }
    public void OnExitButtonPressed(){
        controller.ExitButtonPressed();
    }
    public void OnRestartButtonPressed(){
        controller.RestartButtonPressed();
    }
    public void OnMainMenuButtonPressed(){
        controller.MainMenuButtonPressed();
    }
    public bool IsCheating(){
        return cheatToggle.isOn;
    }
    public TimerView GetTimerView(){
        return timerView;
    }
}
