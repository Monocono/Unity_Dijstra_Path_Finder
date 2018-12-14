using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDetailPath : MonoBehaviour
{
	FindPath fp;
	public Text[] text = new Text[16];
	public GameObject detail;

	List<string> path;
	List<int> cost;

	private bool paused = false;
	// Use this for initialization
	void Start()
	{
		initText();
		fp = GameObject.Find("OptionPanel").GetComponent<FindPath>();
		detail.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (paused)
		{
			detail.SetActive(true);
			Time.timeScale = 0;
		}
		if (!paused)
		{
			detail.SetActive(false);
			Time.timeScale = 1f;
		}
		View();
	}
	public void DetailView()
	{
		paused = !paused;
		fp.Line(path);
	}
	public void initText()
	{
		for (int i = 0; i < 16; i++)
			text[i].gameObject.SetActive(false);
	}
	public void View()
	{
		int sum;
		sum = 0;
		path = fp.temp;
		cost = fp.listcost;
		for (int i = 0; i < path.Count; i++)
		{
			text[i].gameObject.SetActive(true);
			if (i == path.Count - 1)
				text[i].text = path[0] + " >> " + path[path.Count - 1] + "\nTotal Cost : " + sum + "\n";
			else
			{
				text[i].text = path[i] + " >> " + path[i + 1] + "\nCost : " + cost[i] + "\n";
				sum += cost[i];
			}
		}
	}
}
