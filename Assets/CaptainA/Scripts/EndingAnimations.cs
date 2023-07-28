using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EndingAnimations : MonoBehaviour
{
    [SerializeField]
    private Transform EndingText1;
    [SerializeField]
    private Transform EndingText1Target;
    [SerializeField]
    private GameObject BathroomPage;
    [SerializeField]
    private GameObject EndingText2;
    [SerializeField]
    private Transform EndingText2Target;
    [SerializeField]
    private GameObject CoverPage;
    [SerializeField]
    private GameObject EndingText3_4;
    [SerializeField]
    private GameObject EndingButton;

    void Start()
    {
        StartCoroutine(MoveEndingPageText());
    }

    private IEnumerator MoveEndingPageText()
    {
        Vector3 targetPos1 = EndingText1Target.position;
        Vector3 targetPos2 = EndingText2Target.position;
        SpriteRenderer coverPage = CoverPage.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(5f);
        EndingText1.DOLocalMove(targetPos1, 1f)
            .OnComplete(() => BathroomPage.SetActive(true));
        yield return new WaitForSeconds(3f);
        EndingText2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        EndingText2.transform.DOLocalMove(targetPos2, 1f)
            .OnComplete(() => CoverPage.SetActive(true));
        yield return new WaitForSeconds(1.75f);
        EndingText3_4.SetActive(true);
        yield return new WaitForSeconds(4.25f);
        EndingButton.SetActive(true);
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
