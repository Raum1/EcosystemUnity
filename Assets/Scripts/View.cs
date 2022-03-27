using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Water>() != null)
        {
            transform.parent.GetComponent<Animal>().WaterDetected(this, other.transform);
        }
    }
    
}
