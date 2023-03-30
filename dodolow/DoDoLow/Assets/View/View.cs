using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnStartGameButtonPressed();
public delegate void OnExitButtonPressed();
public delegate void OnRestartButtonPressed();
public delegate void OnMainMenuButtonPressed();
public delegate void SlotClicked(int slotNum);

public class View : MonoBehaviour
{
    [SerializeField] private List<SlotView> slotViews = new List<SlotView>();
    [SerializeField] private TimerView timerView;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Toggle cheatToggle;
    [SerializeField] private GameObject menuButton , restartButton;
    public event OnStartGameButtonPressed StartGameEvent;
    public event OnExitButtonPressed ExitGameEvent;
    public event OnRestartButtonPressed RestartEvent;
    public event OnMainMenuButtonPressed MainMenuEvent;
    public event SlotClicked SlotClickedEvent;
    private void Awake() {
        Controller controller = new Controller();
        controller.Initialize(this);
        for(int i = 0; i < slotViews.Count; i++){
            slotViews[i].Initialize(this , i);
        }
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
        StartGameEvent();
    }
    public void OnExitButtonPressed(){
        ExitGameEvent();
    }
    public void OnRestartButtonPressed(){
        RestartEvent();
    }
    public void OnMainMenuButtonPressed(){
        MainMenuEvent();
    }
    public bool IsCheating(){
        return cheatToggle.isOn;
    }
    public TimerView GetTimerView(){
        return timerView;
    }
    public void SlotClicked(int index){
        SlotClickedEvent(index);
    }
    public void SetSlotView(SlotViewParameters parameter){
        Sprite sprite;
        string slotId; 
        switch(parameter.state){
            case 0:
                sprite = Assets.Instance.IdCardDict[parameter.slotId].image;
                slotId = "";
                break;
            case 1:
                sprite = Assets.Instance.blankSprite;
                slotId = cheatToggle.isOn ? parameter.slotId.ToString() : "";
                break;
            default:
                sprite = Assets.Instance.disableSprite;
                slotId = "";
                break;
        }
        slotViews[parameter.slotIndex].UpdateDisplay(sprite , slotId);
    }
}
public class SlotViewParameters{
    public int slotIndex;
    public int slotId;
    public int state; //0 = open , 1 = close , 2 = disabled
}
