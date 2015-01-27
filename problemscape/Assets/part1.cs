using UnityEngine;
using System.Collections;

public class part1 : MonoBehaviour {

	public Texture2D submitBtn, closeBtn, nextBtn;
	public static GameObject scene2;
	public GUISkin mySkin;
	public static int sceneNo = 1;
	public int currentScene;

	// Use this for initialization
	void Start () {
		//sceneNo = 1;
		mySkin = (GUISkin)Resources.Load ("skinPlain", typeof(GUISkin));
		submitBtn = (Texture2D)Resources.Load ("okBtn", typeof(Texture2D));
		closeBtn = (Texture2D)Resources.Load ("closeBtn", typeof(Texture2D));
		nextBtn = (Texture2D)Resources.Load ("nextArrowBig", typeof(Texture2D));
		currentScene = 0;

	
	}
	
	// Update is called once per frame
	void OnGUI () {

		GUI.skin = mySkin;
		//print (sceneNo);
		switch (sceneNo) {
		case 1:
			GUI.Label (new Rect (760, 550, 150, 60), "Click Through &\nPlay Trailer");
			if (GUI.Button (new Rect (800, 530, 300, 100), nextBtn)) {
				//scene2 = (GameObject)Instantiate (Resources.Load ("prefab_theStory"));
				sceneNo = 2;
			}
			break;
		case 2:
			if(currentScene != 2) {
				Destroy(scene2);
				scene2 = (GameObject)Instantiate (Resources.Load ("prefab_theStory"));
				currentScene = 2;
				}
				if (GUI.Button (new Rect (800, 430, 300, 100), nextBtn)) {
					sceneNo = 3;
				}
			GUI.Box(new Rect (580, 100, 100, 40), "The Story");
			break;
		case 3:
			if(currentScene != 3) {
				Destroy(scene2);
				scene2 = (GameObject)Instantiate (Resources.Load ("prefab_lockProblem"));
				currentScene = 3;
					//sceneNo = 3;
				}
			//if (GUI.Button (new Rect (800, 430, 300, 100), nextBtn)) {

			//}
			break;
		case 4:
			if(currentScene != 4) {
				Destroy(scene2);
				scene2 = (GameObject)Instantiate (Resources.Load ("prefab_openBox"));
				currentScene = 4;
			}
			if (GUI.Button (new Rect (800, 430, 300, 100), nextBtn)) {
				sceneNo = 5;
			}
			//print ("3");
			break;
		case 5:
			if(currentScene != 5) {
				Destroy(scene2);
				scene2 = (GameObject)Instantiate (Resources.Load ("prefab_letter"));
				currentScene = 5;
			}
			if (GUI.Button (new Rect (800, 430, 300, 100), nextBtn)) {
				sceneNo = 6;
			}
			GUI.Box(new Rect (740, 150, 100, 40), "The Letter");

			break;
		case 6:
			if(currentScene != 6) {
				Destroy(scene2);
				scene2 = (GameObject)Instantiate (Resources.Load ("prefab_playerHelps"));
				currentScene = 6;
			}
			if (GUI.Button (new Rect (800, 430, 300, 100), nextBtn)) {
				sceneNo = 7;
			}
			break;
		case 7:
			if(currentScene != 7) {
				Destroy(scene2);
				scene2 = (GameObject)Instantiate (Resources.Load ("prefab_mining"));
				currentScene = 7;
			}
			//GUI.Label (new Rect (790, 375, 100, 60), "Let's try a quest");
			if (GUI.Button (new Rect (800, 360, 300, 100), nextBtn)) {
				//Destroy(scene2);
				Application.LoadLevel("theShopQuest");
			}
			break;
		default:
			break;
		}
	}
}
