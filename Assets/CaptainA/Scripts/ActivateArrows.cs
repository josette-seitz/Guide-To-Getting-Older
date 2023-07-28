using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateArrows : MonoBehaviour
{
    public GameObject Arrow1;
    public GameObject Arrow2;
    public GameObject WipeCatCollider;
    public AudioSource BathroomAudioSource;
    public AudioClip BathroomClip10;

    private bool arrowsActive = false;

    public void OnCatGrab()
    {
        if (!arrowsActive)
        {
            BathroomAudioSource.PlayOneShot(BathroomClip10);
            Arrow1.SetActive(false);
            Arrow2.SetActive(true);
            WipeCatCollider.SetActive(true);
            arrowsActive = true;
        }
    }
}
