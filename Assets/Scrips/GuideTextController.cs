using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.ComponentModel;

public class GuideTextController : MonoBehaviour
{
    public TextMeshProUGUI guideText; // 引用要显示的Text (TMP)
    public float displayDuration = 5.0f; // 显示时长
    public string[] initialMessageList;
    public string initialMessage = "Welcome to the game! If you are playing the game for the first time, hold down the A key to point to Help and press the back trigger key to select."; // 初始提示文字

    private float timer = 0f;
    private bool isDisplaying = false;
    private int currentLineIndex = 0;
    public SceneController sceneController;

    private void Start()
    {
        //if (currentLineIndex < initialMessageList.Length)
        //{
        //    ShowGuideText(initialMessageList[currentLineIndex]);
        //    currentLineIndex++;
        //    Invoke("ShowNextLine", guideTextController.displayDuration);
        //}

        // 初始化时显示初始提示文字
        

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            ShowNextLine();
            //sceneController.LoadNextScene();
        }
        else
        {
            ShowNextLine();
        }
    }

    private void ShowNextLine()
    {
        if (currentLineIndex < initialMessageList.Length)
        {
            ShowGuideText(initialMessageList[currentLineIndex]);
            currentLineIndex++;
            Invoke("ShowNextLine", displayDuration);
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                //ShowNextLine();
                sceneController.LoadNextScene();
            }
        }
    }

    private void Update()
    {
        if (isDisplaying)
        {
            // 更新计时器
            timer += Time.deltaTime;
            if (timer >= displayDuration)
            {
                // 达到显示时长后隐藏Canvas
                HideGuideText();
            }
        }
    }

    public void ShowGuideText(string message)
    {
        // 设置引导文字内容
        guideText.text = message;
        // 显示Canvas
        gameObject.SetActive(true);
        // 重置计时器
        timer = 0f;
        // 设置为正在显示状态
        isDisplaying = true;
    }

    public void HideGuideText() // 修改为public
    {
        // 隐藏Canvas
        gameObject.SetActive(false);
        // 重置状态
        isDisplaying = false;
    }
}
