using UnityEngine;
using System.Collections;
using System.Linq;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class teachMeWorkingOld : MonoBehaviour {

	public GUISkin mySkin;
	public GUIStyle stepStyle;

	public Texture2D submitBtn;
	public  bool showAnswer = false;
	public string enteredInstruction; 
	public string ans = " ";


	// Use this for initialization
	void Start () {
		mySkin = (GUISkin)Resources.Load ("skinTeachPad", typeof(GUISkin));
		submitBtn = (Texture2D)Resources.Load ("okBtn", typeof(Texture2D));

		stepStyle = new GUIStyle ();
		stepStyle.fontSize = 14;
		stepStyle.fontStyle = FontStyle.Bold;

	}
	
	// Update is called once per frame
	void OnGUI () {

		GUI.skin = mySkin;
		float xPos = 120;
		float yPos = Screen.height / 2;

		//GUI.Label (new Rect (xPos, yPos + 10, 500, 50), "Thank you, kind Giant! ");
		GUI.Label (new Rect (xPos, yPos-10, 700, 100), "Can you please teach me one step at a time. If I understand what you are saying, we can test the step before moving on to the next one.");

		if (showAnswer) {
			GUI.Label (new Rect (xPos, yPos+100, 680, 100), ans);
		}
		GUI.Label (new Rect (xPos, yPos + 40, 500, 50), "Step1", stepStyle);
		GUI.Label (new Rect (xPos + 45, yPos + 37, 500, 50), "(Enter your step and press the OK button)");
		enteredInstruction = GUI.TextArea (new Rect (xPos, yPos + 60, 700, 40), enteredInstruction);
		//GUI.Label (new Rect (520, 300, 240, 60), enteredInstruction);
		if (GUI.Button (new Rect (xPos + 710,yPos + 55, 50, 50), submitBtn )) {
			ans = processInput(enteredInstruction);
			showAnswer = true;
			//ans = enteredInstruction +". What is that? I don't get it!";
		}
	}

	string processInput(string inputText) {

		//First add all input to a file - to keep track of responses so that we can learn to process them
		System.IO.StreamWriter sr = new System.IO.StreamWriter("allresponses.txt", true);
		sr.WriteLine(inputText);
		sr.Close();

		Match match;

		bool thereIsANumber = false, thereIsAChangeWord = false, thereIsAGemWord = false;
		int number1=0, number2 =0, number3=0, number4=0;
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
		/*else {
			int ind = 0;
			int noOfWords = words.Count();
			print(noOfWords);
			foreach (string s in words) {
				if (String.Equals(s,"multiply", StringComparison.OrdinalIgnoreCase)) {
					//print(s); print(words[ind+1]); print(words[ind+3]);

					if( (int.TryParse (words[ind+1], out number1)) & (int.TryParse (words[ind+3], out number2))) {
						answerString = "Multiply "+number1.ToString()+" and "+number2.ToString();
					 	//break;
					}
					//print(words[ind+4]+" "+words[ind+6]);
					if ( (int.TryParse (words[ind+4], out number3)) & (int.TryParse (words[ind+6], out number4))) {
						answerString = answerString+" Multiply "+number3.ToString()+" and "+number4.ToString();
						//break;
					} 

				}

				ind++;
			}
		}*/
		else {
			//string pattern = * Multiply 12 [by] 5 [then] add 6 [to it]  [to get the number of rubies]

			//strip input
			string strippedInput = Regex.Replace(inputText, @"\W+", " ");
			print (inputText+":"+strippedInput);
			//string pattern1 = @"^((\w+(\,?)(\s?)){0,})(multiply)(?>(\.?)(\,?)(\s?)(12))(?>(\s?)(\w+(\s?))(5)(\.?)(\,?)(\s?))(?>((\w+(\s?)){0,3})(\s?)(add))(?>(\s?)(6)(\.?)(\,?)(\s?)((\w+(\s?)){0,}))$";
			//WORKS
			string pattern1 = @"(multiply)(\s)((\w+(\s?)){0,3})(emeralds|12)(\s)((\w+(\s?)){0,3})(5)(\s)((\w+(\s?)){0,5})(add)(\s)((\w+(\s?)){0,5})(rubies|6)";
			//string pattern1 = @"(multiply)(\s)(emeralds)(\s)((\w+(\s?)){0,3})(5)(\s)((\w+(\s?)){0,5})(add)(\s)(rubies)";

			//string pattern2 = @"^((\w+(\,?)(\s?)){0,})(multiply)(\.?)(\,?)(\s?)(emeralds)(\s?)(\w+(\s?))(5)(\s?)((\w+(\s?)){0,3})(\s?)(add)$";
			//string pattern2 = @"^((\w+(\,?)(\s?)){0,})(multiply)(\s)(?>emeralds)(\s)(?>(\w+(\s?)))(5)(\s?)((\w+(\s?)){0,3})(\s)(add)(\s?)(6)(\s?)((\w+(\s?)){0,})$";

				if (Regex.IsMatch(strippedInput, pattern1, RegexOptions.IgnoreCase)) {
					answerString = "ok. Got it. Multiply 12 by 5 and then add 6.";
				}
				else {
					answerString = "Your instruction is confusing me. Can you reword or make it simpler?";
				}

			//string txt="[First] Multiply [number of ] emeralds [by] 5 [then] add [number of ] rubies";
			
			/*string re1=".*?";	// Non-greedy match on filler
			string re2="(?:[a-z][a-z]+)";	// Uninteresting: word
			string re3=".*?";	// Non-greedy match on filler
			string re4="((?:multiply)";	// Word 1
			string re5=".*?";	// Non-greedy match on filler
			string re6="(?:[a-z][a-z]+)";	// Uninteresting: word
			string re7=".*?";	// Non-greedy match on filler
			string re8="(?:[a-z][a-z]+)";	// Uninteresting: word
			string re9=".*?";	// Non-greedy match on filler
			string re10="((?:[a-z][a-z]+))";	// Word 2
			string re11=".*?";	// Non-greedy match on filler
			string re12="(\\d+)";	// Integer Number 1
			string re13=".*?";	// Non-greedy match on filler
			string re14="(?:[a-z][a-z]+)";	// Uninteresting: word
			string re15=".*?";	// Non-greedy match on filler
			string re16="((?:[a-z][a-z]+))";	// Word 3
			string re17=".*?";	// Non-greedy match on filler
			string re18="(?:[a-z][a-z]+)";	// Uninteresting: word
			string re19=".*?";	// Non-greedy match on filler
			string re20="(?:[a-z][a-z]+)";	// Uninteresting: word
			string re21=".*?";	// Non-greedy match on filler
			string re22="((?:[a-z][a-z]+))";	// Word 4
			
			Regex r = new Regex(re1+re2+re3+re4+re5+re6+re7+re8+re9+re10+re11+re12+re13+re14+re15+re16+re17+re18+re19+re20+re21+re22,RegexOptions.IgnoreCase|RegexOptions.Singleline);
			Match m = r.Match(strippedInput);
			if (m.Success)
			{
				String word1=m.Groups[1].ToString();
				String word2=m.Groups[2].ToString();
				String int1=m.Groups[3].ToString();
				String word3=m.Groups[4].ToString();
				String word4=m.Groups[5].ToString();
				print("("+word1.ToString()+")"+"("+word2.ToString()+")"+"("+int1.ToString()+")"+"("+word3.ToString()+")"+"("+word4.ToString()+")"+"\n");
			}
			else 
				print("Failed");*/
			/*else {
				match = Regex.Match(inputText, pattern2, RegexOptions.IgnoreCase);
				if (match.Success) {
					answerString = "ok. Got it. Multiply number of emeralds(12) by 5 and then add number of rubies(6).";
				}
			}*/
			/*if(Regex.IsMatch(inputText, pattern, RegexOptions.IgnoreCase) ) {
				answerString = "matches";
			}
			else {
				answerString = "no match";
			}*/
		}
		return(answerString);
	}
}
