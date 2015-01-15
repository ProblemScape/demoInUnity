using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;

public class createShopProblem : MonoBehaviour {

	public GUISkin thisSkin;
	public GUIStyle headingStyle;
	public string answerString;
	public Texture2D useAlgebraPadBtn, closeAlgebraPadBtn, checkAnsBtn, laterBtn, problemBox, teachBtn, noTeachBtn;
	public bool ansCorrect;
	public int noOfTries;
	public GameObject myGameObj;
	public GameObject algebraPad;
	public bool usingAlgebraPad; 
	public GUIContent forAlgebraPadToggle;
	public bool aPadClosed = true;
	public string pStatement, pAnswer;
	
	void Start () {
		ansCorrect = false;
		noOfTries = 0;
		thisSkin = (GUISkin)Resources.Load("skinShopProblem", typeof(GUISkin));
		headingStyle = new GUIStyle ();
		headingStyle.fontSize = 15;
		headingStyle.normal.textColor = Color.black;
		headingStyle.fontStyle = FontStyle.Bold;
		headingStyle.wordWrap = true;

		problemBox = (Texture2D)Resources.Load ("problemBox", typeof(Texture2D));
		useAlgebraPadBtn = (Texture2D)Resources.Load("useAPad", typeof(Texture2D));
		closeAlgebraPadBtn = (Texture2D)Resources.Load("closeAPad", typeof(Texture2D));
		checkAnsBtn = (Texture2D)Resources.Load ("checkAnsBtn", typeof(Texture2D));
		laterBtn = (Texture2D)Resources.Load ("later", typeof(Texture2D));
		teachBtn = (Texture2D)Resources.Load ("teachBtn", typeof(Texture2D));
		noTeachBtn = (Texture2D)Resources.Load ("noTeachBtn", typeof(Texture2D));

		myGameObj = new GameObject("new"); 
		forAlgebraPadToggle = new GUIContent ("", useAlgebraPadBtn);
		//pStatement = "test";
		pStatement = shopQuest.problemSet[shopQuest.problemNum].statement;
		pAnswer = shopQuest.problemSet[shopQuest.problemNum].answer;

		//var problemCollection = ProblemSetContainer.Load(Path.Combine(Application.dataPath, "shopQuest1ProblemSet.xml"));
		//print(problemCollection.Problems[1].Statement);
	}
	
	// Update is called once per frame
	void OnGUI () {
		if ( ansCorrect == false)  
		{
			GUI.skin = thisSkin;
			GUI.Box(new Rect(30, 180, 400,480), problemBox);
			GUI.Label (new Rect (50, 265, 350, 250), "How many paintballs can I get?");
			//GUI.Label (new Rect (50, 210, 350, 250), "I have 12 emeralds and 6 rubies. I want to get as many paintballs as I have gems for.");
			GUI.Label (new Rect (50, 210, 350, 250), pStatement);
			GUI.Label (new Rect (50, 320, 180, 50), "Enter your answer here: ", headingStyle);
			answerString = GUI.TextField (new Rect (225, 310, 40, 40), answerString);

			if (GUI.Button (new Rect (300, 370, 102, 56), laterBtn)) {
				Destroy (this);
				if(!aPadClosed) {
					Destroy(algebraPad);
				}

			}

			if(GUI.Toggle(new Rect(30, 370, 120, 60), usingAlgebraPad, forAlgebraPadToggle))
			{
				if(aPadClosed) {
					algebraPad = (GameObject)Instantiate(Resources.Load("algebraPad"));
					forAlgebraPadToggle.image = closeAlgebraPadBtn;
					aPadClosed = false;
				}
				else {
					Destroy(algebraPad);
					forAlgebraPadToggle.image = useAlgebraPadBtn;
					aPadClosed = true;
				}
			}

			if (GUI.Button (new Rect (175, 370, 102, 56), checkAnsBtn)) {
				if(answerString == pAnswer) {
					ansCorrect = true;
					shopQuest.problemsSolved++;
					if(!aPadClosed) {
						Destroy(algebraPad);
					}
					//print(shopQuest.problemsSolved);

				}
				else {
					ansCorrect = false;
					noOfTries++;
				}
			}
			if (noOfTries > 0){
				GUI.Label (new Rect (300, 320, 200, 250), "Try again!");
			}
		}
		else {
			if(myGameObj != null) {
				shopQuest.ifSolved[shopQuest.problemNum] = true;
				if(shopQuest.problemsSolved >= 2) {
					GUI.Box (new Rect (200, 210, 320, 200), "Do you want to teach?");
					if (GUI.Button (new Rect (220, 240, 280, 100), teachBtn)) {
						//print ("Yes");
						Application.LoadLevel("teachPad");
						Destroy(myGameObj);

					}
					if (GUI.Button (new Rect (220, 350, 280, 40), noTeachBtn)) {
						//print ("No");
						Destroy(myGameObj);
					}
				}
				else 
				{
					myGameObj.AddComponent<goodJob>();
					Destroy(myGameObj, 3);
				}
			}
		}
	}

}
				  
/*[XmlRoot("Problem")]
public class Problem
{ 
	[XmlAttribute("name")]
	public string name;
	
	public string Statement;
	
	public int Answer;
}

[XmlRoot("ProblemSetCollection")]
public class ProblemSetContainer
{
	[XmlArray("ProblemSet"),XmlArrayItem("Problem")]
	public Problem[] Problems;
	
	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(ProblemSetContainer));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static ProblemSetContainer Load(string path)
	{
		var serializer = new XmlSerializer(typeof(ProblemSetContainer));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as ProblemSetContainer;
		}
	}
	
	//Loads the xml directly from the given string. Useful in combination with www.text.
	public static ProblemSetContainer LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(ProblemSetContainer));
		return serializer.Deserialize(new StringReader(text)) as ProblemSetContainer;
	}
}*/

