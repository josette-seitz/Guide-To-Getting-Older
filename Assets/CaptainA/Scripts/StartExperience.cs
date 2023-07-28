using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartExperience : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject CaptainTalking;
    public GameObject PressButton;
    public Image book;

    public void DisableButton()
    {
        StartCoroutine(Disable());
    }

    public void BeginExperience()
    {
        StartCoroutine(Begin());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.25f);
        StartButton.SetActive(false);
        PressButton.SetActive(false);
    }

    private IEnumerator Begin()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
        CaptainTalking.SetActive(true);
    }
}
