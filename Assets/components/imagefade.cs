using UnityEngine;
using UnityEngine.UI;

public class imagefade : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 1.0f;
    public float delayBetweenFades = 1.0f;

    private bool fadingIn = true;
    private float currentAlpha = 0.0f;
    private float timer = 0.0f;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, currentAlpha);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delayBetweenFades)
        {
            timer = 0.0f;
            fadingIn = !fadingIn;
        }

        if (fadingIn)
        {
            currentAlpha += Time.deltaTime / fadeDuration;
        }
        else
        {
            currentAlpha -= Time.deltaTime / fadeDuration;
        }

        currentAlpha = Mathf.Clamp01(currentAlpha);
        image.color = new Color(image.color.r, image.color.g, image.color.b, currentAlpha);
    }
}