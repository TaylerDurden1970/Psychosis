using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPItem : MonoBehaviour
{
    public GameObject xpItem;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(xpItem);
        }
       
    }


}
