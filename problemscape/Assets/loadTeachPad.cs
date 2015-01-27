using UnityEngine;
using System.Collections;

public class loadTeachPad : MonoBehaviour {

	public bool teachPadOpen;	
	public GameObject teachPad;
	public GameObject scene1;
	public int currentScene, sceneNo;
	public Texture2D nextBtn;
	public GUISkin mySkin;

	// Use this for initialization
	void Start () {
		teachPadOpen = false;
		currentScene = 0;
		sceneNo = 1;
		nextBtn = (Texture2D)Resources.Load ("nextArrowBig", typeof(Texture2D));
		mySkin = (GUISkin)Resources.Load ("skinPlain", typeof(GUISkin));

	}
	
	// Update is called once per frame
	void OnGUI () {

		GUI.skin = mySkin;

		if (!teachPadOpen) {
			teachPad = (GameObject)Instantiate (Resources.Load ("prefab_teachPad"));
			teachPadOpen = true;
		}
		if (teachMe3.destroyTeachPad) {
			Destroy (teachPad);

			switch (sceneNo) {
			case 1:
				if (currentScene != 1) {
					scene1 = (GameObject)Instantiate (Resources.Load ("prefab_iGotIt"));
					currentScene = 1;
					}
				if (GUI.Button (new Rect (800, 430, 300, 100), nextBtn)) {
					sceneNo = 2;
				}
				break;
			case 2:
				if (currentScene != 2) {
					scene1 = (GameObject)Instantiate (Resources.Load ("prefab_theEnd"));
					currentScene = 2;
				}
				break;
			}
		}
	}
}
