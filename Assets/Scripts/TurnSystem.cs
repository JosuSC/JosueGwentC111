using Assets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerTurn { Player1, Player2 }


public class TurnSystem : MonoBehaviour
{
    public static PlayerTurn playerTurn = PlayerTurn.Player1;

    public TextMeshProUGUI turnText;
    public Button player1Button;
    public Button player2Button;
    public Button EndRonda;
    public static int youturn;
    public static int opponentTurn;
    public static Deck deck;
    public Image h1;
    public Image h2;    

    public TurnSystem()
    {
        deck= new Deck();
    }

    private void Start()
    {
        turnText.text = "Tu turno";
        player1Button.interactable = false;
        youturn++;

        Deck.OcultarCartas(deck.Hand2);
        Deck.MostarCartas(deck.Hand1);

    }

    public static bool IsPlayer1Turn()
    {
        return playerTurn == PlayerTurn.Player1;
    }

    public static bool IsPlayer2Turn()
    {
        return playerTurn == PlayerTurn.Player2;
    }

    public void StarPlayer1Turn()
    {

        playerTurn = PlayerTurn.Player1;
        turnText.text = "Tu turno";
        player1Button.interactable = false;
        player2Button.interactable = true;
        youturn++;
        Deck.OcultarCartas(deck.Hand2);
        Deck.MostarCartas(deck.Hand1);


    }

    public void StarPlayer2Turn()
    {
        playerTurn = PlayerTurn.Player2;
        turnText.text = "Turno del Oponente";
        player1Button.interactable = true;
        player2Button.interactable = false;
        opponentTurn++;
        Deck.OcultarCartas(deck.Hand1);
        Deck.MostarCartas(deck.Hand2);
    }

    public static bool SePuedeTeerminarRonda() 
    {
        if (youturn == opponentTurn && youturn >= 2)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public  async void TerminarRonda() 
    {
        if (SePuedeTeerminarRonda())
        {
            GameManager.GanarRonda();

            Invocar.Vaciar();
            PowerPoints.ReiniciarCount();
            TurnSystem.deck.asignar2cartas1(deck.h1p);
            TurnSystem.deck.asignar2cartas2(deck.h2p);
            youturn = 0;
            opponentTurn = 0;   
        }
    }
    
}
