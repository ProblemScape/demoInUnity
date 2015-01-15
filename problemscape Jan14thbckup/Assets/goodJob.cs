using UnityEngine;
using System.Collections;

public class goodJob : MonoBehaviour {

	public GUIStyle myStyle;
	//private RectOffset rctOff;
	// Use this for initialization
	void Start () {

		myStyle = new GUIStyle ();
		myStyle.normal.background = (Texture2D)Resources.Load ("emptyBox", typeof(Texture2D));
		myStyle.fontSize = 16;
		myStyle.fontStyle = FontStyle.Normal;
		myStyle.wordWrap = true;
		//rctOff.left = 5;
		//rctOff.right = 5;
		//rctOff.top = 5;
		//rctOff.bottom = 5;

		//myStyle.padding = rctOff;
		myStyle.alignment = TextAnchor.MiddleCenter;
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Box (new Rect (160, 160, 182, 86), "Good one! Why don't you help another Arithmen?", myStyle);
	}
}
