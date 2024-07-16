using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GopherOnClick : MonoBehaviour
{

    public bool Clicked = false;

    public bool CanBeClicked = true;

    public void GopherClicked()
    {
        if(CanBeClicked)
        {
            Clicked = true;
        }  
    }

    void LateUpdate()
    {
        if(Clicked) CanBeClicked = false;
    }
}
