using UnityEngine;
using System.Collections;

public class clickOn1 : MonoBehaviour {

public Texture2D oneBtn, twoBtn, threeBtn, xBtn, plusBtn, minusBtn, equalsBtn;
public string expressionBoxString;
public GUISkin mySkin; 

	void Start()
	{
		mySkin = (GUISkin)Resources.Load("algebraPadSkin", typeof(GUISkin));
		oneBtn = (Texture2D)Resources.Load("one", typeof(Texture2D));
		twoBtn = (Texture2D)Resources.Load("two", typeof(Texture2D));
		threeBtn = (Texture2D)Resources.Load("three", typeof(Texture2D));
		xBtn = (Texture2D)Resources.Load("x", typeof(Texture2D));
		plusBtn = (Texture2D)Resources.Load("plus", typeof(Texture2D));
		minusBtn = (Texture2D)Resources.Load("minus", typeof(Texture2D));
		equalsBtn = (Texture2D)Resources.Load("equals", typeof(Texture2D));
	}

	void OnGUI () {

		GUI.skin = mySkin;

		GUI.Label (new Rect (240, 150, 130, 30), "Enter Expression");
		expressionBoxString = GUI.TextField (new Rect (380, 150, 200, 30), expressionBoxString);

		if (GUI.Button (new Rect (340,300, 30, 30), oneBtn)) {
			expressionBoxString = expressionBoxString+"1";
		}
		if (GUI.Button (new Rect (380,300, 30, 30), twoBtn)) {
			expressionBoxString = expressionBoxString+"2";
		}
		if (GUI.Button (new Rect (420,300, 30, 30), threeBtn)) {
			expressionBoxString = expressionBoxString+"3";
		}
		if (GUI.Button (new Rect (210,300, 30, 30), xBtn)) {
			expressionBoxString = expressionBoxString+"x";
		}
		if (GUI.Button (new Rect (520,300, 30, 30), plusBtn)) {
			expressionBoxString = expressionBoxString+"+";
		}
		if (GUI.Button (new Rect (560,300, 30, 30), minusBtn)) {
			expressionBoxString = expressionBoxString+"-";
		}
		if (GUI.Button (new Rect (600,300, 30, 30), equalsBtn)) {
			expressionBoxString = expressionBoxString+"=";
		}

	}


}
