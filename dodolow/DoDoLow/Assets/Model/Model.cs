using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidEventIntInt(int index1 , int index2);
public class Model
{
    public event VoidEvent RandomizeGameCompleteEvent , GameFinishedEvent;
    public event EventInt SlotClickedEvent;
    public event VoidEventIntInt MatchEvent , MisMatchEvent;
    private List<SlotModel> slotModels = new List<SlotModel>();
    private List<int> slotIds = new List<int>();
    private SlotModel currentSlot;
    private bool mouseLock = false;
    private int openedSlotCount = 0;
    public void Initialize(){
        for(int i = 0; i < 40; i++){
            SlotModel s = new SlotModel();
            s.index = i;
            s.slotId = 0;
            slotModels.Add(s);
            if(i % 2 == 0){
                int index = i / 2;
                slotIds.Add(index);
                slotIds.Add(index);
            }
        }
    }
    public void RandomizeGame(){
        openedSlotCount = 0;
        slotIds = Shuffle(slotIds);
        for(int i = 0; i < slotModels.Count; i++){
            slotModels[i].slotId = slotIds[i];
        }
        RandomizeGameCompleteEvent();
    }
    public void SetSlotClickable(int index , bool state){
        slotModels[index].canBeClick = state;
    }
    public void SlotClicked(int index){
        if(!slotModels[index].canBeClick || mouseLock) return;
        slotModels[index].canBeClick = false;
        SlotClickedEvent(index);
        if(currentSlot == null){
            currentSlot = slotModels[index];
        }
        else{
            if(slotModels[index].slotId == currentSlot.slotId){
                MatchEvent(index , currentSlot.index);
                openedSlotCount += 2;
                if(openedSlotCount == slotModels.Count){
                    GameFinishedEvent();
                }
            }
            else{
                MisMatchEvent(index , currentSlot.index);
            }
            currentSlot = null;
        }
    }
    public int GetSlotId(int index){
        return slotModels[index].slotId;
    }
    public int GetSlotSize(){
        return slotModels.Count;
    }
    public void SetMouseLock(bool state){
        mouseLock = state;
    }
    private List<T> Shuffle<T>(List<T> list){
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0 , n+1);
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }
        return list;
    }
}
class SlotModel{
    public int index;
    public int slotId;
    public bool canBeClick = false;
}
