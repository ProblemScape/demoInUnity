using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

public class teachMe2 : MonoBehaviour {

	public GUISkin mainSkin, stepsSkin;
	public GUIStyle textStyle, answerStyle;

	public GameObject instructions; 
	public bool insOpen;

	public Texture2D submitBtn, closeBtn;
	public  bool showAnswer = false, showAnswer2 = false, showAnswer3 = false;
	public string enterInstruction1, enterInstruction2, enterInstruction3; 
	public string ans = " ";

	public bool firstStepDone = false;
	public int stepNum;

	public float xPos;
	//public float yPos = Screen.height / 4;
	public float yPos;
	public string inputDumpFilename;
	public bool startNow;

	public static List<StepPatternClass> step1PatternSet = new List<StepPatternClass> ();
	public static List<StepPatternClass> step2PatternSet = new List<StepPatternClass> ();
	public static List<StepPatternClass> step3PatternSet = new List<StepPatternClass> ();

	public string p2step1, step2, step3;

	// Use this for initialization
	void Start () {
		xPos = 90;
		yPos = 350;
		mainSkin = (GUISkin)Resources.Load ("skinTeachPad_main", typeof(GUISkin));
		stepsSkin = (GUISkin)Resources.Load ("skinTeachPad_steps", typeof(GUISkin));
		submitBtn = (Texture2D)Resources.Load ("okBtn", typeof(Texture2D));
		closeBtn = (Texture2D)Resources.Load ("closeBtn", typeof(Texture2D));

		textStyle = new GUIStyle ();
		textStyle.fontSize = 14;
		textStyle.fontStyle = FontStyle.Bold;
		textStyle.wordWrap = true;

		answerStyle = new GUIStyle ();
		answerStyle.fontSize = 16;
		answerStyle.normal.textColor = Color.red;
		answerStyle.wordWrap = true;

		enterInstruction1  = " ";
		enterInstruction2  = " ";
		enterInstruction3  = " ";
		stepNum = 1;
		inputDumpFilename = "allresponses_test.txt";

		startNow = true;
		insOpen = false;

		getPatternsFromXML(Path.Combine(Application.dataPath, "TPad_lvl3_p1s1.xml"), 1);
		getPatternsFromXML(Path.Combine(Application.dataPath, "TPad_lvl3_p1s2.xml"), 2);
		getPatternsFromXML(Path.Combine(Application.dataPath, "TPad_lvl3_p1s3.xml"), 3);

	}
	
