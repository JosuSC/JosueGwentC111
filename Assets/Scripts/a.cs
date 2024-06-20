using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.XR;
using UnityEngine.UI;
public class a : MonoBehaviour
{
    // Start is called before the first frame update
    public Image h1, h2;

    public void cambio()
    {
        if(h1.GetComponent<Image>().enabled == false) h1.GetComponent<Image>().enabled = true;
        else h1.GetComponent<Image>().enabled = false;

        if (h2.GetComponent<Image>().enabled == false) h2.GetComponent<Image>().enabled = true;
        else h2.GetComponent<Image>().enabled = false;

    }



}
