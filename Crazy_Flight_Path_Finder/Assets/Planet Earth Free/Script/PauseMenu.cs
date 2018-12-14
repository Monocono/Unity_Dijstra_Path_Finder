using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	public GameObject PauseUI;

	private bool paused = false;
	// Use this for initialization
	void Start()
	{
		PauseUI.SetActive(false);
		paused = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			paused = !paused;
		if (paused == true)
		{
			PauseUI.SetActive(true);
			Time.timeScale = 0;
		}
		if (paused == false)
		{
			PauseUI.SetActive(false);
			Time.timeScale = 1f;
		}
	}

	public void ResumeClick()
	{
		paused = !paused;
	}
	public void ExitClick()
	{
		Application.Quit();
	}
}
