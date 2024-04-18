using Assets;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Invocar : MonoBehaviour
{
    public static GameObject cartaactual;


    static bool[,] maskplayer = new bool[3, 5];
    static bool[,] mask = new bool[3, 5];
    Efectos efectos = new Efectos();
    public newgameboard tablero;
    int totaldecartas1 = 0;
    int totalpower1 = 0;
    int totaldecartas2 = 0;
    int totalpower2 = 0;
    int p = 0;
    TurnSystem turn;
    GameObject totalcards;
    public Deck deck;
    GameManager manager;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        manager.GanarJuego();
    }

    public void PonerCarta(string cellName, bool[,] matriz, int cellX, int cellY)
    {
        Debug.Log("Tablero encontrado");
        totalcards = GameObject.Find("CardsTotal");
        if (totalcards != null)
        {
            Transform t = totalcards.transform;
            cartaactual.transform.SetParent(t, false);

        }
       

        GameObject pos = GameObject.Find(cellName);
        Vector3 v = pos.transform.position;
        cartaactual.transform.position = new Vector3(v.x, v.y, 1f);
        matriz[cellX, cellY] = true;
    
        if (matriz == maskplayer)
        {
            totaldecartas1 += 1;
            totalpower1 += cartaactual.GetComponent<Card>().power;

            deck.Hand1.Remove(cartaactual);
        }
        else
        {
            totaldecartas2 += 1;
            totalpower2 += cartaactual.GetComponent<Card>().power;
            deck.Hand2.Remove(cartaactual);

        }


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
                PonerCarta("asedio0", maskplayer, 1, 1);
            }
            else if (maskplayer[1, 2] == false)
            {
                PonerCarta("asedio1", maskplayer, 1, 2);
            }
            else if (maskplayer[1, 3] == false)
            {
                PonerCarta("asedio2", maskplayer, 1, 3);

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
                PonerCarta("clima0", maskplayer, 0, 0);
                PowerPoints.addPointotal1(-PowerPoints.filag1);
                PowerPoints.filag1 = 0;
                    //fila guererero de ambos jugadores//jugador 1
                    if (maskplayer[0, 1])
                    {
                        int despejeg0 = cartaactual.GetComponent<Card>().power;
                        cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior1(1);
                    }
                    if (maskplayer[0, 2])
                    {
                        int despejeg1 = cartaactual.GetComponent<Card>().power;
                        cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior1(1);
                    }
                    if (maskplayer[0, 3])
                    {
                        int despejeg2 = cartaactual.GetComponent<Card>().power;
                        cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior1(1);
                    }
                PowerPoints.Actualizar1();
                //jugador 2
                PowerPoints.addPointotal2(-PowerPoints.filag2);
                PowerPoints.filag2 = 0;
                if (mask[0, 1])
                {
                    int despejeg10 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior2(1);
                }
                if (mask[0, 2])
                {
                    int despejeg11 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior2(1);
                }
                if (mask[0, 3])
                {
                    int despejeg12 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior2(1);
                }
                PowerPoints.Actualizar2();




            }
            else if (maskplayer[1, 0] == false)
            {
                PonerCarta("clima1", maskplayer, 1, 0);
                PowerPoints.addPointotal1(-PowerPoints.filaa1);
                PowerPoints.filaa1 = 0;


                //fila asedio de ambos jugadores // j 1

                if (maskplayer[1, 1])
                    {
                        int despejea0 = cartaactual.GetComponent<Card>().power;
                        cartaactual.GetComponent<Card>().power = 1;
                        PowerPoints.addPointToasedio1(1);
                    }
                    if (maskplayer[1, 2])
                    {
                        int despejea1 = cartaactual.GetComponent<Card>().power;
                        cartaactual.GetComponent<Card>().power = 1;
                        PowerPoints.addPointToasedio1(1);

                    }
                    if (maskplayer[1, 3])
                    {
                        int despejea2 = cartaactual.GetComponent<Card>().power;
                        cartaactual.GetComponent<Card>().power = 1;
                        PowerPoints.addPointToasedio1(1);

                    }
                    PowerPoints.Actualizar1();
                //j2
                PowerPoints.addPointotal2(-PowerPoints.filaa2);
                PowerPoints.filaa2 = 0;

                if (mask[1, 1])
                {
                    int despejea10 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToasedio2(1);
                }
                if (mask[1, 2])
                {
                    int despejea11 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToasedio2(1);

                }
                if (mask[1, 3])
                {
                    int despejea2 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToasedio2(1);

                }
                PowerPoints.Actualizar2();



            }
            else if (maskplayer[2, 0] == false)
            {
                PonerCarta("clima2", maskplayer, 2, 0);
                PowerPoints.addPointotal1(-PowerPoints.filad1);
                PowerPoints.filad1 = 0;
                
                //fila distancia de ambos jugadores//j1
                if (maskplayer[2, 1])
                {
                    int despejed0 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance1(1);
                }
                if (maskplayer[2, 2])
                {
                    int despejed1 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance1(1);
                }
                if (maskplayer[2, 3])
                {
                    int despejed2 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance2(1);

                }
                PowerPoints.Actualizar1();
                //j2
                PowerPoints.addPointotal2(-PowerPoints.filad2);
                PowerPoints.filad2 = 0;

                if (mask[2, 1])
                {
                    int despejed10 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance2(1);
                }
                if (mask[2, 2])
                {
                    int despejed11 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance2(1);
                }
                if (mask[2, 3])
                {
                    int despejed12 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance2(1);

                }
                PowerPoints.Actualizar2();



            }

            
        }
        //-----------------------------------------------------------------------------aumento
        else if (cartaactual.GetComponent<Card>().type == "aumento")
        {
            if (maskplayer[0, 4] == false)
            {
                PonerCarta("aumento0", maskplayer, 0, 4);
                PowerPoints.addPointToWarrior1(PowerPoints.filag1 * cartaactual.GetComponent<Card>().power);
                PowerPoints.Actualizar1();
            }
            else if (maskplayer[1, 4] == false)
            {
                PonerCarta("aumento1", maskplayer, 1, 4);
                PowerPoints.addPointToasedio1(PowerPoints.filaa1 * cartaactual.GetComponent<Card>().power);
                PowerPoints.Actualizar1();

            }
            else if (maskplayer[2, 4] == false)
            {
                PonerCarta("aumento2", maskplayer, 2, 4);
                PowerPoints.addPointTodistance1(PowerPoints.filad1 * cartaactual.GetComponent<Card>().power);
                PowerPoints.Actualizar1();

            }
        }
        //------------------------------------------------------------------------guerrero
        else if (cartaactual.GetComponent<Card>().type == "guerrero")
        {
            if (maskplayer[0, 1] == false)
            {
                PonerCarta("guerrero0", maskplayer, 0, 1);


                //PowerPoints.addPointToWarrior(cartaactual.GetComponent<Card>().power);
            }
            else if (maskplayer[0, 2] == false)
            {
                PonerCarta("guerrero1", maskplayer, 0, 2);

            }
            else if (maskplayer[0, 3] == false)
            {
                PonerCarta("guerrero2", maskplayer, 0, 3);

            }
            //puntos
            PowerPoints.addPointToWarrior1(cartaactual.GetComponent<Card>().power);
            PowerPoints.Actualizar1();

        }//-----------------------------------------------------------------------------------------------------distancia

        else if (cartaactual.GetComponent<Card>().type == "distancia")
        {
            if (maskplayer[2, 1] == false)
            {
                PonerCarta("distancia0", maskplayer, 2, 1);


            }
            else if (maskplayer[2, 2] == false)
            {
                PonerCarta("distancia1", maskplayer, 2, 2);


            }
            else if (maskplayer[2, 3] == false)
            {
                PonerCarta("distancia2", maskplayer, 2, 3);


            }
            //puntos
            PowerPoints.addPointTodistance1(cartaactual.GetComponent<Card>().power);
            PowerPoints.Actualizar1();

        }//--------------------------------------------------------------------------------trampa
        else if (cartaactual.GetComponent<Card>().type == "trampa")
        {
            if (mask[0, 1] == false)
            {
                PonerCarta("guerrero10", mask, 0, 1);


                p = 1;

            }
            else if (mask[0, 2] == false)
            {
                PonerCarta("guerrero11", mask, 0, 2);


                p = 1;

            }
            else if (mask[0, 3] == false)
            {
                PonerCarta("guerrero12", mask, 0, 3);


                p = 1;

            }
            else if (mask[2, 1] == false)
            {
                PonerCarta("distancia10", mask, 2, 1);


                p = 3;

            }
            else if (mask[2, 2] == false)
            {
                PonerCarta("distancia11", mask, 2, 2);


                p = 3;

            }
            else if (mask[2, 3] == false)
            {
                PonerCarta("distancia12", mask, 2, 3);

                p = 3;
            }
            else if (mask[1, 1] == false)
            {
                PonerCarta("asedio10", mask, 1, 1);


                p = 2;
            }
            else if (mask[1, 2] == false)
            {
                PonerCarta("asedio11", mask, 1, 2);


                p = 2;
            }
            else if (mask[1, 3] == false)
            {
                PonerCarta("asedio12", mask, 1, 3);


                p = 2;
            }
        }//-------------------------------------------------------------------heroe
        else if (cartaactual.GetComponent<Card>().type == "heroe")
        {
            if (maskplayer[0, 1] == false)
            {
                PonerCarta("guerrero0", maskplayer, 0, 1);


                p = 1;

            }
            else if (maskplayer[0, 2] == false)
            {
                PonerCarta("guerrero1", maskplayer, 0, 2);


                p = 1;

            }
            else if (maskplayer[0, 3] == false)
            {
                PonerCarta("guerrero2", maskplayer, 0, 3);


                p = 1;

            }
            else if (maskplayer[2, 1] == false)
            {
                PonerCarta("distancia0", maskplayer, 2, 1);


                p = 3;

            }
            else if (maskplayer[2, 2] == false)
            {
                PonerCarta("distancia1", maskplayer, 2, 2);


                p = 3;

            }
            else if (maskplayer[2, 3] == false)
            {
                PonerCarta("distancia2", maskplayer, 2, 3);

                p = 3;
            }
            else if (maskplayer[1, 1] == false)
            {
                PonerCarta("asedio0", maskplayer, 1, 1);


                p = 2;
            }
            else if (maskplayer[1, 2] == false)
            {
                PonerCarta("asedio1", maskplayer, 1, 2);


                p = 2;
            }
            else if (maskplayer[1, 3] == false)
            {
                PonerCarta("asedio2", maskplayer, 1, 3);


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
                        if (i == 0 && j == 0 || j == 4 || i == 1 && j == 0 || j == 4 || i == 2 && j == 0 || j == 4 || i == 3 && j == 0 || j == 4)
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

                efectos.Parathux(ref PowerPoints.allCount1, cardlen);
                PowerPoints.Actualizar1();
            }
            //---------------Overhix
            if (cartaactual.GetComponent<Card>().name == "overhix")
            {
                efectos.Overhix(ref PowerPoints.allCount2);
                PowerPoints.Actualizar2();
            }

            //---------------Duvenik
            if (cartaactual.GetComponent<Card>().name == "duvenik")
            {
                efectos.Duvenik(ref PowerPoints.allCount1, ref PowerPoints.allCount2, tablero.cementerio1, tablero.cementerio2);

                PowerPoints.Actualizar1();
                PowerPoints.Actualizar2();
            }

        }//------------------------------------------------------------------------silver
        else if (cartaactual.GetComponent<Card>().type == "silver")
        {
            if (maskplayer[0, 1] == false)
            {
                PonerCarta("guerrero0", maskplayer, 0, 1);


                p = 1;

            }
            else if (maskplayer[0, 2] == false)
            {
                PonerCarta("guerrero1", maskplayer, 0, 2);


                p = 1;

            }
            else if (maskplayer[0, 3] == false)
            {
                PonerCarta("guerrero2", maskplayer, 0, 3);


                p = 1;

            }
            else if (maskplayer[2, 1] == false)
            {
                PonerCarta("distancia0", maskplayer, 2, 1);


                p = 3;

            }
            else if (maskplayer[2, 2] == false)
            {
                PonerCarta("distancia1", maskplayer, 2, 2);


                p = 3;

            }
            else if (maskplayer[2, 3] == false)
            {
                PonerCarta("distancia2", maskplayer, 2, 3);

                p = 3;
            }
            else if (maskplayer[1, 1] == false)
            {
                PonerCarta("asedio0", maskplayer, 1, 1);


                p = 2;
            }
            else if (maskplayer[1, 2] == false)
            {
                PonerCarta("asedio1", maskplayer, 1, 2);


                p = 2;
            }
            else if (maskplayer[1, 3] == false)
            {
                PonerCarta("asedio2", maskplayer, 1, 3);


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

            }

            //------sacerdote dragon 
            if (cartaactual.GetComponent<Card>().name == "sacerdote dragon")
            {
                for (int i = 0; i < maskplayer.GetLength(0); i++)
                {
                    for (int j = 0; j < maskplayer.GetLength(1); j++)
                    {
                        if (maskplayer[1, 1] == true || maskplayer[1, 2] == true || maskplayer[1, 3] == true)
                        {
                            PowerPoints.addPointotal1(2);

                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                PowerPoints.Actualizar1();

            }
            //-----serana
            if (cartaactual.GetComponent<Card>().name == "serana")
            {
                PowerPoints.addPointotal2(2 * tablero.cementerio1 * -1);
                PowerPoints.Actualizar2();
            }
            //-----talos
            if (cartaactual.GetComponent<Card>().name == "talos")
            {
                if (totaldecartas2 != 0)
                {
                    int promedio = totalpower2 / totaldecartas2;
                    PowerPoints.addPointotal2(promedio);
                    PowerPoints.Actualizar2();
                }

            }
            //-----ulfric
            if (cartaactual.GetComponent<Card>().name == "ulfric")
            {


                for (int i = 0; i < mask.GetLength(0); i++)
                {
                    for (int j = 0; j < mask.GetLength(1); j++)
                    {
                        if (mask[0, 1] == true)
                        {
                            PowerPoints.addPointotal2(-1);

                        }
                        if (mask[0, 2] == true)
                        {
                            PowerPoints.addPointotal2(-1);
                        }
                        if (mask[0, 2] == true)
                        {
                            PowerPoints.addPointotal2(-1);
                        }
                    }
                }
                PowerPoints.Actualizar2();

            }
            //---vampiro
            if (cartaactual.GetComponent<Card>().name == "vampiro")
            {
                PowerPoints.addPointotal1(5);
                PowerPoints.Actualizar1();
                PowerPoints.addPointotal2(-5);
                PowerPoints.Actualizar2();

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

            }  


            Destroy(cartaactual);
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
                    PonerCarta("asedio10", mask, 1, 1);
                }
                else if (mask[1, 2] == false)
                {
                    PonerCarta("asedio11", mask, 1, 2);
                }
                else if (mask[1, 3] == false)
                {
                    PonerCarta("asedio12", mask, 1, 3);

                }
                //puntos
                PowerPoints.addPointToasedio2(cartaactual.GetComponent<Card>().power);
            }
        //--------------------------------------------------------------------------clima
        else if (cartaactual.GetComponent<Card>().type == "clima")
        {
            if (mask[0, 0] == false)
            {
                PonerCarta("clima0", mask, 0, 0);
                PowerPoints.addPointotal2(-PowerPoints.filag2);
                PowerPoints.filag2 = 0;
                //fila guererero de ambos jugadores//jugador 2
                if (mask[0, 1])
                {
                    int despejeg10 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior2(1);
                }
                if (mask[0, 2])
                {
                    int despejeg11 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior2(1);
                }
                if (mask[0, 3])
                {
                    int despejeg12 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior2(1);
                }
                PowerPoints.Actualizar2();
                //jugador 1
                PowerPoints.addPointotal1(-PowerPoints.filag1);
                PowerPoints.filag1 = 0;
                if (maskplayer[0, 1])
                {
                    int despejeg0 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior1(1);
                }
                if (maskplayer[0, 2])
                {
                    int despejeg1 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior1(1);
                }
                if (maskplayer[0, 3])
                {
                    int despejeg2 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToWarrior1(1);
                }
                PowerPoints.Actualizar1();




            }
            else if (mask[1, 0] == false)
            {
                PonerCarta("clima1", mask, 1, 0);

                //j2
                PowerPoints.addPointotal2(-PowerPoints.filaa2);
                PowerPoints.filaa2 = 0;

                if (mask[1, 1])
                {
                    int despejea10 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToasedio2(1);
                }
                if (mask[1, 2])
                {
                    int despejea11 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToasedio2(1);

                }
                if (mask[1, 3])
                {
                    int despejea2 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToasedio2(1);

                }
                PowerPoints.Actualizar2();


                //fila asedio de ambos jugadores // j 1
                PowerPoints.addPointotal1(-PowerPoints.filaa1);
                PowerPoints.filaa1 = 0;


                

                if (maskplayer[1, 1])
                {
                    int despejea0 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToasedio1(1);
                }
                if (maskplayer[1, 2])
                {
                    int despejea1 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToasedio1(1);

                }
                if (maskplayer[1, 3])
                {
                    int despejea2 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointToasedio1(1);

                }
                PowerPoints.Actualizar1();
               
            }
            else if (maskplayer[2, 0] == false)
            {
                PonerCarta("clima2", mask, 2, 0);

                //j2
                PowerPoints.addPointotal2(-PowerPoints.filad2);
                PowerPoints.filad2 = 0;

                if (mask[2, 1])
                {
                    int despejed10 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance2(1);
                }
                if (mask[2, 2])
                {
                    int despejed11 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance2(1);
                }
                if (mask[2, 3])
                {
                    int despejed12 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance2(1);

                }
                PowerPoints.Actualizar2();

                PowerPoints.addPointotal1(-PowerPoints.filad1);
                PowerPoints.filad1 = 0;

                //fila distancia de ambos jugadores//j1
                if (maskplayer[2, 1])
                {
                    int despejed0 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance1(1);
                }
                if (maskplayer[2, 2])
                {
                    int despejed1 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance1(1);
                }
                if (maskplayer[2, 3])
                {
                    int despejed2 = cartaactual.GetComponent<Card>().power;
                    cartaactual.GetComponent<Card>().power = 1;
                    PowerPoints.addPointTodistance2(1);

                }
                PowerPoints.Actualizar1();
               
            }


        }
        //-----------------------------------------------------------------------------aumento
        else if (cartaactual.GetComponent<Card>().type == "aumento")
            {
                if (mask[0, 4] == false)
                {
                    PonerCarta("aumento10", mask, 0, 4);
                    PowerPoints.addPointToWarrior2(PowerPoints.filag2 * cartaactual.GetComponent<Card>().power);
                    PowerPoints.Actualizar2();
                }
                else if (mask[1, 4] == false)
                {
                    PonerCarta("aumento11", mask, 1, 4);
                    PowerPoints.addPointToasedio2(PowerPoints.filaa2 * cartaactual.GetComponent<Card>().power);
                    PowerPoints.Actualizar2();

                }
                else if (mask[2, 4] == false)
                {
                    PonerCarta("aumento12", mask, 2, 4);
                    PowerPoints.addPointTodistance2(PowerPoints.filad2 * cartaactual.GetComponent<Card>().power);
                    PowerPoints.Actualizar2();

                }
            }
            //------------------------------------------------------------------------guerrero
            else if (cartaactual.GetComponent<Card>().type == "guerrero")
            {
                if (mask[0, 1] == false)
                {
                    PonerCarta("guerrero10", mask, 0, 1);


                    
                }
                else if (mask[0, 2] == false)
                {
                    PonerCarta("guerrero11", mask, 0, 2);

                }
                else if (mask[0, 3] == false)
                {
                    PonerCarta("guerrero12", mask, 0, 3);

                }
                //puntos
                PowerPoints.addPointToWarrior2(cartaactual.GetComponent<Card>().power);

            }//-----------------------------------------------------------------------------------------------------distancia

            else if (cartaactual.GetComponent<Card>().type == "distancia")
            {
                if (mask[2, 1] == false)
                {
                    PonerCarta("distancia10", mask, 2, 1);


                }
                else if (mask[2, 2] == false)
                {
                    PonerCarta("distancia11", mask, 2, 2);


                }
                else if (mask[2, 3] == false)
                {
                    PonerCarta("distancia12", mask, 2, 3);


                }
                //puntos 
                PowerPoints.addPointTodistance2(cartaactual.GetComponent<Card>().power);
                
            }//--------------------------------------------------------------------------------trampa
            else if (cartaactual.GetComponent<Card>().type == "trampa")
            {
                if (maskplayer[0, 1] == false)
                {
                    PonerCarta("guerrero0", maskplayer, 0, 1);


                    p = 1;

                }
                else if (maskplayer[0, 2] == false)
                {
                    PonerCarta("guerrero1", maskplayer, 0, 2);


                    p = 1;

                }
                else if (maskplayer[0, 3] == false)
                {
                    PonerCarta("guerrero2", maskplayer, 0, 3);


                    p = 1;

                }
                else if (maskplayer[2, 1] == false)
                {
                    PonerCarta("distancia0", maskplayer, 2, 1);


                    p = 3;

                }
                else if (maskplayer[2, 2] == false)
                {
                    PonerCarta("distancia1", maskplayer, 2, 2);


                    p = 3;

                }
                else if (maskplayer[2, 3] == false)
                {
                    PonerCarta("distancia2", maskplayer, 2, 3);

                    p = 3;
                }
                else if (maskplayer[1, 1] == false)
                {
                    PonerCarta("asedio0", maskplayer, 1, 1);


                    p = 2;
                }
                else if (maskplayer[1, 2] == false)
                {
                    PonerCarta("asedio1", maskplayer, 1, 2);


                    p = 2;
                }
                else if (maskplayer[1, 3] == false)
                {
                    PonerCarta("asedio2", maskplayer, 1, 3);


                    p = 2;
                }
            }//-------------------------------------------------------------------heroe
            else if (cartaactual.GetComponent<Card>().type == "heroe")
            {
                if (mask[0, 1] == false)
                {
                    PonerCarta("guerrero10", mask, 0, 1);


                    p = 1;

                }
                else if (mask[0, 2] == false)
                {
                    PonerCarta("guerrero11", mask, 0, 2);


                    p = 1;

                }
                else if (mask[0, 3] == false)
                {
                    PonerCarta("guerrero12", mask, 0, 3);


                    p = 1;

                }
                else if (mask[2, 1] == false)
                {
                    PonerCarta("distancia10", mask, 2, 1);


                    p = 3;

                }
                else if (mask[2, 2] == false)
                {
                    PonerCarta("distancia11", mask, 2, 2);


                    p = 3;

                }
                else if (mask[2, 3] == false)
                {
                    PonerCarta("distancia12", mask, 2, 3);

                    p = 3;
                }
                else if (mask[1, 1] == false)
                {
                    PonerCarta("asedio10", mask, 1, 1);


                    p = 2;
                }
                else if (mask[1, 2] == false)
                {
                    PonerCarta("asedio11", mask, 1, 2);


                    p = 2;
                }
                else if (mask[1, 3] == false)
                {
                    PonerCarta("asedio12", mask, 1, 3);


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

                //----------------Parathux

                if (cartaactual.GetComponent<Card>().name == "parathux")
                {
                    int cardlen = 0;
                    for (int i = 0; i < maskplayer.GetLength(0); i++)
                    {
                        for (int j = 0; j < maskplayer.GetLength(1); j++)
                        {
                            if (i == 0 && j == 0 || j == 4 || i == 1 && j == 0 || j == 4 || i == 2 && j == 0 || j == 4 || i == 3 && j == 0 || j == 4)
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

                    efectos.Parathux(ref PowerPoints.allCount2, cardlen);
                    PowerPoints.Actualizar2();
                }
                //---------------Overhix
                if (cartaactual.GetComponent<Card>().name == "overhix")
                {
                    efectos.Overhix(ref PowerPoints.allCount1);
                    PowerPoints.Actualizar1();
                }

                //---------------Duvenik
                if (cartaactual.GetComponent<Card>().name == "duvenik")
                {
                    efectos.Duvenik(ref PowerPoints.allCount2, ref PowerPoints.allCount1, tablero.cementerio1, tablero.cementerio2);
                    PowerPoints.Actualizar1();
                    PowerPoints.Actualizar2();
                }

            }//------------------------------------------------------------------------silver
            else if (cartaactual.GetComponent<Card>().type == "silver")
            {
                if (mask[0, 1] == false)
                {
                    PonerCarta("guerrero10", mask, 0, 1);


                    p = 1;

                }
                else if (mask[0, 2] == false)
                {
                    PonerCarta("guerrero11", mask, 0, 2);


                    p = 1;

                }
                else if (mask[0, 3] == false)
                {
                    PonerCarta("guerrero12", mask, 0, 3);


                    p = 1;

                }
                else if (mask[2, 1] == false)
                {
                    PonerCarta("distancia10", mask, 2, 1);


                    p = 3;

                }
                else if (mask[2, 2] == false)
                {
                    PonerCarta("distancia11", mask, 2, 2);


                    p = 3;

                }
                else if (mask[2, 3] == false)
                {
                    PonerCarta("distancia12", mask, 2, 3);

                    p = 3;
                }
                else if (mask[1, 1] == false)
                {
                    PonerCarta("asedio10", mask, 1, 1);


                    p = 2;
                }
                else if (mask[1, 2] == false)
                {
                    PonerCarta("asedio11", mask, 1, 2);


                    p = 2;
                }
                else if (mask[1, 3] == false)
                {
                    PonerCarta("asedio12", mask, 1, 3);


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


                //------hombre lobo
                if (cartaactual.GetComponent<Card>().name == "hombre lobo")
                {
                    PowerPoints.addPointotal1(-5);

                }

                //------sacerdote dragon 
                if (cartaactual.GetComponent<Card>().name == "sacerdote dragon")
                {
                    for (int i = 0; i < mask.GetLength(0); i++)
                    {
                        for (int j = 0; j < mask.GetLength(1); j++)
                        {
                            if (mask[1, 1] == true || mask[1, 2] == true || mask[1, 3] == true)
                            {
                                PowerPoints.addPointotal2(2);

                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    PowerPoints.Actualizar2();

                }
                //-----serana
                if (cartaactual.GetComponent<Card>().name == "serana")
                {
                    PowerPoints.addPointotal1(2 * tablero.cementerio2 * -1);
                    PowerPoints.Actualizar1();
                }
                //-----talos
                if (cartaactual.GetComponent<Card>().name == "talos")
                {
                    if (totaldecartas1 != 0)
                    {
                        int promedio = totalpower1 / totaldecartas1;
                        PowerPoints.addPointotal1(promedio);
                        PowerPoints.Actualizar1();
                    }

                }
                //-----ulfric
                if (cartaactual.GetComponent<Card>().name == "ulfric")
                {


                    for (int i = 0; i < maskplayer.GetLength(0); i++)
                    {
                        for (int j = 0; j < maskplayer.GetLength(1); j++)
                        {
                            if (maskplayer[0, 1] == true)
                            {
                                PowerPoints.addPointotal1(-1);

                            }
                            if (maskplayer[0, 2] == true)
                            {
                                PowerPoints.addPointotal1(-1);
                            }
                            if (maskplayer[0, 2] == true)
                            {
                                PowerPoints.addPointotal1(-1);
                            }
                        }
                    }
                    PowerPoints.Actualizar1();

                }
                //---vampiro
                if (cartaactual.GetComponent<Card>().name == "vampiro")
                {
                    PowerPoints.addPointotal2(5);
                    PowerPoints.Actualizar2();
                    PowerPoints.addPointotal1(-5);
                    PowerPoints.Actualizar1();

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

            }


            Destroy(cartaactual);
        }



    }


    public void OnMouseDown()
        {
            if (TurnSystem.IsPlayer1Turn())
            {
                Debug.Log("on");
                cartaactual = gameObject;
                Poner();
                turn.StarPlayer2Turn();
            }
            else if (TurnSystem.IsPlayer2Turn())
            {
                Debug.Log("on");
                cartaactual = gameObject;
                Poner2();

                turn.StarPlayer1Turn();
            }

        }
            public void Vaciar() 
            {
                //ver cuantas cartas van para el cementerio 1
                for (int i = 0; i < maskplayer.GetLength(0); i++)
                {
                    for (int j = 0; j < maskplayer.GetLength(1); j++)
                    {
                        if (maskplayer[i,j])
                        {
                            tablero.cementerio1 += 1;
                            maskplayer[i, j] = false;
                        }
                    }
                }
                //ver cuantas cartas van para el cementerio 1
                for (int i = 0; i < mask.GetLength(0); i++)
                {
                    for (int j = 0; j < mask.GetLength(1); j++)
                    {
                        if (mask[i, j])
                        {
                            tablero.cementerio2 += 1;
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


    public void CambiardeRonda() 
    {
        if (TurnSystem.SePuedeTeerminarRonda())
        {
            Vaciar();
            deck.asignar2cartas1(deck.h1p);
            deck.asignar2cartas2(deck.h2p);
            manager.GanarRonda();
        }
    }



        

    
}

     

