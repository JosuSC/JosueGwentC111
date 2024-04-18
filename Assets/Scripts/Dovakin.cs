using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dovakin : MonoBehaviour
{
    private Button button; 

    void Start()
    {
        button = GetComponent<Button>(); 
        button.onClick.AddListener(TaskOnClick); 
    }

    public void TaskOnClick()
    {
        Debug.Log("Has pulsado el botón");
        PowerPoints.addPointotal2(-25);
        PowerPoints.Actualizar2();
        button.interactable = false; 
    }


}
