using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;


public class inicio : MonoBehaviour
{
    public string nombredeescena;

    public void cargarescena()
    {
        SceneManager.LoadScene(nombredeescena);
    }

}
