using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionToSelectedScene : MonoBehaviour
{
    // *TODO: Come up with a better way to share objects
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case nameof(Enums.ChapterTarget.Poop):
                obj1.transform.DORotate(new Vector3(90f, 0f, 90f), 0.5f)
                    .SetLoops(-1, LoopType.Incremental)
                    .SetEase(Ease.Linear);

                obj1.transform.DOMove(Camera.main.transform.position, 5f)
                    .OnComplete(TriggerTransitionToBathroom);
                break;
            case nameof(Enums.ChapterTarget.Tutorial):
                StartCoroutine(Fade());
                break;
            default:
                Debug.LogError("Captain-A: Missing Tag, cannot transition.");
                break;
        }
    }

    private void TriggerTransitionToBathroom()
    {
        obj2.SetActive(true);
        EventDispatcher.Instance.Dispatch(ConstantStrings.FADE_NEXT_SCENE, this, null);
    }

    private IEnumerator Fade()
    {
        var fadeAnim = obj1.GetComponent<Animator>();
        var fadeImage = obj1.GetComponent<Image>();

        fadeAnim.SetTrigger(ConstantStrings.FADE_TRIGGER);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        obj3.SetActive(true);
        obj1.SetActive(false);
        obj1.SetActive(true);
        obj4.SetActive(true);
        obj2.SetActive(false);
    }
}
