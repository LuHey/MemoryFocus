using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetViewpoint; // 剧情时的固定视角
    public Transform playerCamera; // 玩家相机（头显相机）
    public float transitionSpeed = 2.0f; // 视角切换的速度

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isAdjusting = false;
    private bool isReturning = false;

    private void Start()
    {
        // 保存初始位置和旋转
        originalPosition = playerCamera.position;
        originalRotation = playerCamera.rotation;
    }

    public void StartStoryView()
    {
        // 开始剧情时，调整视角
        isAdjusting = true;
        isReturning = false;
    }

    public void EndStoryView()
    {
        // 剧情结束时，恢复视角
        isReturning = true;
        isAdjusting = false;
    }

    private void Update()
    {
        if (isAdjusting)
        {
            // 切换到目标视角
            playerCamera.position = Vector3.Lerp(playerCamera.position, targetViewpoint.position, Time.deltaTime * transitionSpeed);
            playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, targetViewpoint.rotation, Time.deltaTime * transitionSpeed);

            // 如果接近目标位置和旋转，停止调整
            if (Vector3.Distance(playerCamera.position, targetViewpoint.position) < 0.01f &&
                Quaternion.Angle(playerCamera.rotation, targetViewpoint.rotation) < 1.0f)
            {
                isAdjusting = false;
            }
        }

        if (isReturning)
        {
            // 返回初始视角
            playerCamera.position = Vector3.Lerp(playerCamera.position, originalPosition, Time.deltaTime * transitionSpeed);
            playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, originalRotation, Time.deltaTime * transitionSpeed);

            // 如果接近初始位置和旋转，停止恢复
            if (Vector3.Distance(playerCamera.position, originalPosition) < 0.01f &&
                Quaternion.Angle(playerCamera.rotation, originalRotation) < 1.0f)
            {
                isReturning = false;
            }
        }
    }
}
