using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSettings : MonoBehaviour
{
    public string pnjName;
    
    [TextArea(3, 10)]
    public List<string> sentences;

    [TextArea(3, 10)]
    public string[] choices = new string[2];
}
