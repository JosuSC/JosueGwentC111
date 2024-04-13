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
    SpriteRenderer Megacarta;
    GameObject Gigant;

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


    private void OnMouseEnter()
    {
        Gigant.transform.localScale = new Vector3(5, 5, 5); 
        Megacarta.sprite = GetComponent<SpriteRenderer>().sprite;  
    }

    private void OnMouseExit()
    {
        Gigant.transform.localScale = Vector3.zero; 
    }

     void Start()
    {
        Gigant = GameObject.Find("Mega Carta");
        Megacarta = Gigant.GetComponent<SpriteRenderer>();
        Gigant.transform.localScale = Vector3.zero;
    }
}
