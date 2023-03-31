using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    private List<SlotModel> slotModels = new List<SlotModel>();
    private List<int> slotIds = new List<int>();
    private SlotModel currentSlot;
    private bool mouseLock = false;
    private int openedSlotCount = 0 , SlotHalfCount = 20;
    private bool cheat = false;
    private int resetSlotTime = 1 , allOpenSlotTime = 5;
    public void Initialize(){
        SlotModel s;
        int SlotCount = SlotHalfCount + SlotHalfCount;
        for(int i = 0; i < SlotCount; i++){
            s = new SlotModel();
            s.index = i;
            s.slotId = 0;
            slotModels.Add(s);
        }
        for(int i = 0; i < SlotHalfCount; i++){
            slotIds.Add(i);
            slotIds.Add(i);
        }
    }
    public void SetCheat(bool state){
        cheat = state;
    }
    public string GetSlotIdString(int index){
        if(cheat){
            return slotModels[index].slotId.ToString();
        }
        else{
            return "";
        }
    }
    public void RandomizeGame(){
        openedSlotCount = 0;
        slotIds = Shuffle(slotIds);
        for(int i = 0; i < slotModels.Count; i++){
            slotModels[i].slotId = slotIds[i];
        }
    }
    public void SetSlotClickable(int index){
        slotModels[index].canBeClick = true;
    }
    public GameStat SlotClicked(int index){
        if(!slotModels[index].canBeClick || mouseLock) return null;
        slotModels[index].canBeClick = false;
        GameStat stat = new GameStat();
        if(currentSlot == null){
            currentSlot = slotModels[index];
            stat.state = GameState.OneClicked;
            return stat;
        }
        else{
            mouseLock = true;
            stat.clickedIndex1 = index;
            stat.clickedIndex2 = currentSlot.index;
            if(slotModels[index].slotId == currentSlot.slotId){
                openedSlotCount += 2;
                if(openedSlotCount == slotModels.Count){
                    stat.state = GameState.End;
                }
                else{
                    stat.state = GameState.Match;
                }
            }
            else{
                stat.state = GameState.MisMatch;
            }
            currentSlot = null;
            return stat;
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
    public int GetAllOpenTime(){
        return allOpenSlotTime;
    }
    public int GetResetSlotTime(){
        return resetSlotTime;
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
public class GameStat{
    public GameState state;
    public int clickedIndex1 , clickedIndex2;
}
public enum GameState{
    OneClicked,
    Match,
    MisMatch,
    End
}
