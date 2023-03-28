using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotBehavior : MonoBehaviour
{
    bool canBeClicked = true;
    [HideInInspector] public bool opened = false;
    [HideInInspector] public byte slotId;
    Image image;
    TextMeshProUGUI cheat;
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(OnSlotClicked);
        image = GetComponent<Image>();
        cheat = GetComponentInChildren<TextMeshProUGUI>();
        cheat.color = Color.black;
        cheat.raycastTarget = false;
    }
    public void OnSlotClicked(){
        if(!canBeClicked || !GameController.instance.canClick) return;
        Open();
        GameController.instance.SlotClicked(this);
    }
    public void Open(){
        cheat.text = "";
        opened = true;
        canBeClicked = false;
        image.sprite = Assets.Instance.IdCardDict[slotId].image;
    }
    public void Close(){
        opened = false;
        canBeClicked = true;
        image.sprite = Assets.Instance.blankSprite;
        if(MainMenu.instance.cheatMode.isOn) cheat.text = slotId.ToString();
        else cheat.text = "";
    }
    public void DisableSlot(){
        canBeClicked = false;
        image.sprite = Assets.Instance.disableSprite;
    }
}
