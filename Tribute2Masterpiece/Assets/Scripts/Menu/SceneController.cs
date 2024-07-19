using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public AudioSource audioSource; // ����AudioSource���
    public float fadeDuration ; // �������ʱ��
    public GameObject into,UI;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(FadeOutAndLoadScene(nextSceneIndex));
        }
        else
        {
            nextSceneIndex = 0;
            StartCoroutine(FadeOutAndLoadScene(nextSceneIndex));
        }
    }

    private System.Collections.IEnumerator FadeOutAndLoadScene(int sceneIndex)
    {
        into.SetActive(true); // ���� GameObject
        float elapsedTime = 0f;
        float startVolume = audioSource.volume;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // ���س���
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void intoGame()
    {
        UI.SetActive(true);
    }
}