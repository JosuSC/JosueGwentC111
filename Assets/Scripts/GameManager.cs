using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int rondasplayer1 = 0;
    int rondasplayer2 = 0;
   
    Player player1;
    Player player2;
    string acttivo = "";
    GameBoard board;
    GameObject game;
    public CardScript h;

    GameObject cartajugada = new GameObject();
    CardScript card = new CardScript();
 
    Deck deck;

    public GameManager()
    {
        this.rondasplayer1 = 0;
        this.rondasplayer2 = 0;
      
        this.player1= new Player();
        this.player2= new Player();
        this.deck = new Deck();

     
      
        this.board = new GameBoard();
        //this.game = game;
        //this.game = game;
            this.game = null;
     
    }


    // Start is called before the first frame update
    void Start()
    {


    }
  
    

    // Update is called once per frame
    void Update()
    {
        //ver la condiciones A VER QUIEN GANA

        GanarJuego();


    }

    public void GanarRonda() 
    {
        if (PowerPoints.allCount1 > PowerPoints.allCount2)
        {
            rondasplayer1 += 1;
        }
        else if (PowerPoints.allCount1 < PowerPoints.allCount2)
        {
            rondasplayer2 += 1;
        }
        else 
        {
            rondasplayer1 += 1;
            rondasplayer2 += 1;
        }
    }

    public void CambiarTurno() 
    {

    }

    public void GanarJuego() 
    {
        if (rondasplayer1 == 2)
        {
            //gana el primer jugador
        }

        if (rondasplayer2 == 2)
        {
            //gana el segundo jugador 
        }
    }






    // List<Card> temp = new();
    // string nombre = "Guh";
    // var card = temp.First(c => c.Type == nombre);
    // 


   //  h = game.GetComponent<CardScript>();
  


}


