using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [Header("Variables for food")]
    public GameObject[] food;
    public float minTimeTilFoodSpawn, maxTimeTilFoodSpawn;
    public static int missedFood = 0;
    public Transform spawnPoint;
    //public static int live = 5;


    [Header("Variabels for defining how far and high food is thrown")]
    [Tooltip("Add points where food shall be thrown to in here")]
    public Transform[] endPoints;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNewFood());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnNewFood()
    {
        float timeTilSpawn = Random.Range(minTimeTilFoodSpawn, maxTimeTilFoodSpawn);
        int foodIDX = Random.Range(0, food.Length);
        int endPointsIDX = Random.Range(0, endPoints.Length);
        float rotation = Random.Range(-8, -4);

        GameObject spawnedFood = Instantiate(food[foodIDX], spawnPoint);

        Rigidbody foodRigid = spawnedFood.GetComponent<Rigidbody>();
        if (foodRigid)
        {

            float yVelocity = height;
            float acceleration = Physics.gravity.y;
            float time = DisplacementFormula(acceleration, yVelocity, 0);
            float xVelocity = (endPoints[endPointsIDX].position.x - transform.position.x) / time;
            foodRigid.velocity = new Vector3(xVelocity, yVelocity);
            foodRigid.angularVelocity = new Vector3(0, 0, rotation);
        }

        yield return new WaitForSeconds(timeTilSpawn);
        StartCoroutine(SpawnNewFood());
    }

    //calculates time needed to throw something over displacement distance (in y direction)
    float DisplacementFormula(float acceleration, float velocity, float displacement)
    {
        var squareRoot = Mathf.Sqrt(velocity * velocity + 2 * acceleration * displacement);
        var resultPlus = - (velocity + squareRoot) / acceleration;
        var resultMinus = (-velocity + squareRoot) / acceleration;
        return Mathf.Max(resultMinus, resultPlus);
    }

    private void OnDrawGizmos()
    {
        foreach(var point in endPoints)
        {
            Gizmos.DrawWireCube(point.position, point.localScale);
        }
    }
}
