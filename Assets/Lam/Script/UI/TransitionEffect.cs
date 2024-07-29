using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionEffect : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float fadeDuration = 3f;
    [SerializeField] private bool isFadeIn;

    private void Start() 
    {
        if (isFadeIn)
        {
            FadeIn();
        } 
        else
        {
            FadeOut();
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInImage(image, 0, 1, fadeDuration));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutImage(image, 1, 0, fadeDuration));
    }

    private IEnumerator FadeInImage(Image img, float start, float end, float duration)
    {
        image.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color color = img.color;
        Debug.Log("Start ");
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(start, end, elapsedTime / duration);
            img.color = color;
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator FadeOutImage(Image img, float start, float end, float duration)
    {
        image.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color color = img.color;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(start, end, elapsedTime / duration);
            img.color = color;
            yield return null;
        }
        image.gameObject.SetActive(false);
    }
}
