using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class newgameboard : MonoBehaviour
{
    public GameObject Dovakin;
    public GameObject Alduin;

    public int cementerio1 = 0;
    public int cementerio2 = 0; 
    //player 1
    public  TextMeshProUGUI Filag1;
    public TextMeshProUGUI Filaa1;
    public TextMeshProUGUI Filadi;

    public int filag1 = 0;
    public int filaa1 = 0;
    public int filad1 = 0;
    //player2
    public TextMeshProUGUI Filag2;
    public TextMeshProUGUI Filad2;
    public TextMeshProUGUI Filaa2;

     public  int filag2 = 0;
     public  int filaa2 = 0;
     public int filad2 = 0;
    //todo el conteo de ambos
    public TextMeshProUGUI AllCount1;
    public TextMeshProUGUI AllCount2;

    public int allcount1 = 0;
    public int allcount2 = 0;


    private void Start()
    {
        PowerPoints.Load(Filag1, Filaa1,Filadi,Filag2,Filaa2,Filad2,AllCount1,AllCount2);
    }

    public void SumarFila(ref int fila, int num)
    {
        fila += num;
    }

    public void Cuenta1()
    {
        allcount1 = filaa1 + filad1 + filag1;
        ActualizarText(AllCount1, allcount1);
    }

    public void Cuenta2()
    {
        allcount2 = filaa2 + filag2 + filad2;
        ActualizarText(AllCount2, allcount2);
    }

    public void ActualizarText(TextMeshProUGUI a, int b)
    {
        a.text = b.ToString();
    }
}
