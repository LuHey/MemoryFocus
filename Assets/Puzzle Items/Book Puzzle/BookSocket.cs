using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BookSocket : MonoBehaviour
{
    public string requiredBookID;
    private Book currentBook;
    private XRSocketInteractor socketInteractor;
    public PuzzleManager puzzleManager; // ����PuzzleManager

    private void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        socketInteractor.selectEntered.AddListener(OnSelectEntered);
        socketInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Book book = args.interactableObject.transform.GetComponent<Book>();
        if (book != null)
        {
            currentBook = book;
            CheckIfCorrect();
            puzzleManager.CheckSolution(); // �����ʱ������״̬
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Book book = args.interactableObject.transform.GetComponent<Book>();
        if (book != null && currentBook == book)
        {
            currentBook = null;
            puzzleManager.CheckSolution(); // ��ȡ��ʱ������״̬
        }
    }

    // ��鵱ǰ����Ƿ�����
    public bool IsFilled()
    {
        return currentBook != null;
    }

    // �����õ����Ƿ���ȷ
    public bool IsCorrectBook()
    {
        return currentBook != null && currentBook.bookID == requiredBookID;
    }

    private void CheckIfCorrect()
    {
        if (IsCorrectBook())
        {
            UnityEngine.Debug.Log("Books are properly placed in slots");
        }
        else
        {
            UnityEngine.Debug.Log("The book is incorrect, please put in the correct book");
        }
    }
}
