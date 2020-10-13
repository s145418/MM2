using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MotionMatchingRig : MonoBehaviour
{
    public Transform hips;
	public Transform leftUpLeg, leftLeg, leftFoot, leftToeBase;
	public Transform rightUpLeg, rightLeg, rightFoot, rightToeBase;
	public Transform spine, spine1, spine2;
	public Transform leftShoulder, leftArm, leftForeArm, leftHand;
	public Transform rightShoulder, rightArm, rightForeArm, rightHand;
	public Transform neck, head, headTopEnd;

    void Start()
    {
        hips = SearchTransformTree("mixamorig:Hips");

		leftUpLeg = SearchTransformTree("mixamorig:LeftUpLeg");
		leftLeg = SearchTransformTree("mixamorig:LeftLeg");
		leftFoot = SearchTransformTree("mixamorig:LeftFoot");
		leftToeBase = SearchTransformTree("mixamorig:LeftToeBase");

		rightUpLeg = SearchTransformTree("mixamorig:RightUpLeg");
		rightLeg = SearchTransformTree("mixamorig:RightLeg");
		rightFoot = SearchTransformTree("mixamorig:RightFoot");
		rightToeBase = SearchTransformTree("mixamorig:RightToeBase");

		spine = SearchTransformTree("mixamorig:Spine");
		spine1 = SearchTransformTree("mixamorig:Spine1");
		spine2 = SearchTransformTree("mixamorig:Spine2");

		leftShoulder = SearchTransformTree("mixamorig:LeftShoulder");
		leftArm = SearchTransformTree("mixamorig:LeftArm");
		leftForeArm = SearchTransformTree("mixamorig:LeftForeArm");
		leftHand = SearchTransformTree("mixamorig:LeftHand");
		
		rightShoulder = SearchTransformTree("mixamorig:RightShoulder");
		rightArm = SearchTransformTree("mixamorig:RightArm");
		rightForeArm = SearchTransformTree("mixamorig:RightForeArm");
		rightHand = SearchTransformTree("mixamorig:RightHand");
		
		neck = SearchTransformTree("mixamorig:Neck");
		head = SearchTransformTree("mixamorig:Head");
		headTopEnd = SearchTransformTree("mixamorig:HeadTop_End");
    }

    Transform SearchTransformTree(string name)
	{
		return SearchTransformTree(transform, name);
	}

    Transform SearchTransformTree(Transform parent, string name)
	{
		Transform target = parent.Find(name);

		if (target != null)
			return target;

		foreach (Transform child in parent)
		{
			Transform result = SearchTransformTree(child, name);
			if (result != null)
				return result;
		}

		return null;
	}

    public void ApplyPose(Pose p)
    {
        if (p.parent != null)
        {
            transform.position = p.parent.position;
            transform.localRotation = p.parent.rotation;
        }
        if (p.hips != null)
        {
            hips.localRotation = p.hips.rotation;
        }
        if (p.leftUpLeg != null) 
        {
            leftUpLeg.localRotation = p.leftUpLeg.rotation;
        }
        if (p.leftLeg != null)
        {
            leftLeg.localRotation = p.leftLeg.rotation;
        }
        if (p.leftFoot != null)
        {
            leftFoot.localRotation = p.leftFoot.rotation;
        }
        if (p.leftToeBase != null)
        {
            leftToeBase.localRotation = p.leftToeBase.rotation;
        }
        if (p.rightUpLeg != null) 
        {
            rightUpLeg.localRotation = p.rightUpLeg.rotation;
        }
        if (p.rightLeg != null)
        {
            rightLeg.localRotation = p.rightLeg.rotation;
        }
        if (p.rightFoot != null)
        {
            rightFoot.localRotation = p.rightFoot.rotation;
        }
        if (p.rightToeBase != null)
        {
            rightToeBase.localRotation = p.rightToeBase.rotation;
        }
        if (p.spine != null)
        {
            spine.localRotation = p.spine.rotation;
        }
        if (p.spine1 != null)
        {
            spine1.localRotation = p.spine1.rotation;
        }
        if (p.spine2 != null)
        {
            spine2.localRotation = p.spine2.rotation;
        }
        if (p.leftShoulder != null){
            leftShoulder.localRotation = p.leftShoulder.rotation;
        }
        if (p.leftArm != null){
            leftArm.localRotation = p.leftArm.rotation;
        }
        if (p.leftForeArm != null){
            leftForeArm.localRotation = p.leftForeArm.rotation;
        }
        if (p.leftHand != null){
            leftHand.localRotation = p.leftHand.rotation;
        }
        if (p.neck != null){
            neck.localRotation = p.neck.rotation;
        }
        if (p.head != null){
            head.localRotation = p.head.rotation;
        }
        if (p.rightShoulder != null){
            rightShoulder.localRotation = p.rightShoulder.rotation;
        }
        if (p.rightArm != null){
            rightArm.localRotation = p.rightArm.rotation;
        }
        if (p.rightForeArm != null){
            rightForeArm.localRotation = p.rightForeArm.rotation;
        }
        if (p.rightHand != null){
            rightHand.localRotation = p.rightHand.rotation;
        }
    }


}
