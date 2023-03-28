using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField] TextMeshProUGUI timer;
    bool timerstart = false;
    int time = 0;
    private void Awake() {
        instance = this;
    }
    public async void StartTimer(){
        timerstart = true;
        while(timerstart){
            timer.text = time.ToString();
            time++;
            await Task.Delay(1000);
        }
    }
    public void StopTimer(){
        timerstart = false;
    }
    public void ResetTimer(){
        time = 0;
        timer.text = time.ToString();
    }
}
