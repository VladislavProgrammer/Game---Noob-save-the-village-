using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public DialogueData currentDialogueData;
    [SerializeField] private TextMeshProUGUI textView;

    [SerializeField] private TextMeshProUGUI _nameTextView;
    [HideInInspector] public int currentPhraseIndex;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float textSpeed = 0.01f;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject dialogBG;
    [SerializeField] private Image profileIconView;
    
    private string currentString;

    private string currentName;
    private AudioClip currentAudioClip;
    private bool  canNextPhrase;
    private bool dialogInProcess;
    public bool CanTalk;
    
    private DialogueTrigger _dialogueTrigger;
    
    private void Awake() => dialogBG.SetActive(false);

    void SetCurrentProfileIcon()
    {
        profileIconView.sprite = currentDialogueData.personIcon;
    }

    void SetCurrentName()
    {
        currentName = DialogueLocalization.Instance.GetLocalizedName(currentDialogueData.NameRu,
            currentDialogueData.NameEn);
        _nameTextView.text = currentName;
    }
    
    void SetCurrentStringText()
    {
        currentString = DialogueLocalization.Instance.GetLocalizedText(
            currentDialogueData.Phrases[currentPhraseIndex].textRu,
            currentDialogueData.Phrases[currentPhraseIndex].textEn);
    }
    
    
    void SetCurrentAudio()
    {
        currentAudioClip = DialogueLocalization.Instance.GetLocalizedAudio(
            currentDialogueData.Phrases[currentPhraseIndex].audioRu,
            currentDialogueData.Phrases[currentPhraseIndex].audioEn);
        PlayCurrentAudio();
    }

    void PlayCurrentAudio()
    {
        int soundIndex = PlayerPrefs.GetInt("soundIndex");

        if (currentAudioClip != null && _audioSource != null && soundIndex != 1)
        {
            _audioSource.Stop();
            _audioSource.clip = currentAudioClip;
            _audioSource.Play();
        }
    }

    
    public void StartDialogue()
    {
        if (currentDialogueData.CanTalk)
        {
            EventManager.CallLockControls();
            Debug.Log("Начали диалог");
            SetCurrentProfileIcon();
            SetCurrentName();
            nextButton.onClick.AddListener(OnClickNextPhrase);
            dialogBG.SetActive(true);
            dialogInProcess = true;
            StartCoroutine(ShowText());
        }
        else
        {
            Debug.Log("Уже поговорили");
        }
    
    }

    public void EndDialog()
    {
        EventManager.CallUnlockControls();
        dialogInProcess = false;
        currentDialogueData.CanTalk = false;
        currentPhraseIndex = 0;
        dialogBG.SetActive(false); 
        _dialogueTrigger.OnEndDialogue?.Invoke();
    }

    public void SetEventEndDialogue(DialogueTrigger dialogueTrigger)
    {
        _dialogueTrigger = dialogueTrigger;
    }

    private IEnumerator ShowText()
    {
        if(currentPhraseIndex < currentDialogueData.Phrases.Length)
        {
            SetCurrentStringText();
            yield return  StartCoroutine(WriteWords(currentString));
            SetCurrentAudio();
            yield return new WaitUntil(() => canNextPhrase); // ждем нажатия кнопки
            currentPhraseIndex++;
            canNextPhrase = false;
            StartCoroutine(ShowText());
        }

        else
        {
            Debug.Log("Фразы закончились");
            EndDialog();
        }
       
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && dialogInProcess) 
        {
            
            OnClickNextPhrase();
        }
    }
    public void OnClickNextPhrase() => canNextPhrase = true;

    private IEnumerator WriteWords(string phrase)
    {
        textView.text = "";
        foreach(char letter in phrase.ToCharArray())
        {
            textView.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
