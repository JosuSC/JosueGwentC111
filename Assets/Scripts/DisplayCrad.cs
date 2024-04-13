using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayCrad : MonoBehaviour
{

    public List<Card> displaycard =new List<Card>();
    public int displaynumber;

    public int number;
    public string name;
    public string description;
    public Sprite imagen;
    public int power;
    public int life;
    public string type;
    public string gerarqui;


    public Text nameText;
    public Text descriptionText;
    public Text powerText;
    public Text lifeText;
    public Text typeText;
    public Text gerarquiText;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        number = displaycard[0].number; 
        name = displaycard[0].name;
        description = displaycard[0].description;
        power = displaycard[0].power;   
        life = displaycard[0].life; 
        type= displaycard[0].type;  
        gerarqui= displaycard[0].gerarqui;

        nameText.text = " " + name;
        descriptionText.text = " " + description;
        powerText.text = " " + power;
        lifeText.text = " " + life; 
        typeText.text = " " + type; 
        gerarquiText.text = " " + gerarqui;

    }
}
