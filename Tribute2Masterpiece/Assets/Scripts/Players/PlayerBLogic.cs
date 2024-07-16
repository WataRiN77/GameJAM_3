using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBLogic : MonoBehaviour
{
    Vector3 mousePos;

    Vector3 holeQ = new Vector3(-3f,  3f, 0f);
    Vector3 holeW = new Vector3( 0f,  3f, 0f);
    Vector3 holeE = new Vector3( 3f,  3f, 0f);
    Vector3 holeA = new Vector3(-3f,  0f, 0f);
    Vector3 holeS = new Vector3( 0f,  0f, 0f);
    Vector3 holeD = new Vector3( 3f,  0f, 0f);
    Vector3 holeZ = new Vector3(-3f, -3f, 0f);
    Vector3 holeX = new Vector3( 0f, -3f, 0f);
    Vector3 holeC = new Vector3( 3f, -3f, 0f);

    private bool clickQ = false;
    private bool clickW = false;
    private bool clickE = false;
    private bool clickA = false;
    private bool clickS = false;
    private bool clickD = false;
    private bool clickZ = false;
    private bool clickX = false;
    private bool clickC = false;

    [HideInInspector]
    public Vector3 PosQ = new Vector3(-2f,  3f, 0f);
    [HideInInspector]
    public Vector3 PosW = new Vector3( 1f,  3f, 0f);
    [HideInInspector]
    public Vector3 PosE = new Vector3( 4f,  3f, 0f);
    [HideInInspector]
    public Vector3 PosA = new Vector3(-2f,  0f, 0f);
    [HideInInspector]
    public Vector3 PosS = new Vector3( 1f,  0f, 0f);
    [HideInInspector]
    public Vector3 PosD = new Vector3( 4f,  0f, 0f);
    [HideInInspector]
    public Vector3 PosZ = new Vector3(-2f, -3f, 0f);
    [HideInInspector]
    public Vector3 PosX = new Vector3( 1f, -3f, 0f);
    [HideInInspector]
    public Vector3 PosC = new Vector3( 4f, -3f, 0f);

    public Transform p_Transform;

    public float delayTime = 0.5f;

    private bool  canMove = true;

    [HideInInspector]
    public float timer = 0f;

    public float tolerance = 1f;

    public PlayerManager PM;

    //[HideInInspector]
    public bool playerAvailable = true;

    void Start()
    {
        p_Transform.position = p_Transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // LeftClick
        {
            // Mouse Position
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
            
            //Debug.Log("Mouse Click Position: " + mousePos);
            CalcDistance(tolerance);
        }

        if(clickQ && canMove && (!PM.playerSmash))
        {
            p_Transform.position = PosQ;
            canMove = false;
        }
        if(clickW && canMove && (!PM.playerSmash))
        {
            p_Transform.position = PosW;
            canMove = false;
        }
        if(clickE && canMove && (!PM.playerSmash))
        {
            p_Transform.position = PosE;
            canMove = false;
        }
        if(clickA && canMove && (!PM.playerSmash))
        {
            p_Transform.position = PosA;
            canMove = false;
        }
        if(clickS && canMove && (!PM.playerSmash))
        {
            p_Transform.position = PosS;
            canMove = false;
        }
        if(clickD && canMove && (!PM.playerSmash))
        {
            p_Transform.position = PosD;
            canMove = false;
        }
        if(clickZ && canMove && (!PM.playerSmash))
        {
            p_Transform.position = PosZ;
            canMove = false;
        }
        if(clickX && canMove && (!PM.playerSmash))
        {
            p_Transform.position = PosX;
            canMove = false;
        }
        if(clickC && canMove && (!PM.playerSmash))
        {
            p_Transform.position = PosC;
            canMove = false;
        }

        if(canMove) timer = 0f; // Reset Timer

        if(!canMove)
        {
            canMove = false;
            if(timer <= delayTime) timer += Time.deltaTime;
            else canMove = true;
            //Debug.Log(timer);
        }

        if(canMove && (!PM.playerSmash)) playerAvailable = true;
        else playerAvailable = false;

        reset();
    }

    private void CalcDistance(float tolerance)
    {
        clickQ = (Vector3.Distance(mousePos, holeQ) <= tolerance);
        clickW = (Vector3.Distance(mousePos, holeW) <= tolerance);
        clickE = (Vector3.Distance(mousePos, holeE) <= tolerance);
        clickA = (Vector3.Distance(mousePos, holeA) <= tolerance);
        clickS = (Vector3.Distance(mousePos, holeS) <= tolerance);
        clickD = (Vector3.Distance(mousePos, holeD) <= tolerance);
        clickZ = (Vector3.Distance(mousePos, holeZ) <= tolerance);
        clickX = (Vector3.Distance(mousePos, holeX) <= tolerance);
        clickC = (Vector3.Distance(mousePos, holeC) <= tolerance);
    }

    private void reset()
    {
        clickQ = false;
        clickW = false;
        clickE = false;
        clickA = false;
        clickS = false;
        clickD = false;
        clickZ = false;
        clickX = false;
        clickC = false;
    }
}
