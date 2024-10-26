using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.ComponentModel;

public class SceneController : MonoBehaviour
{
    public FadeEffect fadeEffect; // 引用淡入淡出效果的脚本
    public float lightResetDelay = 1.0f; // 加载场景前的延迟时间

    // 加载下一个场景的方法
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 3)
        {
            currentSceneIndex = -1;
        }
        //if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        //{
        //    StartCoroutine(LoadSceneWithFade(currentSceneIndex + 1));
        //}
        StartCoroutine(LoadSceneWithFade(currentSceneIndex + 1));
    }

    // 根据索引加载场景的方法
    public void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadSceneWithFade(sceneIndex));
        }
    }

    // 使用淡入淡出加载场景的协程
    private IEnumerator LoadSceneWithFade(int sceneIndex)
    {
        // 1. 执行淡入效果，使画面逐渐变黑
        //yield return StartCoroutine(fadeEffect.FadeIn());

        // 2. 重置光照设置，确保切换到新场景时环境光照正确
        //ResetLightingSettings();

        // 3. 异步加载场景，并禁用自动激活
        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;

        // 4. 等待加载进度达到90%
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // 5. 增加延迟，确保场景资源加载完全
        yield return new WaitForSeconds(lightResetDelay);

        // 6. 激活场景
        asyncLoad.allowSceneActivation = true;

        // 7. 等待场景加载完成
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 8. 场景加载完成后执行淡出效果，使画面逐渐变亮
        //yield return StartCoroutine(fadeEffect.FadeOut());
    }

    // 重置Lighting设置的方法，确保切换时光照正确
    private void ResetLightingSettings()
    {
        // 重置环境光和全局光照
        RenderSettings.ambientIntensity = 1.0f;
        RenderSettings.ambientLight = Color.white;
        DynamicGI.UpdateEnvironment();
    }
}
