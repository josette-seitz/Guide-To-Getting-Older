using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabEffects : MonoBehaviour
{
    private ParticleSystem pinchEffect;

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(ConstantStrings.ON_GRAB_PARTICLES, OnGrabParticles);
        EventDispatcher.Instance.RegisterListener(ConstantStrings.OFF_GRAB_PARTICLES, OffGrabParticles);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.UnregisterListener(ConstantStrings.ON_GRAB_PARTICLES, OnGrabParticles);
        EventDispatcher.Instance.UnregisterListener(ConstantStrings.OFF_GRAB_PARTICLES, OffGrabParticles);
    }

    void Start()
    {
        pinchEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void OnGrabParticles(object sender, object payload)
    {
        pinchEffect.Play();
    }

    private void OffGrabParticles(object sender, object payload)
    {
        pinchEffect.Stop();
    }

    //private MixedRealityPose? GetHandPose(Handedness hand, bool hasBeenGrabbed)
    //{
    //    if ((trackedHand & hand) == hand)
    //    {
    //        if (HandJointService.IsHandTracked(hand) &&
    //            ((GestureUtils.IsPinching(hand) && trackPinch) ||
    //             (GestureUtils.IsGrabbing(hand) && trackGrab)))
    //        {
    //            var jointTransform = HandJointService.RequestJointTransform(trackedHandJoint, hand);
    //            var palmTransForm = HandJointService.RequestJointTransform(TrackedHandJoint.Palm, hand);

    //            if (hasBeenGrabbed ||
    //               Vector3.Distance(gameObject.transform.position, jointTransform.position) <= grabDistance)
    //            {
    //                return new MixedRealityPose(jointTransform.position, palmTransForm.rotation);
    //            }
    //        }
    //    }

    //    return null;
    //}


}
