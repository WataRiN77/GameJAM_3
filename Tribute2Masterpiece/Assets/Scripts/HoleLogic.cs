using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleLogic : MonoBehaviour
{
    public float minSpawnCD = 1.0f;
    public float maxSpawnCD = 2.0f;
    public GameObject m_Gopher;
    public float InitLifeTime = 1.0f; //Init lifetime
    public float LifeTimeGain = 1.0f; //Gain extra lifetime if hit
    public char HitKey = 'Q';   //Define the keycode
    public bool DifficultyIncrease = true;
    public float IncreaseDiffBySeconds = 10.0f;
    public float IncreaseTimeScale = 0.8f;

    public GameManager GM;

    float spawnCD;
    // Start is called before the first frame update
    void Start()
    {
        spawnCD = Random.Range(minSpawnCD, maxSpawnCD);
        spawnCD += Random.Range(0, maxSpawnCD);
        spawnCD %= maxSpawnCD;
        InvokeRepeating("IncreaseDifficulty", 0f, IncreaseDiffBySeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCD <= 0 /*NEW added*/&& !GM.GameEnd)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 1f;
            GameObject m_gopher = Instantiate(m_Gopher, spawnPosition, Quaternion.identity);
            GopherLogic m_gopherLogic = m_gopher.GetComponent<GopherLogic>();
            m_gopherLogic.GopherSetup(InitLifeTime, LifeTimeGain, HitKey);
            spawnCD = Random.Range(minSpawnCD, maxSpawnCD);
        }
        else
        {
            spawnCD -= Time.deltaTime;
        }
    }

    void IncreaseDifficulty()
    {
        // Debug.Log("111");
        if (DifficultyIncrease)
            if (minSpawnCD > 2)
            {
                minSpawnCD *= IncreaseTimeScale;
                maxSpawnCD = Mathf.Max(maxSpawnCD * IncreaseTimeScale, minSpawnCD + 1.0f);
            }
    }
}
