using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotView : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI text;
    private View view;
    private int index;
    public void Initialize(View v , int i){
        view = v;
        index = i;
        image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(OnSlotClicked);
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.raycastTarget = false;
        text.color = Color.black;
    }
    public void UpdateDisplay(Sprite sprite , string slotId){
        image.sprite = sprite;
        text.text = slotId;
    }
    private void OnSlotClicked(){
        view.SlotClicked(index);
    }
}
