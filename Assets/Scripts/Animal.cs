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
    [SerializeField] Material male, female;
    [SerializeField] GameObject animal;
    public Sex sex;

    public float thirst, hungry, age;
    [Range(0, 30)]
    [SerializeField] float minAge, maxAge;

    [Range(0, 30)]
    [SerializeField] int pergantPeriod, amountOfChildrenMin, amountOfChildrenMax;
    float deathAge, numOfChildren;
    Vector3 directionMoving;

    public bool isHungry, isThirst, isWantToPair, hasNoTarget;
    bool wasPaired, isPergant;
    private void Start()
    {
        view.radius = viewRadius;
        deathAge = Random.Range(minAge, maxAge);
        if(Random.value > 0.65f)
            sex = Sex.Female;

        if (sex == Sex.Male)
            GetComponent<Renderer>().material = male;
        else
            GetComponent<Renderer>().material = female;

        if (sex == Sex.Female)
            numOfChildren = Random.Range(amountOfChildrenMin, amountOfChildrenMax);
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
                if (hasNoTarget)
                    transform.position = Vector3.MoveTowards(transform.position, directionMoving, Time.deltaTime * speed);
                break;
            case Activity.Eating:
                hungry -= 7.5f * Time.deltaTime;
                if (hungry <= 0)
                {
                    hungry = 0;
                    activity = Activity.Moving;
                    hasNoTarget = true;
                }
                break;
            case Activity.Drinking:
                thirst -= 7.5f * Time.deltaTime;
                if (thirst <= 0)
                {
                    thirst = 0;
                    activity = Activity.Moving;
                    hasNoTarget = true;
                }
                break;
            case Activity.Pairing:
                isWantToPair = false;
                wasPaired = true;
                hasNoTarget = true;
                StartCoroutine(Pairing());
                if (sex == Sex.Female && !isPergant)
                    StartCoroutine(Pergant(pergantPeriod));
                break;

        }
        if (thirst > 20f)
        {
            isThirst = true;
            if (thirst > 40f)
            {
                Destroy(gameObject);
            }
        }
        if (hungry > 30f)
        {
            isHungry = true;
            if (hungry > 50f)
            {
                Destroy(gameObject);
            }
        }
        if (age > 1.5f && age < 2.5f && !wasPaired)
            isWantToPair = true;

        if (age > maxAge)
            Destroy(gameObject);
    }

    private void OnValidate()
    {
        if (sex == Sex.Male)
            GetComponent<Renderer>().material = male;
        else
            GetComponent<Renderer>().material = female;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<Food>() != null)
        {
            other.gameObject.GetComponent<Food>().foodParts--;
            if (other.gameObject.GetComponent<Food>().foodParts <= 0)
                Destroy(other.gameObject);
        }
    }

    IEnumerator Pairing()
    {
        yield return new WaitForSeconds(5f);
        activity = Activity.Moving;
    }
    IEnumerator Pergant(float pergantPeriod)
    {
        Debug.Log(numOfChildren);
        isPergant = true;
        yield return new WaitForSeconds(pergantPeriod);
        for (int i = 0; i < numOfChildren; i++)
        {
            var newAnimal = Instantiate(animal, transform.position, Quaternion.identity);
            
        }
    }

    public void WaterDetected(View view, Transform waterPosition)
    {
        if (isThirst)
        {
            hasNoTarget = false;
            transform.position = Vector3.MoveTowards(transform.position, waterPosition.position, Time.deltaTime * speed / 2f);
            float distance = Mathf.Abs((waterPosition.position - transform.position).magnitude);
            if (distance < 3f)
            {
                isThirst = false;
                activity = Activity.Drinking;
            }
        }
    }
    public void FoodDetected(View view, Transform foodPosition)
    {
        if (isHungry)
        {
            hasNoTarget = false;
            transform.position = Vector3.MoveTowards(transform.position, foodPosition.position, Time.deltaTime * speed / 2f);
            float distance = Mathf.Abs((foodPosition.position - transform.position).magnitude);
            if (distance < 0.80f)
            {
                isHungry = false;
                activity = Activity.Eating;
            }
        }
    }
    public void PairDetected(View view, Transform pairPosition)
    {
        if (isWantToPair)
        {
            hasNoTarget = false;
            transform.position = Vector3.MoveTowards(transform.position, pairPosition.position, Time.deltaTime * speed / 2f);
            float distance = Mathf.Abs((pairPosition.position - transform.position).magnitude);
            if (distance < 1.2f)
            {
                activity = Activity.Pairing;
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
            yield return new WaitForSeconds(3f);
        }
    }


}