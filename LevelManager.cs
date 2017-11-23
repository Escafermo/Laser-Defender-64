using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		Debug.Log ("Level load requested: "+name);
		Application.LoadLevel (name);
	}
	public void Quit(){
		Debug.Log ("Quit level requested");
		Application.Quit();
	}
	
	public void BackToStart(string name){
		Debug.Log ("Back to start requested");
		Application.LoadLevel (name);
	}
	
}