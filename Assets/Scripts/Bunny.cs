using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sex { Male, Female }
public class Bunny : MonoBehaviour
{
    [SerializeField] float viewRadius;
    [SerializeField] SphereCollider view;
    public bool isMoving;
    private void Start()
    {
        view.radius = viewRadius;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Water>() != null)
        {
            Debug.Log("aaaaaaaaaa");
        }
    }
    public void WaterDetected(View view, Transform waterPosition)
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, waterPosition.position, Time.deltaTime);
            float distance = Mathf.Abs((waterPosition.position - transform.position).magnitude);
            Debug.Log(distance);
            if(distance < 2.5f)
                isMoving = false;
        }
    }

}