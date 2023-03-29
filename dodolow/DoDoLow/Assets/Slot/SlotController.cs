using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController
{
    SlotModel model;
    SlotView view;
    Controller controller;
    bool canBeClicked = false;
    public void Initialize(Controller c , int index){
        controller = c;
        model = new SlotModel();
        view = controller.GetSlotViewByIndex(index);
        view.Initialize(this);
    }
    public void SetSlotId(int id){
        model.SetSlotId(id);
    }
    public int GetSlotId(){
        return model.GetSlotId();
    }
    public void SlotClicked(){
        if(!canBeClicked || controller.MouseLocked()) return;
        Open();
        controller.SlotClicked(this);
    }
    public void Open(){
        canBeClicked = false;
        view.UpdateDisplay(Assets.Instance.IdCardDict[model.GetSlotId()].image , "");
    }
    public void Close(){
        canBeClicked = true;
        view.UpdateDisplay(Assets.Instance.blankSprite , CheckCheat());
    }
    public void Disable(){
        canBeClicked = false;
        view.UpdateDisplay(Assets.Instance.disableSprite , "");
    }
    string CheckCheat(){
        string s;
        if(controller.IsCheating()) s = model.GetSlotId().ToString();
        else s = "";
        return s;
    }
}
