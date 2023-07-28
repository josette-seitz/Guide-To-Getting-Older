using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Enums;

public class TransitionToScene : MonoBehaviour
{
    [SerializeField]
    private CapScenes nextScene;

    private Image fadeImage;
    private Animator fadeAnim;

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(ConstantStrings.FADE_NEXT_SCENE, OnFadeToNextScene);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.UnregisterListener(ConstantStrings.FADE_NEXT_SCENE, OnFadeToNextScene);
    }

    private void Start()
    {
        Camera cam = Camera.main;
        fadeImage = cam.GetComponentInChildren<Image>();
        fadeAnim = cam.GetComponentInChildren<Animator>();
    }

    private void OnFadeToNextScene(object sender, object payload)
    {
        //Fade to next scene
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        fadeAnim.SetTrigger(ConstantStrings.FADE_TRIGGER);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        SceneManager.LoadScene(((int)nextScene));
    }
}
