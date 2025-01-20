using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        this.transform.position += new Vector3(0, 0.1f, 0);
    }
    private void OnMouseExit()
    {
        this.transform.position -= new Vector3(0, 0.1f, 0);
    }
}
