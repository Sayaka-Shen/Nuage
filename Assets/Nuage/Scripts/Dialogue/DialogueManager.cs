using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _textSentence;
    
    [Header("PNJ Collision")]
    private List<string> _sentencesManager = new List<string>();
    private DialogueTrigger _triggerDialogue;
    private int _currentIdSentences = 0;
    
    public int CurrentIdSentences
    {
        get { return _currentIdSentences; }
    }

    public List<string> SentencesManager
    {
        get { return _sentencesManager; }
    }
    
    [Header("Animator")]
    [SerializeField] private Animator _animatorCanvas;
    
    [Header("Hero Script")]
    [SerializeField] private HeroEntity _heroEntity;
    [SerializeField] private HeroController _heroController;
    
    [Header("Barre Psychologique")]
    [SerializeField] private Slider _psychoBar;
    
    [Header("Camera")]
    private CinemachineBasicMultiChannelPerlin _cineMarchine;
    
    [Header("Teleporter")]
    [SerializeField] private GameObject secondTeleporter;
    

    private void Start()
    {
        _triggerDialogue = FindObjectOfType<DialogueTrigger>();
        _cineMarchine = FindObjectOfType<CinemachineBasicMultiChannelPerlin>();
    }
    
    #region Functions Gestion Dialogue

    public void StartDialogue()
    {
        _animatorCanvas.SetBool("IsOpen", true);
        _heroController.GetComponent<HeroController>().enabled = false;
        
        _nameText.text = _triggerDialogue.NpcTriggered.GetComponentInParent<DialogueSettings>().pnjName;
        
        _sentencesManager.Clear();
        _sentencesManager.AddRange(_triggerDialogue.NpcTriggered.GetComponentInParent<DialogueSettings>().sentences);

        _textSentence.text = _sentencesManager[_currentIdSentences];
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_currentIdSentences >= _sentencesManager.Count)
        {
            _EndDialogue();
            _currentIdSentences = 0;
            return;
        }
        
        // Checks if the currentIdSentences is the second and if the NPC is the Gymnase Man
        _PsychoBarManagement();

        string sentence = _sentencesManager[_currentIdSentences];

        Debug.Log(sentence);
        Debug.Log(_currentIdSentences);
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        _currentIdSentences++; 
    }
 
    IEnumerator TypeSentence(string sentence)
    {
        _textSentence.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            _textSentence.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void _EndDialogue()
    {
        _animatorCanvas.SetBool("IsOpen", false);
        _heroController.GetComponent<HeroController>().enabled = true;
        
        // Check if it's the end of the dialogue with the Gymnase Man
        _TeleportEndDialogue();
    }
    
    #endregion
    
    #region Functions Action During Dialogue

    private void _PsychoBarManagement()
    {
        if (_triggerDialogue.NpcTriggered.transform.parent.name == "NPC_Guichet" && _currentIdSentences == 1)
        {
            _psychoBar.gameObject.SetActive(true);
            _psychoBar.value = 0.5f;
            _cineMarchine.m_AmplitudeGain = 1.5f;
            _cineMarchine.m_FrequencyGain = 1f;
        }
    }

    private void _TeleportEndDialogue()
    {
        if (_triggerDialogue.NpcTriggered.transform.parent.name != "NPC_Guichet") return;
        _heroEntity.GetComponent<HeroTeleporter>()._TeleportLabyrinth();
    }
    
    #endregion
}
