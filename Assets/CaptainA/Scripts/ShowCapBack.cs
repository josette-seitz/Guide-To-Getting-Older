using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCapBack : MonoBehaviour
{
    public Sprite cap;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstantStrings.CAPTAIN_TAG))
        {
           other.gameObject.GetComponent<Animator>().enabled = false;
           var capBack = other.gameObject.GetComponent<SpriteRenderer>();
           capBack.sprite = cap;
        }
    }
}
