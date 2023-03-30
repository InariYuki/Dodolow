using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public delegate void WinEvent();
public delegate void SlotClickedEvent(int index);
public delegate void DisableSlotEvent(int index);
public delegate void CloseSlotEvent(int index);
public class Model
{
    public event WinEvent WinEvent;
    public event SlotClickedEvent SlotClickedEvent;
    public event DisableSlotEvent DisableSlotEvent;
    public event CloseSlotEvent CloseSlotEvent;
    private List<SlotModel> slotModels = new List<SlotModel>();
    private SlotModel currentSlotModel = null;
    private bool canClick = true;
    private int slotOpened = 0;
    public void Initialize(){
        for(int i = 0; i < 40 ; i++){
            SlotModel m = new SlotModel();
            m.Initialize(i);
            slotModels.Add(m);
        }
    }
    public async void SlotClicked(int slotIndex){
        if(!canClick) return;
        if(currentSlotModel == null){
            if(slotModels[slotIndex].CanBeClicked()){
                currentSlotModel = slotModels[slotIndex];
                SlotClickedEvent(slotIndex);
            }
        }
        else{
            if(slotModels[slotIndex].CanBeClicked()){
                canClick = false;
                SlotClickedEvent(slotIndex);
                await Task.Delay(1000);
                if(currentSlotModel.GetSlotId() == slotModels[slotIndex].GetSlotId()){
                    currentSlotModel.SetCanBeClicked(false);
                    slotModels[slotIndex].SetCanBeClicked(false);
                    DisableSlotEvent(currentSlotModel.GetSlotIndex());
                    DisableSlotEvent(slotIndex);
                    slotOpened += 2;
                    if(slotOpened == slotModels.Count){
                        canClick = true;
                        currentSlotModel = null;
                        WinEvent();
                        return;
                    }
                }
                else{
                    currentSlotModel.SetCanBeClicked(false);
                    slotModels[slotIndex].SetCanBeClicked(false);
                    CloseSlotEvent(currentSlotModel.GetSlotIndex());
                    CloseSlotEvent(slotIndex);
                }
                canClick = true;
                currentSlotModel = null;
            }
        }
    }
    public async Task StartGame(){
        canClick = false;
        slotOpened = 0;
        slotModels = Shuffle(slotModels);
        for(int i = 0; i < 20; i++){
            slotModels[2 * i].SetSlotId(i);
            slotModels[2 * i + 1].SetSlotId(i);
        }
        for(int i = 0; i < slotModels.Count; i++){
            slotModels[i].SetCanBeClicked(false);
        }
        await Task.Delay(5000);
        canClick = true;
        for(int i = 0; i < slotModels.Count; i++){
            slotModels[i].SetCanBeClicked(true);
        }
    }
    public bool MouseLocked(){
        return !canClick;
    }
    public int GetSlotModelByIndex(int index){
        return slotModels[index].GetSlotId();
    }
    List<T> Shuffle<T>(List<T> list){
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
