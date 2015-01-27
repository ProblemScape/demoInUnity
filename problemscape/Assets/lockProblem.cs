using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;

public class lockProblem : MonoBehaviour {

	public GUISkin mySkin;
	public GameObject algebraPad;
	public bool usingAlgebraPad; 
	public GUIContent forAlgebraPadToggle;
	public bool aPadClosed = true;
	public Texture2D useAlgebraPadBtn, closeAlgebraPadBtn;
	public static int currentProblemNo, level;
	public static bool practise = true;
	public string lockAnswer1, lockAnswer2, lockAnswer3; 
	public bool answer1Correct, answer2Correct, answer3Correct;


	// Use this for initialization
	void Start () {
		answer1Correct = false;
		answer2Correct = false;
		answer3Correct = false;
		mySkin = (GUISkin)Resources.Load("skinLockProblem", typeof(GUISkin));
		useAlgebraPadBtn = (Texture2D)Resources.Load("useAPad", typeof(Texture2D));
		closeAlgebraPadBtn = (Texture2D)Resources.Load("closeAPad", typeof(Texture2D));
		forAlgebraPadToggle = new GUIContent ("", useAlgebraPadBtn);
		currentProblemNo = 0;
		level = 1; 
		lockAnswer1 = " ";
		lockAnswer2 = " ";
		lockAnswer3 = " ";
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.skin = mySkin;
		if ((answer1Correct) && (answer2Correct) && (answer3Correct)) {
			if(!aPadClosed){
				Destroy(algebraPad);
			}
			part1.sceneNo = 4;
		}
		else {
			GUI.Box (new Rect (110, 525, 370, 100), "Mayor: I have seen this kind of lock before. I think when he enters the correct answer, the number will turn green. Perhaps he should use the algebra pad for help.");
			GUI.Box (new Rect (280, 105, 200, 50), "Click on the brown box and enter the answer");
			if (!lockAnswer1.Equals ("15"))  {
				lockAnswer1 = GUI.TextField (new Rect (260, 185, 30, 30), lockAnswer1);
				lockAnswer1 = Regex.Replace (lockAnswer1, @"[^0-9]", "");
			} else {
				GUI.Label (new Rect (260, 185, 30, 30), "15");
				answer1Correct = true;;
			}
			if (!lockAnswer2.Equals ("8"))  {
				lockAnswer2 = GUI.TextField (new Rect (260, 235, 30, 30), lockAnswer2);
				lockAnswer2 = Regex.Replace (lockAnswer2, @"[^0-9]", "");
			} else {
				GUI.Label (new Rect (260, 235, 30, 30), "8");
				answer2Correct = true;
			}
			if (!lockAnswer3.Equals ("0"))  {
				lockAnswer3 = GUI.TextField (new Rect (260, 284, 30, 30), lockAnswer3);
				lockAnswer3 = Regex.Replace (lockAnswer3, @"[^0-9]", "");
			} else {
				GUI.Label (new Rect (260, 284, 30, 30), "0");
				answer3Correct = true;
			}
			if(GUI.Toggle(new Rect(635, 530, 180, 75), usingAlgebraPad, forAlgebraPadToggle))
			{
				if(aPadClosed) {
					algebraPad = (GameObject)Instantiate(Resources.Load("algebraPad0"));
					forAlgebraPadToggle.image = closeAlgebraPadBtn;
					aPadClosed = false;
				}
				else {
					Destroy(algebraPad);
					forAlgebraPadToggle.image = useAlgebraPadBtn;
					aPadClosed = true;
				}
			}
	
		}
	}
}
