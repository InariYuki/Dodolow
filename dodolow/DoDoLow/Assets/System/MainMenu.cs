using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    public void OnStartGameButtonPressed(){
        mainMenu.SetActive(false);
        GameController.instance.InitializeSlots();
    }
    public void OnExitButtonPressed(){
        Application.Quit();
    }
    public void OnRestartButtonPressed(){
        GameController.instance.InitializeSlots();
    }
    public void OnMainMenuButtonPressed(){
        mainMenu.SetActive(true);
    }
}
