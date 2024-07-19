using System.Collections;
using UnityEngine;

public class boomeffectLogic : MonoBehaviour
{
    public float initialScale = 0.1f; // 初始缩放比例
    public float targetScale = 1.0f; // 目标缩放比例
    public float scaleTime = 0.5f; // 缩放时间
    public float fadeTime = 1.0f; // 淡出时间

    private SpriteRenderer spriteRenderer;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f); // 初始透明度为0
        transform.localScale = new Vector3(initialScale, initialScale, initialScale); // 初始缩放
        StartCoroutine(PopEffect());
    }

    private void OnEnable()
    {
        startTime = Time.time;
        StartCoroutine(PopEffect());
    }

    IEnumerator PopEffect()
    {
        // 缩放到目标大小并透明度到1
        while (Time.time - startTime < scaleTime)
        {
            float t = (Time.time - startTime) / scaleTime;
            transform.localScale = new Vector3(Mathf.Lerp(initialScale, targetScale, t), Mathf.Lerp(initialScale, targetScale, t), Mathf.Lerp(initialScale, targetScale, t));
            spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, t));
            yield return null;
        }

        // 透明度到0
        startTime = Time.time;
        while (Time.time - startTime < fadeTime)
        {
            float t = (Time.time - startTime) / fadeTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, t));
            yield return null;
        }

        // 销毁物体
        //Destroy(gameObject);
    }
}