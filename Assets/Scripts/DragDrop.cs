using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool Isdragging;
    private bool  IsoverDropZone = false;
    private GameObject dropzone;
    private Vector2 Startposition;


    // Start is called before the first frame update
    void Start()
    {
      


    }

    // Update is called once per frame
    void Update()
    {
        if (Isdragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        

      
    }


    public void StarDrag()
    {
        Startposition = transform.position;
        Isdragging = true;
    }
    public void EndDrag() 
    {
        Isdragging = false;
        if (IsoverDropZone)
        {
            transform.SetParent(dropzone.transform, false);
        }
        else 
        {
            transform.position = Startposition; 
        }
    }
}
