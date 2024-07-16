using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GopherLogic : MonoBehaviour
{
    enum GopherState
    {
        Idle,
        HalfLife,
        Dead,
        Miss
    };

    GopherState m_state = GopherState.Idle;
    float InitLifeTime = 1.0f; //Init lifetime
    float LifeTimeGain = 1.0f; //Gain extra lifetime if hit
    float m_life;
    char HitKey = 'Q';   //Define the keycode
    SpriteRenderer m_spriteRenderer;

/*NEW added*/
    PlayerALogic PAL;
    PlayerBLogic PBL;
    GameObject PlayerA; // Using KEYBOARD
    GameObject PlayerB; // Using MOUSE
    Vector3 mousePos;
/*NEW added*/

    // Start is called before the first frame update
    void Start()
    {
        m_life = InitLifeTime;
        m_spriteRenderer = GetComponent<SpriteRenderer>();

/*NEW added*/
        PlayerA = GameObject.FindWithTag("PlayerA");
        PlayerB = GameObject.FindWithTag("PlayerB");
        PAL = PlayerA.GetComponent<PlayerALogic>();
        PBL = PlayerB.GetComponent<PlayerBLogic>();
/*NEW added*/

    }

    // Update is called once per frame
    void Update()
    {
        if (m_life <= 0f)
        {
            m_state = GopherState.Miss;
        }
        if (Input.GetKeyDown(KeyCode.None + 32 + HitKey) && m_state != GopherState.Dead /*NEW added*/&& PAL.timer <= Time.deltaTime)
        {
            BeHit();
        }

/*NEW added*/
        if (Input.GetMouseButtonDown(0)) // LeftClick
        {
            // Mouse Position
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
            if(Vector3.Distance(mousePos, this.gameObject.GetComponent<Transform>().position) <= PBL.tolerance && PBL.timer <= Time.deltaTime)
            {
                BeHit();
            }
        }
/*NEW added*/

        m_life -= Time.deltaTime;

        StateHandler();
    }

    public void GopherSetup(float m_InitLifeTime, float m_LifeTimeGain, char m_HitKey)
    {
        InitLifeTime = m_InitLifeTime;
        LifeTimeGain = m_LifeTimeGain;
        HitKey = m_HitKey;
    }

    void Miss()
    {
        Destroy(gameObject);
    }
    void BeHit()
    {
        m_state += 1;
        m_life += LifeTimeGain;
    }
    void Die()
    {
        Destroy(gameObject);
    }
    void StateHandler()
    {
        switch (m_state)
        {
            case GopherState.Idle:
                break;
            case GopherState.HalfLife:
                m_spriteRenderer.color = Color.yellow;
                break;
            case GopherState.Dead:
                // Debug.Log("Dead");
                Die();
                break;
            case GopherState.Miss:
                // Debug.Log("Miss");
                Miss();
                break;
        }
    }
}
