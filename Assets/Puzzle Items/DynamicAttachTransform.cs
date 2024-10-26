using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DynamicAttachTransform : MonoBehaviour
{
    public Transform leftHandAttach; // ����ץȡ��
    //public Transform rightHandAttach; // ����ץȡ��
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // ����Select Entered�¼�
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDestroy()
    {
        // �Ƴ��¼�����
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // �������ֻ��ץȡ
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null)
        {
            if (interactor.name.Contains("LeftHand"))
            {
                // ����Ϊ����ץȡ��
                grabInteractable.attachTransform = leftHandAttach;
            }
            else if (interactor.name.Contains("RightHand"))
            {
                // ����Ϊ����ץȡ��
                // ��̬�������ֵķ���Ϊ���ֵľ���
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
        // �ͷ�ʱ����ΪĬ�ϵ�ץȡ��
        grabInteractable.attachTransform = null;
    }
}
