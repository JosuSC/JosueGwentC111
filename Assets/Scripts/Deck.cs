using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Analytics;
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

  

    //Lista con las 25 cartas de cada jugador
    public List<GameObject> PlayerOne;
    public List<GameObject> PlayerTwo; 

    public List<GameObject> Hand1 = new List<GameObject>();
    public List<GameObject> Hand2 = new List<GameObject>();

  

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
            GameObject newposition = Instantiate(c,posicion.position,Quaternion.identity);
            float scale = 1f;
            newposition.transform.localScale = new Vector3(scale, scale, scale);
      
        }

        // mostrarlas en el tablero para el jugador 2
        Transform position2 = transform.Find("Hand2Position");
        for (int j = 0; j < Hand2.Count; j++)
        {
            GameObject c2 = Hand2[j];
            Transform posicion2 = position.GetChild(j);
            Debug.Log(posicion2.name);
            Debug.Log(position);
            GameObject newposition = Instantiate(c2, posicion2.position, Quaternion.identity);
            float scale = 1f;
            newposition.transform.localScale = new Vector3(scale, scale, scale);

        }







        //mostrar en el tablero


        //GameObject playercard = Instantiate(despeje[2], new Vector3(0, 0, 0), Quaternion.identity);
        //Transform hand1Transform = hand1.GetComponent<Transform>();
        ////GameObject playercard = Instantiate(Hand1[0], hand1Transform);
        //playercard.transform.SetParent(hand1.transform, false);


        //GameObject temp;
        //GameObject playercard;
        //for (int i = 0; i < 10; i++)
        //{
        //    temp = MyCards[Random.Range(0, MyCards.Count)];
        //    Hand1.Add(temp);
        //    playercard = Instantiate(Hand1[i],new Vector3(0,0,0),Quaternion.identity);
        //    playercard .transform.localScale= new Vector3(0.2f , 0.2f , 0.2f);
        //    playercard.transform.SetParent(HandPosition.transform, false);
        //}

        ////}
        //for (int i = 0; i < 10; i++)
        //{
        //     temp = MyCards[Random.Range(0, MyCards.Count)];
        //    Hand2.Add(temp);
        //    playercard = Instantiate(Hand2[i], new Vector3(0, 0, 0), Quaternion.identity);
        //    playercard.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //    playercard.transform.SetParent(HandPosition.transform, false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }




 
    public void Asignar(List<GameObject> player)
    {
        AsignarCartas(player,heroes, 1);
        AsignarCartas(player, silver, 3);
        AsignarCartas(player, asedio, 4);
        AsignarCartas(player, trampa, 2);
        AsignarCartas(player, despeje, 2);
        AsignarCartas(player, aumento, 3);
        AsignarCartas(player, distancia, 3);
        AsignarCartas(player, clima, 2);
        AsignarCartas(player, guerreros, 5);


    }
    void AsignarCartas(List<GameObject> player,List<GameObject> lista, int cantidad)
    {
        int n = 0;
        int aleatorio = 0;
        while (n < cantidad)
        {
            aleatorio= Random.Range(0, lista.Count);
            player.Add(lista[aleatorio]);
            n++;
        }
    }



    public void CammbiarCartas(GameObject card1,GameObject card2 ,List<GameObject> deck, List<GameObject> hand) 
    {
        int a = Random.Range(0,deck.Count);
        int b = Random.Range(0, deck.Count);

        hand.Remove(card1); hand.Remove(card2); 
        deck.Add(card1); deck.Add(card2);
        hand.Add(deck[a]); hand.Add(deck[b]);
    }
    // Asignar un héroe

    // Función para asignar cartas 



    // Asignar cartas de diferentes tipos

}

    /*
    public void Asignar(List<GameObject> h) 
    {
        List<GameObject> yaesta = new List<GameObject>();

        //asignar un heroe
        aleatorio = Random.Range(0, heroes.Count);
        h.Add(heroes[aleatorio]);
        GameObject carta;

        //asignar 3 silver
        n = 0;
        while (n < 3)
        {
            aleatorio = Random.Range(0, silver.Count);
            if (yaesta.Contains(silver[aleatorio])) 
            {
                continue;
            }
            h.Add(silver[aleatorio]);
            yaesta.Add(silver[aleatorio]);
            n++;
        }

        //asignar 4 asedio
        n = 0;
        while (n < 4)
        {
            aleatorio = Random.Range(0, asedio.Count);
            if (yaesta.Contains(asedio[aleatorio]))
            {
                continue;
            }
            h.Add(asedio[aleatorio]);
            yaesta.Add(asedio[aleatorio]);
            n++;
        }

        //asignar 2 trampas
        n = 0;
        while (n < 2)
        {
            aleatorio = Random.Range(0, trampa.Count);
            if (yaesta.Contains(asedio[aleatorio]))
            {
                continue;
            }
            h.Add(asedio[aleatorio]);
            yaesta.Add(asedio[aleatorio]);
            n++;
        }

        //asignar 2 despeje
        n = 0;
        while (n < 2)
        {
            aleatorio = Random.Range(0, despeje.Count);
            if (yaesta.Contains(despeje[aleatorio]))
            {
                continue;
            }
            h.Add(despeje[aleatorio]);
            yaesta.Add(despeje[aleatorio]);
            n++;
        }

        //asignar 3 aumento
        n = 0;
        while (n < 3)
        {
            aleatorio = Random.Range(0, aumento.Count);
            if (yaesta.Contains(aumento[aleatorio]))
            {
                continue;
            }
            h.Add(aumento[aleatorio]);
            yaesta.Add(aumento[aleatorio]);
            n++;
        }

        //asignar 2 clima
        n = 0;
        while (n < 2)
        {
            aleatorio = Random.Range(0, clima.Count);
            if (yaesta.Count > 0 && aleatorio < yaesta.Count)
            {
                if (yaesta.Contains(clima[aleatorio]))
                {
                    continue;
                }
                h.Add(clima[aleatorio]);
                yaesta.Add(clima[aleatorio]);
                n++;
            }
        }
          

        //asignar 5 cuerpo a cuerpo
        n = 0;
        while (n < 2)
        {
            aleatorio = Random.Range(0, guerreros.Count);
            if (yaesta.Contains(guerreros[aleatorio]))
            {
                continue;
            }
            h.Add(guerreros[aleatorio]);
            yaesta.Add(guerreros[aleatorio]);
            n++;
        }

        //asignar 3 distancia
        n = 0;
        while (n < 2)
        {
            aleatorio = Random.Range(0, distancia.Count);
            if (yaesta.Contains(distancia[aleatorio]))
            {
                continue;
            }
            h.Add(distancia[aleatorio]);
            yaesta.Add(distancia[aleatorio]);
            n++;
        }

    } 
    */




   /* public List<GameObject> AsignarCartas(List<GameObject> a, List<GameObject> Result)
    {

        List<int> yaesta = new List<int>();
        int indice = Random.Range(0, a.Count);


        //poner las 3 cartas de oro
        for (int o = 0; o < 3; o++)
        {
            while (MyCards[indice].Type != V)
            {
                if (yaesta.Contains(indice))
                {
                    indice = Random.Range(0, a.Count);
                    continue;
                }
                yaesta.Add(indice);
                Result.Add(MyCards[indice]);
                indice = Random.Range(0, a.Count);

            }

        }

        //poner las 5 cartas de plata
        for (int o = 0; o < 5; o++)
        {

            while (MyCards[indice].level != "Silver")
            {
                if (yaesta.Contains(indice))
                {
                    indice = Random.Range(0, a.Count);
                    continue;
                }
                yaesta.Add(indice);
                Result.Add(MyCards[indice]);
                indice = Random.Range(0, a.Count);

            }

        }

        //poner dos cartas clima
        for (int o = 0; o < 2; o++)
        {
            while (MyCards[indice].level != "Clima")
            {
                if (yaesta.Contains(indice))
                {
                    indice = Random.Range(0, a.Count);
                    continue;
                }
                yaesta.Add(indice);
                Result.Add(MyCards[indice]);
                indice = Random.Range(0, a.Count);

            }

        }

        //poner tres armas fuertes
        for (int o = 0; o < 3; o++)
        {
            while (MyCards[indice].level != "Mejorado")
            {
                if (yaesta.Contains(indice))
                {
                    indice = Random.Range(0, a.Count);
                    continue;
                }
                yaesta.Add(indice);
                Result.Add(MyCards[indice]);
                indice = Random.Range(0, a.Count);

            }

        }


        //poner 12 cartas simples
        for (int o = 0; o < 3; o++)
        {
            while (MyCards[indice].level != "Simple")
            {
                if (yaesta.Contains(indice))
                {
                    indice = Random.Range(0, a.Count);
                    continue;
                }
                yaesta.Add(indice);
                Result.Add(MyCards[indice]);

                indice = Random.Range(0, a.Count);
            }

        }

        return Result;
    }

    */

