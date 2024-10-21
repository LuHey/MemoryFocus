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

        // 保存初始位置和旋转
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;

        // 订阅事件
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // 重置物体位置和旋转
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
    }

    private void OnDestroy()
    {
        // 取消订阅事件
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }
}
