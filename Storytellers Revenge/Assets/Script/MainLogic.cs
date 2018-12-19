using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour {

	public GameObject videoSphere;
	public GameObject dummySphere;

	public GameObject startUI;

	public List<string> Urls;

	private VideoPlayer videoPlayer;

	// Use this for initialization
	void Start () {
		videoPlayer = videoSphere.transform.GetComponent<VideoPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayVideo() {
		Debug.Log ("clicked");
		dummySphere.SetActive (false);
		videoSphere.SetActive (true);
		startUI.SetActive (false);
		InitializeVideoPlayer ();
	}

	private void InitializeVideoPlayer() {
		videoPlayer.source = VideoSource.Url;
		videoPlayer.url = Urls [0];
		Debug.Log ("Success");
		videoPlayer.Play ();
	}
}
