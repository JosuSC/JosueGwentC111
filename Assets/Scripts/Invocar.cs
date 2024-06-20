using Assets;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Invocar : MonoBehaviour
{
    //gameobject q va a ser la carta actual
    public static GameObject cartaactual;

    //matriz para saber las posiciones de  las cartas del player 1
    static bool[,] maskplayer = new bool[3, 5];
    //matriz para saber las posiciones de  las cartas del player 2
    static bool[,] mask = new bool[3, 5];
   
    public static int totaldecartas1 = 0;
   public static int totalpower1 = 0;
   public  static int totaldecartas2 = 0;
   public static int totalpower2 = 0;
    //variable usada para cuano se va a jugar una carta heroe o silver saber a cual fila se le va a sumar su puntos 
    int p = 0;

    //cemeterios
   public  static int cementerio1 = 0;
    public static int cementerio2 = 0;
   
  //game object para hacerlo padre las cartas jugas
    GameObject totalcards;
   

    //para despeje
    int despejeg0 =0;
    int despejeg1 = 0;
    int despejeg2 = 0;

    int despejeg10 = 0;
    int despejeg11 = 0;
    int despejeg12 = 0;


    int despejea0 = 0;
    int despejea1 = 0;
    int despejea2 = 0;

    int despejea10 = 0;
    int despejea11 = 0;
    int despejea12 = 0;

    int despejed0 = 0;
    int despejed1 = 0;
    int despejed2 = 0;

    int despejed10 = 0;
    int despejed11 = 0;
    int despejed12 = 0;

    //-----------------------------------------------------
    Deck deck;

    public Invocar()
    {
        this.deck = new Deck();
    }

    //------------------------------------------

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
 
    }

    //metodo para poner las cartas en el tablero
    public void PonerCarta(string cellName, bool[,] matriz, int cellX, int cellY,int player)
    {
        
        Debug.Log("Tablero encontrado");
        totalcards = GameObject.Find("CardsTotal");
        if (totalcards != null)
        {
            //hacemos la carta jugada hija de cardastotal
            Transform t = totalcards.transform;
            cartaactual.transform.SetParent(t, false);

        }
       
        //busca el gameobject con el nombre de cellName se le asigna un vector3 y se le cambia de posicion
        GameObject pos = GameObject.Find(cellName);
        Vector3 v = pos.transform.position;
        cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
        matriz[cellX, cellY] = true;
    
        if (player == 1)
        {
            totaldecartas1 += 1;
            totalpower1 += cartaactual.GetComponent<Card>().power;
            Deck.VerificarPosition1(cartaactual,deck.Hand1,deck.h1p);
            Debug.Log("Cartaspuntos1 :" + totalpower1);
            Debug.Log("Cartastotal1 :" + totaldecartas1);
        }
        else
        {
            totaldecartas2 += 1;
            totalpower2 += cartaactual.GetComponent<Card>().power;
            TurnSystem.deck.VerificarPosition2(cartaactual);

            Debug.Log("Cartaspuntos2 :" + totalpower2);
            Debug.Log("Cartastotal2 :" + totaldecartas2);

        }
        Collider2D MiCollider = cartaactual.GetComponent<Collider2D>(); 
        MiCollider.enabled = false; 
    }

    //-------------------------------------------------------------------------poner en player 1
    public void Poner()
    {
        Debug.Log("poner");
        //--------------------------------------------------------asedio
        if (cartaactual.GetComponent<Card>().type == "asedio")
        {
            if (maskplayer[1, 1] == false)
            {
                PonerCarta("asedio0", maskplayer, 1, 1,1);
            }
            else if (maskplayer[1, 2] == false)
            {
                PonerCarta("asedio1", maskplayer, 1, 2,1);
            }
            else if (maskplayer[1, 3] == false)
            {
                PonerCarta("asedio2", maskplayer, 1, 3,1);

            }
            //puntos
            PowerPoints.addPointToasedio1(cartaactual.GetComponent<Card>().power);
            PowerPoints.Actualizar1();
        }
        //------------------------------------------------------------------------------clima
        else if (cartaactual.GetComponent<Card>().type == "clima")
        {
            if (maskplayer[0, 0] == false)
            {
                //poner la carta en la posicion vacia
                PonerCarta("clima0", maskplayer, 0, 0,1);

                //quitarle los puntos de la fila  al total
                PowerPoints.addPointotal1(-PowerPoints.filag1);
                PowerPoints.filag1 = 0;
                    //fila guererero de ambos jugadores//jugador 1
                    if (maskplayer[0, 1])
                    {
                      Clima(cartaactual,despejeg0,"g1");
                     
                    }
                    if (maskplayer[0, 2])
                    {
                      Clima(cartaactual,despejeg1,"g1");
                    }
                    if (maskplayer[0, 3])
                    {
                      Clima(cartaactual, despejeg2, "g1");
                    }
                PowerPoints.Actualizar1();

                //jugador 2
                PowerPoints.addPointotal2(-PowerPoints.filag2);
                PowerPoints.filag2 = 0;
                if (mask[0, 1])
                {
                    Clima(cartaactual, despejeg10, "g2");

                    
                }
                if (mask[0, 2])
                {
                    Clima(cartaactual, despejeg11, "g2");

                   
                }
                if (mask[0, 3])
                {
                    Clima(cartaactual, despejeg12, "g2");

                }
                PowerPoints.Actualizar2();

            }
            else if (maskplayer[1, 0] == false)
            {
                PonerCarta("clima1", maskplayer, 1, 0,1);
                PowerPoints.addPointotal1(-PowerPoints.filaa1);
                PowerPoints.filaa1 = 0;


                //fila asedio de ambos jugadores // j 1

                    if (maskplayer[1, 1])
                    {
                       Clima(cartaactual, despejea0, "a1");

               
                    }
                    if (maskplayer[1, 2])
                    {
                     Clima(cartaactual, despejea1, "a1");

                    }
                    if (maskplayer[1, 3])
                    {
                    Clima(cartaactual, despejea2, "a1");

                    }
                    PowerPoints.Actualizar1();
                //j2
                PowerPoints.addPointotal2(-PowerPoints.filaa2);
                PowerPoints.filaa2 = 0;

                if (mask[1, 1])
                {
                    Clima(cartaactual, despejea10, "a2");                  
                }
                if (mask[1, 2])
                {
                    Clima(cartaactual, despejea11, "a2");

                }
                if (mask[1, 3])
                {
                    Clima(cartaactual, despejea12, "a2");

                }
                PowerPoints.Actualizar2();

            }
            else if (maskplayer[2, 0] == false)
            {
                PonerCarta("clima2", maskplayer, 2, 0, 1);
                PowerPoints.addPointotal1(-PowerPoints.filad1);
                PowerPoints.filad1 = 0;
                
                //fila distancia de ambos jugadores//j1
                if (maskplayer[2, 1])
                {
                    Clima(cartaactual, despejed0, "d1");

                }
                if (maskplayer[2, 2])
                {
                    Clima(cartaactual, despejed1, "d1");

                   
                }
                if (maskplayer[2, 3])
                {
                    Clima(cartaactual, despejed2, "d1");
                }
                PowerPoints.Actualizar1();
                //j2
                PowerPoints.addPointotal2(-PowerPoints.filad2);
                PowerPoints.filad2 = 0;

                if (mask[2, 1])
                {
                    Clima(cartaactual, despejed10, "d2");  
                }
                if (mask[2, 2])
                {
                    Clima(cartaactual, despejed11, "d2");  
                }
                if (mask[2, 3])
                {
                    Clima(cartaactual, despejed12, "d2");
                }
                PowerPoints.Actualizar2();
            } 
        }
        //-----------------------------------------------------------------------------aumento
        else if (cartaactual.GetComponent<Card>().type == "aumento")
        {
            if (maskplayer[0, 4] == false)
            {
                PonerCarta("aumento0", maskplayer, 0, 4, 1);
                PowerPoints.addPointToWarrior1(PowerPoints.filag1 * cartaactual.GetComponent<Card>().power);
                PowerPoints.Actualizar1();
            }
            else if (maskplayer[1, 4] == false)
            {
                PonerCarta("aumento1", maskplayer, 1, 4, 1);
                PowerPoints.addPointToasedio1(PowerPoints.filaa1 * cartaactual.GetComponent<Card>().power);
                PowerPoints.Actualizar1();

            }
            else if (maskplayer[2, 4] == false)
            {
                PonerCarta("aumento2", maskplayer, 2, 4, 1);
                PowerPoints.addPointTodistance1(PowerPoints.filad1 * cartaactual.GetComponent<Card>().power);
                PowerPoints.Actualizar1();
            }
        }
        //------------------------------------------------------------------------guerrero
        else if (cartaactual.GetComponent<Card>().type == "guerrero")
        {
            if (maskplayer[0, 1] == false)
            {
                PonerCarta("guerrero0", maskplayer, 0, 1, 1);
               
            }
            else if (maskplayer[0, 2] == false)
            {
                PonerCarta("guerrero1", maskplayer, 0, 2, 1);
            }
            else if (maskplayer[0, 3] == false)
            {
                PonerCarta("guerrero2", maskplayer, 0, 3, 1);
            }
            //puntos
            Debug.Log("hgh");
            PowerPoints.addPointToWarrior1(cartaactual.GetComponent<Card>().power);
            PowerPoints.Actualizar1();

        }//-----------------------------------------------------------------------------------------------------distancia

        else if (cartaactual.GetComponent<Card>().type == "distancia")
        {
            if (maskplayer[2, 1] == false)
            {
                PonerCarta("distancia0", maskplayer, 2, 1, 1);


            }
            else if (maskplayer[2, 2] == false)
            {
                PonerCarta("distancia1", maskplayer, 2, 2, 1);


            }
            else if (maskplayer[2, 3] == false)
            {
                PonerCarta("distancia2", maskplayer, 2, 3, 1);


            }
            //puntos
            PowerPoints.addPointTodistance1(cartaactual.GetComponent<Card>().power);
            PowerPoints.Actualizar1();

        }//--------------------------------------------------------------------------------trampa
        else if (cartaactual.GetComponent<Card>().type == "trampa")
        {
            if (mask[0, 1] == false)
            {
                PonerCarta("guerrero10", mask, 0, 1,2);

            }
            else if (mask[0, 2] == false)
            {
                PonerCarta("guerrero11", mask, 0, 2,2);

            }
            else if (mask[0, 3] == false)
            {
                PonerCarta("guerrero12", mask, 0, 3,2);

            }
            else if (mask[2, 1] == false)
            {
                PonerCarta("distancia10", mask, 2, 1,2);

            }
            else if (mask[2, 2] == false)
            {
                PonerCarta("distancia11", mask, 2, 2,2);

            }
            else if (mask[2, 3] == false)
            {
                PonerCarta("distancia12", mask, 2, 3,2);
 
            }
            else if (mask[1, 1] == false)
            {
                PonerCarta("asedio10", mask, 1, 1,2);
            }
            else if (mask[1, 2] == false)
            {
                PonerCarta("asedio11", mask, 1, 2,2);
            }
            else if (mask[1, 3] == false)
            {
                PonerCarta("asedio12", mask, 1, 3,2);
            }
        }//-------------------------------------------------------------------heroe
        else if (cartaactual.GetComponent<Card>().type == "heroe")
        {
            if (maskplayer[0, 1] == false)
            {
                PonerCarta("guerrero0", maskplayer, 0, 1,1);


                p = 1;

            }
            else if (maskplayer[0, 2] == false)
            {
                PonerCarta("guerrero1", maskplayer, 0, 2,1);


                p = 1;

            }
            else if (maskplayer[0, 3] == false)
            {
                PonerCarta("guerrero2", maskplayer, 0, 3,1);


                p = 1;

            }
            else if (maskplayer[2, 1] == false)
            {
                PonerCarta("distancia0", maskplayer, 2, 1,1);


                p = 3;

            }
            else if (maskplayer[2, 2] == false)
            {
                PonerCarta("distancia1", maskplayer, 2, 2, 1);


                p = 3;

            }
            else if (maskplayer[2, 3] == false)
            {
                PonerCarta("distancia2", maskplayer, 2, 3, 1);

                p = 3;
            }
            else if (maskplayer[1, 1] == false)
            {
                PonerCarta("asedio0", maskplayer, 1, 1, 1);


                p = 2;
            }
            else if (maskplayer[1, 2] == false)
            {
                PonerCarta("asedio1", maskplayer, 1, 2, 1);


                p = 2;
            }
            else if (maskplayer[1, 3] == false)
            {
                PonerCarta("asedio2", maskplayer, 1, 3, 1);


                p = 2;
            }



            if (p == 1)
            {
                PowerPoints.addPointToWarrior1(cartaactual.GetComponent<Card>().power);
            }
            else if (p == 2)
            {
                PowerPoints.addPointToasedio1(cartaactual.GetComponent<Card>().power);

            }
            else if (p == 3)
            {
                PowerPoints.addPointTodistance1(cartaactual.GetComponent<Card>().power);

            }
            PowerPoints.Actualizar1();
            //----------------Parathux

            if (cartaactual.GetComponent<Card>().name == "parathux")
            {
                int cardlen = 0;
                for (int i = 0; i < mask.GetLength(0); i++)
                {
                    for (int j = 0; j < mask.GetLength(1); j++)
                    {
                        if (i == 0 && j == 0 || i == 1 && j == 0 || i == 2 && j == 0 || i == 0 && j == 4 || i == 1 && j == 4 || i == 2 && j == 4)
                        {
                            continue;
                        }
                        if (mask[i, j])
                        {
                            cardlen += 1;
                        }
                        else
                        {
                            continue;
                        }

                    }
                }

                

               
                PowerPoints.addPointotal1(5*cardlen);
                PowerPoints.Actualizar1();
            }
            //---------------Overhix
            if (cartaactual.GetComponent<Card>().name == "overhik")
            {
              
                
                PowerPoints.addPointotal2(-20);
                PowerPoints.Actualizar2();
            }

            //---------------Duvenik
            if (cartaactual.GetComponent<Card>().name == "duvenik")
            {
               
                int total = cementerio1 + cementerio2;

                PowerPoints.addPointotal1(5 * total);
                PowerPoints.addPointotal2(-5*total);
                PowerPoints.Actualizar1();
                PowerPoints.Actualizar2();
            }

        }//------------------------------------------------------------------------silver
        else if (cartaactual.GetComponent<Card>().type == "silver")
        {
            if (maskplayer[0, 1] == false)
            {
                PonerCarta("guerrero0", maskplayer, 0, 1, 1);
                p = 1;

            }
            else if (maskplayer[0, 2] == false)
            {
                PonerCarta("guerrero1", maskplayer, 0, 2, 1);
                p = 1;

            }
            else if (maskplayer[0, 3] == false)
            {
                PonerCarta("guerrero2", maskplayer, 0, 3, 1);
                p = 1;

            }
            else if (maskplayer[2, 1] == false)
            {
                PonerCarta("distancia0", maskplayer, 2, 1, 1);
                p = 3;

            }
            else if (maskplayer[2, 2] == false)
            {
                PonerCarta("distancia1", maskplayer, 2, 2, 1);
                p = 3;

            }
            else if (maskplayer[2, 3] == false)
            {
                PonerCarta("distancia2", maskplayer, 2, 3, 1);
                p = 3;
            }
            else if (maskplayer[1, 1] == false)
            {
                PonerCarta("asedio0", maskplayer, 1, 1, 1);
                p = 2;
            }
            else if (maskplayer[1, 2] == false)
            {
                PonerCarta("asedio1", maskplayer, 1, 2, 1);
                p = 2;
            }
            else if (maskplayer[1, 3] == false)
            {
                PonerCarta("asedio2", maskplayer, 1, 3, 1);
                p = 2;
            }
            if (p == 1)
            {
                PowerPoints.addPointToWarrior1(cartaactual.GetComponent<Card>().power);
            }
            else if (p == 2)
            {
                PowerPoints.addPointToasedio1(cartaactual.GetComponent<Card>().power);

            }
            else if (p == 3)
            {
                PowerPoints.addPointTodistance1(cartaactual.GetComponent<Card>().power);

            }
            PowerPoints.Actualizar2();

            //------hombre lobo
            if (cartaactual.GetComponent<Card>().name == "hombre lobo")
            {
                PowerPoints.addPointotal2(-5);
                PowerPoints.Actualizar2();
            }

            //------sacerdote dragon 
            if (cartaactual.GetComponent<Card>().name == "sacerdote dragon")
            {
                //se anade 2 puntos por cada carta que tengas en la fila asedio
                if (maskplayer[1, 1] == true)
                {
                    PowerPoints.addPointotal1(2);

                }
                if (maskplayer[1, 2] == true)
                {
                    PowerPoints.addPointotal1(2);


                }
                if (maskplayer[1, 3] == true)
                {
                    PowerPoints.addPointotal1(2);

                }

                PowerPoints.Actualizar1();

            }
            //-----serana
            if (cartaactual.GetComponent<Card>().name == "serana")
            {
                PowerPoints.addPointotal2(-2 * cementerio1 );
                PowerPoints.Actualizar2();
            }
            //-----talos
            if (cartaactual.GetComponent<Card>().name == "talos")
            {
                if (totaldecartas2 != 0)
                {
                    //cambiamos la cantidad de puntos del total del enemigo y por el promedio
                    PowerPoints.allCount2 = 0;
                    int promedio = totalpower2 / totaldecartas2;
                    PowerPoints.addPointotal2(promedio);
                    PowerPoints.Actualizar2();
                }

            }
            //-----ulfric
            if (cartaactual.GetComponent<Card>().name == "ulfric")
            {
                
             
                if (mask[0, 1] == true)
                {
                   
                    PowerPoints.addPointToWarrior2(-1);

                }
                if (mask[0, 2] == true)
                {
                    
                    PowerPoints.addPointToWarrior2(-1);

                }
                if (mask[0, 3] == true)
                {
                    PowerPoints.addPointToWarrior2(-1);
                }
                 
                PowerPoints.Actualizar2();

            }
            //---vampiro
            if (cartaactual.GetComponent<Card>().name == "vampiro")
            {
                

                int MayorFila = Math.Max(PowerPoints.filaa2, Math.Max(PowerPoints.filad2, PowerPoints.filag2));

                
                    if (MayorFila == PowerPoints.filaa2)
                    {
                        //obtener los hijos tipo asedio

                        foreach (Transform child in GameObject.Find("CardsTotal").transform)
                        {
                            if (child.GetComponent<Card>().type == "asedio")
                            {
                                Destroy(child.gameObject);
                            }
                        }
                        maskplayer[1, 1] = false;
                        maskplayer[1, 2] = false;
                        maskplayer[1, 3] = false;
                        mask[1, 1] = false;
                        mask[1, 2] = false;
                        mask[1, 3] = false;
                        Debug.Log("asediooooooooooooooooo");
                        PowerPoints.addPointotal2(-PowerPoints.filaa2);
                        PowerPoints.filaa2 = 0;
                        PowerPoints.Actualizar2();
                    }
                    else if (MayorFila == PowerPoints.filad2)
                    {
                        //obtener los hijos tipo distancia

                        foreach (Transform child in GameObject.Find("CardsTotal").transform)
                        {
                            if (child.GetComponent<Card>().type == "distancia")
                            {
                                Destroy(child.gameObject);
                            }
                        }
                        maskplayer[2, 1] = false;
                        maskplayer[2, 2] = false;
                        maskplayer[2, 3] = false;
                        mask[2, 1] = false;
                        mask[2, 2] = false;
                        mask[2, 3] = false;
                        Debug.Log("distanciaaaaaaaaaaaaaaaaaaaaaa");

                        PowerPoints.addPointotal2(-PowerPoints.filad2);
                        PowerPoints.filad2 = 0;
                        PowerPoints.Actualizar2();
                        
                    }
                    else if (MayorFila == PowerPoints.filag2)
                    {
                        //obtener los hijos tipo guerrero

                        foreach (Transform child in GameObject.Find("CardsTotal").transform)
                        {
                            if (child.GetComponent<Card>().type == "guerrero")
                            {
                                Destroy(child.gameObject);
                            }
                        }
                        maskplayer[0, 1] = false;
                        maskplayer[0, 2] = false;
                        maskplayer[0, 3] = false;
                        mask[0, 1] = false;
                        mask[0, 2] = false;
                        mask[0, 3] = false;
                        Debug.Log("guerreroooooooooooooooo");

                        PowerPoints.addPointotal1(-PowerPoints.filag2);
                        PowerPoints.filag2 = 0;
                        PowerPoints.Actualizar2();
                       
                    }

                


                //PowerPoints.addPointotal1(5);
                //PowerPoints.Actualizar1();
                //PowerPoints.addPointotal2(-5);
                //PowerPoints.Actualizar2();
            }



        }
        //----------------------------------------------------------------despeje
        if (cartaactual.GetComponent<Card>().type == "despeje")
        {
            if (mask[0, 0])
            {
                //darle valor original a guerrero 1 y guerrero 2
                //g1
                PowerPoints.addPointotal1(-PowerPoints.filag1);
                PowerPoints.filag1 = 0;

                PowerPoints.addPointToWarrior1(despejeg0);
                PowerPoints.addPointToWarrior1(despejeg1);
                PowerPoints.addPointToWarrior1(despejeg2);
                PowerPoints.Actualizar1();
                //g2
                PowerPoints.addPointotal2(-PowerPoints.filag2);
                PowerPoints.filag2 = 0;

                PowerPoints.addPointToWarrior1(despejeg10);
                PowerPoints.addPointToWarrior1(despejeg11);
                PowerPoints.addPointToWarrior1(despejeg12);
                PowerPoints.Actualizar2();
                cementerio2 += 1;
                mask[0, 0] = false;
                GameObject g = GameObject.Find("clima0");
                g.SetActive(false);
            }
            else if (mask[1,0]) 
            {
                //darle valor original a asedio 1 asedio 2
                //a1
                PowerPoints.addPointotal1(-PowerPoints.filaa1);
                PowerPoints.filaa1 = 0;

                PowerPoints.addPointToasedio1(despejea0);
                PowerPoints.addPointToasedio1(despejea1);
                PowerPoints.addPointToasedio1(despejea2);
                PowerPoints.Actualizar1();
                //a2
                PowerPoints.addPointotal2(-PowerPoints.filaa2);
                PowerPoints.filaa2 = 0;

                PowerPoints.addPointToasedio1(despejea10);
                PowerPoints.addPointToasedio1(despejea11);
                PowerPoints.addPointToasedio1(despejea12);
                PowerPoints.Actualizar2();
                cementerio2 += 1;
                mask[1,0] = false;  
            }
            else if (mask[2,0])
            {
                // darle valor original a distancia 1 y distancia 2
                //d1
                PowerPoints.addPointotal1(-PowerPoints.filad1);
                PowerPoints.filad1 = 0;

                PowerPoints.addPointTodistance1(despejed0);
                PowerPoints.addPointTodistance1(despejed1);
                PowerPoints.addPointTodistance1(despejed2);
                PowerPoints.Actualizar1();
                //d2
                PowerPoints.addPointotal2(-PowerPoints.filad2);
                PowerPoints.filad2 = 0;

                PowerPoints.addPointTodistance1(despejed10);
                PowerPoints.addPointTodistance1(despejed11);
                PowerPoints.addPointTodistance1(despejed12);
                PowerPoints.Actualizar2();
                cementerio2 += 1;
                mask[2,0] = false;  
            }  


            Destroy(cartaactual);
            cementerio1 += 1;
        }
        
     }
        //--------------------------------------------------------------------------------------------------fin




        //---------------------------------------------------------------------------------------------poner para player 2
        void Poner2()
        {//--------------------------------------------------------------------asedio
            if (cartaactual.GetComponent<Card>().type == "asedio")
            {
                if (mask[1, 1] == false)
                {
                    PonerCarta("asedio10", mask, 1, 1,2);
                }
                else if (mask[1, 2] == false)
                {
                    PonerCarta("asedio11", mask, 1, 2,2);
                }
                else if (mask[1, 3] == false)
                {
                    PonerCarta("asedio12", mask, 1, 3,2);

                }
                //puntos
                PowerPoints.addPointToasedio2(cartaactual.GetComponent<Card>().power);
                PowerPoints.Actualizar2();
            }
        //--------------------------------------------------------------------------clima
        else if (cartaactual.GetComponent<Card>().type == "clima")
        {
            if (mask[0, 0] == false)
            {
                
                PonerCarta("clima10", mask, 0, 0,2);
                PowerPoints.addPointotal2(-PowerPoints.filag2);
                PowerPoints.filag2 = 0;

                //fila guererero de ambos jugadores//jugador 2
                if (mask[0, 1])
                {
                    Clima(cartaactual,despejeg10,"g2");

                  
                }
                if (mask[0, 2])
                {
                    Clima(cartaactual, despejeg11, "g2");
                  
                }
                if (mask[0, 3])
                {
                    Clima(cartaactual, despejeg12, "g2");
                }
                PowerPoints.Actualizar2();
                //jugador 1
                PowerPoints.addPointotal1(-PowerPoints.filag1);
                PowerPoints.filag1 = 0;
                if (maskplayer[0, 1])
                {
                    Clima(cartaactual,despejeg0,"g1");
                  
                }
                if (maskplayer[0, 2])
                {
                    Clima(cartaactual,despejeg1,"g1");
                 
                }
                if (maskplayer[0, 3])
                {
                    Clima(cartaactual,despejeg2,"g1");
                }
                PowerPoints.Actualizar1();



            }
            else if (mask[1, 0] == false)
            {
                PonerCarta("clima11", mask, 1, 0,2);

                //j2
                PowerPoints.addPointotal2(-PowerPoints.filaa2);
                PowerPoints.filaa2 = 0;

                if (mask[1, 1])
                {
                    Clima(cartaactual,despejea10,"a2");
                    
                }
                if (mask[1, 2])
                {
                    Clima(cartaactual,despejea11,"a2");
                   

                }
                if (mask[1, 3])
                {
                    Clima(cartaactual,despejea12,"a2");
                    

                }
                PowerPoints.Actualizar2();


                //fila asedio de ambos jugadores // j 1
                PowerPoints.addPointotal1(-PowerPoints.filaa1);
                PowerPoints.filaa1 = 0;


                

                if (maskplayer[1, 1])
                {
                    Clima(cartaactual,despejea0,"a1");
                  
                }
                if (maskplayer[1, 2])
                {
                    Clima(cartaactual, despejea1, "a1");
                   

                }
                if (maskplayer[1, 3])
                {
                    Clima(cartaactual, despejea2, "a1");

                    
                }
                PowerPoints.Actualizar1();
               
            }
            else if (maskplayer[2, 0] == false)
            {
                PonerCarta("clima12", mask, 2, 0,2);

                //j2
                PowerPoints.addPointotal2(-PowerPoints.filad2);
                PowerPoints.filad2 = 0;

                if (mask[2, 1])
                {
                    Clima(cartaactual, despejed10, "d2");

                    
                }
                if (mask[2, 2])
                {
                    Clima(cartaactual, despejed11, "d2");

                   
                }
                if (mask[2, 3])
                {
                    Clima(cartaactual, despejed12, "d2");

                   

                }
                PowerPoints.Actualizar2();

                PowerPoints.addPointotal1(-PowerPoints.filad1);
                PowerPoints.filad1 = 0;

                //fila distancia de ambos jugadores//j1
                if (maskplayer[2, 1])
                {
                    Clima(cartaactual, despejed0, "d1");

                  
                }
                if (maskplayer[2, 2])
                {
                    Clima(cartaactual, despejed1, "d1");

                }
                if (maskplayer[2, 3])
                {
                    Clima(cartaactual, despejed2, "d1");

                   

                }
                PowerPoints.Actualizar1();
               
            }


        }
        //-----------------------------------------------------------------------------aumento
        else if (cartaactual.GetComponent<Card>().type == "aumento")
            {
                if (mask[0, 4] == false)
                {
                    PonerCarta("aumento10", mask, 0, 4,2);
                    PowerPoints.addPointToWarrior2(PowerPoints.filag2 * cartaactual.GetComponent<Card>().power);
                    PowerPoints.Actualizar2();
                }
                else if (mask[1, 4] == false)
                {
                    PonerCarta("aumento11", mask, 1, 4,2);
                    PowerPoints.addPointToasedio2(PowerPoints.filaa2 * cartaactual.GetComponent<Card>().power);
                    PowerPoints.Actualizar2();

                }
                else if (mask[2, 4] == false)
                {
                    PonerCarta("aumento12", mask, 2, 4,2);
                    PowerPoints.addPointTodistance2(PowerPoints.filad2 * cartaactual.GetComponent<Card>().power);
                    PowerPoints.Actualizar2();

                }
            }
            //------------------------------------------------------------------------guerrero
            else if (cartaactual.GetComponent<Card>().type == "guerrero")
            {
                if (mask[0, 1] == false)
                {
                    PonerCarta("guerrero10", mask, 0, 1,2);


                    
                }
                else if (mask[0, 2] == false)
                {
                    PonerCarta("guerrero11", mask, 0, 2, 2);

                }
                else if (mask[0, 3] == false)
                {
                    PonerCarta("guerrero12", mask, 0, 3, 2);

                }
                //puntos
                PowerPoints.addPointToWarrior2(cartaactual.GetComponent<Card>().power);
            PowerPoints.Actualizar2();

            }//-----------------------------------------------------------------------------------------------------distancia

            else if (cartaactual.GetComponent<Card>().type == "distancia")
            {
                if (mask[2, 1] == false)
                {
                    PonerCarta("distancia10", mask, 2, 1, 2);


                }
                else if (mask[2, 2] == false)
                {
                    PonerCarta("distancia11", mask, 2, 2, 2);


                }
                else if (mask[2, 3] == false)
                {
                    PonerCarta("distancia12", mask, 2, 3, 2);


                }
                //puntos 
                PowerPoints.addPointTodistance2(cartaactual.GetComponent<Card>().power);
                PowerPoints.Actualizar2();
                
            }//--------------------------------------------------------------------------------trampa
            else if (cartaactual.GetComponent<Card>().type == "trampa")
            {
                if (maskplayer[0, 1] == false)
                {
                    PonerCarta("guerrero0", maskplayer, 0, 1,1);


                    p = 1;

                }
                else if (maskplayer[0, 2] == false)
                {
                    PonerCarta("guerrero1", maskplayer, 0, 2, 1);


                    p = 1;

                }
                else if (maskplayer[0, 3] == false)
                {
                    PonerCarta("guerrero2", maskplayer, 0, 3,1);


                    p = 1;

                }
                else if (maskplayer[2, 1] == false)
                {
                    PonerCarta("distancia0", maskplayer, 2, 1,1);


                    p = 3;

                }
                else if (maskplayer[2, 2] == false)
                {
                    PonerCarta("distancia1", maskplayer, 2, 2,1);


                    p = 3;

                }
                else if (maskplayer[2, 3] == false)
                {
                    PonerCarta("distancia2", maskplayer, 2, 3,1);

                    p = 3;
                }
                else if (maskplayer[1, 1] == false)
                {
                    PonerCarta("asedio0", maskplayer, 1, 1,1);


                    p = 2;
                }
                else if (maskplayer[1, 2] == false)
                {
                    PonerCarta("asedio1", maskplayer, 1, 2,1);


                    p = 2;
                }
                else if (maskplayer[1, 3] == false)
                {
                    PonerCarta("asedio2", maskplayer, 1, 3,1);


                    p = 2;
                }
            }//-------------------------------------------------------------------heroe
            else if (cartaactual.GetComponent<Card>().type == "heroe")
            {
                if (mask[0, 1] == false)
                {
                    PonerCarta("guerrero10", mask, 0, 1,2);


                    p = 1;

                }
                else if (mask[0, 2] == false)
                {
                    PonerCarta("guerrero11", mask, 0, 2, 2);


                    p = 1;

                }
                else if (mask[0, 3] == false)
                {
                    PonerCarta("guerrero12", mask, 0, 3, 2);


                    p = 1;

                }
                else if (mask[2, 1] == false)
                {
                    PonerCarta("distancia10", mask, 2, 1,2);


                    p = 3;

                }
                else if (mask[2, 2] == false)
                {
                    PonerCarta("distancia11", mask, 2, 2, 2);


                    p = 3;

                }
                else if (mask[2, 3] == false)
                {
                    PonerCarta("distancia12", mask, 2, 3, 2);

                    p = 3;
                }
                else if (mask[1, 1] == false)
                {
                    PonerCarta("asedio10", mask, 1, 1, 2);


                    p = 2;
                }
                else if (mask[1, 2] == false)
                {
                    PonerCarta("asedio11", mask, 1, 2, 2);


                    p = 2;
                }
                else if (mask[1, 3] == false)
                {
                    PonerCarta("asedio12", mask, 1, 3, 2);


                    p = 2;
                }

                if (p == 1)
                {
                    PowerPoints.addPointToWarrior2(cartaactual.GetComponent<Card>().power);
                }
                else if (p == 2)
                {
                    PowerPoints.addPointToasedio2(cartaactual.GetComponent<Card>().power);

                }
                else if (p == 3)
                {
                    PowerPoints.addPointTodistance2(cartaactual.GetComponent<Card>().power);

                }
            PowerPoints.Actualizar2();
                //----------------Parathux

                if (cartaactual.GetComponent<Card>().name == "parathux")
                {
                    int cardlen = 0;
                    for (int i = 0; i < maskplayer.GetLength(0); i++)
                    {
                        for (int j = 0; j < maskplayer.GetLength(1); j++)
                        {
                            if (i==0 && j == 0 || i == 1 && j ==0 || i == 2 && j == 0 || i == 0 && j == 4 || i == 1 && j ==4 || i == 2 && j == 4)
                            {
                                continue;
                            }
                            if (maskplayer[i, j])
                            {
                                cardlen += 1;
                            }
                            else
                            {
                                continue;
                            }

                        }
                    }


                    PowerPoints.addPointotal2(5*cardlen);
                    PowerPoints.Actualizar2();
                }
                //---------------Overhix
                if (cartaactual.GetComponent<Card>().name == "overhik")
                {
                    PowerPoints.addPointotal1(-20);
                    PowerPoints.Actualizar1();
                }

                //---------------Duvenik
                if (cartaactual.GetComponent<Card>().name == "duvenik")
                {
                
                int total = cementerio1 + cementerio2;
                
                    PowerPoints.addPointotal2(5*total);
                    PowerPoints.addPointotal1(-5*total);
                    PowerPoints.Actualizar1();
                    PowerPoints.Actualizar2();
                }

            }//------------------------------------------------------------------------silver
            else if (cartaactual.GetComponent<Card>().type == "silver")
            {
                if (mask[0, 1] == false)
                {
                    PonerCarta("guerrero10", mask, 0, 1, 2);


                    p = 1;

                }
                else if (mask[0, 2] == false)
                {
                    PonerCarta("guerrero11", mask, 0, 2, 2);


                    p = 1;

                }
                else if (mask[0, 3] == false)
                {
                    PonerCarta("guerrero12", mask, 0, 3, 2);


                    p = 1;

                }
                else if (mask[2, 1] == false)
                {
                    PonerCarta("distancia10", mask, 2, 1, 2);


                    p = 3;

                }
                else if (mask[2, 2] == false)
                {
                    PonerCarta("distancia11", mask, 2, 2, 2);


                    p = 3;

                }
                else if (mask[2, 3] == false)
                {
                    PonerCarta("distancia12", mask, 2, 3, 2);

                    p = 3;
                }
                else if (mask[1, 1] == false)
                {
                    PonerCarta("asedio10", mask, 1, 1, 2);


                    p = 2;
                }
                else if (mask[1, 2] == false)
                {
                    PonerCarta("asedio11", mask, 1, 2, 2);


                    p = 2;
                }
                else if (mask[1, 3] == false)
                {
                    PonerCarta("asedio12", mask, 1, 3, 2);


                    p = 2;
                }

                if (p == 1)
                {
                    PowerPoints.addPointToWarrior2(cartaactual.GetComponent<Card>().power);
                }
                else if (p == 2)
                {
                    PowerPoints.addPointToasedio2(cartaactual.GetComponent<Card>().power);

                }
                else if (p == 3)
                {
                    PowerPoints.addPointTodistance2(cartaactual.GetComponent<Card>().power);

                }
                    PowerPoints.Actualizar2();

                //------hombre lobo
                if (cartaactual.GetComponent<Card>().name == "hombre lobo")
                {
                    PowerPoints.addPointotal1(-5);
                    PowerPoints.Actualizar1();
                    
                }

                //------sacerdote dragon 
                if (cartaactual.GetComponent<Card>().name == "sacerdote dragon")
                {
                  
                            if (mask[1, 1] == true)
                            {
                              PowerPoints.addPointotal2(2);

                            }
                            if (mask[1, 2] == true)
                            {
                              PowerPoints.addPointotal2(2);

                            }
                            if (mask[1, 3] == true)
                            {
                               PowerPoints.addPointotal2(2);

                            }

                     PowerPoints.Actualizar2();

                }
                //-----serana
                if (cartaactual.GetComponent<Card>().name == "serana")
                {
                    PowerPoints.addPointotal1(-2 * cementerio2);
                    PowerPoints.Actualizar1();
                }
                //-----talos
                if (cartaactual.GetComponent<Card>().name == "talos")
                {


                if (totaldecartas1 != 0)
                {
                    PowerPoints.allCount1 = 0;
                    int promedio = totalpower1 / totaldecartas1;
                    PowerPoints.addPointotal1(promedio);
                    PowerPoints.Actualizar1();
                }

            }
                //-----ulfric
                if (cartaactual.GetComponent<Card>().name == "ulfric")
                {


                   
                            if (maskplayer[0, 1] == true)
                            {
                               PowerPoints.addPointToWarrior1(-1);

                            }
                            if (maskplayer[0, 2] == true)
                            {
                               PowerPoints.addPointToWarrior1(-1);

                            }
                if (maskplayer[0, 2] == true)
                            {
                                PowerPoints.addPointToWarrior1(-1);
                            }
                   
                    PowerPoints.Actualizar1();

                }
                //---vampiro
                if (cartaactual.GetComponent<Card>().name == "vampiro")
                {
             

                int MayorFila = Math.Max(PowerPoints.filaa1, Math.Max(PowerPoints.filad1, PowerPoints.filag1));

               
                    if (MayorFila == PowerPoints.filaa1)
                    {
                        //obtener los hijos tipo asedio

                        foreach (Transform child in GameObject.Find("CardsTotal").transform)
                        {
                            if (child.GetComponent<Card>().type == "asedio")
                            {
                                Destroy(child.gameObject);
                            }
                        }
                        maskplayer[1,1] = false;
                        maskplayer[1, 2] = false;
                        maskplayer[1, 3] = false;
                        mask[1, 1] = false;
                        mask[1, 2] = false;
                        mask[1, 3] = false;

                        PowerPoints.addPointotal1(-PowerPoints.filaa1);
                        PowerPoints.filaa1 = 0;

                        PowerPoints.Actualizar1();
                        
                    }
                    else if (MayorFila == PowerPoints.filad1)
                    {
                        //obtener los hijos tipo distancia

                        foreach (Transform child in GameObject.Find("CardsTotal").transform)
                        {
                            if (child.GetComponent<Card>().type == "distancia")
                            {
                                Destroy(child.gameObject);
                            }
                        }
                        maskplayer[2, 1] = false;
                        maskplayer[2, 2] = false;
                        maskplayer[2, 3] = false;
                        mask[2, 1] = false;
                        mask[2, 2] = false;
                        mask[2, 3] = false;

                        PowerPoints.addPointotal1(-PowerPoints.filad1);
                        PowerPoints.filad1 = 0;

                        PowerPoints.Actualizar1();
                       
                    }
                    else if (MayorFila == PowerPoints.filag1)
                    {
                        //obtener los hijos tipo guerrero

                        foreach (Transform child in GameObject.Find("CardsTotal").transform)
                        {
                            if (child.GetComponent<Card>().type == "guerrero")
                            {
                                Destroy(child.gameObject);
                            }
                        }
                        maskplayer[0, 1] = false;
                        maskplayer[0, 2] = false;
                        maskplayer[0, 3] = false;
                        mask[0, 1] = false;
                        mask[0, 2] = false;
                        mask[0, 3] = false;

                        PowerPoints.addPointotal1(-PowerPoints.filag1);
                        PowerPoints.filag1 = 0;
                        PowerPoints.Actualizar1();
                    }

                



                    //PowerPoints.addPointotal2(5);
                    //PowerPoints.Actualizar2();
                    //PowerPoints.addPointotal1(-5);
                    //PowerPoints.Actualizar1();
                  
                }

            }

        //----------------------------------------------------------------despeje
        if (cartaactual.GetComponent<Card>().type == "despeje")
        {
            if (maskplayer[0, 0])
            {
                //darle valor original a guerrero 1 y guerrero 2
                //g1
                PowerPoints.addPointotal1(-PowerPoints.filag1);
                PowerPoints.filag1 = 0;

                PowerPoints.addPointToWarrior1(despejeg0);
                PowerPoints.addPointToWarrior1(despejeg1);
                PowerPoints.addPointToWarrior1(despejeg2);
                PowerPoints.Actualizar1();
                //g2
                PowerPoints.addPointotal2(-PowerPoints.filag2);
                PowerPoints.filag2 = 0;

                PowerPoints.addPointToWarrior1(despejeg10);
                PowerPoints.addPointToWarrior1(despejeg11);
                PowerPoints.addPointToWarrior1(despejeg12);
                PowerPoints.Actualizar2();
                cementerio1 += 1;
                maskplayer[0, 0] = false;
                GameObject g = GameObject.Find("clima10");
                g.SetActive(false);
            }
            else if (maskplayer[1, 0])
            {
                //darle valor original a asedio 1 asedio 2
                //a1
                PowerPoints.addPointotal1(-PowerPoints.filaa1);
                PowerPoints.filaa1 = 0;

                PowerPoints.addPointToasedio1(despejea0);
                PowerPoints.addPointToasedio1(despejea1);
                PowerPoints.addPointToasedio1(despejea2);
                PowerPoints.Actualizar1();
                //a2
                PowerPoints.addPointotal2(-PowerPoints.filaa2);
                PowerPoints.filaa2 = 0;

                PowerPoints.addPointToasedio1(despejea10);
                PowerPoints.addPointToasedio1(despejea11);
                PowerPoints.addPointToasedio1(despejea12);
                PowerPoints.Actualizar2();
                cementerio1 += 1;
                maskplayer[1, 0] = false;
            }
            else if (mask[2, 0])
            {
                // darle valor original a distancia 1 y distancia 2
                //d1
                PowerPoints.addPointotal1(-PowerPoints.filad1);
                PowerPoints.filad1 = 0;

                PowerPoints.addPointTodistance1(despejed0);
                PowerPoints.addPointTodistance1(despejed1);
                PowerPoints.addPointTodistance1(despejed2);
                PowerPoints.Actualizar1();
                //d2
                PowerPoints.addPointotal2(-PowerPoints.filad2);
                PowerPoints.filad2 = 0;

                PowerPoints.addPointTodistance1(despejed10);
                PowerPoints.addPointTodistance1(despejed11);
                PowerPoints.addPointTodistance1(despejed12);
                PowerPoints.Actualizar2();
                cementerio1 += 1;
                maskplayer[2, 0] = false;

            }


            Destroy(cartaactual);
            cementerio2 += 1;
        }



    }


    public void OnMouseDown()
        {
            if (TurnSystem.IsPlayer1Turn())
            {
                Debug.Log("on");
                cartaactual = gameObject;
                Poner();
              
            }
            else if (TurnSystem.IsPlayer2Turn())
            {
                Debug.Log("on");
                cartaactual = gameObject;
                Poner2();

               
            }

        }
            public static void Vaciar() 
            {
                //ver cuantas cartas van para el cementerio 1
                for (int i = 0; i < maskplayer.GetLength(0); i++)
                {
                    for (int j = 0; j < maskplayer.GetLength(1); j++)
                    {
                        if (maskplayer[i,j])
                        {
                            cementerio1 += 1;
                            maskplayer[i, j] = false;
                        }
                    }
                }
                //ver cuantas cartas van para el cementerio 2
                for (int i = 0; i < mask.GetLength(0); i++)
                {
                    for (int j = 0; j < mask.GetLength(1); j++)
                    {
                        if (mask[i, j])
                        {
                            cementerio2 += 1;
                            mask[i, j] = false;

                        }
                    }
                }
                //destruir todas las cartas sobre la mesa
                foreach (Transform child in GameObject.Find("CardsTotal").transform) 
                {
                    Destroy(child.gameObject);
                }

            }


    public void Clima(GameObject carta ,int puntos,string fila ) 
    {
        if (carta.GetComponent<Card>().type == "heroe")
        {
            return;
        }
        else 
        {
                puntos = carta.GetComponent<Card>().power;

            if (fila == "g1")
            {
                
                PowerPoints.addPointToWarrior1(-cartaactual.GetComponent<Card>().power);
                carta.GetComponent<Card>().power = 1;
                PowerPoints.addPointToWarrior1(1);
            }
            else if (fila == "g2")
            {
                
                PowerPoints.addPointToWarrior2(-cartaactual.GetComponent<Card>().power);
                carta.GetComponent<Card>().power = 1;
                PowerPoints.addPointToWarrior2(1);
            }
            else if (fila == "a1")
            {
                PowerPoints.addPointToasedio1(-cartaactual.GetComponent<Card>().power);
                carta.GetComponent<Card>().power = 1;
                PowerPoints.addPointToasedio1(1);
            }
            else if (fila == "a2")
            {
                PowerPoints.addPointToasedio2(-cartaactual.GetComponent<Card>().power);
                carta.GetComponent<Card>().power = 1;
                PowerPoints.addPointToasedio2(1);
            }
            else if (fila == "d1")
            {
                PowerPoints.addPointTodistance1(-cartaactual.GetComponent<Card>().power);
                carta.GetComponent<Card>().power = 1;
                PowerPoints.addPointTodistance1(1);
            }
            else if (fila == "d2")
            {
                PowerPoints.addPointTodistance2(-cartaactual.GetComponent<Card>().power);
                carta.GetComponent<Card>().power = 1;
                PowerPoints.addPointTodistance2(1);
            }

        }
    }
}