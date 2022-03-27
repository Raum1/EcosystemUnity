using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] Animal parent;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Water>() != null)
        {
            parent.GetComponent<Animal>().WaterDetected(this, other.transform);
        }
        else if (other.GetComponent<Food>() != null)
        {
            parent.GetComponent<Animal>().FoodDetected(this, other.transform);
        }
        else if (other.GetComponent<Animal>() != null)
        {
            if(parent.sex != other.GetComponent<Animal>().sex)
                if(parent.age > 1.5f && other.GetComponent<Animal>().age > 1.5f)
                    parent.GetComponent<Animal>().PairDetected(this, other.transform);
        }
    }
}
