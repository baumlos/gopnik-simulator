using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class AngryDopnikPlayer : MonoBehaviour
{
    public float power;
    public bool freezed;
    public int live;
    public Image[] vodkaBottles;
    public Transform hook;
    private bool winned = false;

    Vector3 startPos; 
    Vector3 hookStartPos;
    Vector3 hookStartRot;

    Vector3 offset;

    Vector3 screenPoint;

    Rigidbody playerRigid;
    Rigidbody hookRigid;
    //bool hooked = true;

        [Header("Screen to display")]
        public GameObject winningScreen;
        public GameObject losingScreen;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        hookStartPos = hook.position;
        hookStartRot = hook.eulerAngles;
        playerRigid = GetComponent<Rigidbody>();
        hookRigid = hook.GetComponent<Rigidbody>();
        hookRigid.useGravity = false;
        hookRigid.isKinematic = true;
        live = vodkaBottles.Length;
    }

    // Update is called once per frame
    void Update()
    {
    //    if (hooked)
    //    {
    //        hook.position = transform.position;
    //    }
    }

    private void OnMouseUp()
    {
        playerRigid.useGravity = true;
        hookRigid.useGravity = true;
        hook.SetParent(null);
        hookRigid.isKinematic = false;
        

        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;

        Vector3 direction = startPos - cursorPosition;

        playerRigid.velocity = direction * power;
        hookRigid.velocity = direction * power * 0.8f;
        
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;

        if(transform.position.x < 0)
        {
            transform.position = cursorPosition;
        }

        
        hookRigid.velocity = Vector3.zero;
    }


    private void Reset()
    {
        transform.position = startPos;
        playerRigid.useGravity = false;
        playerRigid.velocity = Vector3.zero;
        hookRigid.useGravity = false;
        hookRigid.isKinematic = true;
        hookRigid.velocity = Vector3.zero;
        hook.SetParent(transform);
        hook.transform.position = hookStartPos;
        hook.transform.eulerAngles = hookStartRot;

        live--;
        if (live < 0)
        {
            Instantiate(losingScreen);
            Debug.Log("You lose");
            //StartCoroutine(ChangeToOverworld());
        }
        else
        {
            vodkaBottles[live].enabled = false;
        }

    }

    //private void OnBecameInvisible()
    //{
    //    Reset();

    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hole")
        {
            GlobalVariables.addVodka(live * 100);
            //freezed = true; //do something with this
            Time.timeScale = 0;
            Instantiate(winningScreen);
            winned = true;
            //StartCoroutine(ChangeToOverworld());
            Debug.Log("Should win here");
        } else if(other.tag == "Ice" && !winned)
        {
            Reset();
        }
    }


    IEnumerator ChangeToOverworld()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);

        Time.timeScale = 1;

        Debug.Log("Change to overworld here");
        
    }

}
