using System.Diagnostics;
using UnityEngine;

public class ToggleHelpMenu : MonoBehaviour
{
    public GameObject helpCanvas; // 拖拽需要显示/隐藏的Canvas到这里

    // 这个方法用于切换Canvas的显示状态
    public void ToggleHelp()
    {
        if (helpCanvas != null)
        {
            // 切换Canvas的激活状态
            helpCanvas.SetActive(!helpCanvas.activeSelf);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Help Canvas is not assigned in the inspector.");
        }
    }
}
