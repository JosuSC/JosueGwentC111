using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Efectos : MonoBehaviour
{
    //metodo para carta aumento 
    public static void Aumento(ref int fila, int num, int cantidaddecartasenfila)
    {
        fila += num * cantidaddecartasenfila;
    }
  

    //metodo de overhix
    public static void Overhix(ref int valor)
    {
        valor -= 20;
    }

    //metodo de parathux
    public static void Parathux(ref int totaltuyo, int totaldecartasdelenemigo)
    {
        totaltuyo += totaldecartasdelenemigo * 5 ;
    }

    //metodo para Duvenik
    public static void Duvenik(ref int tuyo, ref int suyo, int tucementerio, int sucementerio)
    {
        int total = tucementerio + sucementerio;
        tuyo += 5 * total;
        suyo -= 5 * total;
    }

    


    
  
    public void Add(ref int fila  ,int num )
    {
        fila += num;
    }

    
}
