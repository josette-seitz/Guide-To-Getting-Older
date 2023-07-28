using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> eventGameObj;

    private GameObject eventObj1;
    private GameObject eventObj2;
    private GameObject eventObj3;

    private void Start()
    {
        eventObj1 = eventGameObj[0];

        if (eventGameObj.Count > 1)
        {
            eventObj2 = eventGameObj[1];
            eventObj3 = eventGameObj[2];
        }
    }

    public void MoveCaptainA()
    {
        eventObj1.GetComponent<Rigidbody>().velocity = -this.transform.forward * 0.55f;
    }

    public void TurnCatToCaptainA()
    {
        //Vector3 mainCameraPos = Camera.main.transform.position;
        AudioSource catHiss = eventObj1.GetComponent<AudioSource>();
        //eventObj.transform.DOLookAt(mainCameraPos, 0.015f);
        eventObj1.transform.DOLocalRotate(new Vector3(0, -135f, 0), 0.015f);

        eventObj3.SetActive(true);
        catHiss.Play();
    }

    public void TurnCatToDoor()
    {
        Vector3 lookAtPos = new Vector3(-1.0f, 0.6f, 0.9f);
        eventObj1.transform.DOLookAt(lookAtPos, 0.02f);
        eventObj1.GetComponent<Animator>().SetTrigger(ConstantStrings.CAT_RUNAWAY_TRIGGER);
    }

    public void PlayEndingBathroomAudioClip()
    {
        eventObj2.SetActive(true);
    }
}
