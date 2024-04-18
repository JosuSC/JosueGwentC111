using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Card",menuName = "Card")]
public class CardScript : ScriptableObject
{
    //Campos
    public int number;
    public new string name;
    public string description;
    public Sprite imagen;
    public int power;
    public int life;
    public string type;
    public  string level;

    public void Print() 
    {
        Debug.Log(name +": "+ description + "Esta carta es del tipo :"+ type + " y tiene nivel: "+level);
    }


    
  

}
