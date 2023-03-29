using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Model
{
    List<SlotController> slotControllers = new List<SlotController>();
    SlotController currentSlotController;
    bool canClick = true;
    int slotOpened = 0;
    public async Task<int> SlotClicked(SlotController slot){
        if(currentSlotController == null){
            currentSlotController = slot;
        }
        else{
            canClick = false;
            await Task.Delay(1000);
            if(currentSlotController.GetSlotId() == slot.GetSlotId()){
                currentSlotController.Disable();
                slot.Disable();
                slotOpened += 2;
                if(slotOpened == slotControllers.Count){
                    canClick = true;
                    currentSlotController = null;
                    return 1;
                }
            }
            else{
                currentSlotController.Close();
                slot.Close();
            }
            canClick = true;
            currentSlotController = null;
        }
        return 0;
    }
    public async Task StartGame(){
        slotControllers = Shuffle(slotControllers);
        slotOpened = 0;
        for(int i = 0; i < 20; i++){
            slotControllers[2 * i].SetSlotId(i);
            slotControllers[2 * i + 1].SetSlotId(i);
        }
        for(int i = 0; i < slotControllers.Count; i++){
            slotControllers[i].Open();
        }
        await Task.Delay(5000);
        for(int i = 0; i < slotControllers.Count; i++){
            slotControllers[i].Close();
        }
    }
    public bool MouseLocked(){
        return !canClick;
    }
    public List<SlotController> GetSlotControllers(){
        return slotControllers;
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
