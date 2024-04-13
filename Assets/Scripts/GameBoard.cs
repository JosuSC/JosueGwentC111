using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;


public class GameBoard : MonoBehaviour
{
    public GameObject[] weatherzone1;
    public GameObject[] aumentozone1;
    public GameObject[] gzone1;
    public GameObject[] dzone1;
    public GameObject[] azone1;

    public GameObject[] gzone2;
    public GameObject[] dzone2;
    public GameObject[] azone2;
    public GameObject[] weatherzone2;
    public GameObject[] aumentozone2;

    public GameObject Dovakin;
    public GameObject Alduin;
                                                                                            
    public List<GameObject> cementerio1;
    public List<GameObject> cementerio2;

    //para llevar cuenta de las filas del player 1
    public int filag1;
    public int filaa1;
    public int filad1;

    //para llevar cuenta de las filas del player 2
    public int filag2;
    public int filaa2;
    public int filad2;

    //cuenta general
    public int allcount1;
    public int allcount2;
    public GameBoard()
    {
        weatherzone1 = new GameObject[3];
        weatherzone2 = new GameObject[3];
        aumentozone1 = new GameObject[3];
        aumentozone2 = new GameObject[3];
        gzone1 = new GameObject[3];
        dzone1 = new GameObject[3];
        azone1 = new GameObject[3];
        gzone2 = new GameObject[3];
        dzone2 = new GameObject[3];
        azone2 = new GameObject[3];
        cementerio1 = new List<GameObject>();
        cementerio2 = new List<GameObject>();
        filaa1 = 0;
        filaa2 = 0;
        filad1= 0;
        filad2= 0;
        filag1 = 0;
        filaa2 = 0;
        allcount1= 0;
        allcount2= 0;
    }

    /*necesito
     *para cartas lider
     para cementerios
      para llevar cuenta de cada fila 
    para llevar cuenta general de cada jugador*/

    //metodo para sumar fila indicada
    public void SumarFila(int fila, int num)
    {
        fila += num;
    }


    //llevar el conteo total del jugador 1
    public void Cuenta1()                 
    {
        allcount1 = filaa1 + filad1 + filag1;
    }

   // llevar conteo total de la jugador 2     
    public void Cuenta2()
    {
        allcount2 = filaa2 + filag2 + filad2;
    }
}
