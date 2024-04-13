using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int rondasplayer1 = 0;
    int rondasplayer2 = 0;
    //int filag1 =0;
    //int filag2 =0;
    //int filad1 = 0;
    //int filad2 = 0;
    //int filaa1 = 0;
    //int filaa2 = 0;
    //int allcount1 = 0;
    //int allcount2 = 0;
    //int cementerio1  = 0;
    //int cementerio2 = 0;
    Player player1;
    Player player2;
    string acttivo = "";
    GameBoard board;
    GameObject game;
    public CardScript h;

    //List<GameObject> cementery1;
    //List<GameObject> cementery2;
    //GameObject lastcardtocementery;
    GameObject cartajugada = new GameObject();
    CardScript card = new CardScript();
 
    Deck deck;

    public GameManager()
    {
        this.rondasplayer1 = 0;
        this.rondasplayer2 = 0;
        //this.filag1 = 0;
        //this.filag2 = 0;
        //this.filad1 = 0;
        //this.filad2 = 0;
        //this.filaa1 = 0;
        //this.filaa2 = 0;
        //this.allcount1 = 0;
        //this.allcount2 = 0;
        //this.cementerio1 = 0;
        //this.cementerio2 = 0;
        this.player1= new Player();
        this.player2= new Player();
        this.deck = new Deck();

        this.acttivo = "";
        //this.cementery1 = new List<GameObject>();
        //this.cementery2= new List<GameObject>();
        //this.lastcardtocementery = lastcardtocementery;
        //this.cartajugada = cartajugada;
        //this.card = card;
        this.board = new GameBoard();
        //this.game = game;
        //this.game = game;
            this.game = null;
     
    }


    // Start is called before the first frame update
    void Start()
    {

       // CardScript.Aumento(board.filag1,);
        //Hand1 = new List<GameObject>();
        //Hand2 = new List<GameObject>();
        //PlayerOne = new List<GameObject>();
        //PlayerTwo = new List<GameObject>();
        //asignar cartas a jugadores
        Asignar(player1.deck);
        Asignar(player2.deck);

        //sacar 10 cartas ramdon del deck para player1
        for (int i = 0; i < 10; i++)
        {
            GameObject t = player1.deck[Random.Range(0, player1.deck.Count)];
            player1.hand.Add(t);
            player1.deck.Remove(t);
        }

        //sacar 10 cartas ramdon del deck para player2
        for (int i = 0; i < 10; i++)
        {
            GameObject t = player2.deck[Random.Range(0, player2.deck.Count)];
            player2.hand.Add(t);
            player2.deck.Remove(t);
        }

    }
    public void Asignar(List<GameObject> player)
    {
        AsignarCartas(player, deck.heroes, 1);
        AsignarCartas(player, deck.silver, 3);
        AsignarCartas(player, deck.asedio, 4);
        AsignarCartas(player, deck.trampa, 2);
        AsignarCartas(player, deck.despeje, 2);
        AsignarCartas(player, deck.aumento, 3);
        AsignarCartas(player, deck.distancia, 3);
        AsignarCartas(player, deck.clima, 2);
        AsignarCartas(player, deck.guerreros, 5);


    }
    void AsignarCartas(List<GameObject> player, List<GameObject> lista, int cantidad)
    {
        int n = 0;
        int aleatorio = 0;
        while (n < cantidad)
        {
            aleatorio = Random.Range(0, lista.Count);
            player.Add(lista[aleatorio]);
            n++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GanarJuego();


    }

    public void GanarRonda() 
    {
        if (board.allcount1 == board.allcount2)
        {
            //ya veremos
            rondasplayer1++;
            rondasplayer2++;
        }


        if (board.allcount1 > board.allcount2)
        {
            //gana ronda el uno,avisar
            rondasplayer1++;
        }
        else 
        {
            //gana ronda el dos,avisar
            rondasplayer2++;
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


