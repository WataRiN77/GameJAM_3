using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerALogic : MonoBehaviour
{
    [HideInInspector]
    public Vector3 PosQ = new Vector3(-4f,  3f, 0f);
    [HideInInspector]
    public Vector3 PosW = new Vector3(-1f,  3f, 0f);
    [HideInInspector]
    public Vector3 PosE = new Vector3( 2f,  3f, 0f);
    [HideInInspector]
    public Vector3 PosA = new Vector3(-4f,  0f, 0f);
    [HideInInspector]
    public Vector3 PosS = new Vector3(-1f,  0f, 0f);
    [HideInInspector]
    public Vector3 PosD = new Vector3( 2f,  0f, 0f);
    [HideInInspector]
    public Vector3 PosZ = new Vector3(-4f, -3f, 0f);
    [HideInInspector]
    public Vector3 PosX = new Vector3(-1f, -3f, 0f);
    [HideInInspector]
    public Vector3 PosC = new Vector3( 2f, -3f, 0f);

    public Transform p_Transform;
    public float delayTime = 0.5f;

    private bool  canMove = true;
    
    [HideInInspector]
    public float timer = 0f;

    public PlayerManager PM;

    [HideInInspector]
    public bool playerAvailable = true;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && canMove && (!PM.playerSmash)) p_Transform.position = PosQ;
        if(Input.GetKeyDown(KeyCode.W) && canMove && (!PM.playerSmash)) p_Transform.position = PosW;
        if(Input.GetKeyDown(KeyCode.E) && canMove && (!PM.playerSmash)) p_Transform.position = PosE;
        if(Input.GetKeyDown(KeyCode.A) && canMove && (!PM.playerSmash)) p_Transform.position = PosA;
        if(Input.GetKeyDown(KeyCode.S) && canMove && (!PM.playerSmash)) p_Transform.position = PosS;
        if(Input.GetKeyDown(KeyCode.D) && canMove && (!PM.playerSmash)) p_Transform.position = PosD;
        if(Input.GetKeyDown(KeyCode.Z) && canMove && (!PM.playerSmash)) p_Transform.position = PosZ;
        if(Input.GetKeyDown(KeyCode.X) && canMove && (!PM.playerSmash)) p_Transform.position = PosX;
        if(Input.GetKeyDown(KeyCode.C) && canMove && (!PM.playerSmash)) p_Transform.position = PosC;

        if(canMove) timer = 0f; // Reset Timer

        if(Input.anyKeyDown && !Input.GetMouseButtonDown(0)) // Has Legal Input
        {
            canMove = false;
        }

        if(!canMove)
        {
            canMove = false;
            if(timer <= delayTime) timer += Time.deltaTime;
            else canMove = true;
            //Debug.Log(timer);
        }

        if(canMove && (!PM.playerSmash)) playerAvailable = true;
        else playerAvailable = false;
    }

}
