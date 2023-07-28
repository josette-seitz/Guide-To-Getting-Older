using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingSoon : MonoBehaviour
{
    private BoxCollider comingSoonCollider;
    
    void Start()
    {
        comingSoonCollider = GetComponent<BoxCollider>();
    }

    public void DisableEnableCollider()
    {
        StartCoroutine(EnableColliderDelay());
    }

    private IEnumerator EnableColliderDelay()
    {
        comingSoonCollider.enabled = false;
        yield return new WaitForSeconds(2f);
        comingSoonCollider.enabled = true;
    }
}
