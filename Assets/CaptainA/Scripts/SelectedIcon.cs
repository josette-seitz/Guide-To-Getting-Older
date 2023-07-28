using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedIcon : MonoBehaviour
{
    public string Tag;
    public AudioSource ChapterNoise;
    public GameObject ChapterParticles;
    public GameObject DisableResetIconFloor;
    public GameObject TransitionSceneTrigger;
    [Space]
    public Image FadeImage;
    public Sprite FadeOutColor;
    public GameObject TransitionToSceneService;

    public virtual void OnTriggerEnter(Collider other)
    {
        ChapterNoise.Play();
        TransitionToSceneService.SetActive(true);
        ChapterParticles.SetActive(true);
        DisableResetIconFloor.SetActive(false);
        TransitionSceneTrigger.SetActive(true);
        FadeImage.sprite = FadeOutColor;
    }
}
