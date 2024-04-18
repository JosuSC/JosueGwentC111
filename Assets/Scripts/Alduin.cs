using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alduin : MonoBehaviour
{
    private Button button;
    int aleatorio;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        Debug.Log("Has pulsado el botón");
        aleatorio = Random.Range(0,3);
        if (aleatorio == 0)
        {
            PowerPoints.addPointotal1(-PowerPoints.filag1);
            PowerPoints.filag1 = 0;
            PowerPoints.Actualizar1();
        }
       else  if (aleatorio == 1)
        {
            PowerPoints.addPointotal1(-PowerPoints.filaa1);

            PowerPoints.filaa1 = 0;
            PowerPoints.Actualizar1();

        }
        else if (aleatorio == 2)
        {
            PowerPoints.addPointotal1(-PowerPoints.filad1);

            PowerPoints.filad1 = 0;
            PowerPoints.Actualizar1();

        }


        button.interactable = false;
    }

}
