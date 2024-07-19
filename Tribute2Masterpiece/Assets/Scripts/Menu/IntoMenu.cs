using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoMenu : MonoBehaviour
{
    public GameObject targetObject;
    public float delay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableObjectAfterDelay(delay));
    }

    IEnumerator DisableObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        targetObject.SetActive(false);
    }
}