using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRunawayTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject PoopCat;
    [SerializeField]
    private GameObject AnimatedArrows;
    [SerializeField]
    private AudioSource poopMeow;
    [SerializeField]
    private Transform bathroomDoor;

    [SerializeField]
    private AudioSource wipe;
    private Vector3 doorOpen = new Vector3(0f, -120f, 0f);
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.tag == ConstantStrings.CAT_TAG)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            AnimatedArrows.SetActive(false);
            wipe.Play();
            poopMeow.Play();

            StartCoroutine(DelayPoopCat(collider));
            
            // Open Door
            bathroomDoor.DORotate(doorOpen, 0.5f)
                .SetEase(Ease.OutBounce);
            //.OnComplete(() => Debug.Log("DONE"));
        }
    }

    private IEnumerator DelayPoopCat(Collider originalCatCollider)
    {
        yield return new WaitForSeconds(2f);
        originalCatCollider.transform.root.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        PoopCat.SetActive(true);
    }
}
