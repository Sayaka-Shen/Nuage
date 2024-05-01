using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPBackManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TpBack());
    }

    IEnumerator TpBack()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(0);
    }
}