	// Update is called once per frame
	void OnGUI () {
		//print (xPos+": "+ yPos);
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

			GUI.skin = mainSkin;
			float yPosNew;
			//GUI.Label (new Rect (xPos, yPos + 10, 500, 50), "Thank you, kind Giant! ");
			GUI.Label (new Rect (xPos+100, yPos - 200, 700, 100), "This fella here has 12 Emeralds and 6 Rubies and wants to get a Paintball shooter with as many paintballs as he has money for! How many paintballs should I give him?\n");
			GUI.Label (new Rect (xPos+70, yPos - 150, 780, 100), "Imagine you are teaching your friend, or brother, or sister to do this problem. What would you tell them to do?");
			switch (stepNum) {
			case 1:
				if (showAnswer) {
					GUI.Label (new Rect (xPos+560, yPos - 95, 280, 100), ans, answerStyle);
				}
				GUI.Label (new Rect (xPos, yPos - 70, 200, 50), "Step1: How much money in Rubies does the Arithman have?", textStyle);
				GUI.Label (new Rect (xPos+225, yPos - 110, 250, 50), "(Enter step in box and press OK)");
				enterInstruction1 = GUI.TextArea (new Rect (xPos+225, yPos - 90, 250, 100), enterInstruction1);
				//GUI.Label (new Rect (520, 300, 240, 60), enteredInstruction);
				if (GUI.Button (new Rect (xPos + 475, yPos - 55, 50, 50), submitBtn)) {
					ans = processStep1 (enterInstruction1);
					showAnswer = true;
					//ans = enteredInstruction +". What is that? I don't get it!";
				}
				break;
			case 2:
				yPosNew = yPos + 30;
				//GUI.Label (new Rect (xPos, yPos + 40, 500, 50), "STEP1:", textStyle);
				GUI.Label (new Rect (xPos, yPos - 70, 200, 50), "Step1: How much money in Rubies does the Arithman have?", textStyle);
				//enterInstruction1 = GUI.TextArea (new Rect (xPos+220, yPos - 90, 250, 100), enterInstruction1);
				GUI.Label (new Rect (xPos+225, yPos - 110, 250, 50), "This is what you want me to do");
				GUI.Label (new Rect (xPos+540, yPos - 110, 250, 50), "Yay! I got the first step! ");
				GUI.skin = stepsSkin;

				GUI.Label (new Rect (xPos+220, yPos - 90, 250, 100), new GUIContent(enterInstruction1));
				GUI.Label (new Rect (xPos + 540, yPos -90, 300, 100), "Multiply 12 by 5 and then add 6.\n He has 66 Rubies.");
				GUI.skin = mainSkin;
				//print(showAnswer);
				if (showAnswer) {
					GUI.Label (new Rect (xPos+560, yPos + 60, 280, 100), ans, answerStyle);
				}
				GUI.Label (new Rect (xPos, yPosNew + 90, 200, 50), "Step2: How much money is left for paintballs?", textStyle);
				GUI.Label (new Rect (xPos + 225, yPosNew + 37, 500, 50), "(Enter step in box and press OK)");
				enterInstruction2 = GUI.TextArea (new Rect (xPos+225, yPosNew + 60, 250, 100), enterInstruction2);
			//GUI.Label (new Rect (520, 300, 240, 60), enterInstruction);
				if (GUI.Button (new Rect (xPos + 475, yPosNew + 110, 50, 50), submitBtn)) {
					ans = processStep2 (enterInstruction2);
					//showAnswer = true;
					//ans = enteredInstruction +". What is that? I don't get it!";
				}
				break;
			case 3:
				yPosNew = yPos + 60;
				GUI.Label (new Rect (xPos, yPos - 70, 200, 50), "Step1: How much money in Rubies does the Arithman have?", textStyle);
				GUI.Label (new Rect (xPos+225, yPos - 110, 250, 50), "This is what you want me to do");
				GUI.Label (new Rect (xPos+540, yPos - 110, 250, 50), "Yay! I got the first step! ");
				//GUI.Label (new Rect (xPos+225, yPos +10, 250, 50), "This is what you want me to do");
				GUI.Label (new Rect (xPos+540, yPos +30, 250, 50), "Now, I got the second step too! ");

				//enterInstruction1 = GUI.TextArea (new Rect (xPos+220, yPos - 100, 250, 100), enterInstruction1);
				GUI.Label (new Rect (xPos, yPos+80, 200, 50), "Step2: How much money is left for paintballs?", textStyle);
				//enterInstruction2 = GUI.TextArea (new Rect (xPos+225, yPos + 50, 250, 100), enterInstruction2);
				GUI.skin = stepsSkin;
				GUI.Label (new Rect (xPos+220, yPos - 90, 250, 100), new GUIContent(enterInstruction1));
				GUI.Label (new Rect (xPos+220, yPos + 50, 250, 100), new GUIContent(enterInstruction2));
				GUI.Label (new Rect (xPos + 540, yPos - 90, 300, 100), "Multiply 12 by 5 and then add 6.\n He has 66 Rubies.");
				GUI.Label (new Rect (xPos + 540, yPos + 50, 300, 100), "Subtract 36 from 66. He has 30 Rubies for paintballs.");
				GUI.skin = mainSkin;
				if (showAnswer) {
					GUI.Label (new Rect (xPos+560, yPos + 180, 280, 100), ans, answerStyle);
				}
				GUI.Label (new Rect (xPos, yPosNew + 150, 200, 50), "Step3: How many paintballs can the Arithman get?", textStyle);
				GUI.Label (new Rect (xPos + 225, yPos + 170, 500, 50), "(Enter step in box and press OK)");
				enterInstruction3 = GUI.TextArea (new Rect (xPos+225, yPos + 190, 250, 100), enterInstruction3);
			//GUI.Label (new Rect (520, 300, 240, 60), enterInstruction);
				if (GUI.Button (new Rect (xPos + 475, yPos + 240, 50, 50), submitBtn)) {
					ans = processStep3 (enterInstruction3);
					//showAnswer = true;
					//ans = enteredInstruction +". What is that? I don't get it!";
				}
				break;
			case 0:
				textStyle.fontStyle = FontStyle.Bold;
				textStyle.fontSize = 14;

				GUI.Label (new Rect (xPos, yPos - 70, 200, 50), "Step1: How much money in Rubies does the Arithman have?", textStyle);
				GUI.Label (new Rect (xPos+225, yPos - 110, 250, 50), "This is what you want me to do");
				GUI.Label (new Rect (xPos+540, yPos - 110, 250, 50), "Yay! I got the first step! ");
				GUI.Label (new Rect (xPos+540, yPos +30, 250, 50), "Now, I got the second step too! ");

				//enterInstruction1 = GUI.TextArea (new Rect (xPos+220, yPos - 100, 250, 100), enterInstruction1);
				GUI.Label (new Rect (xPos, yPos+60, 200, 50), "Step2: How much money is left for paintballs?", textStyle);
				//enterInstruction2 = GUI.TextArea (new Rect (xPos+225, yPos + 30, 250, 100), enterInstruction2);
				GUI.Label (new Rect (xPos, yPos + 220, 200, 50), "Step3: How many paintballs can the Arithman get?", textStyle);
				//enterInstruction3 = GUI.TextArea (new Rect (xPos+225, yPos + 170, 250, 100), enterInstruction3);

				GUI.skin = stepsSkin;
				GUI.Label (new Rect (xPos+220, yPos - 90, 250, 100), new GUIContent(enterInstruction1));
				GUI.Label (new Rect (xPos+220, yPos + 50, 250, 100), new GUIContent(enterInstruction2));
				GUI.Label (new Rect (xPos+220, yPos + 190, 250, 100), new GUIContent(enterInstruction3));

				GUI.Label (new Rect (xPos + 540, yPos - 90, 300, 100), "Multiply 12 by 5 and then add 6.\n He has 66 Rubies.");
				GUI.Label (new Rect (xPos + 540, yPos + 50, 300, 100), "Subtract 36 from 66. He has 30 Rubies for paintballs.");
				GUI.Label (new Rect (xPos + 540, yPos + 190, 300, 100), "Divide 36 by 2 to get the number of paintballs to load.\n\b He can get 15 paintballs.");
				GUI.skin = mainSkin;
				textStyle.fontStyle = FontStyle.Normal;
				textStyle.fontSize = 18;
				GUI.Label (new Rect (xPos, yPos + 320, 700, 50),"Yippie! I know how to sell paintballs now. I want to try selling to a different customer!",textStyle);
				if (GUI.Button (new Rect (xPos + 675, yPos + 305, 50, 50), submitBtn)) {
					doProblem1 ();
				}
				break;
			case -1:
				textStyle.fontStyle = FontStyle.Bold;
				textStyle.fontSize = 14;
				
				//GUI.Label (new Rect (xPos, yPos - 70, 200, 50), "Step1: How much money in Rubies does the Arithman have?", textStyle);
				GUI.Label (new Rect (xPos, yPos - 110, 250, 50), "This is what you want me to do");
				GUI.Label (new Rect (xPos+320, yPos - 110, 250, 50), "First step for first shopper! ");
				GUI.Label (new Rect (xPos+620, yPos - 110, 220, 50), "First step of next shopper! ");
				//GUI.Label (new Rect (xPos+540, yPos +30, 250, 50), "Now, I got the second step too! ");
				
				//enterInstruction1 = GUI.TextArea (new Rect (xPos+220, yPos - 100, 250, 100), enterInstruction1);
				//GUI.Label (new Rect (xPos, yPos+60, 200, 50), "Step2: How much money is left for paintballs?", textStyle);
				//enterInstruction2 = GUI.TextArea (new Rect (xPos+225, yPos + 30, 250, 100), enterInstruction2);
				//GUI.Label (new Rect (xPos, yPos + 220, 200, 50), "Step3: How many paintballs can the Arithman get?", textStyle);
				//enterInstruction3 = GUI.TextArea (new Rect (xPos+225, yPos + 170, 250, 100), enterInstruction3);
				GUI.Label (new Rect (xPos+620, yPos -80, 200, 100), p2step1, answerStyle);
				GUI.skin = stepsSkin;
				GUI.Label (new Rect (xPos, yPos - 90, 250, 100), new GUIContent(enterInstruction1));
				//GUI.Label (new Rect (xPos+220, yPos + 50, 250, 100), new GUIContent(enterInstruction2));
				//GUI.Label (new Rect (xPos+220, yPos + 190, 250, 100), new GUIContent(enterInstruction3));
				
				GUI.Label (new Rect (xPos + 320, yPos - 90, 250, 100), "Multiply 12 by 5 and then add 6.\n He has 66 Rubies.");
				//GUI.Label (new Rect (xPos + 540, yPos + 50, 300, 100), "Subtract 36 from 66. He has 30 Rubies for paintballs.");
				//GUI.Label (new Rect (xPos + 540, yPos + 190, 300, 100), "Divide 36 by 2 to get the number of paintballs to load.\n\b He can get 15 paintballs.");
				GUI.skin = mainSkin;
				textStyle.fontStyle = FontStyle.Normal;
				textStyle.fontSize = 18;

				break;
			}

		}
	}

	void doProblem1 () {
		stepNum = -1;
	}

	public void getPatternsFromXML(string path, int step)
	{
		//print ("Reading XML File from "+Application.dataPath);
		
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.Load (path);
		XmlNodeList stepSet = xmlDoc.GetElementsByTagName ("Step");
		//int problemCount = 0;
		string pattern = "";
		string ans= "";
		bool pass = false;
		string p2Ans = "";
		bool p2AnsPass = false;
		
		//ShopProblemClass prob = new ShopProblemClass();
		foreach (XmlNode s in stepSet) {
			XmlNodeList sList = s.ChildNodes;
			
			foreach (XmlNode patternSet in sList) {
				if(patternSet.Name == "Pattern")
				{	
					pattern = patternSet.InnerText;
					//print("statement: " + pattern);
				}
				if(patternSet.Name == "Action")
				{	
					 if(patternSet.InnerText == "true") {
						pass= true;
					}
					else {
						pass = false;
					}
						
					//print("answer: " + ans);
				}
				if(patternSet.Name == "Answer")
				{	
					ans = patternSet.InnerText;
					//print("answer: " + ans);
				}
				//print (problemItems.InnerText);
				if(patternSet.Name == "p2Answer")
				{	
					p2Ans = patternSet.InnerText;
					//print("answer: " + ans);
				}
				if(patternSet.Name == "p2Action")
				{	
					if(patternSet.InnerText == "true") {
						p2AnsPass= true;
					}
					else {
						p2AnsPass = false;
					}
				}
			}
			//problemCount++;
			//print(problemCount);
			if(step == 1) {
				step1PatternSet.Add(new StepPatternClass(pattern,ans, pass, p2Ans, p2AnsPass));
			} 
			else {
				if(step == 2) {
					step2PatternSet.Add(new StepPatternClass(pattern,ans, pass));
				}
				else {
					if(step == 3) {
						step3PatternSet.Add(new StepPatternClass(pattern,ans, pass));
					}
				}
			}
		}

	}

	string processStep1(string inputText) {

		//First add all input to a file - to keep track of responses so that we can learn to process them
		System.IO.StreamWriter sr = new System.IO.StreamWriter(inputDumpFilename, true);
		sr.WriteLine("STEP 1: "+inputText);
		sr.Close();

		string strippedInput = Regex.Replace (inputText, @"[,;:.()]", " ");
		//print (inputText+":"+strippedInput);
		string answerString = " ";;
		foreach (StepPatternClass s in step1PatternSet) {
	
			answerString = " ";
	
			if (Regex.IsMatch (strippedInput, s.pattern, RegexOptions.IgnoreCase)) {
				if(s.pass) {
					stepNum = 2;
					showAnswer = false;
					p2step1 = s.p2Answer;
					//print(s.p2Answer);
					//print("p2: "+p2step1);
					break;
				}
				else {
					answerString = s.answer;
					break;
				}
			}
			else {
				answerString = "I'm so sorry. I don't understand. Can you rephrase or be more specific?";
			}
		}
		//print (answerString);
		return(answerString);
	}

	string processStep2(string inputText) {

		System.IO.StreamWriter sr = new System.IO.StreamWriter(inputDumpFilename, true);
		sr.WriteLine("STEP 2: "+inputText);
		sr.Close();

		string strippedInput = Regex.Replace (inputText, @"[,;:.]", " ");
		//print (inputText+":"+strippedInput);

		string answerString = "  ";
		/*string pattern2_1 = @"(subtract)(\s)((\w+(\s?)){0,3})((paintball shooter)|36)";
		if (Regex.IsMatch(inputText, pattern2_1, RegexOptions.IgnoreCase)) {
			//answerString = "ok. Got it. Subtract 36 from 66.\nThe answer is 30. Let's move on to the next step.";
			stepNum = 3;
			//enterInstruction = "Enter instruction here: ";
			showAnswer = false;
		}*/
		foreach (StepPatternClass s in step2PatternSet) {
			if (Regex.IsMatch (strippedInput, s.pattern, RegexOptions.IgnoreCase)) {
					if (s.pass) {
						stepNum = 3;
						showAnswer = false;
						break;
					} else {
						answerString = s.answer;
						break;
					}
				}
				else {
					answerString = "Your instruction is confusing me. Can you reword or make it simpler?";
				}
		}
		//print (answerString);
		return answerString;
	}

	string processStep3(string inputText) {

		System.IO.StreamWriter sr = new System.IO.StreamWriter (inputDumpFilename, true);
		sr.WriteLine ("STEP 3: " + inputText);
		sr.Close ();

		string strippedInput = Regex.Replace (inputText, @"[,;:.]", " ");
		//print (inputText+":"+strippedInput);

		string answerString = "  ";
		/*string pattern3_1 = @"(divide)(\s)(by)(\s)(\d+)";
		if (Regex.IsMatch(inputText, pattern3_1, RegexOptions.IgnoreCase)) {
			answerString = "Yes. Divide 36 by 2 to get the number of paintballs to load. The answer is 15. \nYippie! I know how to sell paintballs now. I want to try on a different customer!.";
			//stepNum = 0;
			//showAnswer = false;
		}*/
		foreach (StepPatternClass s in step3PatternSet) {
			if (Regex.IsMatch (strippedInput, s.pattern, RegexOptions.IgnoreCase)) {
				if (s.pass) {
					stepNum = 0;
					showAnswer = false;
					break;
				} else {
					answerString = s.answer;
					break;
				}
			} else {
				answerString = "I don't get it. Can you say that differently?";
			}
		}
		//print (answerString);
		return answerString;
	}
}

/*public class StepPatternClass
{
	public string pattern;
	public string answer;
	public bool pass;
	public string p2Answer;
	public bool p2Pass = false;

	public StepPatternClass(string s, string a, bool b) {
		pattern = s;
		answer = a;
		pass = b;
	}
	public StepPatternClass(string s, string a, bool b, string p2A, bool p2P) {
		pattern = s;
		answer = a;
		pass = b;
		p2Answer = p2A;
		p2Pass = p2P;
	}
}*/
