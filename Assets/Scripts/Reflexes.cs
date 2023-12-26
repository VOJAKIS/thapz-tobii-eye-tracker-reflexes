using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tobii.Gaming;
using UnityEngine.SceneManagement;

public class Reflexes : MonoBehaviour
{
	public bool eyeTrackerOrElseMouse = true;

	// Point to show and or hide
	public GameObject point;
	public static int pointRadius = 10;
	private bool relocatedPoint = false;

	// Reaction time text to modify, "Reaction time: 123ms"
	public TMP_Text reactionTimeText;

	// Time
	private float timeInSeconds = 0;
	private float lastCollisionTimeInSeconds = -1;

	// Reaction time class to OOP this b
	public static ReactionTime reactionTime;
	private float showTimeInSeconds = 0;
	public int MAXIMUM_NUMBER_OF_REACTION_TIMES = 2;

	// Start is called before the first frame update
	void Start()
	{
		reactionTime = new ReactionTime();
		hidePoint();
	}

	// Update is called once per frame
	void Update()
	{
		timeInSeconds += Time.deltaTime;

		/* After 1 second:
		relocate the point
		show the point
		set a show time at the current time
		wait for the gaze points to somehow match the position of the point
		if there is a collision between mouse button and point,
			save reaction time to ReactionTime object
			hide point
		*/

		if (reactionTime.getCount() >= MAXIMUM_NUMBER_OF_REACTION_TIMES)
		{
			DataSaver.saveData<ReactionTime>(reactionTime, "reactionTime");
			SceneManager.LoadScene("End");
			return;
		}

		if (timeInSeconds > 1.0f)
		{
			if (!relocatedPoint && (lastCollisionTimeInSeconds + 2f < timeInSeconds))
			{
				relocatePoint();
				showPoint();
				showTimeInSeconds = timeInSeconds;
				relocatedPoint = true;
			}

			setReflexTimeText();
		}

		if (eyeTrackerOrElseMouse)
		{
			GazePoint gazePoint = TobiiAPI.GetGazePoint();
			// if (gazePoint.IsRecent())
			if (gazePoint.IsRecent() || true)
			{
				Ray ray = Camera.main.ScreenPointToRay(gazePoint.Screen);
				RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

				if (hit.collider != null)
				{
					hidePoint();
					saveReactionTime();
					lastCollisionTimeInSeconds = timeInSeconds;
					relocatedPoint = false;
				}
			}
		}
		else
		{
			if (Input.GetMouseButton(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

				if (hit.collider != null)
				{
					hidePoint();
					saveReactionTime();
					lastCollisionTimeInSeconds = timeInSeconds;
					relocatedPoint = false;
				}
			}
		}
	}

	private void saveReactionTime()
	{
		float toAddReactionTime = convertSecondsToMiliseconds(timeInSeconds - showTimeInSeconds);
		reactionTime.add(toAddReactionTime);
	}

	private void setReflexTimeText()
	{
		float reactionTimeInMiliseconds = convertSecondsToMiliseconds(timeInSeconds - showTimeInSeconds);
		if (!point.activeSelf)
		{
			reactionTimeInMiliseconds = reactionTime.getLast();
		}
		string text = "Reaction time: " + reactionTimeInMiliseconds.ToString("F0") + "ms";
		reactionTimeText.text = text;
	}

	private float convertSecondsToMiliseconds(float seconds)
	{
		return seconds * 100;
	}

	private void relocatePoint()
	{
		float absoluteMaximumPositionX = 7.2f;
		float absoluteMaximumPositionY = 4.5f;

		float x = Random.Range(-absoluteMaximumPositionX, absoluteMaximumPositionX);
		float y = Random.Range(-absoluteMaximumPositionY, absoluteMaximumPositionY);

		Vector3 newPosition = new Vector3(x, y);
		point.transform.position = newPosition;
	}

	private void showPoint()
	{
		point.SetActive(true);
	}

	private void hidePoint()
	{
		point.SetActive(false);
	}
}
