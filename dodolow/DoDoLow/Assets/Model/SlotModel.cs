using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotModel
{
    int slotId;
    int slotIndex;
    Sprite slotSprite;
    bool canBeClicked = false;
    public void Initialize(int index){
        slotIndex = index;
    }
    public void SetSlotId(int id){
        slotId = id;
    }
    public void SetSlotSprite(Sprite sprite){
        slotSprite = sprite;
    }
    public int GetSlotId(){
        return slotId;
    }
    public bool CanBeClicked(){
        return canBeClicked;
    }
    public void SetCanBeClicked(bool stat){
        canBeClicked = stat;
    }
    public int GetSlotIndex(){
        return slotIndex;
    }
}
