using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] Animal parent;

    List<Water> waterList = new List<Water>();
    List<Food> foodList = new List<Food>();
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Water>() != null)
        {
            if (!waterList.Contains(other.GetComponent<Water>()))
                waterList.Add(other.GetComponent<Water>());

            parent.GetComponent<Animal>().WaterDetected(this, GetClosestWater(waterList));
        }
        else if (other.GetComponent<Food>() != null)
        {
            if (!foodList.Contains(other.GetComponent<Food>()))
                foodList.Add(other.GetComponent<Food>());
            parent.GetComponent<Animal>().FoodDetected(this, GetClosestFood(foodList));
        }
        else if (other.GetComponent<Animal>() != null)
        {
            if (parent.sex != other.GetComponent<Animal>().sex)
                if (parent.age > 1.5f && other.GetComponent<Animal>().age > 1.5f)
                    parent.GetComponent<Animal>().PairDetected(this, other.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        waterList = new List<Water>();
        foodList = new List<Food>();
    }

    Transform GetClosestWater(List<Water> waterList)
    {
        var closestWater = transform;
        var minDistance = Mathf.Infinity;
        foreach (Water water in waterList)
        {
            var distance = (water.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                closestWater = water.transform;
            }
        }
        return closestWater;
    }
    Transform GetClosestFood(List<Food> foodList)
    {
        var closestFood = transform;
        var minDistance = Mathf.Infinity;
        foreach (Food food in foodList)
        {
            if (food.foodParts > 0)
            {
                var distance = (food.transform.position - transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestFood = food.transform;
                }
            }
        }
        return closestFood;
    }
}
