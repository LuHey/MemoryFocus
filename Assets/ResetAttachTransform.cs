using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetAttachTransform : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // �����ʼλ�ú���ת
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;

        // �����¼�
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // ��������λ�ú���ת
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
    }

    private void OnDestroy()
    {
        // ȡ�������¼�
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }
}
