using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetViewpoint; // ����ʱ�Ĺ̶��ӽ�
    public Transform playerCamera; // ��������ͷ�������
    public float transitionSpeed = 2.0f; // �ӽ��л����ٶ�

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isAdjusting = false;
    private bool isReturning = false;

    private void Start()
    {
        // �����ʼλ�ú���ת
        originalPosition = playerCamera.position;
        originalRotation = playerCamera.rotation;
    }

    public void StartStoryView()
    {
        // ��ʼ����ʱ�������ӽ�
        isAdjusting = true;
        isReturning = false;
    }

    public void EndStoryView()
    {
        // �������ʱ���ָ��ӽ�
        isReturning = true;
        isAdjusting = false;
    }

    private void Update()
    {
        if (isAdjusting)
        {
            // �л���Ŀ���ӽ�
            playerCamera.position = Vector3.Lerp(playerCamera.position, targetViewpoint.position, Time.deltaTime * transitionSpeed);
            playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, targetViewpoint.rotation, Time.deltaTime * transitionSpeed);

            // ����ӽ�Ŀ��λ�ú���ת��ֹͣ����
            if (Vector3.Distance(playerCamera.position, targetViewpoint.position) < 0.01f &&
                Quaternion.Angle(playerCamera.rotation, targetViewpoint.rotation) < 1.0f)
            {
                isAdjusting = false;
            }
        }

        if (isReturning)
        {
            // ���س�ʼ�ӽ�
            playerCamera.position = Vector3.Lerp(playerCamera.position, originalPosition, Time.deltaTime * transitionSpeed);
            playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, originalRotation, Time.deltaTime * transitionSpeed);

            // ����ӽ���ʼλ�ú���ת��ֹͣ�ָ�
            if (Vector3.Distance(playerCamera.position, originalPosition) < 0.01f &&
                Quaternion.Angle(playerCamera.rotation, originalRotation) < 1.0f)
            {
                isReturning = false;
            }
        }
    }
}
