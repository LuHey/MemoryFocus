using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.ComponentModel;

public class GuideTextController : MonoBehaviour
{
    public TextMeshProUGUI guideText; // ����Ҫ��ʾ��Text (TMP)
    public float displayDuration = 5.0f; // ��ʾʱ��
    public string[] initialMessageList;
    public string initialMessage = "Welcome to the game! If you are playing the game for the first time, hold down the A key to point to Help and press the back trigger key to select."; // ��ʼ��ʾ����

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

        // ��ʼ��ʱ��ʾ��ʼ��ʾ����
        

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
            // ���¼�ʱ��
            timer += Time.deltaTime;
            if (timer >= displayDuration)
            {
                // �ﵽ��ʾʱ��������Canvas
                HideGuideText();
            }
        }
    }

    public void ShowGuideText(string message)
    {
        // ����������������
        guideText.text = message;
        // ��ʾCanvas
        gameObject.SetActive(true);
        // ���ü�ʱ��
        timer = 0f;
        // ����Ϊ������ʾ״̬
        isDisplaying = true;
    }

    public void HideGuideText() // �޸�Ϊpublic
    {
        // ����Canvas
        gameObject.SetActive(false);
        // ����״̬
        isDisplaying = false;
    }
}
