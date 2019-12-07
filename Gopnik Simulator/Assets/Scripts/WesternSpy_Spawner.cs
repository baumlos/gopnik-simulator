using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WesternSpy_Spawner : MonoBehaviour {

    [SerializeField]
    private float distance;

    [SerializeField]
    private int counter;

    //lower body
    [SerializeField]
    private GameObject[] TRC_b;
    //upper body
    [SerializeField]
    private GameObject[] TRC_t;
    //face
    [SerializeField]
    private GameObject[] TRC_f;
    //hat
    [SerializeField]
    private GameObject[] TRC_h;

    static private GameObject[][] trueRussianComrades;

    static private int[][] randomNums;

    [SerializeField]
    private GameObject[] decadentWesternCapitalistPig;

    [SerializeField]
    private float[] pos;


    static private int spy = -1;


    // Start is called before the first frame update
    void Start()
    {
        if (spy == -1)
        {
            trueRussianComrades = new GameObject[4][];
            trueRussianComrades[0] = TRC_b;
            trueRussianComrades[1] = TRC_t;
            trueRussianComrades[2] = TRC_f;
            trueRussianComrades[3] = TRC_h;

            spy = (int)Random.Range(0, counter) % counter;
            randomNums = new int[counter][];
            for(int i=0; i<counter; i++)
            {
                randomNums[i] = new int[trueRussianComrades.Length];
                for(int j=0; j<trueRussianComrades.Length; j++)
                {
                    randomNums[i][j] = (int)Random.Range(0, trueRussianComrades[j].Length) % trueRussianComrades[j].Length;

                }
            }
        }
        for (int i = 0; i < counter; i++)
        {
            for (int j = 0; j < trueRussianComrades.Length; j++)
            {
                if (j == 0 && i == spy)
                {
                    Instantiate(decadentWesternCapitalistPig[randomNums[i][j]], new Vector3(i * distance, pos[j], j==0?-3:-j), new Quaternion(0, 0, 0, 1));
                }
                else
                {
                    Instantiate(trueRussianComrades[j][randomNums[i][j]], new Vector3(i * distance, pos[j], j == 0 ? -3 : -j), new Quaternion(0, 0, 0, 1));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getCounter()
    {
        return counter;
    }

    public float getDistance()
    {
        return distance;
    }

    public int getSpy()
    {
        return spy;
    }

    public void resetSpy()
    {
        spy = -1;
    }
}
