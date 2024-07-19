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
    public float lifeAfterDeath = 0.5f;
    char HitKey = 'Q';   //Define the keycode
    SpriteRenderer m_spriteRenderer;
    public Sprite m_idleSprite;
    public Sprite m_hitBlueSprite;
    public Sprite m_hitOrangeSprite;
    public Sprite m_DieSprite;
    public GameObject m_missSFX;
    public GameObject m_hitSFX;
    public GameObject m_dieSFX;

    /*NEW added*/
    PlayerALogic PAL;
    PlayerBLogic PBL;
    GameObject PlayerA; // Using KEYBOARD
    GameObject PlayerB; // Using MOUSE
    Vector3 mousePos;
    GameObject GameMgr;
    GameManager GM;
    GameObject PlayerMgr;
    PlayerManager PM;
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

        GameMgr = GameObject.FindWithTag("GameMgr");
        PlayerMgr = GameObject.FindWithTag("PlayerMgr");
        GM = GameMgr.GetComponent<GameManager>();
        PM = PlayerMgr.GetComponent<PlayerManager>();
        /*NEW added*/

    }

    // Update is called once per frame
    void Update()
    {
        if (m_life <= 0f)
        {
            Instantiate(m_missSFX, transform.position, Quaternion.identity);
            m_state = GopherState.Miss;
        }
        if (Input.GetKeyDown(KeyCode.None + 208 + HitKey) /*NEW added*/&& PAL.timer <= Time.deltaTime)
        {
            if (m_spriteRenderer.sprite != m_hitOrangeSprite)
            {
                BeHit(true);
            }
        }

        /*NEW added*/
        if (Input.GetMouseButtonDown(0)) // LeftClick
        {
            // Mouse Position
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
            if (Vector3.Distance(mousePos, this.gameObject.GetComponent<Transform>().position) <= PBL.tolerance && PBL.timer <= Time.deltaTime)
            {
                if (m_spriteRenderer.sprite != m_hitBlueSprite)
                {
                    BeHit(false);
                }
            }
        }
        /*NEW added*/
        if (m_life > 0.0f)
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
    void BeHit(bool isOrange)
    {
        bool strictSmash = (PBL.gameObject.transform.position.x - PAL.gameObject.transform.position.x) == 2 && PBL.gameObject.transform.position.y == PAL.gameObject.transform.position.y;
        //strange bug occur when not using strictSmash, 1st orange then blue won't get score, but the opposite does
        if (m_state != GopherState.Dead && !PM.playerSmash && !strictSmash)
        {
            // Debug.Log("hit");
            if (m_state == GopherState.Idle)
            {
                Instantiate(m_hitSFX, transform.position, Quaternion.identity);
            }
            else if (m_state == GopherState.HalfLife)
            {
                Instantiate(m_dieSFX, transform.position, Quaternion.identity);
            }
            ChangeSprite(isOrange);
            m_state += 1;
            m_life += LifeTimeGain;
        }   // previously dead gophers can be hit, now fixed, should we hit the dead gophers for fun?
    }
    void Die()
    {
        m_spriteRenderer.sprite = m_DieSprite;
        if (lifeAfterDeath <= 0)
        {
            GM.Score = GM.Score + 1;
            Destroy(gameObject);
        }
        lifeAfterDeath -= Time.deltaTime;
    }
    void StateHandler()
    {
        switch (m_state)
        {
            case GopherState.Idle:
                break;
            case GopherState.HalfLife:
                // m_spriteRenderer.color = Color.yellow;
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

    public void ChangeSprite(bool isOrange)
    {
        if (isOrange)
        {
            m_spriteRenderer.sprite = m_hitOrangeSprite;
        }
        else
        {
            m_spriteRenderer.sprite = m_hitBlueSprite;
        }
    }

}
