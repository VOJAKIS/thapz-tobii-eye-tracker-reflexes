using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScene : MonoBehaviour
{
	public Button startButton;
	public Button exitButton;

	// Start is called before the first frame update
	void Start()
	{
		startButton.onClick.AddListener(startGame);

		exitButton.onClick.AddListener(exitGame);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void startGame()
	{
		SceneManager.LoadScene("Game");
	}

	void exitGame()
	{
		Application.Quit();
	}
}
