using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotView : MonoBehaviour
{
    Image image;
    TextMeshProUGUI text;
    SlotController controller;
    public void Initialize(SlotController c){
        image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(OnSlotClicked);
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.raycastTarget = false;
        text.color = Color.black;
        controller = c;
    }
    public void UpdateDisplay(Sprite sprite , string slotId){
        image.sprite = sprite;
        text.text = slotId;
    }
    void OnSlotClicked(){
        controller.SlotClicked();
    }
}
