using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;

public class shopQuest : MonoBehaviour {

	public Texture2D a1;
	public GUISkin mySkin;
	public GameObject shopProblem;
	public static bool [] ifSolved = new bool[] {false, false, false, false,false, false};
	public static int problemNum;
	public static List<ShopProblemClass> problemSet = new List<ShopProblemClass> ();
	public static int problemsSolved = 0;

	// Use this for initialization
	void Start () {
		mySkin = (GUISkin)Resources.Load ("skinAlgebraPad", typeof(GUISkin));
		a1 = (Texture2D)Resources.Load("arithmenBackB&W", typeof(Texture2D));
		parseXMLProblemSet(Path.Combine(Application.dataPath, "shopQuest1ProblemSet.xml"));
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.skin = mySkin;
		GUI.depth = 10;

			if (ifSolved [0] == false) {
				if (GUI.Button (new Rect (460, 400, 45, 90), a1)) {
					GUI.depth = 5;
					problemNum = 0;
					shopProblem = (GameObject)Instantiate (Resources.Load ("shopProblem"));
				}
			}
			if (ifSolved [1] == false) {
				if (GUI.Button (new Rect (460, 310, 45, 90), a1)) {
					GUI.depth = 5;
					problemNum = 1;
					shopProblem = (GameObject)Instantiate (Resources.Load ("shopProblem"));
				}
			}
			if (ifSolved [2] == false) {
				if (GUI.Button (new Rect (400, 365, 45, 90), a1)) {
					GUI.depth = 5;
					problemNum = 2;
					shopProblem = (GameObject)Instantiate (Resources.Load ("shopProblem"));
				}
			}
			if (ifSolved [3] == false) {
				if (GUI.Button (new Rect (360, 340, 45, 90), a1)) {
					GUI.depth = 5;
					problemNum = 3;
					shopProblem = (GameObject)Instantiate (Resources.Load ("shopProblem"));
				}
			}
			if (ifSolved [4] == false) {
				if (GUI.Button (new Rect (430, 330, 45, 90), a1)) {
					GUI.depth = 5;
					problemNum = 4;
					shopProblem = (GameObject)Instantiate (Resources.Load ("shopProblem"));
				}
			}
			if (ifSolved [5] == false) {
				if (GUI.Button (new Rect (335, 375, 45, 90), a1)) {
					GUI.depth = 5;
					problemNum = 5;
					shopProblem = (GameObject)Instantiate (Resources.Load ("shopProblem"));
				}
			}

	}

	/*public bool wantToTeach() 
	{
		while (problemsSolved == 2) {
			GUI.Box (new Rect (200, 200, 300, 200), "Do you want to teach?");
			if (GUI.Button (new Rect (210, 240, 150, 120), "Hey, you are good at this! Do you want to teach the shopkeeper to do the problem himself? He will reward you with gems and game tokens. Say 'YES'")) {
				print ("Yes");
				return(true);
			}
			if (GUI.Button (new Rect (360, 240, 100, 120), "Not now. Maybe later!")) {
				print ("No");
				return(false);
			}
		} return(false);
	}
*/
	public void parseXMLProblemSet(string path)
	{
		print ("Reading XML File from "+Application.dataPath);

		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.Load (path);
		XmlNodeList problemset = xmlDoc.GetElementsByTagName ("Problem");
		int problemCount = 0;
		string stmt = "";
		string ans= "";

		//ShopProblemClass prob = new ShopProblemClass();
		foreach (XmlNode problem in problemset) {
			XmlNodeList problemContent = problem.ChildNodes;
			
			foreach (XmlNode problemItems in problemContent) {
				if(problemItems.Name == "Statement")
				{	
					stmt = problemItems.InnerText;
					//print("statement: " + stmt);
				}
				if(problemItems.Name == "Answer")
				{	
					ans = problemItems.InnerText;
					//print("answer: " + ans);
				}

				//print (problemItems.InnerText);
			}
			problemCount++;
			//print(problemCount);
			problemSet.Add(new ShopProblemClass(stmt,ans));
		}
		/*print (problemCount);
		print (problemSet[0].statement);
		print (problemSet[1].statement);
		print (problemSet[2].statement);*/
	}
}

public class ShopProblemClass
{
	public string statement;
	public string answer;

	public ShopProblemClass(string s, string a) {
		statement = s;
		answer = a;
	}
}
