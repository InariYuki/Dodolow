using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotModel
{
    int slotId;
    Sprite slotSprite;
    public void SetSlotId(int id){
        slotId = id;
    }
    public void SetSlotSprite(Sprite sprite){
        slotSprite = sprite;
    }
    public int GetSlotId(){
        return slotId;
    }
}
