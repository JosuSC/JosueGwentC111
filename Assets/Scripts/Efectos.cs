using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efectos : MonoBehaviour
{
    //metodo para carta aumento 
    public void Aumento(ref int fila, int num, int cantidaddecartasenfila)
    {
        fila += num * cantidaddecartasenfila;
    }
    //metodo clima
    public void Clima(ref int fila1, ref int fila2, int num, int lenfila1, int lenfila2)
    {
        fila1 -= num * lenfila1;
        fila2 -= num * lenfila2;
    }

    //metodo de overhix
    public void Overhix(ref int valor)
    {
        valor -= 20;
    }

    //metodo de parathux
    public void Parathux(ref int totaltuyo, int totaldecartasdelenemigo)
    {
        totaltuyo += totaldecartasdelenemigo * 5 ;
    }

    //metodo para Duvenik
    public void Duvenik(ref int tuyo, ref int suyo, int tucementerio, int sucementerio)
    {
        int total = tucementerio + sucementerio;
        tuyo += 5 * total;
        suyo -= 5 * total;
    }

    

    //efecto clima
    public void Clima(GameObject[] a, GameObject[] b, ref int acount, ref int bcount)
    {
        Reducir(a, ref acount);
        Reducir(b, ref bcount);
    }


    //reducir el poder de los elementos a 1
    public void Reducir(GameObject[] row, ref int rowcount)
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

    public void Talos(ref int suyo, bool[,] b) 
    {
        for (int i = 0; i < b.GetLength(0); i++)
        {
            for (int j = 0; j <b.GetLength(1); j++)
            {
                
            }
        }
    }
    public void Add(ref int fila  ,int num )
    {
        fila += num;
    }

    public void Clima() 
    {

    }
}
