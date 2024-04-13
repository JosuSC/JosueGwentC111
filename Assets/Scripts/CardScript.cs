using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Card",menuName = "Card")]
public class CardScript : ScriptableObject
{
    //Campos
    public int number;
    public new string name;
    public string description;
    public Sprite imagen;
    public int power;
    public int life;
    public string type;
    public  string level;

    public void Print() 
    {
        Debug.Log(name +": "+ description + "Esta carta es del tipo :"+ type + " y tiene nivel: "+level);
    }


    //metodo para carta aumento 
    public void Aumento(int fila, int num, int cantidaddecartasenfila)
    {
        fila += num * cantidaddecartasenfila;
    }
    //metodo clima
    public void Clima(int fila1, int fila2, int num, int lenfila1, int lenfila2)
    {
        fila1 -= num * lenfila1;
        fila2 -= num * lenfila2;

    }

    //metodo de overhix
    public void Overhix(int valor)
    {
        valor -= 20;
    }

    //metodo de parathux
    public void Parathux(int totaltuyo, int enemyg, int enemyd, int enemya)
    {
        totaltuyo += enemya * 5 + enemyd * 5 + enemyg * 5;

    }

    //metodo para Duverik
    public void Duvenik(int tuyo, int suyo, int tucementerio, int sucementerio)
    {
        int total = tucementerio + sucementerio;
        tuyo += 5 * total;
        suyo -= 5 * total;
    }
    /*
        public void HayClima()
        {
            for (int i = 0; i < 3; i++)
            {

                if (weatherzone[0] != null)
                {
                    Clima(gzone1, gzone2, filag1, filag2);
                }
                if (weatherzone[1] != null)
                {
                    Clima(azone1, azone2, filaa1, filaa2);

                }
                if (weatherzone[2] != null)
                {
                    Clima(dzone1, dzone2, filad1, filad2);

                }

            }
        }*/


    //public void Aumentof()
    //{

    //    for (int i = 0; i < 3; i++)
    //    {
    //        if (aumentozone[0] != null)
    //        {
    //            GameManager.Aumento();
    //        }
    //        if (aumentozone[1] != null)
    //        {

    //        }
    //        if (aumentozone[2] != null)
    //        {

    //        }
    //    }
    //}

    //efecto clima
    public void Clima(GameObject[] a, GameObject[] b, int acount, int bcount)
    {
        Reducir(a, acount);
        Reducir(b, bcount);
    }


    //reducir el poder de los elementos a 1
    public void Reducir(GameObject[] row, int rowcount)
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
    public void Efectos()
    {

        // realizar los efectos3



    }

}
