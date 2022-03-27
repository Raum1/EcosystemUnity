using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sex { Male, Female }
public enum Activity { Moving, Eating, Drinking, Pairing }
public class Animal : MonoBehaviour
{
    [SerializeField] float viewRadius;
    [SerializeField] SphereCollider view;
    [SerializeField] Activity activity;
    [SerializeField] float speed = 1f;
    public Sex sex;

    public float thirst, hungry, age;
    [Range(0, 30)]
    [SerializeField] float minAge, maxAge;
    float deathAge;
    Vector3 directionMoving;

    public bool isHungry, isThirst, isWantToPair, isMoving;
    private void Start()
    {
        view.radius = viewRadius;
        deathAge = Random.Range(minAge, maxAge);
        StartCoroutine(GetDirection());
    }
    void Update()
    {
        thirst += .5f * Time.deltaTime;
        hungry += .5f * Time.deltaTime;
        age += .035f * Time.deltaTime;
        switch (activity)
        {
            case Activity.Moving:
                thirst += .5f * Time.deltaTime;
                hungry += .5f * Time.deltaTime;
                if (isMoving)
                    transform.position = Vector3.MoveTowards(transform.position, directionMoving, Time.deltaTime * speed); ;
                break;
            case Activity.Eating:
                hungry -= 7.5f * Time.deltaTime;
                if (hungry <= 0)
                {
                    hungry = 0;
                    activity = Activity.Moving;
                    isMoving = true;
                }
                break;
            case Activity.Drinking:
                thirst -= 7.5f * Time.deltaTime;
                if (thirst <= 0)
                {
                    thirst = 0;
                    activity = Activity.Moving;
                    isMoving = true;
                }
                break;
            case Activity.Pairing:
                isWantToPair = false;
                //soon
                break;

        }
        if (thirst > 20f)
        {
            isThirst = true;
            if (thirst > 40)
            {
                Death();
            }
        }
        if (hungry > 30f)
        {
            isHungry = true;
            if (thirst > 50f)
            {
                Death();
            }
        }
    }

    public void WaterDetected(View view, Transform waterPosition)
    {
        if (isThirst)
        {
            isMoving = false;
            transform.position = Vector3.MoveTowards(transform.position, waterPosition.position, Time.deltaTime * speed/2f);
            float distance = Mathf.Abs((waterPosition.position - transform.position).magnitude);
            if (distance < 3f)
            {
                isThirst = false;
                activity = Activity.Drinking;
            }
        }
    }
    void Death()
    {
        Destroy(gameObject);
    }

    IEnumerator GetDirection()
    {
        while (true)
        {
            Vector3 directionMoving = Random.insideUnitCircle.normalized;
            this.directionMoving = new Vector3(directionMoving.x * 4f, 0f, directionMoving.y * 4f) + transform.position;
            Debug.Log(this.directionMoving);
            yield return new WaitForSeconds(3f);
        }
    }
}