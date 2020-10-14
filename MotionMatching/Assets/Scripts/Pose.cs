using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose
{
    public Joint parent;
    public Joint hips;

    #region leftleg
    //leftleg
    public Joint leftUpLeg;
    public Joint leftLeg;
    public Joint leftFoot;
    public Joint leftToeBase;
    #endregion

    #region rightleg
    //rightleg
    public Joint rightUpLeg;
    public Joint rightLeg;
    public Joint rightFoot;
    public Joint rightToeBase;
    #endregion

    #region spine
    //spine
    public Joint spine;
    public Joint spine1;
    public Joint spine2;
    #endregion

    #region leftarm
    //leftshoulder
    public Joint leftShoulder;
    public Joint leftArm;
    public Joint leftForeArm;
    public Joint leftHand;
    #endregion

    #region lefthand
    //lefthand
    public Joint leftHandIndex1;
    public Joint leftHandMiddle1;
    public Joint leftHandPinky1;
    public Joint leftHandRing1;
    public Joint leftHandThumb1;
    public Joint leftHandIndex2;
    public Joint leftHandMiddle2;
    public Joint leftHandPinky2;
    public Joint leftHandRing2;
    public Joint leftHandThumb2;
    public Joint leftHandIndex3;
    public Joint leftHandMiddle3;
    public Joint leftHandPinky3;
    public Joint leftHandRing3;
    public Joint leftHandThumb3;
    #endregion

    #region rightarm
    //rightshoulder
    public Joint rightShoulder;
    public Joint rightArm;
    public Joint rightForeArm;
    public Joint rightHand;
    #endregion

    #region righthand
    //righthand
    public Joint rightHandIndex1;
    public Joint rightHandMiddle1;
    public Joint rightHandPinky1;
    public Joint rightHandRing1;
    public Joint rightHandThumb1;
    public Joint rightHandIndex2;
    public Joint rightHandMiddle2;
    public Joint rightHandPinky2;
    public Joint rightHandRing2;
    public Joint rightHandThumb2;
    public Joint rightHandIndex3;
    public Joint rightHandMiddle3;
    public Joint rightHandPinky3;
    public Joint rightHandRing3;
    public Joint rightHandThumb3;
    #endregion

    #region head
    //neck
    public Joint neck;
    public Joint head;
    public Joint headTopEnd;
    #endregion
}

public class Joint
{
    public Joint(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }

    public Joint(Transform t)
    {
        this.position = t.localPosition;
        this.rotation = t.localRotation;
    }

    public Vector3 position;
    public Quaternion rotation;
}