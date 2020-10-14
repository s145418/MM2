using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionMatchingCharacter : MonoBehaviour
{
    public PoseSequence ps;

    private int poseIndex = 0;
    public MotionMatchingRig motionMatchingRig;

    void Start()
    {
        motionMatchingRig = transform.GetComponent<MotionMatchingRig>();
    }

    void FixedUpdate()
    {
        motionMatchingRig.ApplyPose(ps.poseSequence[poseIndex]);
        Debug.Log(ps.poseSequence.Length);
        Debug.Log(poseIndex);
        poseIndex++;
        if (poseIndex >= ps.poseSequence.Length)
        {
            poseIndex = 0;
        }
    }
}
