using UnityEngine;
using System.Collections;
using System;

public class clickOn1 : MonoBehaviour {

	public Texture2D oneBtn, twoBtn, threeBtn, fourBtn, fiveBtn, sixBtn, sevenBtn, eightBtn, nineBtn, zeroBtn;
	public Texture2D xBtn, plusBtn, minusBtn, equalsBtn, multBtn, divBtn,submitBtn;
	public Texture2D  plusOpBtn, minusOpBtn, equalsOpBtn, multOpBtn, divOpBtn, restartBtn;
	public string expressionBoxString, expressionBoxString2, expressionBoxString3, expressionBoxString4, expressionBoxString5, resultBoxString;
	public GUISkin mySkin; 
	public int selectionGridInt=0, selectionGridInt1 = -1,selectionGridInt2 = -1;
	public string[] selectionStrings = new string[] {"+","-","*","/"}; 
	public int pastSelection = -1;
	public bool added = false, subtracted =  false, multiplied = false, divided = false, computed = false;
	public int addResult = 0, subResult = 0, multResult = 0;
	public float divResult = 0.0f;

	void Start()
	{
		OnStart ();
	}

	void OnStart()
	{
		mySkin   = (GUISkin)Resources.Load("algebraPadSkin", typeof(GUISkin));

		oneBtn   = (Texture2D)Resources.Load("one", typeof(Texture2D));
		twoBtn   = (Texture2D)Resources.Load("two", typeof(Texture2D));
		threeBtn = (Texture2D)Resources.Load("three", typeof(Texture2D));
		fourBtn   = (Texture2D)Resources.Load("four", typeof(Texture2D));
		fiveBtn   = (Texture2D)Resources.Load("five", typeof(Texture2D));
		sixBtn = (Texture2D)Resources.Load("six", typeof(Texture2D));
		sevenBtn   = (Texture2D)Resources.Load("seven", typeof(Texture2D));
		eightBtn   = (Texture2D)Resources.Load("eight", typeof(Texture2D));
		nineBtn = (Texture2D)Resources.Load("nine", typeof(Texture2D));
		zeroBtn = (Texture2D)Resources.Load("zero", typeof(Texture2D));

		xBtn     = (Texture2D)Resources.Load("x", typeof(Texture2D));
		plusBtn    = (Texture2D)Resources.Load("plus", typeof(Texture2D));
		minusBtn   = (Texture2D)Resources.Load("minus", typeof(Texture2D));
		equalsBtn  = (Texture2D)Resources.Load("equals", typeof(Texture2D));
		plusOpBtn  = (Texture2D)Resources.Load("plus", typeof(Texture2D));
		minusOpBtn = (Texture2D)Resources.Load("minus", typeof(Texture2D));
		multOpBtn  = (Texture2D)Resources.Load("multiply", typeof(Texture2D));
		divOpBtn   = (Texture2D)Resources.Load("divide", typeof(Texture2D));
		restartBtn   = (Texture2D)Resources.Load("Restart", typeof(Texture2D));

		expressionBoxString = "0";
		expressionBoxString2 = "0";
		expressionBoxString3 = "0";
		expressionBoxString4 = "1";
		expressionBoxString5 = "1"; 
		resultBoxString = expressionBoxString + "+" + expressionBoxString2 + "-" + expressionBoxString3 + "*" + expressionBoxString4 + "/" + expressionBoxString5;
		Debug.Log ("result = " + resultBoxString);

		added = subtracted = multiplied = divided = computed = false;
		addResult = subResult = multResult = 0; divResult = 0.0f;
	}

