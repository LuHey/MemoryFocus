using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoryTrigger : MonoBehaviour
{
    public GuideTextController guideTextController; // ����GuideTextController
    public string[] storyLines; // Ҫչʾ�ľ����ı�����
    public XRDirectInteractor leftHandInteractor; // ���ֿ������Ľ������
    public XRDirectInteractor rightHandInteractor; // ���ֿ������Ľ������
    public CameraController cameraController; // ����CameraController
    public SceneController sceneController; // ����SceneController

    private int currentLineIndex = 0;
    private bool isStoryPlaying = false;

    public void TriggerStory()
    {
        if (!isStoryPlaying && storyLines.Length > 0)
        {
            isStoryPlaying = true;
            currentLineIndex = 0;
            guideTextController.gameObject.SetActive(true); // ��ʾCanvas
            //cameraController.StartStoryView(); // �����ӽ�
            ShowNextLine();

            // �����ֲ���������������ֹ����ƶ�
            //leftHandInteractor.enabled = false;
            //rightHandInteractor.enabled = false;
        }
    }

    private void ShowNextLine()
    {
        if (currentLineIndex < storyLines.Length)
        {
            guideTextController.ShowGuideText(storyLines[currentLineIndex]);
            currentLineIndex++;
            Invoke("ShowNextLine", guideTextController.displayDuration);
        }
        else
        {
            EndStory();
        }
    }

    private void EndStory()
    {
        isStoryPlaying = false;
        guideTextController.HideGuideText();
        //cameraController.EndStoryView(); // �ָ��ӽ�

        // �ָ��ֲ��������Ľ���
        //leftHandInteractor.enabled = true;
        //rightHandInteractor.enabled = true;
        if (sceneController != null)
        {
            sceneController.LoadNextScene(); // ������һ������
            guideTextController.ShowGuideText("Loading..."); // ��ʾ������ʾ
        }
            

    }
}
