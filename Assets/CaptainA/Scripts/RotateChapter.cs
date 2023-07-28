using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class RotateChapter : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<RectTransform>().Rotate(0, 80 * Time.deltaTime, 0); 


    }

    //public void TestPinch()
    //{
    //    pinchEffect.Play();
    //}

    //public void TestUnPinch()
    //{
    //    pinchEffect.Pause();
    //}


}
