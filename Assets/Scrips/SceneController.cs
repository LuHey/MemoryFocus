using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.ComponentModel;

public class SceneController : MonoBehaviour
{
    public FadeEffect fadeEffect; // ���õ��뵭��Ч���Ľű�
    public float lightResetDelay = 1.0f; // ���س���ǰ���ӳ�ʱ��

    // ������һ�������ķ���
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

    // �����������س����ķ���
    public void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadSceneWithFade(sceneIndex));
        }
    }

    // ʹ�õ��뵭�����س�����Э��
    private IEnumerator LoadSceneWithFade(int sceneIndex)
    {
        // 1. ִ�е���Ч����ʹ�����𽥱��
        //yield return StartCoroutine(fadeEffect.FadeIn());

        // 2. ���ù������ã�ȷ���л����³���ʱ����������ȷ
        //ResetLightingSettings();

        // 3. �첽���س������������Զ�����
        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;

        // 4. �ȴ����ؽ��ȴﵽ90%
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // 5. �����ӳ٣�ȷ��������Դ������ȫ
        yield return new WaitForSeconds(lightResetDelay);

        // 6. �����
        asyncLoad.allowSceneActivation = true;

        // 7. �ȴ������������
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 8. ����������ɺ�ִ�е���Ч����ʹ�����𽥱���
        //yield return StartCoroutine(fadeEffect.FadeOut());
    }

    // ����Lighting���õķ�����ȷ���л�ʱ������ȷ
    private void ResetLightingSettings()
    {
        // ���û������ȫ�ֹ���
        RenderSettings.ambientIntensity = 1.0f;
        RenderSettings.ambientLight = Color.white;
        DynamicGI.UpdateEnvironment();
    }
}
