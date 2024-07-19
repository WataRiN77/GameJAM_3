using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] smashSounds; // Array of smash sounds
    private AudioSource[] audioSources; // Array of AudioSources for playing sounds
    public GameObject Boom;
    public GameObject PlayerManager;
    private PlayerManager playerManager;

    public float shakeDuration = 0.1f; // Duration of the shake
    public float shakeAmount = 0.1f; // Amount of shake
    public float decreaseFactor = 1.0f; // Decrease factor of the shake

    private Vector3 originalCameraPosition; // Original position of the camera
    private float shakeTimer = 0f; // Timer for the shake effect

    // Start is called before the first frame update
    void Start()
    {
        audioSources = new AudioSource[3];
        for (int i = 0; i < 3; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
        }
        playerManager = PlayerManager.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    public void PlayerBoom()
    {
        int randomIndex = Random.Range(0, smashSounds.Length); // Randomly select an index from the array
        if (!audioSources[randomIndex].isPlaying)
        {
            audioSources[randomIndex].PlayOneShot(smashSounds[randomIndex]); // Play the selected sound
        }
        originalCameraPosition = Camera.main.transform.position;
        shakeTimer = shakeDuration;
        StartCoroutine(ShakeCamera());
        Boom.transform.position = Vector3.Lerp(playerManager.posA.position, playerManager.posB.position, 0.5f);
        Boom.SetActive(true);
    }

    public void reCarema()
    {
        originalCameraPosition = new Vector3(0f, 0f, -10f);
        Camera.main.transform.position = originalCameraPosition;
    }
    IEnumerator ShakeCamera()
    {
        while (shakeTimer > 0)
        {
            Camera.main.transform.position = originalCameraPosition + Random.insideUnitSphere * shakeAmount;
            shakeTimer -= Time.deltaTime * decreaseFactor;
            yield return null;
        }

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);


    }
}
