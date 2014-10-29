using UnityEngine;
using System.Collections;

public class teachMe : MonoBehaviour {

	public GUISkin mySkin;
	public Texture2D submitBtn;
	public  bool showAnswer = false;
	public string enteredInstruction = " "; 
	public string ans = " ";

	// Use this for initialization
	void Start () {
		mySkin = (GUISkin)Resources.Load ("algebraPadSkin", typeof(GUISkin));
		submitBtn = (Texture2D)Resources.Load ("submit", typeof(Texture2D));
	}
	
	// Update is called once per frame
	void OnGUI () {

		GUI.skin = mySkin;
		if (showAnswer) {
			GUI.Label (new Rect (500, 250, 180, 100), ans);
		}
		enteredInstruction = GUI.TextArea (new Rect (225, 280, 240, 40), enteredInstruction);
		//GUI.Label (new Rect (520, 300, 240, 60), enteredInstruction);
		if (GUI.Button (new Rect (390,320, 80, 20), submitBtn )) {
			showAnswer = true;
			ans = enteredInstruction +". What is that? I don't get it!";
		}
	}
}
