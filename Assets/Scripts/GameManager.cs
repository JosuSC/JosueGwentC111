using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameManager 
{
    static int rondasplayer1 = 0;
   static  int rondasplayer2 = 0;
   
  
   
   

   




  

    static  public void GanarRonda() 
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
        GanarJuego();
        

    }

   

   static public void GanarJuego() 
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


