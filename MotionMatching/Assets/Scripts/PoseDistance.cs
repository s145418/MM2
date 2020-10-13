using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseDistance : MonoBehaviour
{
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
