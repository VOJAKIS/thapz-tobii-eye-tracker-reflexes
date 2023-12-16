using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
	public TMP_Text endReactionsText;

	ReactionTime reactionTime;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene("Game");
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu");
		}
	}

	void OnEnable()
	{
		reactionTime = DataSaver.loadData<ReactionTime>("reactionTime");

		string reactionTimes = "REACTION TIMES";
		string numberOfPoints = "number of points: " + reactionTime.getCount().ToString("F0");
		string slowestReactionTime = "slowest: " + reactionTime.getMaximum().ToString("F0") + "ms";
		string averageReactionTime = "average: " + reactionTime.getAverage().ToString("F0") + "ms";
		string fastestReactionTime = "fastest: " + reactionTime.getMinimum().ToString("F0") + "ms";

		endReactionsText.text = reactionTimes + "\n\n" + numberOfPoints + "\n" + slowestReactionTime + "\n" + averageReactionTime + "\n" + fastestReactionTime;
	}
}
