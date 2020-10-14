using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SampleClipTool: EditorWindow {

	class Styles
	{
		public Styles()
		{
		}
	}
	static Styles s_Styles;

	#region mixamorig
	protected Transform hips;
	protected Transform leftUpLeg, leftLeg, leftFoot, leftToeBase;
	protected Transform rightUpLeg, rightLeg, rightFoot, rightToeBase;
	protected Transform spine, spine1, spine2;
	protected Transform leftShoulder, leftArm, leftForeArm, leftHand;
	protected Transform rightShoulder, rightArm, rightForeArm, rightHand;
	protected Transform neck, head, headTopEnd;

	//protected Transform leftHandIndex1;
    //protected Transform leftHandMiddle1;
    //protected Transform leftHandPinky1;
    //protected Transform leftHandRing1;
    //protected Transform leftHandThumb1;
    //protected Transform leftHandIndex2;
    //protected Transform leftHandMiddle2;
    //protected Transform leftHandPinky2;
    //protected Transform leftHandRing2;
    //protected Transform leftHandThumb2;
    //protected Transform leftHandIndex3;
    //protected Transform leftHandMiddle3;
    //protected Transform leftHandPinky3;
    //protected Transform leftHandRing3;
    //protected Transform leftHandThumb3;

	//protected Transform rightHandIndex1;
    //protected Transform rightHandMiddle1;
    //protected Transform rightHandPinky1;
    //protected Transform rightHandRing1;
    //protected Transform rightHandThumb1;
    //protected Transform rightHandIndex2;
    //protected Transform rightHandMiddle2;
    //protected Transform rightHandPinky2;
    //protected Transform rightHandRing2;
    //protected Transform rightHandThumb2;
    //protected Transform rightHandIndex3;
    //protected Transform rightHandMiddle3;
    //protected Transform rightHandPinky3;
    //protected Transform rightHandRing3;
    //protected Transform rightHandThumb3;
	#endregion

	protected GameObject go;
	protected AnimationClip animationClip;
	protected float time = 0.0f;
	protected bool lockSelection = false;
	protected bool animationMode = false;
    protected int samplingRate = 60;
    
    private int poseCounter = 0;

	[MenuItem("MotionMatching/Convert animation to poses", false, 2000)]
	public static void DoWindow()
	{
		GetWindow<SampleClipTool>();
	}

	public void OnEnable()
	{		
	}

	public void OnSelectionChange()
	{
		if (!lockSelection)
		{
			go = Selection.activeGameObject;
			Repaint();
		}
	}

	public void OnGUI()
	{
		if (s_Styles == null)
			s_Styles = new Styles();

		if (go == null)
		{
			EditorGUILayout.HelpBox("Please select a GO", MessageType.Info);
			return;
		}

		GUILayout.BeginHorizontal(EditorStyles.toolbar);

		EditorGUI.BeginChangeCheck();
		GUILayout.Toggle(AnimationMode.InAnimationMode(), "Animate", EditorStyles.toolbarButton);
		if (EditorGUI.EndChangeCheck())
			ToggleAnimationMode();

		GUILayout.FlexibleSpace();
		lockSelection = GUILayout.Toggle(lockSelection, "Lock", EditorStyles.toolbarButton);

		GUILayout.EndHorizontal();
		
		EditorGUILayout.BeginVertical();
		animationClip = EditorGUILayout.ObjectField(animationClip, typeof(AnimationClip), false) as AnimationClip;

        samplingRate = EditorGUILayout.IntField("Sampling FPS:", samplingRate);

		if (animationClip != null)
		{
			float startTime = 0.0f;
			float stopTime  = animationClip.length;
			time = EditorGUILayout.Slider(time, startTime, stopTime);
            if (GUILayout.Button("Generate Poses"))
                GeneratePoses(startTime, stopTime);
		}
		else if (AnimationMode.InAnimationMode())
			AnimationMode.StopAnimationMode();

		EditorGUILayout.EndVertical();

		hips = AddTransformField("mixamorig:Hips");

		leftUpLeg = AddTransformField("mixamorig:LeftUpLeg");
		leftLeg = AddTransformField("mixamorig:LeftLeg");
		leftFoot = AddTransformField("mixamorig:LeftFoot");
		leftToeBase = AddTransformField("mixamorig:LeftToeBase");

		rightUpLeg = AddTransformField("mixamorig:RightUpLeg");
		rightLeg = AddTransformField("mixamorig:RightLeg");
		rightFoot = AddTransformField("mixamorig:RightFoot");
		rightToeBase = AddTransformField("mixamorig:RightToeBase");

		spine = AddTransformField("mixamorig:Spine");
		spine1 = AddTransformField("mixamorig:Spine1");
		spine2 = AddTransformField("mixamorig:Spine2");

		leftShoulder = AddTransformField("mixamorig:LeftShoulder");
		leftArm = AddTransformField("mixamorig:LeftArm");
		leftForeArm = AddTransformField("mixamorig:LeftForeArm");
		leftHand = AddTransformField("mixamorig:LeftHand");
		
		rightShoulder = AddTransformField("mixamorig:RightShoulder");
		rightArm = AddTransformField("mixamorig:RightArm");
		rightForeArm = AddTransformField("mixamorig:RightForeArm");
		rightHand = AddTransformField("mixamorig:RightHand");
		
		neck = AddTransformField("mixamorig:Neck");
		head = AddTransformField("mixamorig:Head");
		headTopEnd = AddTransformField("mixamorig:HeadTop_End");
		
	}

	//REMINDER: maybe change this to Joint so you dont have to tranlate all the Transform values later;
	Transform AddTransformField(string autofind)
	{
		Transform jointTransform = SearchTransformTree(go.transform, autofind);
		EditorGUILayout.ObjectField(autofind.Substring(10), jointTransform, typeof(Transform), true);
		return jointTransform;
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

    void GeneratePoses(float startTime, float stopTime)
    {
		List<Pose> poses = new List<Pose>();
        float timeStep = 1f / (float)samplingRate;
        for (float sampleTime = startTime; sampleTime < stopTime; sampleTime += timeStep)
        {
            poses.Add(GeneratePose(sampleTime));
        }
		PoseSequence ps = (PoseSequence) ScriptableObject.CreateInstance(typeof(PoseSequence));
		ps.poseSequence = poses.ToArray();
		ps.samplingFPS = samplingRate;
		Debug.Log(ps.poseSequence[5].leftArm.rotation.eulerAngles);
		string name = "Assets/PoseSequences/" + animationClip.name + ".asset";
		AssetDatabase.CreateAsset(ps, name);
    }

    Pose GeneratePose(float sampleTime)
    {
        if (go == null)
			return null;

		if (animationClip == null)
			return null;
        
        Animator animator = go.GetComponent<Animator>();
		if (animator != null && animator.runtimeAnimatorController == null)
			return null;

		if (!EditorApplication.isPlaying && AnimationMode.InAnimationMode())
		{
			AnimationMode.BeginSampling();
			AnimationMode.SampleAnimationClip(go, animationClip, sampleTime);
			AnimationMode.EndSampling();

			SceneView.RepaintAll();
		}
        return CreatePose();
    }

	void Update()
	{
		if (go == null)
			return;

		if (animationClip == null)
			return;

		// there is a bug in AnimationMode.SampleAnimationClip which crash unity if there is no valid controller attached
		Animator animator = go.GetComponent<Animator>();
		if (animator != null && animator.runtimeAnimatorController == null)
			return;

		if (!EditorApplication.isPlaying && AnimationMode.InAnimationMode())
		{
			AnimationMode.BeginSampling();
			AnimationMode.SampleAnimationClip(go, animationClip, time);
			AnimationMode.EndSampling();

			SceneView.RepaintAll();
		}
	}

    Pose CreatePose()
    {
        Pose pose = new Pose();
		pose.parent = new Joint(go.transform);
		pose.hips = new Joint(hips);
		pose.leftUpLeg = new Joint(leftUpLeg);
		pose.leftLeg = new Joint(leftLeg);
		pose.leftFoot = new Joint(leftFoot);
		pose.leftToeBase = new Joint(leftToeBase);
		pose.rightUpLeg = new Joint(rightUpLeg);
		pose.rightLeg = new Joint(rightLeg);
		pose.rightFoot = new Joint(rightFoot);
		pose.rightToeBase = new Joint(rightToeBase);
		pose.spine = new Joint(spine);
		pose.spine1 = new Joint(spine1);
		pose.spine2 = new Joint(spine2);
		pose.leftShoulder = new Joint(leftShoulder);
		pose.leftArm = new Joint(leftArm);
		pose.leftForeArm = new Joint(leftForeArm);
		pose.leftHand = new Joint(leftHand);
		pose.rightShoulder = new Joint(rightShoulder);
		pose.rightArm = new Joint(rightArm);
		pose.rightForeArm = new Joint(rightForeArm);
		pose.rightHand = new Joint(rightHand);
		pose.neck = new Joint(neck);
		pose.head = new Joint(head);
		pose.headTopEnd = new Joint(headTopEnd);
		return pose;
		
		
		//Pose newPose = (Pose) ScriptableObject.CreateInstance(typeof(Pose));
        //newPose.pose = go.transform.rotation.eulerAngles;
        //string name = "Assets/Poses/" + animationClip.name + poseCounter.ToString() + ".asset";
        //poseCounter++;
        //AssetDatabase.CreateAsset(newPose, name);
        //Debug.Log("Created asset: " + name);
    }

	void ToggleAnimationMode()
	{
		if(AnimationMode.InAnimationMode())
			AnimationMode.StopAnimationMode();
		else
			AnimationMode.StartAnimationMode();
	}
}
