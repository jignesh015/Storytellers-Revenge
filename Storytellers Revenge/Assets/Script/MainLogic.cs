using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour {

	//sphere gameobjects
	public GameObject videoSphere;
	public GameObject dummyRoom;


	//UI gameobjects
	public GameObject startUI;
	public GameObject titleUI;
	public GameObject creditUI;
	public Text startText;
	public Text titleText;
	public List<Button> startButtons;
	public List<string> placeName;

	//audio objects
	public AudioSource audioPlayer;
	public List<GameObject> AudioContainers;

	public List<string> Urls;

	private VideoPlayer videoPlayer;
	private Animator titleAnimation;
	private bool isVideoPlayingFlag = false;
	private bool videoStarted = false;
	private int videoIndex;

	// Use this for initialization
	void Start () {
		videoPlayer = videoSphere.transform.GetComponent<VideoPlayer> ();
		titleAnimation = titleUI.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Check if title animation is over. 
		if (titleAnimation.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			titleUI.SetActive (false);
		}
		//Check if video started playing
		if (videoPlayer.isPlaying && !videoStarted) {
			isVideoPlayingFlag = true;
			videoStarted = true;
		}
		if (isVideoPlayingFlag) {
			InitializeTitleUI (videoIndex);
			isVideoPlayingFlag = false;
		}
	}

	public void Enter() {
		startText.text = "Choose your destination";
		startButtons [0].gameObject.SetActive (false);
		startButtons [1].gameObject.SetActive (true);
		startButtons [2].gameObject.SetActive (true);
	}

	public void ChooseDestination(int index) {
		videoIndex = index;
		startUI.SetActive (false);
		dummyRoom.SetActive (false);
		videoSphere.SetActive (true);
		//InitializeTitleUI (index);
		InitializeVideoPlayer (index);
		InitializeSpatialAudio (index);
	}

	private void InitializeVideoPlayer(int index) {
		videoPlayer.source = VideoSource.Url;
		videoPlayer.url = Urls [index];
		videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		videoPlayer.EnableAudioTrack(0, true);
		videoPlayer.SetTargetAudioSource(0, audioPlayer);
		videoPlayer.controlledAudioTrackCount = 1;
		audioPlayer.volume = 1.0f;
		videoPlayer.Play ();
		audioPlayer.Play ();
	}

	public void InitializeSpatialAudio(int index) {
		
	}

	public void InitializeTitleUI(int index) {
		titleUI.SetActive (true);
		titleAnimation.Play ("Title_animation");
		titleText.text = placeName [index];
	}
}
