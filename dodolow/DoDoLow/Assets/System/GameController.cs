using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] GameObject restartButton , mainMenuButton;
    [SerializeField] List<SlotBehavior> slots = new List<SlotBehavior>();
    [HideInInspector] public SlotBehavior currentSlot;
    [HideInInspector] public bool canClick = false;
    int openedSlotCount = 0;
    private void Awake() {
        instance = this;
    }
    public async void InitializeSlots(){
        Timer.instance.ResetTimer();
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        openedSlotCount = 0;
        canClick = false;
        slots = Shuffle(slots);
        for(byte i = 0; i < 20 ; i++){
            slots[2 * i].slotId = i;
            slots[2 * i + 1].slotId = i;
        }
        for(int i = 0; i < slots.Count; i++){
            slots[i].Open();
        }
        await Task.Delay(5000);
        mainMenuButton.SetActive(true);
        Timer.instance.StartTimer();
        for(int i = 0; i < slots.Count; i++){
            slots[i].Close();
        }
        canClick = true;
    }
    public async void SlotClicked(SlotBehavior slot){
        if(currentSlot == null){
            currentSlot = slot;
        }
        else{
            canClick = false;
            await Task.Delay(1000);
            if(currentSlot.slotId == slot.slotId){
                currentSlot.DisableSlot();
                slot.DisableSlot();
                openedSlotCount += 2;
                if(openedSlotCount == slots.Count){
                    EndGame();
                }
            }
            else{
                currentSlot.Close();
                slot.Close();
            }
            canClick = true;
            currentSlot = null;
        }
    }
    void EndGame(){
        restartButton.SetActive(true);
        Timer.instance.StopTimer();
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
