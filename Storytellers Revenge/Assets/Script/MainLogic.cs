using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	public List<GameObject> videoControlPanel;

	//UI button material
	public Material playMaterial;
	public Material pauseMaterial; 

	//audio objects
	public AudioSource audioPlayer;
	public List<AudioSource> AudioContainers;

	//video objects
	public List<string> Urls;

	//private variables
	private VideoPlayer videoPlayer;
	private Animator titleAnimation;
	private Animator creditAnimation;
	private bool isVideoPlayingFlag = false;
	private bool videoStarted = false;
	private int videoIndex;

	// Use this for initialization
	void Start () {
		videoPlayer = videoSphere.transform.GetComponent<VideoPlayer> ();
		titleAnimation = titleUI.GetComponent<Animator> ();
		creditAnimation = creditUI.GetComponent<Animator> ();
		videoPlayer.loopPointReached += CheckOver;
	}
	
	// Update is called once per frame
	void Update () {
		//Check if title animation is over. 
		if (titleAnimation.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			titleUI.SetActive (false);
		}
		//Check if credit animation is over. 
		if (creditAnimation.GetCurrentAnimatorStateInfo(0).IsName("Credit_Idle")) {
			foreach (AudioSource aud in AudioContainers) {
				aud.Stop ();
			}
			creditUI.SetActive (false);
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

	//Executes when video finishes playing
	public void CheckOver(UnityEngine.Video.VideoPlayer vp) {
		videoControlPanel [0].SetActive (false);
		videoControlPanel [1].SetActive (true);
		creditUI.SetActive (true);
	}

	public void Enter() {
		startText.text = "Choose your destination";
		startButtons [0].gameObject.SetActive (false);
		startButtons [1].gameObject.SetActive (true);
		startButtons [2].gameObject.SetActive (true);
	}

	//Executes when player chooses a destination
	public void ChooseDestination(int index) {
		videoIndex = index;
		startUI.SetActive (false);
		dummyRoom.SetActive (false);
		videoSphere.SetActive (true);
		InitializeVideoPlayer (index);
		InitializeSpatialAudio (index);

		titleUI.SetActive (true);
		titleText.text = placeName [index];
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
		if (index == 0) {
			AudioContainers [0].Play ();
			AudioContainers [1].Play ();
		} else {
			AudioContainers [2].Play ();
		}

	}

	public void InitializeTitleUI(int index) {
		titleAnimation.Play ("Title_animation");

		//Also initialize video control panel
		videoControlPanel[0].SetActive(true);
	}

	//Executes when user hits Play/Pause button
	public void TogglePause(Button btn) {
		if (videoPlayer.isPlaying) {
			videoPlayer.Pause ();
			if (videoIndex == 0) {
				AudioContainers [0].Pause ();
				AudioContainers [1].Pause ();
			} else {
				AudioContainers [2].Pause ();
			}
			Image render = btn.GetComponent<Image> ();
			render.material = playMaterial;
		} else {
			videoPlayer.Play ();
			if (videoIndex == 0) {
				AudioContainers [0].Play ();
				AudioContainers [1].Play ();
			} else {
				AudioContainers [2].Play ();
			}
			Image render = btn.GetComponent<Image> ();
			render.material = pauseMaterial;
		}
	}

	public void Replay() {
		ChooseDestination (videoIndex);
		videoControlPanel[0].SetActive(true);
		videoControlPanel[1].SetActive(false);
	}

	public void Home() {
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene (scene.name);
	}
}
