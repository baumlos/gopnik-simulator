using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public float speed = 1;
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime));
    }
}
