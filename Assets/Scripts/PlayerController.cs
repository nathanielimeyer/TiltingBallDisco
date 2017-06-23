using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float speed;
	public Text countText;
	public Text timeRemainingDisplayText;
	public Text winText;
//	public GameObject roundEndDisplay;

	private DataController dataController;
	private RoundData currentRoundData;

	private bool isRoundActive;
	private float timeRemaining;


	private Rigidbody rb;
	private int count;

	void Start ()
	{
		dataController = FindObjectOfType<DataController> ();
		currentRoundData = dataController.GetCurrentRoundData ();
		timeRemaining = currentRoundData.timeLimitInSeconds;
		UpdateTimeRemainingDisplay();

		rb = GetComponent<Rigidbody> ();	
		count = 0;
		SetCountText ();
		winText.text = "";
		isRoundActive = true;

	}

	void Update () 
	{
		if (isRoundActive) 
		{
			timeRemaining -= Time.deltaTime;
			UpdateTimeRemainingDisplay();

			if (timeRemaining <= 0f)
			{
				EndRound();
			}

		}
	}
		
	void FixedUpdate ()
	{

		Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.y);
		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pickup"))
		{
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	public void EndRound()
	{
		isRoundActive = false;
	}

	private void UpdateTimeRemainingDisplay()
	{
		timeRemainingDisplayText.text = "Time: " + Mathf.Round (timeRemaining).ToString ();
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12)
		{
			winText.text = "You win!";
			ReturnToMenu ();
		}
	}



	void ReturnToMenu ()
	{
		SceneManager.LoadScene ("MenuScreen");
	}		
}
