using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PsychoBarCheckpointLabyrinth : MonoBehaviour
{
    [Header("Psycho Bar")] 
    [SerializeField] private Slider _psychoBar;
    [SerializeField] private float _valuePsychoBar;

    [Header("Cinemachine")] 
    private CinemachineBasicMultiChannelPerlin _cineMachine;
    [SerializeField] private float _valueCineMachine;

    private void Start()
    {
        _cineMachine = FindObjectOfType<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _psychoBar.value = _valuePsychoBar;
            _cineMachine.m_AmplitudeGain = _valueCineMachine;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
