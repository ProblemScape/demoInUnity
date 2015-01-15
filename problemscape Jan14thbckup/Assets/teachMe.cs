using UnityEngine;
using System.Collections;
using System.Linq;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class teachMe : MonoBehaviour {

	public GUISkin mySkin;
	public GUIStyle stepStyle;

	public GameObject instructions; 
	public bool insOpen;

	public Texture2D submitBtn, closeBtn;
	public  bool showAnswer = false, showAnswer2 = false, showAnswer3 = false;
	public string enterInstruction; 
	public string ans = " ";

	public bool firstStepDone = false;
	public int stepNum;

	public float xPos = 120;
	public float yPos = Screen.height / 2;
	public string inputDumpFilename;
	public bool startNow;


	// Use this for initialization
	void Start () {
		mySkin = (GUISkin)Resources.Load ("skinTeachPad", typeof(GUISkin));
		submitBtn = (Texture2D)Resources.Load ("okBtn", typeof(Texture2D));
		closeBtn = (Texture2D)Resources.Load ("closeBtn", typeof(Texture2D));

		stepStyle = new GUIStyle ();
		stepStyle.fontSize = 14;
		stepStyle.fontStyle = FontStyle.Bold;

		enterInstruction = "Enter instruction here: ";
		stepNum = 1;
		inputDumpFilename = "allresponses_test.txt";

		startNow = true;
		insOpen = false;

	}
	
	// Update is called once per frame
	void OnGUI () {
		if (startNow) {
			if(!insOpen) {
				insOpen = true;
				instructions = (GameObject)Instantiate (Resources.Load ("teachPadInstructions"));
			}
			if (GUI.Button (new Rect (xPos + 740, 75, 50, 50), closeBtn)) {
				startNow = false;
				Destroy(instructions);
			}
		} else {
			GUI.skin = mySkin;
			float yPosNew;
			//GUI.Label (new Rect (xPos, yPos + 10, 500, 50), "Thank you, kind Giant! ");
			GUI.Label (new Rect (xPos, yPos - 10, 700, 100), "Can you please teach me one step at a time. If I understand what you are saying, and get the correct answer, we can move on to the next step.");
			switch (stepNum) {
			case 1:
				if (showAnswer) {
					GUI.Label (new Rect (xPos, yPos + 115, 700, 100), ans);
				}
				GUI.Label (new Rect (xPos, yPos + 40, 500, 50), "Step1: How much money in Rubies does the Arithman have?", stepStyle);
				GUI.Label (new Rect (xPos + 420, yPos + 37, 500, 50), "(Enter your step and press the OK button)");
				enterInstruction = GUI.TextArea (new Rect (xPos, yPos + 60, 700, 40), enterInstruction);
			//GUI.Label (new Rect (520, 300, 240, 60), enteredInstruction);
				if (GUI.Button (new Rect (xPos + 710, yPos + 55, 50, 50), submitBtn)) {
					ans = processStep1 (enterInstruction);
					showAnswer = true;
					//ans = enteredInstruction +". What is that? I don't get it!";
				}
				break;
			case 2:
				yPosNew = yPos + 30;
				GUI.Label (new Rect (xPos, yPos + 40, 500, 50), "STEP1:", stepStyle);
				GUI.Label (new Rect (xPos + 55, yPos + 38, 500, 50), "Yay! I got the first step! Multiply 12 by 5 and then add 6. The answer is 66.");
				if (showAnswer) {
					GUI.Label (new Rect (xPos, yPosNew + 115, 700, 100), ans);
				}
				GUI.Label (new Rect (xPos, yPosNew + 40, 500, 50), "Step2: How much money is left for paintballs?", stepStyle);
				GUI.Label (new Rect (xPos + 365, yPosNew + 37, 500, 50), "(Enter your step and press the OK button)");
				enterInstruction = GUI.TextArea (new Rect (xPos, yPosNew + 60, 700, 40), enterInstruction);
			//GUI.Label (new Rect (520, 300, 240, 60), enterInstruction);
				if (GUI.Button (new Rect (xPos + 710, yPosNew + 55, 50, 50), submitBtn)) {
					ans = processStep2 (enterInstruction);
					showAnswer = true;
					//ans = enteredInstruction +". What is that? I don't get it!";
				}
				break;
			case 3:
				yPosNew = yPos + 60;
				GUI.Label (new Rect (xPos, yPos + 40, 500, 50), "STEP1:", stepStyle);
				GUI.Label (new Rect (xPos + 55, yPos + 38, 500, 50), "Yay! I got the first step! Multiply 12 by 5 and then add 6. The answer is 66.");
				GUI.Label (new Rect (xPos, yPos + 60, 500, 50), "STEP2:", stepStyle);
				GUI.Label (new Rect (xPos + 55, yPos + 58, 500, 50), "Yay! I got the next step! Subtract 36 from 66. The answer is 30.");

				if (showAnswer) {
					GUI.Label (new Rect (xPos, yPosNew + 115, 700, 100), ans);
				}
				GUI.Label (new Rect (xPos, yPosNew + 40, 500, 50), "Step3: How many paintballs can the Arithman get?", stepStyle);
				GUI.Label (new Rect (xPos + 365, yPosNew + 37, 500, 50), "(Enter your step and press the OK button)");
				enterInstruction = GUI.TextArea (new Rect (xPos, yPosNew + 60, 700, 40), enterInstruction);
			//GUI.Label (new Rect (520, 300, 240, 60), enterInstruction);
				if (GUI.Button (new Rect (xPos + 710, yPosNew + 55, 50, 50), submitBtn)) {
					ans = processStep3 (enterInstruction);
					showAnswer = true;
					//ans = enteredInstruction +". What is that? I don't get it!";
				}
				break;
			case 0:
				GUI.Label (new Rect (xPos, yPos + 40, 500, 50), "STEP1:", stepStyle);
				GUI.Label (new Rect (xPos + 55, yPos + 38, 500, 50), "Yay! I got the first step! Multiply 12 by 5 and then add 6. The answer is 66.");
				GUI.Label (new Rect (xPos, yPos + 64, 500, 50), "STEP2:", stepStyle);
				GUI.Label (new Rect (xPos + 55, yPos + 62, 500, 50), "Yay! I got the next step! Subtract 36 from 66. The answer is 30.");
				GUI.Label (new Rect (xPos, yPos + 94, 500, 50), "STEP3:", stepStyle);
				GUI.Label (new Rect (xPos + 55, yPos + 86, 600, 50), "Yes. Divide 36 by 2 to get the number of paintballs to load. The answer is 15. \nYippie! I know how to sell paintballs now. I want to try this with a different customer!");
				break;
			}

		}
	}
	string processStep1(string inputText) {

		//First add all input to a file - to keep track of responses so that we can learn to process them
		System.IO.StreamWriter sr = new System.IO.StreamWriter(inputDumpFilename, true);
		sr.WriteLine("STEP 1: "+inputText);
		sr.Close();

		bool thereIsANumber = false, thereIsAChangeWord = false, thereIsAGemWord = false;
		string answerString = " ";
		string [] table1 = {
			"convert",
			"Convert",
			"change",
			"Change",
			"transfer",
			"Transfer",
			"modify",
			"Modify",
			"morph",
			"Morph"
		};
		string [] tableGems = {"gems", "Gems", "emeralds", "Emeralds", "emerald", "Emerald","rubies", "Rubies", "ruby", "Ruby", "sapphires", "Sapphires", "sapphire", "Sapphire", "saphire", "Saphire", "diamond","Diamond","diamonds","Diamonds", "topaz", "Topaz"};
		int i = 0;
		string conv = " ", gemWord = " ";
		char[] delimiterChars = { ' ', ',', '.', ':', '\t', '!' };
		string[] words = inputText.Split (delimiterChars);

		foreach (string s in words) {
			//print(s);
			if (int.TryParse (s, out i)) {
				thereIsANumber = true;
				break;
			}
		}
		if (!thereIsANumber) {
			foreach (string s in words) {
				if (table1.Contains (s)) {
					thereIsAChangeWord = true;
					conv = s;
					break;
				}
			}
			if (thereIsAChangeWord) {
				foreach (string s in words) {
					if (tableGems.Contains (s)) {
						thereIsAGemWord = true;
						gemWord = s;
						break;
					}
				}
				if (!thereIsAGemWord) {
					answerString = "I don't understand. What should I " + conv + "?";
				} else {
					answerString = "OK. You want to " + conv + " " + gemWord + ". Can you explain how?";
				}
			} else
				answerString = "I'm so sorry. I don't understand. Can you rephrase?";
		}

		//there are numbers in the input. Deal with it!

		else {
			//strip input
			string strippedInput = Regex.Replace(inputText, @"\W+", " ");
			//print (inputText+":"+strippedInput);

			string pattern1 = @"(multiply)(\s)((\w+(\s?)){0,3})(emeralds|12)(\s)((\w+(\s?)){0,3})(5)(\s)((\w+(\s?)){0,5})(add)(\s)((\w+(\s?)){0,3})(rubies|6)";

			//string tstPattern1 = @"(multiply)(\s)((\w+(\s?)){0,3})(?<num1>(\d+){1,2})(\s)((\w+(\s?)){0,3})(5)(\s)((\w+(\s?)){0,5})(add)(\s)((\w+(\s?)){0,3})(rubies|6)";
			/*match = Regex.Match(strippedInput, pattern1, RegexOptions.IgnoreCase);
			while (match.Success)
			{
				print(match.Groups["num1"].Value);
				print(match.Value+":    "+ match.Index);
				match = match.NextMatch();
			}*/
			//string pattern1 = @"(multiply)(\s)(emeralds)(\s)((\w+(\s?)){0,3})(5)(\s)((\w+(\s?)){0,5})(add)(\s)(rubies)";
			string pattern2 = @"(twelve|12)(\s*)(times)(\s*)(five|5)(\s*)(plus)(\s*)(six|6)";
			//string pattern3 = @"(multiply*)(rubies
			if ((Regex.IsMatch(strippedInput, pattern1, RegexOptions.IgnoreCase)) | (Regex.IsMatch(strippedInput, pattern2, RegexOptions.IgnoreCase))) {
				//answerString = "ok. Got it. Multiply 12 by 5 and then add 6.\nThe answer is 66. Let's move on to the next step.";
				stepNum = 2;
				enterInstruction = "Enter instruction here: ";
				showAnswer = false;
			}
			else {
				answerString = "I didn't understand that. Can you rephrase the instruction?";
			}

		}
		return(answerString);
	}

	string processStep2(string inputText) {

		System.IO.StreamWriter sr = new System.IO.StreamWriter(inputDumpFilename, true);
		sr.WriteLine("STEP 2: "+inputText);
		sr.Close();

		string answerString = "  ";
		string pattern2_1 = @"(subtract)(\s)((\w+(\s?)){0,3})((paintball shooter)|36)";
		if (Regex.IsMatch(inputText, pattern2_1, RegexOptions.IgnoreCase)) {
			//answerString = "ok. Got it. Subtract 36 from 66.\nThe answer is 30. Let's move on to the next step.";
			stepNum = 3;
			enterInstruction = "Enter instruction here: ";
			showAnswer = false;
		}
		else {
			answerString = "Your instruction is confusing me. Can you reword or make it simpler?";
		}
		//print (answerString);
		return answerString;
	}

	string processStep3(string inputText) {

		System.IO.StreamWriter sr = new System.IO.StreamWriter(inputDumpFilename, true);
		sr.WriteLine("STEP 3: "+inputText);
		sr.Close();

		string answerString = "  ";
		string pattern3_1 = @"(divide)(\s)((\w+(\s?)){0,3})((cost of paintball)| 2)";
		if (Regex.IsMatch(inputText, pattern3_1, RegexOptions.IgnoreCase)) {
			//answerString = "Yes. Divide 36 by 2 to get the number of paintballs to load. The answer is 15. \nYippie! I know how to sell paintballs now. I want to try on a different customer!.";
			stepNum = 0;
			showAnswer = false;
		}
		else {
			answerString = "I don't get it. Can you say that differently?";
		}
		//print (answerString);
		return answerString;
	}
}
