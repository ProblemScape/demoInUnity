using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class clickOn1 : MonoBehaviour {

	public Texture2D oneBtn, twoBtn, threeBtn, fourBtn, fiveBtn, sixBtn, sevenBtn, eightBtn, nineBtn, zeroBtn;
	public Texture2D xBtn, yBtn, zBtn, plusBtn, minusBtn, equalsBtn, multBtn, divBtn,submitBtn;
	public Texture2D  plusOpBtn, minusOpBtn, equalsOpBtn, multOpBtn, divOpBtn, restartBtn;
	public Texture2D okBtn, dropdownBox;
	public string expressionBoxString, expressionBoxString2, expressionBoxString3, expressionBoxString4, expressionBoxString5, resultBoxString;
	public GUISkin mySkin; 
	public int selectionGridInt=0, selectionGridInt1 = -1,selectionGridInt2 = -1;
	public string[] selectionStrings = new string[] {"+","-","*","/"}; 
	public int pastSelection = -1;
	public bool added = false, subtracted =  false, multiplied = false, divided = false, computed = false;
	public int addResult = 0, subResult = 0, multResult = 0;
	public float divResult = 0.0f;
	public GUIContent inputExpression;

	public static string[] partsOfExpression;
	public static List<string> listOfExpression;
	public bool processExpression;
	public GUIStyle exprStyle;

	void Start()
	{
		OnStart ();
	}

	void OnStart()
	{
		processExpression = false;

		exprStyle = new GUIStyle ();
		exprStyle.fontSize = 24;
		exprStyle.fontStyle = FontStyle.Bold;

		mySkin   = (GUISkin)Resources.Load("skinAlgebraPad", typeof(GUISkin));

		inputExpression = new GUIContent ();
		inputExpression.text = "";

		//dropdownBox = (Texture2D)Resources.Load("dropDownBox", typeof(Texture2D));

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
		yBtn     = (Texture2D)Resources.Load("y", typeof(Texture2D));
		zBtn     = (Texture2D)Resources.Load("z", typeof(Texture2D));
		plusBtn    = (Texture2D)Resources.Load("plus", typeof(Texture2D));
		minusBtn   = (Texture2D)Resources.Load("minus", typeof(Texture2D));
		equalsBtn  = (Texture2D)Resources.Load("equals", typeof(Texture2D));
		plusOpBtn  = (Texture2D)Resources.Load("plus", typeof(Texture2D));
		minusOpBtn = (Texture2D)Resources.Load("minus", typeof(Texture2D));
		multOpBtn  = (Texture2D)Resources.Load("multiply", typeof(Texture2D));
		divOpBtn   = (Texture2D)Resources.Load("divide", typeof(Texture2D));
		restartBtn   = (Texture2D)Resources.Load("Restart", typeof(Texture2D));

		okBtn = (Texture2D)Resources.Load ("okBtn", typeof(Texture2D));
		listOfExpression =  new List<string>();

	}

	void OnGUI () {
		
		float xPos = 550;
		float yPos = 350;
		float xGap = 42;
		float yGap = 42;
		float key_xPos = 550;
		float key_yPos = 450;
		float buttonSize = 40;

		int r;

		GUI.skin = mySkin; 

		if (!processExpression) {
			GUI.Label (new Rect (550, 240, 400, 30), "Enter Expression using keypad and press ok when done");
			GUI.Label (new Rect (560, 280, 600, 30), inputExpression);
			if (GUI.Button (new Rect (900, 280, 30, 30), okBtn)) {
				if(inputExpression.text != "") {
				//print(inputExpression.text);
				processInput (inputExpression.text);
				//showAnswer = true;
				//ans = enteredInstruction +". What is that? I don't get it!";
				}
			}
		} else {
			int listLength = listOfExpression.Count;
			float xPosExp = 550;
			if(listLength == 1) {
				GUI.Label (new Rect (550, 240, 400, 30), "Your expression evaluated to:");
				GUI.Label (new Rect (xPosExp, 280, 400, 30), listOfExpression[0],exprStyle);
				GUI.Label (new Rect (550, 340, 250, 60), "Do you want to evaluate another expression? Click ok");
				if (GUI.Button (new Rect (800, 340, 30, 30), okBtn)) {
					OnStart();
				}
			}
			else 
			{
				GUI.Label (new Rect (550, 240, 400, 30), "Click on the operator to evaluate");
				for(int i=0; i<listLength; i++) {
					if (int.TryParse(listOfExpression[i], out r)) {
						GUI.Label (new Rect (xPosExp, 280, 400, 30), listOfExpression[i],exprStyle);
						if(r>=100)
							xPosExp += 50;
						else 
							if (r>=10)
								xPosExp += 40;
							else
								xPosExp += 30;
					}
					else {
						if (GUI.Button (new Rect (xPosExp, 280, 30, 30),listOfExpression[i], exprStyle)) {
							int newVal = doOperation(listOfExpression[i-1],listOfExpression[i],listOfExpression[i+1]);
							listOfExpression[i+1] = newVal.ToString();
							listOfExpression.RemoveAt(i);
							listOfExpression.RemoveAt(i-1);
						}
						xPosExp += 30;
					}

				}
			}
		}

		//GUI.Label(new Rect(key_xPos, key_yPos -50, 250,30), dropdownBox);
		//Keypad
		if (GUI.Button (new Rect (key_xPos, key_yPos, buttonSize, buttonSize), oneBtn)) {
			inputExpression.text +="1";
			//updateExpression(focusBox, "1");
		}
		if (GUI.Button (new Rect (key_xPos+xGap,key_yPos, buttonSize, buttonSize), twoBtn)) {
			//updateExpression(focusBox, "2");
			inputExpression.text +="2";
		}
		if (GUI.Button (new Rect ((key_xPos + xGap * 2), key_yPos, buttonSize, buttonSize), threeBtn)) {
			//updateExpression(focusBox, "3");
			inputExpression.text +="3";
		}
		if (GUI.Button (new Rect ((key_xPos+xGap*3),key_yPos, buttonSize, buttonSize), fourBtn)) {
			//updateExpression(focusBox, "4");
			inputExpression.text +="4";
		}
		if (GUI.Button (new Rect (key_xPos,key_yPos+yGap, buttonSize, buttonSize), fiveBtn)) {
			//updateExpression(focusBox, "5");
			inputExpression.text +="5";
		}
		if (GUI.Button (new Rect (key_xPos+xGap,key_yPos+yGap, buttonSize, buttonSize), sixBtn)) {
			//updateExpression(focusBox, "6");
			inputExpression.text +="6";
		}
		if (GUI.Button (new Rect ((key_xPos+xGap*2),key_yPos+yGap, buttonSize, buttonSize), sevenBtn)) {
			//updateExpression(focusBox, "7");
			inputExpression.text +="7";
		}
		if (GUI.Button (new Rect ((key_xPos+xGap*3),key_yPos+yGap, buttonSize, buttonSize), eightBtn)) {
			//updateExpression(focusBox, "8");
			inputExpression.text +="8";
		}
		if (GUI.Button (new Rect (key_xPos,(key_yPos+yGap*2), buttonSize, buttonSize), nineBtn)) {
			//updateExpression(focusBox, "9");
			inputExpression.text +="9";
		}
		if (GUI.Button (new Rect (key_xPos + xGap, (key_yPos + yGap * 2), buttonSize, buttonSize), zeroBtn)) {
			//updateExpression(focusBox, "0");
			inputExpression.text +="0";
		}


		int add = 0, subtract = 1, multiply = 2, divide = 3;

		if (GUI.Button (new Rect (key_xPos + xGap*5,key_yPos, buttonSize , buttonSize ), plusOpBtn)) {
			//resultBoxString = calculate(add);
			inputExpression.text +=" + ";
		}
		if (GUI.Button (new Rect (key_xPos + xGap*5,key_yPos+yGap, buttonSize , buttonSize ), equalsBtn)) {
			//resultBoxString = calculate(add);
			//inputExpression.text +=" + ";
		}
		if (GUI.Button (new Rect (key_xPos+xGap*6, key_yPos, buttonSize , buttonSize ), minusOpBtn)) {
			//resultBoxString = calculate(subtract);
			inputExpression.text +=" - ";
		}
		if (GUI.Button (new Rect (key_xPos+xGap*7, key_yPos, buttonSize , buttonSize ), multOpBtn)) {
			//resultBoxString = calculate(multiply);
			inputExpression.text +=" * ";
		}
		if (GUI.Button (new Rect (key_xPos+xGap*8, key_yPos, buttonSize, buttonSize), divOpBtn)) {
			//resultBoxString = calculate (divide);
			inputExpression.text +=" / ";
		} 

		if (GUI.Button (new Rect (key_xPos,key_yPos+yGap*3+20, buttonSize, buttonSize), xBtn)) {
			//inputExpression.text +="8";
		}
		if (GUI.Button (new Rect (key_xPos+xGap,key_yPos+yGap*3+20, buttonSize, buttonSize), yBtn)) {
			//inputExpression.text +="8";
		}
		if (GUI.Button (new Rect (key_xPos+xGap*2,key_yPos+yGap*3+20, buttonSize, buttonSize), zBtn)) {
			//inputExpression.text +="8";
		}
		/*-v  else {
				resultBoxString = calculate(-1);
			 }
		
		
		if (GUI.Button (new Rect (500,300, 100 , buttonSize ), restartBtn)) {
			OnStart();
		}
*/
	
	}

	void processInput(string inputString) {
		partsOfExpression = inputString.Split (' ');
		foreach (string s in partsOfExpression) {
			listOfExpression.Add (s);
		}
		processExpression = true;
	}

	int doOperation(string a, string op, string b) {

		int a_int, b_int;
		int ans = 0;
		//print(a+"  "+op+"  "+b);
		int.TryParse (a, out a_int);
		int.TryParse (b, out b_int);
		switch(op) {
		case "*":
			ans = a_int * b_int;
			//return ans;
			//print (ans);
			break;
		case "/":
			ans = a_int / b_int;
			//return ans;
			break;
		case "+":
			ans = a_int + b_int;
			//return ans;
			break;
		case "-":
			ans = a_int - b_int;
			//retuen ans;
			break;
		}
		return ans;
	}

}
