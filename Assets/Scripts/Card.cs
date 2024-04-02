using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card : MonoBehaviour
{
    //Campos
    public int number;
    public string name;
    public string description;
    public Sprite imagen;
    public int power;
    public int life;
    public string type;
    public string gerarqui;

    //costructor
    public Card(int number,string name,string description,int power,int life,string type,string gerarqui) 
    {
        this.number = number;   
        this.name = name;   
        this.description = description;        
        this.power = power;
        this.life = life;
        this.type = type;
        this.gerarqui = gerarqui;
    }

    //propiedades
    public int Number{ get { return number; } set { number = value; } }
    public string Name { get { return name; } set { name = value; } }   
    public string Description { get { return description; } set { description = value; } }   
    public int Power { get { return power; }set { power = value; }}
    public int Life { get { return life; } set { life = value; } }
    public string Type { get { return type; } set{ type = value;} }
    public string Gerarqui { get { return gerarqui; } set { gerarqui = value; } }



   
}
