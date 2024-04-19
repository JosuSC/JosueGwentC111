using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Deck : MonoBehaviour
{

    //Listas con todas mis cartas
    public List<GameObject> guerreros;
    public List<GameObject> heroes;
    public List<GameObject> silver;
    public List<GameObject> asedio;
    public List<GameObject> trampa;
    public List<GameObject> despeje;
    public List<GameObject> clima;
    public List<GameObject> aumento;
    public List<GameObject> distancia;
    public List<GameObject> lider;

    ////posiciones  de cartas 1
    //public GameObject Hand1Position0;
    //public GameObject Hand1Position1;
    //public GameObject Hand1Position2;
    //public GameObject Hand1Position3;
    //public GameObject Hand1Position4;
    //public GameObject Hand1Position5;
    //public GameObject Hand1Position6;
    //public GameObject Hand1Position7;
    //public GameObject Hand1Position8;
    //public GameObject Hand1Position9;

    ////posiciones de las cartas 2
    //public GameObject Hand2Position0;
    //public GameObject Hand2Position1;
    //public GameObject Hand2Position2;
    //public GameObject Hand2Position3;
    //public GameObject Hand2Position4;
    //public GameObject Hand2Position5;
    //public GameObject Hand2Position6;
    //public GameObject Hand2Position7;
    //public GameObject Hand2Position8;
    //public GameObject Hand2Position9;


    //Lista con las 25 cartas de cada jugador
    public List<GameObject> PlayerOne;
    public List<GameObject> PlayerTwo;

    public List<GameObject> Hand1 = new List<GameObject>();
    public List<GameObject> Hand2 = new List<GameObject>();
     public GameObject[] h1p = new GameObject[10] ;
   public GameObject[] h2p = new GameObject[10];
    

    // Start is called before the first frame update

    void Start()
    {

        PlayerOne = new List<GameObject>();
        PlayerTwo = new List<GameObject>();
        //asignar cartas a jugadores
        Asignar(PlayerOne);
        Asignar(PlayerTwo);

        //sacar 10 cartas ramdon del deck para player1
        for (int i = 0; i < 10; i++)
        {
            GameObject t = PlayerOne[Random.Range(0, PlayerOne.Count)];
            Hand1.Add(t);
            PlayerOne.Remove(t);
        }

        //sacar 10 cartas ramdon del deck para player2
        for (int i = 0; i < 10; i++)
        {
            GameObject t = PlayerTwo[Random.Range(0, PlayerTwo.Count)];
            Hand2.Add(t);
            PlayerTwo.Remove(t);
        }

        // mostrarlas en el tablero para el jugador 1
        Transform position = transform.Find("Hand1Position");
        for (int j = 0; j < Hand1.Count; j++)
        {
            GameObject c = Hand1[j];
            Transform posicion = position.GetChild(j);
            Debug.Log(posicion.name);
            Debug.Log(position);
           // GameObject newposition = Instantiate(c, posicion.position, Quaternion.identity);
            //float scale = 1f;
            //newposition.transform.localScale = new Vector3(scale, scale, scale);
            c.transform.SetParent(posicion);
            c.transform.position = posicion.position;
            c.transform.localScale = Vector3.one;
        }

        // mostrarlas en el tablero para el jugador 2
        Transform position2 = transform.Find("Hand2Position");
        for (int j = 0; j < Hand2.Count; j++)
        {
            GameObject c2 = Hand2[j];
            Transform posicion2 = position2.GetChild(j);
            Debug.Log(posicion2.name);
            Debug.Log(position);
            //GameObject newposition2 = Instantiate(c2, posicion2.position, Quaternion.identity);
            float scale = 1f;
            //newposition2.transform.localScale = new Vector3(scale, scale, scale);
            c2.transform.SetParent(posicion2);
            c2.transform.position = posicion2.position;
            c2.transform.localScale = Vector3.one;
        }

       
    }

    // Update is called once per frame
    void Update()
    {

    }





    public void Asignar(List<GameObject> player)
    {
        AsignarCartas(player, heroes, 1);
        AsignarCartas(player, silver, 3);
        AsignarCartas(player, asedio, 4);
        AsignarCartas(player, trampa, 2);
        AsignarCartas(player, despeje, 2);
        AsignarCartas(player, aumento, 3);
        AsignarCartas(player, distancia, 3);
        AsignarCartas(player, clima, 2);
        AsignarCartas(player, guerreros, 5);


    }
    void AsignarCartas(List<GameObject> player, List<GameObject> lista, int cantidad)
    {
        int n = 0;
        int aleatorio = 0;
        while (n < cantidad)
        {
            aleatorio = Random.Range(0, lista.Count);
           // player.Add(lista[aleatorio]);
            player.Add(Instantiate(lista[aleatorio]));
         //var invovarSc=  lista[aleatorio].GetComponent<Invocar>();
            //invovarSc.deck = this;
            n++;
       //var card=     Instantiate(lista[aleatorio]);
        }
    }



    public void CammbiarCartas(GameObject card1, GameObject card2, List<GameObject> deck, List<GameObject> hand)
    {
        int a = Random.Range(0, deck.Count);
        int b = Random.Range(0, deck.Count);

        hand.Remove(card1); hand.Remove(card2);
        deck.Add(card1); deck.Add(card2);
        hand.Add(deck[a]); hand.Add(deck[b]);
    }

    public  void asignar2cartas1(GameObject[] hand1position) 
    {
        int n = 0;
        foreach (var position in hand1position) 
        {
            if (n == 2)
            {
                break;
            }
            if (position != null)
            {
                //hay algo
                continue;
            }
            else
            {
                //no hay nada
                n += 1;
                //tomar carta
                GameObject t = PlayerOne[Random.Range(0, PlayerOne.Count)];
                Hand1.Add(t);
                PlayerOne.Remove(t);
                // ponerla en la posicion vacia
                t.transform.SetParent(position.transform);
                t.transform.localScale = Vector3.zero;
                 
            }
        }

        
    
    }

    public void asignar2cartas2(GameObject[] hand1position)
    {
        int n = 0;
        foreach (var position in hand1position)
        {
            if (n == 2)
            {
                break;
            }
            if (position != null)
            {
                //hay algo
                continue;
            }
            else
            {
                //no hay nada
                n += 1;
                //tomar carta
                GameObject t = PlayerTwo[Random.Range(0, PlayerTwo.Count)];
                Hand1.Add(t);
                PlayerTwo.Remove(t);
                // ponerla en la posicion vacia
                t.transform.SetParent(position.transform);
                t.transform.localScale = Vector3.zero;

            }
        }



    }




}