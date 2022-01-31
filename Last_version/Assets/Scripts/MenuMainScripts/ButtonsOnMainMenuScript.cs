using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonsOnMainMenuScript : MonoBehaviour
{

	private Resolution[] resolutions;
	private Resolution[] res;
	[SerializeField] private Text text;

	
	

	private void Start()
	{
		resolutions = Screen.resolutions;
		res = resolutions.Distinct().ToArray();
		string[] strResolutions = new string[res.Length];
		for (int i = 0; i < resolutions.Length; i++)
		{
			strResolutions[i] = res[i].width.ToString() + " x " + res[i].height.ToString();
		}
		Screen.SetResolution(res[res.Length - 1].width, res[res.Length - 1].height, true);

	}

	public void OnPlayClick()
	{
	
		SceneManager.LoadScene("MainScene");
	}


	public void OnScreenOptionsClick()
	{
		text.gameObject.SetActive(!text.gameObject.activeSelf);
	}
	

	public void ResolutionChange()
	{
		text.gameObject.SetActive(!text.gameObject.activeSelf);
	}


	public void GameExit()
	{
		Application.Quit();
	
	}


}
