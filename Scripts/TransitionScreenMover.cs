using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreenMover : MonoBehaviour
{
    public GameObject transitionUI;

    void Start()
    {
        StartCoroutine(TransitionRemove());
    }

    IEnumerator TransitionRemove()
    {
        yield return new WaitForSeconds(1.4f);
        transitionUI.SetActive(false);
    }
}
