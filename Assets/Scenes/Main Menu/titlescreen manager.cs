using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public AudioClip introClip;

    public RawImage blackScreen;
    public Image titleScreenImage;
    public TextMeshProUGUI teamText;
    public TextMeshProUGUI pressAnyKeyText;

    private bool canPressKey = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        titleScreenImage.gameObject.SetActive(true);
        titleScreenImage.color = new Color(1, 1, 1, 0); // Inizia trasparente
        pressAnyKeyText.gameObject.SetActive(false);
        teamText.alpha = 0f;
        blackScreen.color = new Color(0, 0, 0, 1); // forza nero pieno all'avvio
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        // Avvia canzone
        audioSource.clip = introClip;
        audioSource.Play();

        // Aspetta 1s, poi mostra "Team Hyrule Production" con fade-in
        yield return new WaitForSeconds(1f);
        teamText.gameObject.SetActive(true);
        yield return StartCoroutine(FadeInText(teamText, 0.5f));

        // Inizia dissolvenza prima della transizione
        yield return new WaitForSeconds(4.39f); // fino a 5.89s, lasciando 0.5s per la dissolvenza
        StartCoroutine(FadeOutText(teamText, 0.5f));

        // Aspetta fino a 6.39s
        yield return new WaitForSeconds(0.5f);

        // Dissolvenza del blackScreen verso trasparente
        yield return StartCoroutine(FadeOutRawImage(blackScreen, 0.4f));

        // Mostra title screen con fade-in più veloce
        yield return StartCoroutine(FadeInImage(titleScreenImage, 0.4f));

        // Aspetta un attimo prima di mostrare "Press Any Button"
        yield return new WaitForSeconds(0.2f);

        pressAnyKeyText.gameObject.SetActive(true);
        StartCoroutine(BlinkPressAnyKey());

        canPressKey = true;
    }

    IEnumerator FadeInText(TextMeshProUGUI text, float duration)
    {
        float rate = 1.0f / duration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            text.alpha = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        text.alpha = 1;
    }

    IEnumerator FadeOutText(TextMeshProUGUI text, float duration)
    {
        float startAlpha = text.alpha;
        float rate = 1.0f / duration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            text.alpha = Mathf.Lerp(startAlpha, 0, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        text.alpha = 0;
    }

    IEnumerator FadeInImage(Image image, float duration)
    {
        float rate = 1.0f / duration;
        float progress = 0.0f;
        Color startColor = image.color;

        while (progress < 1.0f)
        {
            float alpha = Mathf.Lerp(0, 1, progress);
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 1);
    }

    IEnumerator FadeOutRawImage(RawImage image, float duration)
    {
        float rate = 1.0f / duration;
        float progress = 0.0f;
        Color startColor = image.color;

        while (progress < 1.0f)
        {
            float alpha = Mathf.Lerp(startColor.a, 0, progress);
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 0);
    }

    IEnumerator BlinkPressAnyKey()
    {
        while (true)
        {
            pressAnyKeyText.alpha = 1f;
            yield return new WaitForSeconds(0.5f);
            pressAnyKeyText.alpha = 0f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        if (canPressKey && Input.anyKeyDown)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
