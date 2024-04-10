using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;


public class GameBoard : MonoBehaviour
{
    GameObject[] weatherzone = new GameObject[3];
    GameObject[] aumentozone = new GameObject[3];
    GameObject[] gzone1 = new GameObject[3];
    GameObject[] dzone1 = new GameObject[3];
    GameObject[] azone1 = new GameObject[3];

    GameObject[] gzone2 = new GameObject[3];
    GameObject[] dzone2 = new GameObject[3];
    GameObject[] azone2 = new GameObject[3];
   private GameManager game;

    public GameBoard()
    {
           // game = new GameManager();
    }
    public void HayClima() 
    {
        for (int i = 0; i < 3; i++)
        {
            /*
            if (weatherzone[0] != null)
            {
                Clima(gzone1,gzone2,filag1,filag2);
            }
            if (weatherzone[1] != null)
            {
                Clima(azone1, azone2, filaa1, filaa2);

            }
            if (weatherzone[2] != null)
            {
                Clima(dzone1, dzone2, filad1, filad2);

            }
            */
        }
    }

    //efecto clima
    public void Clima(GameObject[] a, GameObject[] b,int acount,int bcount) 
    {
        Reducir(a,acount);
        Reducir(b,bcount);  
    }

    public void Aumento() 
    {
        
        for (int i = 0; i < 3; i++)
        {
            if (aumentozone[0] != null)
            {
                //GameManager.Aumento();
            }
            if (aumentozone[1] != null)
            {

            }
            if (aumentozone[2] != null)
            {

            }
        }
    }

    //reducir el poder de los elementos a 1
    public void Reducir(GameObject[] row ,int rowcount) 
    {
        rowcount = 0;
        for (int i = 0; i < 3; i++)
        {
            if (row[i] != null)
            {
                rowcount += 0;
            }
        }
    }
}
