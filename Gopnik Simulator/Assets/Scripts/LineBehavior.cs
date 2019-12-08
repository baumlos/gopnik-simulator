using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehavior : MonoBehaviour
{
    public Transform anchor;
    public Transform hook;
    public float length;
    Vector3 startPos;
    Vector3 startRot;
    Vector3 startOffset;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.eulerAngles;
        startOffset = anchor.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //float offsetX = anchor.position.x - startPos.x;
        //float offsetY = anchor.position.y - startPos.y;
        Vector3 offset = hook.position - anchor.position;

        //float angle = Vector3.Angle(offset, Vector3.right);

        //should only rotate around z ax
        transform.right = offset;


        //Extrapolate line
        transform.localScale = new Vector3(Vector3.Magnitude(offset) * length, 1, 1);
        transform.position = (anchor.position + hook.position) / 2;


        //only do if position is different from startpos
        if (anchor.position != (transform.position + startOffset))
        {

        }
    }
}
