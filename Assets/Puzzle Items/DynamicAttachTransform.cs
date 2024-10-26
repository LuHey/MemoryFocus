using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DynamicAttachTransform : MonoBehaviour
{
    public Transform leftHandAttach; // 左手抓取点
    //public Transform rightHandAttach; // 右手抓取点
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 监听Select Entered事件
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDestroy()
    {
        // 移除事件监听
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // 检查是哪只手抓取
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null)
        {
            if (interactor.name.Contains("LeftHand"))
            {
                // 设置为左手抓取点
                grabInteractable.attachTransform = leftHandAttach;
            }
            else if (interactor.name.Contains("RightHand"))
            {
                // 设置为右手抓取点
                // 动态设置右手的方向为左手的镜像
                //Transform rightHandAttach = new Transform();
                //rightHandAttach.position = leftHandAttach.position;
                //rightHandAttach.rotation = Quaternion.Inverse(leftHandAttach.rotation);
                grabInteractable.attachTransform = leftHandAttach;
                grabInteractable.attachTransform.rotation = Quaternion.Inverse(leftHandAttach.rotation);
            }
        }
    }


    private void OnSelectExited(SelectExitEventArgs args)
    {
        // 释放时重置为默认的抓取点
        grabInteractable.attachTransform = null;
    }
}
