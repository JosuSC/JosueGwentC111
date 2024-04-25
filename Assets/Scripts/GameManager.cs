using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("Ganador1");
        }

        if (rondasplayer2 == 2)
        {
            SceneManager.LoadScene("Ganador1");
        }
    }






    // List<Card> temp = new();
    // string nombre = "Guh";
    // var card = temp.First(c => c.Type == nombre);
    // 


   //  h = game.GetComponent<CardScript>();
  


}


