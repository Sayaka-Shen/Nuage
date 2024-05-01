using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _textSentence;
    
    [Header("PNJ Collision")]
    private List<string> _sentencesManager = new List<string>();
    private DialogueTrigger _triggerDialogue;
    private int _currentIdSentences = 0;

    [Header("Scene")] 
    private Scene _currentScene;
    
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
    [SerializeField] private Animator _animatorChoice;
    
    [Header("Hero Script")]
    [SerializeField] private HeroEntity _heroEntity;
    [SerializeField] private HeroController _heroController;
    
    [Header("Barre Psychologique")]
    [SerializeField] private Slider _psychoBar;
    
    [Header("Camera")]
    private CinemachineBasicMultiChannelPerlin _cineMarchine;
    
    [Header("Teleporter")]
    [SerializeField] private GameObject secondTeleporter;

    [Header("Gymnase")] 
    [SerializeField] private GameObject _gymnase;

    [Header("Boîte de choix")] 
    [SerializeField] private TextMeshProUGUI _textChoiceOne;
    [SerializeField] private TextMeshProUGUI _textChoiceTwo;

    [SerializeField] private Button _buttonChoiceOne;
    [SerializeField] private Button _buttonChoiceTwo;

    [Header("Boîte de dialogue")] 
    [SerializeField] private Button _dialogueBoxButton;

    private void Start()
    {
        _triggerDialogue = FindObjectOfType<DialogueTrigger>();
        _cineMarchine = FindObjectOfType<CinemachineBasicMultiChannelPerlin>();
        _currentScene = SceneManager.GetActiveScene();
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
        
        // Activate Dialogue with several choice for the mascotte
        _ApplyChoice("NPC_Mascotte", "Niveau_1", 1); 

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
        _animatorChoice.SetBool("IsChoiceOpen", false);
        _heroController.GetComponent<HeroController>().enabled = true;
        
        // Check if it's the end of the dialogue with the Gymnase Man
        _TeleportEndDialogue();
        
        // Functions to activate the tag of the Gymnase when the discussion with the mom is finished
        _ActivateGymnaseTag();
        
        // Teleport player to level 2
        _ChangeSceneMascotteEndDialogue();
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

    private void _ActivateGymnaseTag()
    {
        if (_triggerDialogue.NpcTriggered.transform.parent.name != "NPC_Mom" && 
            _currentScene.name == "Niveau_2") return;
        
        _gymnase.gameObject.tag = "Gymnase";
        _gymnase.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void _ApplyChoice(string name, string scene, int sentence)
    {
        if (_triggerDialogue.NpcTriggered.transform.parent.name == name &&
            _currentScene.name == scene && _currentIdSentences == sentence)
        {
            _animatorChoice.SetBool("IsChoiceOpen", true);

            if (_animatorChoice.GetBool("IsChoiceOpen") == true)
            {
                _dialogueBoxButton.gameObject.SetActive(false);
            }
            
            string[] choiceArray = _triggerDialogue.NpcTriggered.GetComponentInParent<DialogueSettings>().choices;
            
            if (choiceArray != null)
            {
                _textChoiceOne.text = choiceArray[0];
                _textChoiceTwo.text = choiceArray[1];
                
                _buttonChoiceOne.onClick.AddListener(() => _HandleChoice(1));
                _buttonChoiceTwo.onClick.AddListener(() => _HandleChoice(2));
            }
        }
        else
        {
            _animatorChoice.SetBool("IsChoiceOpen", false);
        }
    }
    
    private void _HandleChoice(int choiceNumber)
    {
        switch(choiceNumber)
        {
            case 1:
                if (_triggerDialogue.NpcTriggered.transform.parent.name == "NPC_Mascotte")
                {
                    string newSentenceOne = _textSentence.text = "C'est bien Nué tu vas y arriver continue !!";
                    StartCoroutine(TypeSentence(newSentenceOne));
                    _dialogueBoxButton.gameObject.SetActive(true);
                    _psychoBar.value = 0.3f;
                    _cineMarchine.m_AmplitudeGain = 2;
                }


                _buttonChoiceTwo.gameObject.SetActive(false);
                break;
            case 2:
                if (_triggerDialogue.NpcTriggered.transform.parent.name == "NPC_Mascotte")
                {
                    string newSentenceTwo = _textSentence.text = "Allez Nué ne desespère pas !";
                    StartCoroutine(TypeSentence(newSentenceTwo));
                    _dialogueBoxButton.gameObject.SetActive(true);
                }
                
                _buttonChoiceOne.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void _ChangeSceneMascotteEndDialogue()
    {
        if (_triggerDialogue.NpcTriggered.transform.parent.name == "NPC_Mascotte" && _currentScene.name == "Niveau_1")
        {
            SceneManager.LoadScene(2);
        }
    }
    
    #endregion
}
