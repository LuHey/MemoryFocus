using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

public class FadeEffect : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage; // 引用全屏的Image组件
    public float fadeDuration = 1.0f; // 淡入淡出的持续时间

    private void Start()
    {
        // 初始状态为淡出
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 1);
            StartCoroutine(FadeOut());
        }
    }

    // 淡入方法，使画面逐渐变黑
    public IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            if (fadeImage != null)
            {
                fadeImage.color = new Color(0, 0, 0, alpha);
            }
            yield return null;
        }
    }

    // 淡出方法，使画面逐渐变亮
    public IEnumerator FadeOut()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsed / fadeDuration);
            if (fadeImage != null)
            {
                fadeImage.color = new Color(0, 0, 0, alpha);
            }
            yield return null;
        }
    }
}
