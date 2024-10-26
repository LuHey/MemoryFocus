using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BookSocket : MonoBehaviour
{
    public string requiredBookID;
    private Book currentBook;
    private XRSocketInteractor socketInteractor;
    public PuzzleManager puzzleManager; // 引用PuzzleManager

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
            puzzleManager.CheckSolution(); // 书放入时检测解谜状态
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Book book = args.interactableObject.transform.GetComponent<Book>();
        if (book != null && currentBook == book)
        {
            currentBook = null;
            puzzleManager.CheckSolution(); // 书取出时检测解谜状态
        }
    }

    // 检查当前插槽是否填满
    public bool IsFilled()
    {
        return currentBook != null;
    }

    // 检查放置的书是否正确
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
