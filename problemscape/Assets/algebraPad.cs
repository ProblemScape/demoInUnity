using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class algebraPad : MonoBehaviour {

	public Texture2D oneBtn, twoBtn, threeBtn, fourBtn, fiveBtn, sixBtn, sevenBtn, eightBtn, nineBtn, zeroBtn;
	public Texture2D xBtn, yBtn, zBtn, plusBtn, minusBtn, equalsBtn, multBtn, divBtn,submitBtn;
	public Texture2D  plusOpBtn, minusOpBtn, equalsOpBtn, multOpBtn, divOpBtn, restartBtn;
	public Texture2D okBtn, pemdas;
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
	public static List<Problems> listOfProblems;

	public bool processExpression;
	//public bool practise;
	public GUIStyle exprStyle;
	public int listLength = 0;
	public int correctAnswers;
	
	void Start()
	{
		//currentProblemNo = 0;
		//level = 1; 
		correctAnswers = 0;
		initializeProblemSet ();
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
		
		plusBtn    = (Texture2D)Resources.Load("plus", typeof(Texture2D));
		minusBtn   = (Texture2D)Resources.Load("minus", typeof(Texture2D));
		equalsBtn  = (Texture2D)Resources.Load("equals", typeof(Texture2D));
		plusOpBtn  = (Texture2D)Resources.Load("plus", typeof(Texture2D));
		minusOpBtn = (Texture2D)Resources.Load("minus", typeof(Texture2D));
		multOpBtn  = (Texture2D)Resources.Load("multiply", typeof(Texture2D));
		divOpBtn   = (Texture2D)Resources.Load("divide", typeof(Texture2D));
		restartBtn   = (Texture2D)Resources.Load("Restart", typeof(Texture2D));
		pemdas = (Texture2D)Resources.Load("pemdas", typeof(Texture2D));
		
		okBtn = (Texture2D)Resources.Load ("okBtn", typeof(Texture2D));
		listOfExpression =  new List<string>();

		
	}
	
	void OnGUI () {
		
		float xPos = 540;
		float yPos = 0;
		float xGap = 42;
		float yGap = 42;
		float key_xPos = 540;
		float key_yPos = 340;
		float buttonSize = 40;
		float xPosExp = 570;
		
		int r;

		
		GUI.skin = mySkin; 

		if (lockProblem.practise) {
			GUI.Label (new Rect (xPos, yPos+100, 380, 70), "Since this is your first time evaluating expressions with math pad, let's do some practising!");
			if (!processExpression) {
				//print("ca: "+ correctAnswers);

				if (correctAnswers == 2) {
					switch(lockProblem.level) {
					case 1:
						lockProblem.currentProblemNo = 5;
						lockProblem.level = 2;
						correctAnswers = 0;
						processInput (listOfProblems [lockProblem.currentProblemNo].pStatement);
						break;
					case 2:
						lockProblem.practise = false;
						processExpression = false;
						break;
					}
				}
				else {
					if(lockProblem.currentProblemNo >= 9) {
						lockProblem.practise = false;
						processExpression = false;
					} else {
					processInput (listOfProblems [lockProblem.currentProblemNo].pStatement);
					}	//print("list length: "+ listLength);
				}
			}
			else {
				listLength = listOfExpression.Count;
				if (listLength == 1) {
					GUI.Label (new Rect (xPos+20, yPos+150, 400, 30), "Your expression evaluated to:");
					GUI.Label (new Rect (xPosExp, yPos+180, 400, 30), listOfExpression [0], exprStyle);
					if (listOfExpression [0].Equals (listOfProblems [lockProblem.currentProblemNo].pAnswer)) {
						GUI.Label (new Rect (xPos+20, yPos+240, 280, 60), "Good Job. Try another? ");
						if (GUI.Button (new Rect (780, yPos+238, 30, 30), okBtn)) {
							correctAnswers++;
							lockProblem.currentProblemNo++;
							OnStart ();
						}
					} else 
					{
						GUI.Label (new Rect (630, yPos+180, 280, 60), "That is not correct. Do you remember the order of operations? ");
						GUI.Label (new Rect (xPos+20, yPos+450, 280, 60), "Let's try again. ");
						GUI.Box(new Rect (580, yPos+240, 218, 170),pemdas);
						correctAnswers = 0;
						if (GUI.Button (new Rect (680, yPos+450, 30, 30), okBtn)) {
							OnStart ();
						}
					}
				} else 
				{
					GUI.Label (new Rect (xPos+20, yPos+150, 400, 30), "Click on the operator to evaluate the expression");
					//print(listLength);
					for (int i=0; i<listLength; i++) {
						//print("loe: "+ listOfExpression [i]);
						if (int.TryParse (listOfExpression [i], out r)) {
								//print("r: "+r);
							GUI.Label (new Rect (xPosExp, yPos+180, 400, 30), listOfExpression [i], exprStyle);
								//for number of digits
							if (r >= 100)
								xPosExp += 50;
							else if (r >= 10)
								xPosExp += 40;
							else
								xPosExp += 30;
						} else 
						{
							if (GUI.Button (new Rect (xPosExp, yPos+180, 30, 30), listOfExpression [i], exprStyle)) {
								int newVal = doOperation (listOfExpression [i - 1], listOfExpression [i], listOfExpression [i + 1]);
								listOfExpression [i + 1] = newVal.ToString ();
								listOfExpression.RemoveAt (i);
								listOfExpression.RemoveAt (i - 1);
							}
							xPosExp += 30;
						}
						
					}
				}
			}
		} else 
		{
			GUI.Label (new Rect (530, yPos+100, 380, 70), "You are now ready to write your own expressions for evaluating.");
			if (!processExpression) {
				GUI.Label (new Rect (530, yPos+150, 400, 30), "Enter Expression using keypad and press ok when done");
				GUI.Label (new Rect (530, yPos+180, 600, 30), inputExpression);
				if (GUI.Button (new Rect (850, yPos+180, 30, 30), okBtn)) {
					if (inputExpression.text != "") {
							//print(inputExpression.text);
							processInput (inputExpression.text);
							//showAnswer = true;
							//ans = enteredInstruction +". What is that? I don't get it!";
						}
					}
				} else {
					listLength = listOfExpression.Count;
					if (listLength == 1) {
						GUI.Label (new Rect (550, yPos+150, 400, 30), "Your expression evaluated to:");
						GUI.Label (new Rect (xPosExp, yPos+180, 400, 30), listOfExpression [0], exprStyle);
						GUI.Label (new Rect (550, yPos+240, 250, 60), "Do you want to evaluate another expression? Click ok");
						if (GUI.Button (new Rect (800, yPos+240, 30, 30), okBtn)) {
							OnStart ();
						}
					} else {
						GUI.Label (new Rect (550, yPos+150, 400, 30), "Click on the operator to evaluate");
						for (int i=0; i<listLength; i++) {
							if (int.TryParse (listOfExpression [i], out r)) {
								GUI.Label (new Rect (xPosExp, yPos+180, 400, 30), listOfExpression [i], exprStyle);
								//for number of digits
								if (r >= 100)
									xPosExp += 50;
								else if (r >= 10)
									xPosExp += 40;
								else
									xPosExp += 30;
							} else {
								if (GUI.Button (new Rect (xPosExp, yPos+180, 30, 30), listOfExpression [i], exprStyle)) {
									int newVal = doOperation (listOfExpression [i - 1], listOfExpression [i], listOfExpression [i + 1]);
									listOfExpression [i + 1] = newVal.ToString ();
									listOfExpression.RemoveAt (i);
									listOfExpression.RemoveAt (i - 1);
								}
								xPosExp += 30;
							}
					
						}
					}
				}
		
				//GUI.Label(new Rect(key_xPos, key_yPos -50, 250,30), dropdownBox);
				//Keypad
				if (GUI.Button (new Rect (key_xPos, key_yPos, buttonSize, buttonSize), oneBtn)) {
					inputExpression.text += "1";
					//updateExpression(focusBox, "1");
				}
				if (GUI.Button (new Rect (key_xPos + xGap, key_yPos, buttonSize, buttonSize), twoBtn)) {
					//updateExpression(focusBox, "2");
					inputExpression.text += "2";
				}
				if (GUI.Button (new Rect ((key_xPos + xGap * 2), key_yPos, buttonSize, buttonSize), threeBtn)) {
					//updateExpression(focusBox, "3");
					inputExpression.text += "3";
				}
				if (GUI.Button (new Rect ((key_xPos + xGap * 3), key_yPos, buttonSize, buttonSize), fourBtn)) {
					//updateExpression(focusBox, "4");
					inputExpression.text += "4";
				}
				if (GUI.Button (new Rect (key_xPos, key_yPos + yGap, buttonSize, buttonSize), fiveBtn)) {
					//updateExpression(focusBox, "5");
					inputExpression.text += "5";
				}
				if (GUI.Button (new Rect (key_xPos + xGap, key_yPos + yGap, buttonSize, buttonSize), sixBtn)) {
					//updateExpression(focusBox, "6");
					inputExpression.text += "6";
				}
				if (GUI.Button (new Rect ((key_xPos + xGap * 2), key_yPos + yGap, buttonSize, buttonSize), sevenBtn)) {
					//updateExpression(focusBox, "7");
					inputExpression.text += "7";
				}
				if (GUI.Button (new Rect ((key_xPos + xGap * 3), key_yPos + yGap, buttonSize, buttonSize), eightBtn)) {
					//updateExpression(focusBox, "8");
					inputExpression.text += "8";
				}
				if (GUI.Button (new Rect (key_xPos, (key_yPos + yGap * 2), buttonSize, buttonSize), nineBtn)) {
					//updateExpression(focusBox, "9");
					inputExpression.text += "9";
				}
				if (GUI.Button (new Rect (key_xPos + xGap, (key_yPos + yGap * 2), buttonSize, buttonSize), zeroBtn)) {
					//updateExpression(focusBox, "0");
					inputExpression.text += "0";
				}
		
		
				int add = 0, subtract = 1, multiply = 2, divide = 3;
		
				if (GUI.Button (new Rect (key_xPos + xGap * 5, key_yPos, buttonSize, buttonSize), plusOpBtn)) {
					//resultBoxString = calculate(add);
					inputExpression.text += " + ";
				}
				if (GUI.Button (new Rect (key_xPos + xGap * 6, key_yPos, buttonSize, buttonSize), minusOpBtn)) {
					//resultBoxString = calculate(subtract);
					inputExpression.text += " - ";
				}
				if (GUI.Button (new Rect (key_xPos + xGap * 7, key_yPos, buttonSize, buttonSize), multOpBtn)) {
					//resultBoxString = calculate(multiply);
					inputExpression.text += " * ";
				}
				if (GUI.Button (new Rect (key_xPos + xGap * 8, key_yPos, buttonSize, buttonSize), divOpBtn)) {
					//resultBoxString = calculate (divide);
					inputExpression.text += " / ";
				} 

			}
		}

	void initializeProblemSet() {
		listOfProblems = new List<Problems> ();
		listOfProblems.Add (new Problems("3 + 4", "7"));
		listOfProblems.Add (new Problems("7 + 4 - 2", "9"));
		listOfProblems.Add (new Problems("5 * 4 / 2", "10"));
		listOfProblems.Add (new Problems("6 + 2 - 3", "5"));
		listOfProblems.Add (new Problems("9 - 4 + 1", "6"));
		listOfProblems.Add (new Problems("4 + 4 * 2", "12"));
		listOfProblems.Add (new Problems("7 * 2 + 2", "16"));
		listOfProblems.Add (new Problems("3 + 4 / 2","5"));
		listOfProblems.Add (new Problems("6 - 4 / 2","4"));
		listOfProblems.Add (new Problems("12 / 4 - 2", "1"));
	}

	void processInput(string inputString) {
		//print (inputString);
		partsOfExpression = inputString.Split (' ');
		foreach (string s in partsOfExpression) {
			listOfExpression.Add (s);
			//print(s);
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

public class Problems 
{
	public string pStatement;
	public string pAnswer;

	public Problems(string p, string a) {
		pStatement = p;
		pAnswer = a;
	}

}
