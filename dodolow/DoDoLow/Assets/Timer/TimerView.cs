using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerView : MonoBehaviour
{
    TextMeshProUGUI timerText;
    public void Initialize(){
        timerText = GetComponent<TextMeshProUGUI>();
    }
    public void SetTimerText(string t){
        timerText.text = t;
    }
}
