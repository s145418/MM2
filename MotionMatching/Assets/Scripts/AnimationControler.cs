using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControler : MonoBehaviour
{

    [SerializeField]
    private Transform hips;

    private Transform leftUpLeg, leftLeg, leftFoot, leftToe;
    private Transform rightUpLeg, rightLeg, rightFoot, rightToe;
    private Transform spine, spine1, spine2;

    private Transform leftShoulder, leftArm, leftForeArm, leftHand;
    private Transform neck, head;
    private Transform rightShoulder, rightArm, rightForeArm, rightHand;

    private Transform leftIndex1, leftMiddle1, leftPinky1, leftRing1, leftThumb1;
    private Transform leftIndex2, leftMiddle2, leftPinky2, leftRing2, leftThumb2;
    private Transform leftIndex3, leftMiddle3, leftPinky3, leftRing3, leftThumb3;

    private Transform rightIndex1, rightMiddle1, rightPinky1, rightRing1, rightThumb1;
    private Transform rightIndex2, rightMiddle2, rightPinky2, rightRing2, rightThumb2;
    private Transform rightIndex3, rightMiddle3, rightPinky3, rightRing3, rightThumb3;

    // Start is called before the first frame update
    void Start()
    {
        #region Legs
        leftUpLeg = hips.GetChild(0);
        leftLeg = leftUpLeg.GetChild(0);
        leftFoot = leftLeg.GetChild(0);
        leftToe = leftFoot.GetChild(0);

        rightUpLeg = hips.GetChild(1);
        rightLeg = rightUpLeg.GetChild(0);
        rightFoot = rightLeg.GetChild(0);
        rightToe = rightFoot.GetChild(0);
        #endregion

        #region Spine and head
        spine = hips.GetChild(2);
        spine1 = spine.GetChild(0);
        spine2 = spine1.GetChild(0);

        neck = spine2.GetChild(1);
        head = neck.GetChild(0);
        #endregion

        #region Arms
        leftShoulder = spine2.GetChild(0);
        leftArm = leftShoulder.GetChild(0);
        leftForeArm = leftArm.GetChild(0);
        leftHand = leftForeArm.GetChild(0);

        rightShoulder = spine2.GetChild(2);
        rightArm = rightShoulder.GetChild(0);
        rightForeArm = rightArm.GetChild(0);
        rightHand = rightForeArm.GetChild(0);
        #endregion

        #region Left fingers
        leftIndex1 = leftHand.GetChild(0);
        leftIndex2 = leftHand.GetChild(0).GetChild(0);
        leftIndex3 = leftHand.GetChild(0).GetChild(0).GetChild(0);

        leftMiddle1 = leftHand.GetChild(1);
        leftMiddle2 = leftHand.GetChild(1).GetChild(0);
        leftMiddle3 = leftHand.GetChild(1).GetChild(0).GetChild(0);

        leftPinky1 = leftHand.GetChild(2);
        leftPinky2 = leftHand.GetChild(2).GetChild(0);
        leftPinky3 = leftHand.GetChild(2).GetChild(0).GetChild(0);

        leftRing1 = leftHand.GetChild(3);
        leftRing2 = leftHand.GetChild(3).GetChild(0);
        leftRing3 = leftHand.GetChild(3).GetChild(0).GetChild(0);

        leftThumb1 = leftHand.GetChild(4);
        leftThumb2 = leftHand.GetChild(4).GetChild(0);
        leftThumb3 = leftHand.GetChild(4).GetChild(0).GetChild(0);
        #endregion

        #region Right fingers
        rightIndex1 = rightHand.GetChild(0);
        rightIndex2 = rightHand.GetChild(0).GetChild(0);
        rightIndex3 = rightHand.GetChild(0).GetChild(0).GetChild(0);

        rightMiddle1 = rightHand.GetChild(1);
        rightMiddle2 = rightHand.GetChild(1).GetChild(0);
        rightMiddle3 = rightHand.GetChild(1).GetChild(0).GetChild(0);

        rightPinky1 = rightHand.GetChild(2);
        rightPinky2 = rightHand.GetChild(2).GetChild(0);
        rightPinky3 = rightHand.GetChild(2).GetChild(0).GetChild(0);

        rightRing1 = rightHand.GetChild(3);
        rightRing2 = rightHand.GetChild(3).GetChild(0);
        rightRing3 = rightHand.GetChild(3).GetChild(0).GetChild(0);

        rightThumb1 = rightHand.GetChild(4);
        rightThumb2 = rightHand.GetChild(4).GetChild(0);
        rightThumb3 = rightHand.GetChild(4).GetChild(0).GetChild(0);
        #endregion
    }


    // greate a random pose for testing purposes
    Pose makePose(float flo)
    {
        Vector3 target = new Vector3(0.2f, 0, 0);
        Pose p = new Pose();
        Joint j = new Joint();

        j.rotation = new Quaternion(0.0f, 1f, 0f, 0.002f);
        j.position = target;

        Joint j1 = new Joint();

        j1.rotation = new Quaternion(1f, 0f, 0f, flo / 100);
        j1.position = target;

        Joint j2 = new Joint();

        j2.rotation = new Quaternion(0f, 0f, 1f, 1f);
        j2.position = target;

        p.hips = null;

        p.leftUpLeg = null;
        p.leftLeg = j2;
        p.leftFoot = j2;
        p.leftToeBase = j2;

        p.rightUpLeg = j1;
        p.rightLeg = j1;
        p.rightFoot = j1;
        p.rightToeBase = j1;

        p.spine = j;
        p.spine1 = j;
        p.spine2 = j;

        p.leftShoulder = j1;
        p.leftArm = j2;
        p.leftForeArm = j;
        p.leftHand = j1;

        p.rightShoulder = j2;
        p.rightArm = j2;
        p.rightForeArm = j1;
        p.rightHand = j;

        p.neck = j;
        p.head = j2;
        return p;
    }

    float counter = 0;

    // Update is called once per frame
    void Update()
    {
        counter += 0.1f;
        if (counter > 20)
        {
            counter = -20f;
        }

        PoseCharacter(makePose(counter));
    }

    // pose by rotating the joins by the euler equivalent of the quarternions
    void PoseCharacter(Pose p)
    {
        #region Hips, spine and head
        if (p.hips != null)
        {
            hips.Rotate(p.hips.rotation.eulerAngles);
        }

        if (p.spine != null)
        {
            spine.Rotate(p.spine.rotation.eulerAngles);
        }
        if (p.spine1 != null)
        {
            spine1.Rotate(p.spine1.rotation.eulerAngles);
        }
        if (p.spine2 != null)
        {
            spine2.Rotate(p.spine2.rotation.eulerAngles);
        }

        if (p.neck != null)
        {
            neck.Rotate(p.neck.rotation.eulerAngles);
        }
        if (p.head != null)
        {
            head.Rotate(p.head.rotation.eulerAngles);
        }
        #endregion

        #region Legs
        if (p.leftUpLeg != null) {
            leftUpLeg.Rotate(p.leftUpLeg.rotation.eulerAngles);
        }
        if (p.leftLeg != null)
        {
            leftLeg.Rotate(p.leftLeg.rotation.eulerAngles);
        }
        if (p.leftFoot != null)
        {
            leftFoot.Rotate(p.leftFoot.rotation.eulerAngles);
        }
        if (p.leftToeBase != null)
        {
            leftToe.Rotate(p.leftToeBase.rotation.eulerAngles);
        }

        if (p.rightUpLeg != null) {
            rightUpLeg.Rotate(p.rightUpLeg.rotation.eulerAngles);
        }
        if (p.rightLeg != null)
        {
            rightLeg.Rotate(p.rightLeg.rotation.eulerAngles);
        }
        if (p.rightFoot != null)
        {
            rightFoot.Rotate(p.rightFoot.rotation.eulerAngles);
        }
        if (p.rightToeBase != null)
        {
            rightToe.Rotate(p.rightToeBase.rotation.eulerAngles);
        }
        #endregion

        #region Arms
        if (p.leftShoulder != null){
        leftShoulder.Rotate(p.leftShoulder.rotation.eulerAngles);
        }
        if (p.leftArm != null){
        leftArm.Rotate(p.leftArm.rotation.eulerAngles);
        }
        if (p.leftForeArm != null){
        leftForeArm.Rotate(p.leftForeArm.rotation.eulerAngles);
        }
        if (p.leftHand != null){
        leftHand.Rotate(p.leftHand.rotation.eulerAngles);
        }
        
        if (p.rightShoulder != null){
        rightShoulder.Rotate(p.rightShoulder.rotation.eulerAngles);
        }
        if (p.rightArm != null){
        rightArm.Rotate(p.rightArm.rotation.eulerAngles);
        }
        if (p.rightForeArm != null){
        rightForeArm.Rotate(p.rightForeArm.rotation.eulerAngles);
        }
        if (p.rightHand != null){
        rightHand.Rotate(p.rightHand.rotation.eulerAngles);
        }
        #endregion

        #region Left fingers
        if (p.leftHandIndex1 != null)
        {
            leftIndex1.Rotate(p.leftHandIndex1.rotation.eulerAngles);
        }
        if (p.leftHandIndex2 != null)
        {
            leftIndex2.Rotate(p.leftHandIndex2.rotation.eulerAngles);
        }
        if (p.leftHandIndex3 != null)
        {
            leftIndex3.Rotate(p.leftHandIndex3.rotation.eulerAngles);
        }

        if (p.leftHandMiddle1 != null)
        {
            leftMiddle1.Rotate(p.leftHandMiddle1.rotation.eulerAngles);
        }
        if (p.leftHandMiddle2 != null)
        {
            leftMiddle2.Rotate(p.leftHandMiddle2.rotation.eulerAngles);
        }
        if (p.leftHandMiddle3 != null)
        {
            leftMiddle3.Rotate(p.leftHandMiddle3.rotation.eulerAngles);
        }

        if (p.leftHandPinky1 != null)
        {
            leftPinky1.Rotate(p.leftHandPinky1.rotation.eulerAngles);
        }
        if (p.leftHandPinky2 != null)
        {
            leftPinky2.Rotate(p.leftHandPinky2.rotation.eulerAngles);
        }
        if (p.leftHandPinky3 != null)
        {
            leftPinky3.Rotate(p.leftHandPinky3.rotation.eulerAngles);
        }

        if (p.leftHandRing1 != null)
        {
            leftRing1.Rotate(p.leftHandRing1.rotation.eulerAngles);
        }
        if (p.leftHandRing2 != null)
        {
            leftRing2.Rotate(p.leftHandRing2.rotation.eulerAngles);
        }
        if (p.leftHandRing3 != null)
        {
            leftRing3.Rotate(p.leftHandRing3.rotation.eulerAngles);
        }

        if (p.leftHandThumb1 != null)
        {
            leftThumb1.Rotate(p.leftHandThumb1.rotation.eulerAngles);
        }
        if (p.leftHandThumb2 != null)
        {
            leftThumb2.Rotate(p.leftHandThumb2.rotation.eulerAngles);
        }
        if (p.leftHandThumb3 != null)
        {
            leftThumb3.Rotate(p.leftHandThumb3.rotation.eulerAngles);
        }
        #endregion

        #region Right fingers
        if (p.rightHandIndex1 != null)
        {
            rightIndex1.Rotate(p.rightHandIndex1.rotation.eulerAngles);
        }
        if (p.rightHandIndex2 != null)
        {
            rightIndex2.Rotate(p.rightHandIndex2.rotation.eulerAngles);
        }
        if (p.rightHandIndex3 != null)
        {
            rightIndex3.Rotate(p.rightHandIndex3.rotation.eulerAngles);
        }

        if (p.rightHandMiddle1 != null)
        {
            rightMiddle1.Rotate(p.rightHandMiddle1.rotation.eulerAngles);
        }
        if (p.rightHandMiddle2 != null)
        {
            rightMiddle2.Rotate(p.rightHandMiddle2.rotation.eulerAngles);
        }
        if (p.rightHandMiddle3 != null)
        {
            rightMiddle3.Rotate(p.rightHandMiddle3.rotation.eulerAngles);
        }

        if (p.rightHandPinky1 != null)
        {
            rightPinky1.Rotate(p.rightHandPinky1.rotation.eulerAngles);
        }
        if (p.rightHandPinky2 != null)
        {
            rightPinky2.Rotate(p.rightHandPinky2.rotation.eulerAngles);
        }
        if (p.rightHandPinky3 != null)
        {
            rightPinky3.Rotate(p.rightHandPinky3.rotation.eulerAngles);
        }

        if (p.rightHandRing1 != null)
        {
            rightRing1.Rotate(p.rightHandRing1.rotation.eulerAngles);
        }
        if (p.rightHandRing2 != null)
        {
            rightRing2.Rotate(p.rightHandRing2.rotation.eulerAngles);
        }
        if (p.rightHandRing3 != null)
        {
            rightRing3.Rotate(p.rightHandRing3.rotation.eulerAngles);
        }

        if (p.rightHandThumb1 != null)
        {
            rightThumb1.Rotate(p.rightHandThumb1.rotation.eulerAngles);
        }
        if (p.rightHandThumb2 != null)
        {
            rightThumb2.Rotate(p.rightHandThumb2.rotation.eulerAngles);
        }
        if (p.rightHandThumb3 != null)
        {
            rightThumb3.Rotate(p.rightHandThumb3.rotation.eulerAngles);
        }
        #endregion
    }

    // pose by applying the quarternions directly
    void PoseCharacter(Pose p, bool b)
    {
        hips.rotation = p.hips.rotation;

        leftUpLeg.rotation = p.leftUpLeg.rotation;
        leftLeg.rotation = p.leftLeg.rotation;
        leftFoot.rotation = p.leftFoot.rotation;
        leftToe.rotation = p.leftFoot.rotation;

        rightUpLeg.rotation = p.rightUpLeg.rotation;
        rightLeg.rotation = p.rightLeg.rotation;
        rightFoot.rotation = p.rightFoot.rotation;
        rightToe.rotation = p.rightToeBase.rotation;

        spine.rotation = p.spine.rotation;
        spine1.rotation = p.spine1.rotation;
        spine2.rotation = p.spine2.rotation;

        leftShoulder.rotation = p.leftShoulder.rotation;
        leftArm.rotation = p.leftArm.rotation;
        leftForeArm.rotation = p.leftForeArm.rotation;
        leftHand.rotation = p.leftHand.rotation;

        neck.rotation = p.neck.rotation;
        head.rotation = p.head.rotation;

        rightShoulder.rotation = p.rightShoulder.rotation;
        rightArm.rotation = p.rightArm.rotation;
        rightForeArm.rotation = p.rightForeArm.rotation;
        rightHand.rotation = p.rightHand.rotation;
    }

    // pose by rotating the joins using vecter3 representations of the euler angles
    void PoseCharacter(Vector3[] transforms)
    {
        hips.Rotate(transforms[0]);

        leftUpLeg.Rotate(transforms[1]);
        leftLeg.Rotate(transforms[2]);
        leftFoot.Rotate(transforms[3]);
        leftToe.Rotate(transforms[4]);

        rightUpLeg.Rotate(transforms[5]);
        rightLeg.Rotate(transforms[6]);
        rightFoot.Rotate(transforms[7]);
        rightToe.Rotate(transforms[8]);

        spine.Rotate(transforms[9]);
        spine1.Rotate(transforms[10]);
        spine2.Rotate(transforms[11]);

        leftShoulder.Rotate(transforms[12]);
        leftArm.Rotate(transforms[13]);
        leftForeArm.Rotate(transforms[14]);
        leftHand.Rotate(transforms[15]);

        neck.Rotate(transforms[16]);
        head.Rotate(transforms[17]);

        rightShoulder.Rotate(transforms[18]);
        rightArm.Rotate(transforms[19]);
        rightForeArm.Rotate(transforms[20]);
        rightHand.Rotate(transforms[21]);
    }
}
