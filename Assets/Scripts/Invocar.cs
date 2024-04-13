using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invocar : MonoBehaviour
{
    public static GameObject cartaactual;

   static bool[,] maskplayer = new bool[3, 5];
    static bool[,] mask = new bool[3, 5];   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //-------------------------------------------------------------------------poner en player 1
    public void Poner()
    {
        Debug.Log("poner");
        if (cartaactual.GetComponent<Card>().type == "asedio")
        {
            if (maskplayer[1,1] == false)
            {
                GameObject pos = GameObject.Find("asedio0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x,v.y,1f);
                maskplayer[1, 1] = true;
            }
            else if (maskplayer[1, 2] == false)
            {
                GameObject pos = GameObject.Find("asedio1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 2] = true;
            }
           else  if (maskplayer[1, 3] == false)
            {
                GameObject pos = GameObject.Find("asedio2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 3] = true;
            }
        }

       else if (cartaactual.GetComponent<Card>().type == "clima")
        {
            if (maskplayer[0, 0] == false)
            {
                GameObject pos = GameObject.Find("clima0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0,0 ] = true;
            }
           else if (maskplayer[0, 1] == false)
            {
                GameObject pos = GameObject.Find("clima1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 1] = true;
            }
           else if (maskplayer[0, 2] == false)
            {
                GameObject pos = GameObject.Find("clima2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 2] = true;
            }
        }

       else if (cartaactual.GetComponent<Card>().type == "aumento")
        {
            if (maskplayer[0, 4] == false)
            {
                GameObject pos = GameObject.Find("aumento0");
                Vector3 v = pos.transform.position; 
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 4] = true;
            }
           else if (maskplayer[1, 4] == false)
            {
                GameObject pos = GameObject.Find("aumento1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 4] = true;
            }
           else if (maskplayer[2, 4] == false)
            {
                GameObject pos = GameObject.Find("aumento2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 4] = true;
            }
        }

        else if (cartaactual.GetComponent<Card>().type == "guerrero")
        {
           if (maskplayer[0, 1] == false)
            {
                GameObject pos = GameObject.Find("guerrero0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 1] = true;
            }
            else if (maskplayer[0, 2] == false)
            {
                GameObject pos = GameObject.Find("guerrero1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 2] = true;
            }
            else if (maskplayer[0, 3] == false)
            {
                GameObject pos = GameObject.Find("guerrero2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 3] = true;
            }
        }

       else if (cartaactual.GetComponent<Card>().type == "distancia")
        {
            if (maskplayer[2, 1] == false)
            {
                GameObject pos = GameObject.Find("distancia0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 1] = true;
            }
            else if (maskplayer[2, 2] == false)
            {
                GameObject pos = GameObject.Find("distancia1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 2] = true;
            }
           else  if (maskplayer[2, 3] == false)
            {
                GameObject pos = GameObject.Find("distancia2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 3] = true;
            }
        }
       else if (cartaactual.GetComponent<Card>().type == "trampa")
        {
            if (maskplayer[0, 1] == false)
            {
                GameObject pos = GameObject.Find("guerrero0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 1] = true;
            }
            else if (maskplayer[0, 2] == false)
            {
                GameObject pos = GameObject.Find("guerrero1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 2] = true;
            }
            else if (maskplayer[0, 3] == false)
            {
                GameObject pos = GameObject.Find("guerrero2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 3] = true;
            }
           else  if (maskplayer[2, 1] == false)
            {
                GameObject pos = GameObject.Find("distancia0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 1] = true;
            }
            else if (maskplayer[2, 2] == false)
            {
                GameObject pos = GameObject.Find("distancia1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 2] = true;
            }
            else if (maskplayer[2, 3] == false)
            {
                GameObject pos = GameObject.Find("distancia2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 3] = true;
            }
            else if (maskplayer[1, 1] == false)
            {
                GameObject pos = GameObject.Find("asedio0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 1] = true;
            }
           else  if (maskplayer[1, 2] == false)
            {
                GameObject pos = GameObject.Find("asedio1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 2] = true;
            }
            else if (maskplayer[1, 3] == false)
            {
                GameObject pos = GameObject.Find("asedio2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 3] = true;
            }
        }
       else if (cartaactual.GetComponent<Card>().type == "heroe")
        {
            if (maskplayer[0, 1] == false)
            {
                GameObject pos = GameObject.Find("guerrero0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 1] = true;
            }
            else if (maskplayer[0, 2] == false)
            {
                GameObject pos = GameObject.Find("guerrero1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 2] = true;
            }
            else if (maskplayer[0, 3] == false)
            {
                GameObject pos = GameObject.Find("guerrero2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 3] = true;
            }
            else if (maskplayer[2, 1] == false)
            {
                GameObject pos = GameObject.Find("distancia0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 1] = true;
            }
            else if (maskplayer[2, 2] == false)
            {
                GameObject pos = GameObject.Find("distancia1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 2] = true;
            }
            else if (maskplayer[2, 3] == false)
            {
                GameObject pos = GameObject.Find("distancia2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 3] = true;
            }
            else if (maskplayer[1, 1] == false)
            {
                GameObject pos = GameObject.Find("asedio0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 1] = true;
            }
            else if (maskplayer[1, 2] == false)
            {
                GameObject pos = GameObject.Find("asedio1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 2] = true;
            }
            else if (maskplayer[1, 3] == false)
            {
                GameObject pos = GameObject.Find("asedio2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 3] = true;
            }
        }
       else if (cartaactual.GetComponent<Card>().type == "silver")
        {
            if (maskplayer[0, 1] == false)
            {
                GameObject pos = GameObject.Find("guerrero0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 1] = true;
            }
            else if (maskplayer[0, 2] == false)
            {
                GameObject pos = GameObject.Find("guerrero1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 2] = true;
            }
            else if (maskplayer[0, 3] == false)
            {
                GameObject pos = GameObject.Find("guerrero2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 3] = true;
            }
            else if (maskplayer[2, 1] == false)
            {
                GameObject pos = GameObject.Find("distancia0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 1] = true;
            }
            else if (maskplayer[2, 2] == false)
            {
                GameObject pos = GameObject.Find("distancia1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 2] = true;
            }
            else if (maskplayer[2, 3] == false)
            {
                GameObject pos = GameObject.Find("distancia2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 3] = true;
            }
            else if (maskplayer[1, 1] == false)
            {
                GameObject pos = GameObject.Find("asedio0");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 1] = true;
            }
            else if (maskplayer[1, 2] == false)
            {
                GameObject pos = GameObject.Find("asedio1");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 2] = true;
            }
            else if (maskplayer[1, 3] == false)
            {
                GameObject pos = GameObject.Find("asedio2");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 3] = true;
            }
        }
       
        

    }
    //--------------------------------------------------------------------------------------------------fin
    //--------------------------------------------------poner para player 2
    private void Poner2()
    {
        if (cartaactual.GetComponent<Card>().type == "asedio")
        {
            if (maskplayer[1, 1] == false)
            {
                GameObject pos = GameObject.Find("asedio10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 1] = true;
            }
            else if (maskplayer[1, 2] == false)
            {
                GameObject pos = GameObject.Find("asedio11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 2] = true;
            }
            else if (maskplayer[1, 3] == false)
            {
                GameObject pos = GameObject.Find("asedio12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 3] = true;
            }
        }

        else if (cartaactual.GetComponent<Card>().type == "clima")
        {
            if (maskplayer[0, 0] == false)
            {
                GameObject pos = GameObject.Find("clima10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 0] = true;
            }
            else if (maskplayer[0, 1] == false)
            {
                GameObject pos = GameObject.Find("clima11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 1] = true;
            }
            else if (maskplayer[0, 2] == false)
            {
                GameObject pos = GameObject.Find("clima12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 2] = true;
            }
        }

        else if (cartaactual.GetComponent<Card>().type == "aumento")
        {
            if (maskplayer[0, 4] == false)
            {
                GameObject pos = GameObject.Find("aumento10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 4] = true;
            }
            else if (maskplayer[1, 4] == false)
            {
                GameObject pos = GameObject.Find("aumento11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 4] = true;
            }
            else if (maskplayer[2, 4] == false)
            {
                GameObject pos = GameObject.Find("aumento12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 4] = true;
            }
        }

        else if (cartaactual.GetComponent<Card>().type == "guerrero")
        {
            if (maskplayer[0, 1] == false)
            {
                GameObject pos = GameObject.Find("guerrero10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 1] = true;
            }
            else if (maskplayer[0, 2] == false)
            {
                GameObject pos = GameObject.Find("guerrero11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 2] = true;
            }
            else if (maskplayer[0, 3] == false)
            {
                GameObject pos = GameObject.Find("guerrero12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 3] = true;
            }
        }

        else if (cartaactual.GetComponent<Card>().type == "distancia")
        {
            if (maskplayer[2, 1] == false)
            {
                GameObject pos = GameObject.Find("distancia10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 1] = true;
            }
            else if (maskplayer[2, 2] == false)
            {
                GameObject pos = GameObject.Find("distancia11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 2] = true;
            }
            else if (maskplayer[2, 3] == false)
            {
                GameObject pos = GameObject.Find("distancia12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 3] = true;
            }
        }
        else if (cartaactual.GetComponent<Card>().type == "trampa")
        {
            if (maskplayer[0, 1] == false)
            {
                GameObject pos = GameObject.Find("guerrero10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 1] = true;
            }
            else if (maskplayer[0, 2] == false)
            {
                GameObject pos = GameObject.Find("guerrero11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 2] = true;
            }
            else if (maskplayer[0, 3] == false)
            {
                GameObject pos = GameObject.Find("guerrero12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 3] = true;
            }
            else if (maskplayer[2, 1] == false)
            {
                GameObject pos = GameObject.Find("distancia10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 1] = true;
            }
            else if (maskplayer[2, 2] == false)
            {
                GameObject pos = GameObject.Find("distancia11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 2] = true;
            }
            else if (maskplayer[2, 3] == false)
            {
                GameObject pos = GameObject.Find("distancia12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 3] = true;
            }
            else if (maskplayer[1, 1] == false)
            {
                GameObject pos = GameObject.Find("asedio10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 1] = true;
            }
            else if (maskplayer[1, 2] == false)
            {
                GameObject pos = GameObject.Find("asedio11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 2] = true;
            }
            else if (maskplayer[1, 3] == false)
            {
                GameObject pos = GameObject.Find("asedio12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 3] = true;
            }
        }
        else if (cartaactual.GetComponent<Card>().type == "heroe")
        {
            if (maskplayer[0, 1] == false)
            {
                GameObject pos = GameObject.Find("guerrero10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 1] = true;
            }
            else if (maskplayer[0, 2] == false)
            {
                GameObject pos = GameObject.Find("guerrero11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 2] = true;
            }
            else if (maskplayer[0, 3] == false)
            {
                GameObject pos = GameObject.Find("guerrero12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 3] = true;
            }
            else if (maskplayer[2, 1] == false)
            {
                GameObject pos = GameObject.Find("distancia10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 1] = true;
            }
            else if (maskplayer[2, 2] == false)
            {
                GameObject pos = GameObject.Find("distancia11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 2] = true;
            }
            else if (maskplayer[2, 3] == false)
            {
                GameObject pos = GameObject.Find("distancia12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 3] = true;
            }
            else if (maskplayer[1, 1] == false)
            {
                GameObject pos = GameObject.Find("asedio10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 1] = true;
            }
            else if (maskplayer[1, 2] == false)
            {
                GameObject pos = GameObject.Find("asedio11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 2] = true;
            }
            else if (maskplayer[1, 3] == false)
            {
                GameObject pos = GameObject.Find("asedio12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 3] = true;
            }
        }
        else if (cartaactual.GetComponent<Card>().type == "silver")
        {
            if (maskplayer[0, 1] == false)
            {
                GameObject pos = GameObject.Find("guerrero10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 1] = true;
            }
            else if (maskplayer[0, 2] == false)
            {
                GameObject pos = GameObject.Find("guerrero11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 2] = true;
            }
            else if (maskplayer[0, 3] == false)
            {
                GameObject pos = GameObject.Find("guerrero12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[0, 3] = true;
            }
            else if (maskplayer[2, 1] == false)
            {
                GameObject pos = GameObject.Find("distancia10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 1] = true;
            }
            else if (maskplayer[2, 2] == false)
            {
                GameObject pos = GameObject.Find("distancia11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 2] = true;
            }
            else if (maskplayer[2, 3] == false)
            {
                GameObject pos = GameObject.Find("distancia12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[2, 3] = true;
            }
            else if (maskplayer[1, 1] == false)
            {
                GameObject pos = GameObject.Find("asedio10");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 1] = true;
            }
            else if (maskplayer[1, 2] == false)
            {
                GameObject pos = GameObject.Find("asedio11");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 2] = true;
            }
            else if (maskplayer[1, 3] == false)
            {
                GameObject pos = GameObject.Find("asedio12");
                Vector3 v = pos.transform.position;
                cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
                maskplayer[1, 3] = true;
            }
        }

    }


    public void OnMouseDown() 
    {
        Debug.Log("on");
        cartaactual = gameObject;
        Poner();


    }

}
