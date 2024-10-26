using System.Diagnostics;
using UnityEngine;

public class CabinetController : MonoBehaviour
{
    public bool isLocked = true; // 初始状态为锁定
    public float openAngle = 90f; // 打开的角度
    public float openSpeed = 2f; // 打开的速度
    private bool isOpening = false; // 是否正在打开

    private void Update()
    {
        if (isOpening)
        {
            // 自动打开柜门
            float currentAngle = Mathf.LerpAngle(transform.localRotation.eulerAngles.y, openAngle, Time.deltaTime * openSpeed);
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);

            // 检查是否已达到目标角度
            if (Mathf.Abs(currentAngle - openAngle) < 0.1f)
            {
                isOpening = false; // 停止打开
            }
        }
    }

    public void Unlock()
    {
        isLocked = false;
        UnityEngine.Debug.Log("柜子已解锁！");
    }

    public void Open()
    {
        if (!isLocked)
        {
            isOpening = true;
            UnityEngine.Debug.Log("柜子正在打开！");
        }
        else
        {
            UnityEngine.Debug.Log("柜子被锁住，无法打开！");
        }
    }
}
