using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Deck : MonoBehaviour
{
    //Lista con todas mis cartas
   public  List<CardScript> MyCards;
    

    //Lista con las 25 cartas de cada jugador
    List<CardScript> PlayerOne;
    List<CardScript> PlayerTwo; 




    // Start is called before the first frame update
    void Start()
    {
        //Asignar las cartas a los jugadores
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public List<CardScript> AsignarCartas(List<CardScript> a ,List<CardScript> Result) 
    {
        
        List<int> yaesta = new List<int>(); 
        int indice = Random.Range(0, a.Count);
       

        //poner las 3 cartas de oro
        for (int o = 0; o < 3; o++)
        {
            while (MyCards[indice].level != "Golden") 
            {
              if(yaesta.Contains(indice))
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





}
