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


    // Start is called before the first frame update
    void Start()
    {
        m_life = InitLifeTime;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_life <= 0f)
        {
            m_state = GopherState.Miss;
        }
        if (Input.GetKeyDown(KeyCode.None + 32 + HitKey) && m_state != GopherState.Dead)
        {
            BeHit();
        }

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
