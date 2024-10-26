using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoryTrigger : MonoBehaviour
{
    public GuideTextController guideTextController; // 引用GuideTextController
    public string[] storyLines; // 要展示的剧情文本数组
    public XRDirectInteractor leftHandInteractor; // 左手控制器的交互组件
    public XRDirectInteractor rightHandInteractor; // 右手控制器的交互组件
    public CameraController cameraController; // 引用CameraController
    public SceneController sceneController; // 引用SceneController

    private int currentLineIndex = 0;
    private bool isStoryPlaying = false;

    public void TriggerStory()
    {
        if (!isStoryPlaying && storyLines.Length > 0)
        {
            isStoryPlaying = true;
            currentLineIndex = 0;
            guideTextController.gameObject.SetActive(true); // 显示Canvas
            //cameraController.StartStoryView(); // 调整视角
            ShowNextLine();

            // 禁用手部控制器交互，防止玩家移动
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
        //cameraController.EndStoryView(); // 恢复视角

        // 恢复手部控制器的交互
        //leftHandInteractor.enabled = true;
        //rightHandInteractor.enabled = true;
        if (sceneController != null)
        {
            sceneController.LoadNextScene(); // 加载下一个场景
            guideTextController.ShowGuideText("Loading..."); // 显示加载提示
        }
            

    }
}
