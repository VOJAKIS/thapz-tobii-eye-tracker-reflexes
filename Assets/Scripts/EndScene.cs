using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
	public TMP_Text endReactionsText;

	public Button playAgainButton;
	public Button goBackToMainMenuButton;

	ReactionTime reactionTime;

	// Start is called before the first frame update
	void Start()
	{
		goBackToMainMenuButton.onClick.AddListener(ChangeSceneToMainMenu);
		playAgainButton.onClick.AddListener(ChangeSceneToGame);
	}

	// Update is called once per frame
	void Update()
	{ }

	void ChangeSceneToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	void ChangeSceneToGame()
	{
		SceneManager.LoadScene("Game");
	}

	void OnEnable()
	{
		reactionTime = DataSaver.loadData<ReactionTime>("reactionTime");

		// string reactionTimes = "REACTION TIMES";
		string numberOfPoints = "number of points: " + reactionTime.getCount().ToString("F0");
		string slowestReactionTime = "slowest: " + reactionTime.getMaximum().ToString("F0") + "ms";
		string averageReactionTime = "average: " + reactionTime.getAverage().ToString("F0") + "ms";
		string fastestReactionTime = "fastest: " + reactionTime.getMinimum().ToString("F0") + "ms";

		endReactionsText.text = numberOfPoints + "\n" + slowestReactionTime + "\n" + averageReactionTime + "\n" + fastestReactionTime;
	}
}
