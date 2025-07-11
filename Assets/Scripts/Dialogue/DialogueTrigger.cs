using System;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private DialogueData _dialogueData;
    public UnityEvent OnEndDialogue;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SetCurrentDialogueData();
        }
    }
    
    void SetCurrentDialogueData()
    {
        if(_dialogueManager) _dialogueManager.currentDialogueData = _dialogueData;
        if(_dialogueManager) _dialogueManager.StartDialogue();
        if(_dialogueManager) _dialogueManager.SetEventEndDialogue(this);
    }
}
