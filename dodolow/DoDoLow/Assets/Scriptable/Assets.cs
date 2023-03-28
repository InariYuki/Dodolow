using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/assets")]
public class Assets : ScriptableObject
{
    static Assets instance;
    public static Assets Instance{
        get{
            if(instance == null){
                instance = Resources.Load<Assets>("Assets");
                instance.GenerateIdCardDict();
            }
            return instance;
        }
    }
    [SerializeField] List<Card> cards = new List<Card>();
    public Dictionary<byte , Card> IdCardDict = new Dictionary<byte, Card>();
    public Sprite blankSprite , disableSprite;
    void GenerateIdCardDict(){
        for(int i = 0; i < cards.Count; i++){
            IdCardDict[cards[i].id] = cards[i];
        }
    }
}
[System.Serializable]
public class Card{
    public byte id;
    public Sprite image;
}
