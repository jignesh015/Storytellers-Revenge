using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour {

	//sphere gameobjects
	public GameObject videoSphere;
	public GameObject dummySphere;


	//UI gameobjects
	public GameObject startUI;
	public GameObject titleUI;
	public GameObject creditUI;
	public Text startText;
	public List<Button> startButtons;

	//audio objects
	public AudioSource audioPlayer;
	public List<GameObject> AudioContainers;

	public List<string> Urls;

	private VideoPlayer videoPlayer;

	// Use this for initialization
	void Start () {
		videoPlayer = videoSphere.transform.GetComponent<VideoPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Enter() {
		startText.text = "Choose your destination";
		startButtons [0].gameObject.SetActive (false);
		startButtons [1].gameObject.SetActive (true);
		startButtons [2].gameObject.SetActive (true);
	}

	public void ChooseDestination(int index) {
		startUI.SetActive (false);
		dummySphere.SetActive (false);
		videoSphere.SetActive (true);
		InitializeVideoPlayer (index);
	}

	private void InitializeVideoPlayer(int index) {
		videoPlayer.source = VideoSource.Url;
		videoPlayer.url = Urls [index];
		videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		videoPlayer.EnableAudioTrack(0, true);
		videoPlayer.SetTargetAudioSource(0, audioPlayer);
		videoPlayer.controlledAudioTrackCount = 1;
		audioPlayer.volume = 1.0f;
		Debug.Log ("Success");
		videoPlayer.Play ();
		audioPlayer.Play ();
	}
}
