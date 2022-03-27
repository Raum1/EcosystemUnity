using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBlock : MonoBehaviour
{
    [SerializeField] GameObject food;
    IEnumerator GrowUpFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(100f, 300f));
            var foodObj = Instantiate(food, new Vector3(transform.position.x, 0.75f, transform.position.z), Quaternion.identity).transform;
            foodObj.parent = transform;
        }
    }
    void Start(){
        if(GetComponent<Water>() == null)
            StartCoroutine(GrowUpFood());
    }
}
