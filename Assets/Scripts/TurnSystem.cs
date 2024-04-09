using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public bool Isyourturn;
    public int yourturn;
    public int opponentturn;
    public Text turnText;



    // Start is called before the first frame update
    void Start()
    {
       Isyourturn= true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