	void OnGUI () {
		
		float xPos = 550;
		float yPos = 350;
		float xGap = 40;
		float yGap = 40;
		float key_xPos = 550;
		float key_yPos = 450;
		float buttonSize = 50;
		
		
		GUI.skin = mySkin; 
		GUI.Label (new Rect (240, 100, 130, 30), "Enter Expression");
		GUI.SetNextControlName ("a");
		expressionBoxString  = GUI.TextField (new Rect (xPos,     150, 30, 30), expressionBoxString);
		GUI.SetNextControlName ("b");
		expressionBoxString2 = GUI.TextField (new Rect (xPos+80,  150, 30, 30), expressionBoxString2);
		GUI.SetNextControlName ("c");
		expressionBoxString3 = GUI.TextField (new Rect (xPos+160, 150, 30, 30), expressionBoxString3);
		GUI.SetNextControlName ("d");
		expressionBoxString4 = GUI.TextField (new Rect (xPos+240, 150, 30, 30), expressionBoxString4);
		GUI.SetNextControlName ("e");
		expressionBoxString5 = GUI.TextField (new Rect (xPos+320, 150, 30, 30), expressionBoxString5);

		resultBoxString = GUI.TextField (new Rect (380, 200, 100, 30), resultBoxString);

		//TODO: Update expressionBox according to which part has focus
		//Input.GeMouseButton, TouchPhase
		string focusBox = GUI.GetNameOfFocusedControl ();
		//Debug.Log ("focusBox = " + focusBox);

	

		//Keypad
		if (GUI.Button (new Rect (key_xPos, key_yPos, buttonSize, buttonSize), oneBtn)) {
			updateExpression(focusBox, "1");
		}
		if (GUI.Button (new Rect (key_xPos+xGap,key_yPos, buttonSize, buttonSize), twoBtn)) {
			updateExpression(focusBox, "2");
		}
		if (GUI.Button (new Rect ((key_xPos + xGap * 2), key_yPos, buttonSize, buttonSize), threeBtn)) {
			updateExpression(focusBox, "3");
		}
		if (GUI.Button (new Rect ((key_xPos+xGap*3),key_yPos, buttonSize, buttonSize), fourBtn)) {
			updateExpression(focusBox, "4");
		}
		if (GUI.Button (new Rect (key_xPos,key_yPos+yGap, buttonSize, buttonSize), fiveBtn)) {
			updateExpression(focusBox, "5");
		}
		if (GUI.Button (new Rect (key_xPos+xGap,key_yPos+yGap, buttonSize, buttonSize), sixBtn)) {
			updateExpression(focusBox, "6");
		}
		if (GUI.Button (new Rect ((key_xPos+xGap*2),key_yPos+yGap, buttonSize, buttonSize), sevenBtn)) {
			updateExpression(focusBox, "7");
		}
		if (GUI.Button (new Rect ((key_xPos+xGap*3),key_yPos+yGap, buttonSize, buttonSize), eightBtn)) {
			updateExpression(focusBox, "8");
		}
		if (GUI.Button (new Rect (key_xPos,(key_yPos+yGap*2), buttonSize, buttonSize), nineBtn)) {
			updateExpression(focusBox, "9");
		}
		if (GUI.Button (new Rect (key_xPos + xGap, (key_yPos + yGap * 2), buttonSize, buttonSize), zeroBtn)) {
			updateExpression(focusBox, "0");
		}


		int add = 0, subtract = 1, multiply = 2, divide = 3;

		if (GUI.Button (new Rect (xPos + xGap,yPos, buttonSize , buttonSize ), plusOpBtn)) {
			resultBoxString = calculate(add);
		}
		if (GUI.Button (new Rect (xPos+80+xGap, yPos, buttonSize , buttonSize ), minusOpBtn)) {
			resultBoxString = calculate(subtract);
		}
		if (GUI.Button (new Rect (xPos+160+xGap, yPos, buttonSize , buttonSize ), multOpBtn)) {
			resultBoxString = calculate(multiply);
		}
		if (GUI.Button (new Rect (xPos + 240 + xGap, yPos, buttonSize, buttonSize), divOpBtn)) {
			resultBoxString = calculate (divide);
		} 
		else {
				resultBoxString = calculate(-1);
			 }
		
		
		if (GUI.Button (new Rect (500,300, 100 , buttonSize ), restartBtn)) {
			OnStart();
		}

	
	}

	void 	updateExpression (string focusCtrl, string btnStr)
		{

				switch (focusCtrl) {
				case "a": 
						expressionBoxString = expressionBoxString + btnStr;
						break;
				case "b":
						expressionBoxString2 = expressionBoxString2 + btnStr;
						break;
				case "c":
						expressionBoxString3 = expressionBoxString3 + btnStr;
						break;
				case "d":
						expressionBoxString4 = expressionBoxString4 + btnStr;
						break;
				case "e":
						expressionBoxString5 = expressionBoxString5 + btnStr;
						break;
				default :
						expressionBoxString = expressionBoxString + "0";
						break;
				}
		}

	string calculate(int op=0) {
	
		//right now you can do each operation only once
		int op1 = 0;
		int op2 = 0;
		string a, b, c, d, e; // a+b-c*d/e
		string resultString;

		a = expressionBoxString + " + ";
		b = expressionBoxString2 + " - ";
		c = expressionBoxString3 + " * ";
		d = expressionBoxString4 + " / ";
		e = expressionBoxString5 + " ";
	

				if (op == 0) {
						//if ( added )
						//	return(addResult.ToString());
			           
						op1 = Convert.ToInt32 (expressionBoxString);
						op2 = Convert.ToInt32 (expressionBoxString2);
			 
			            if ( subtracted )
							op2 = subResult;
			            if ( multiplied )
							op2 = multResult;
			
			            //TODO: need to fix with order of op
			            //if ( computed )
						//	op2 = Convert.ToInt32 (resultBoxString);
						computed = true;
						addResult = op1+op2;
						added = true;

				} else if (op == 1) {

						op1 = Convert.ToInt32 (expressionBoxString2);
						if ( added ) 
							op1 += Convert.ToInt32 (expressionBoxString);

						op2 = Convert.ToInt32 (expressionBoxString3);
						if ( multiplied || divided ) 
							op2 = multResult;

						subResult = op1 - op2;
						subtracted = true;
			            computed = true;
						
				} else if (op == 2) {
						op1 = Convert.ToInt32 (expressionBoxString3);
						op2 = Convert.ToInt32 (expressionBoxString4);

			            if ( subtracted )
							op1 = Convert.ToInt32 (expressionBoxString2) - Convert.ToInt32 (expressionBoxString3);
						if( added )
							op1 += Convert.ToInt32 (expressionBoxString);

			            
						multiplied = true;
						multResult = op1 * op2;
						computed = true;
						
				} else if (op == 3) {
						op1 = Convert.ToInt32 (expressionBoxString4);
						op2 = Convert.ToInt32 (expressionBoxString5);

						divided = true;
			            if (multiplied)
							divResult = multResult/op2;
						else 
							divResult = op1 / op2;
						computed = true;
		        }

		if (added) {
			a = "";
			b = addResult.ToString()+ " - ";
		} else {
			a = expressionBoxString + " + ";
		}

		if (subtracted) {
			b = "";
			c = subResult.ToString () + " * ";
				} else {

				}
		if (multiplied) {
			c = "";
			d = multResult.ToString() + " / ";
				}

		if (divided) {
			d = "";
			e = divResult.ToString();
		}


		resultString = a + b + c + d + e;
		              

		return(resultString);
		//return ((new Expression (expressionBoxString).Evaluate ()).ToString());
	}


}
