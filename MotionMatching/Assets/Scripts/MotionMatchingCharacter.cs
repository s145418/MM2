using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionMatchingCharacter : MonoBehaviour
{
    public PoseSequence ps;


    private Queue<int> previous = new Queue<int>();
    private int poseIndex = 0;
    public MotionMatchingRig motionMatchingRig;

    void Start()
    {
        motionMatchingRig = transform.GetComponent<MotionMatchingRig>();
    }

    void QueryDatabase()
    {
        //motionMatchingRig.ApplyPose(ps.poseSequence[poseIndex]);
        //poseIndex++;
        //if (poseIndex >= ps.poseSequence.Length)
        //{
        //    poseIndex = 0;
        //}

        NearestNeighbour();
    }

    void FixedUpdate()
    {
        QueryDatabase();
        motionMatchingRig.ApplyPose(ps.poseSequence[poseIndex]);
        previous.Enqueue(poseIndex);
        while (previous.Count > 40)
        {
            previous.Dequeue();
        }
    }


    void NearestNeighbour()
    {
        Pose current = ps.poseSequence[poseIndex];
        int minIndex = 0;
        float minDistance = float.MaxValue;
        for (int x = 0; x < ps.poseSequence.Length; x++)
        {
            float distance = Distance(ps.poseSequence[x], current);
            if (distance < minDistance && !previous.Contains(x))
            {
                minDistance = distance;
                minIndex = x;
            }
        }
        Debug.Log(poseIndex);
        Debug.Log(minIndex);
        poseIndex = minIndex;
        Debug.Log(minDistance);
        
    }


    float Distance(Pose p1, Pose p2)
    {
        float dis = 0f;

        dis += Quaternion.Angle(p1.hips.rotation, p2.hips.rotation);

        dis += Quaternion.Angle(p1.leftUpLeg.rotation, p2.leftUpLeg.rotation);
        dis += Quaternion.Angle(p1.leftLeg.rotation, p2.leftLeg.rotation);
        dis += Quaternion.Angle(p1.leftFoot.rotation, p2.leftFoot.rotation);
        dis += Quaternion.Angle(p1.leftToeBase.rotation, p2.leftToeBase.rotation);

        dis += Quaternion.Angle(p1.rightUpLeg.rotation, p2.rightUpLeg.rotation);
        dis += Quaternion.Angle(p1.rightLeg.rotation, p2.rightLeg.rotation);
        dis += Quaternion.Angle(p1.rightFoot.rotation, p2.rightFoot.rotation);
        dis += Quaternion.Angle(p1.rightToeBase.rotation, p2.rightToeBase.rotation);

        dis += Quaternion.Angle(p1.spine.rotation, p2.spine.rotation);
        dis += Quaternion.Angle(p1.spine1.rotation, p2.spine1.rotation);
        dis += Quaternion.Angle(p1.spine2.rotation, p2.spine2.rotation);
        dis += Quaternion.Angle(p1.neck.rotation, p2.neck.rotation);
        dis += Quaternion.Angle(p1.head.rotation, p2.head.rotation);

        dis += Quaternion.Angle(p1.leftShoulder.rotation, p2.leftShoulder.rotation);
        dis += Quaternion.Angle(p1.leftArm.rotation, p2.leftArm.rotation);
        dis += Quaternion.Angle(p1.leftForeArm.rotation, p2.leftForeArm.rotation);
        dis += Quaternion.Angle(p1.leftHand.rotation, p2.leftHand.rotation);

        dis += Quaternion.Angle(p1.rightShoulder.rotation, p2.rightShoulder.rotation);
        dis += Quaternion.Angle(p1.rightArm.rotation, p2.rightArm.rotation);
        dis += Quaternion.Angle(p1.rightForeArm.rotation, p2.rightForeArm.rotation);
        dis += Quaternion.Angle(p1.rightHand.rotation, p2.rightHand.rotation);

        return dis;
    }
}
