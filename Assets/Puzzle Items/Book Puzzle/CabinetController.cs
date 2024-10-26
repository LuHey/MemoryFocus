using System.Diagnostics;
using UnityEngine;

public class CabinetController : MonoBehaviour
{
    public bool isLocked = true; // ��ʼ״̬Ϊ����
    public float openAngle = 90f; // �򿪵ĽǶ�
    public float openSpeed = 2f; // �򿪵��ٶ�
    private bool isOpening = false; // �Ƿ����ڴ�

    private void Update()
    {
        if (isOpening)
        {
            // �Զ��򿪹���
            float currentAngle = Mathf.LerpAngle(transform.localRotation.eulerAngles.y, openAngle, Time.deltaTime * openSpeed);
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);

            // ����Ƿ��ѴﵽĿ��Ƕ�
            if (Mathf.Abs(currentAngle - openAngle) < 0.1f)
            {
                isOpening = false; // ֹͣ��
            }
        }
    }

    public void Unlock()
    {
        isLocked = false;
        UnityEngine.Debug.Log("�����ѽ�����");
    }

    public void Open()
    {
        if (!isLocked)
        {
            isOpening = true;
            UnityEngine.Debug.Log("�������ڴ򿪣�");
        }
        else
        {
            UnityEngine.Debug.Log("���ӱ���ס���޷��򿪣�");
        }
    }
}
