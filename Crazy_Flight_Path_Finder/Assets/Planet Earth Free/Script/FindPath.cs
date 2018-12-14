using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;

public class FindPath : MonoBehaviour
{
	string[] airports =
		{
		"ICN","CJU","AER","KZN","CEK","EGO","JJN","KHN","PEK","DEL","BOM","AMD",
		"BBI","BDQ","NRT","NGO","KIX","FUK","KUL","BKI","ARN","GOT","FCO","PSA",
		"LHR","BFS","GLA","STN","MUC","FRA","DUS","CDG","NCE","ORY","YYC","YHZ",
		"YUL","YOW","YVR","JFK","LAX","BNA","DTW","MIA","PHX","AKL","OOL"
		};

	public Dropdown Option;
	public Dropdown Departure;
	public Dropdown Arrival;

	public class Edge
	{
		public Edge(int cost, int timeCost, Vertex dest)
		{
			this.cost = cost;
			this.timeCost = timeCost;
			this.dest = dest;
		}
		public int cost;
		public int timeCost;
		public Vertex dest;
	}

	public class Vertex : IComparable<Vertex>
	{
		public Vertex(string name)
		{
			this.name = name;
		}

		public int lastHeapIndex;
		public bool visited;
		public Vertex from;
		public int cost;
		public string name;
		public List<Edge> edges = new List<Edge>();

		public int CompareTo(Vertex other)
		{
			return cost.CompareTo(other.cost);
		}
	}
	Dictionary<string, Vertex> vertexMap = new Dictionary<string, Vertex>();
	Dictionary<string, string> short2long = new Dictionary<string, string>();
	Dictionary<string, string> long2short = new Dictionary<string, string>();
	public List<int> listcost = new List<int>();
	enum OPTION
	{
		CheapestCost,
		ShortestTime
	}
	void Start()
	{
		initSpline();
		foreach (var str in Airports)
		{
			long2short[str] = str.Substring(0, 3);
			short2long[str.Substring(0, 3)] = str;
		}

		foreach (var airport in airports)
		{
			vertexMap[airport] = new Vertex(airport);
		}

		FileRead();
		Optional();
	}

	// Update is called once per frame
	void Update()
	{
		Optional();
		Line(temp);
	}
	void Optional()
	{
		Option.ClearOptions();
		string[] setenum = Enum.GetNames(typeof(OPTION));
		List<string> op = new List<string>(setenum);
		if (GameObject.FindWithTag("Option_Dropdown"))
		{
			Option.RefreshShownValue();
			Option.AddOptions(op);
		}
		op.Clear();
	}

	void FileRead()
	{
		TextAsset data = Resources.Load("Data", typeof(TextAsset)) as TextAsset;
		StringReader sr = new StringReader(data.text);
		string source = sr.ReadLine();
		string[] temp;

		while (source != null)
		{
			temp = source.Split(' ');
			string dept = temp[0];
			string dest = temp[2];
			int cost = int.Parse(temp[4]);
			int timeCost = int.Parse(temp[6]) - int.Parse(temp[5]);
			vertexMap[dept].edges.Add(new Edge(cost, timeCost, vertexMap[dest]));
			vertexMap[dest].edges.Add(new Edge(cost, timeCost, vertexMap[dept]));

			source = sr.ReadLine();
		}
	}

	public string[] Airports = {
		"ICN_Incheon","CJU_Jeju", "AER_Sochi","KZN_Kazan","CEK_Chelyabinsk",
		"EGO_Belgorod","JJN_Jinjiang","KHN_Nanchang","PEK_Beijing","DEL_Deli",
		"BOM_Mumbai","AMD_Ahmedabad","BBI_Bhubaneswar","BDQ_Vadodara","NRT_Narita",
		"NGO_Chubu","KIX_Kansai","FUK_Fukuoka","KUL_KualaLumpur","BKI_KotaKinabalu",
		"ARN_Stockholm","GOT_Gothenburg","FCO_Rome","PSA_Pisa","LHR_London","BFS_Belfast",
		"GLA_Glasgow","STN_Stansted","MUC_Munich","FRA_Frankfurt","DUS_Dusseldorf","CDG_Paris",
		"NCE_Nice","ORY_Orly","YYC_Calgary","YHZ_Halifax","YUL_Montreal","YOW_Ottawa","YVR_Vancouver",
		"JFK_Newyork","LAX_LA","BNA_Nashville","DTW_Detroit","MIA_Miami","PHX_Phoenix","AKL_Auckland",
		"OOL_Goldcost"};


	public List<string> AmazingDijkstra(string deptStr, string destStr, bool isTime)
	{
		listcost.Clear();
		foreach (var vertex in vertexMap.Values)
		{
			vertex.cost = 987654321;
			vertex.visited = false;
		}

		Vertex dept = vertexMap[deptStr];
		Vertex dest = vertexMap[destStr];

		//print("Dest: " + destStr + " Dept: " + deptStr);

		PriorityQueue q = new PriorityQueue();
		q.Add(dept);
		dept.from = null;
		dept.cost = 0;

		while (q.Count != 0)
		{
			Vertex visit = q.Pop();
			visit.visited = true;
			if (visit == dest) break;
			//print("Visit: " + visit.name);

			foreach (var edge in visit.edges)
			{
				if (isTime)
				{
					if (!edge.dest.visited && visit.cost + edge.timeCost < edge.dest.cost)
					{
						edge.dest.from = visit;
						edge.dest.cost = visit.cost + edge.timeCost;

						if (!q.Contains(edge.dest))
						{
							q.Add(edge.dest);
							listcost.Add(edge.dest.cost);
						}
						else
						{
							q.UpdateItem(edge.dest);

						}
					}
				}
				else
				{
					//print("Node " + edge.dest.name + " Prev cost: " + edge.dest.cost + " New Cost: " + (visit.cost + edge.cost));
					if (!edge.dest.visited && visit.cost + edge.cost < edge.dest.cost)
					{
						//print("Release " + edge.dest.name);
						edge.dest.from = visit;
						edge.dest.cost = visit.cost + edge.cost;

						if (!q.Contains(edge.dest))
						{
							q.Add(edge.dest);
							listcost.Add(edge.dest.cost);
						}
						else
						{
							q.UpdateItem(edge.dest);
						}
					}
				}
			}
		}

		var path = new List<string>();

		Vertex node = dest;
		while (node != null)
		{
			path.Add(short2long[node.name]);
			node = node.from;
		}
		path.Reverse();
		return path;
	}

	public Spline[] spline = new Spline[30];
	public void Line(List<string> temp)
	{
		for (int i = 0; i < 47; i++)
		{
			GameObject init = GameObject.Find(Airports[i]);
			if (init.GetComponent<MeshRenderer>().material.color == Color.black)
				init.GetComponent<MeshRenderer>().material.color = Color.yellow;
		}
		for (int i = 0; i < temp.Count - 1; i++)
		{
			spline[i].gameObject.SetActive(true);
			GameObject dept = GameObject.Find(temp[i]);
			GameObject dest = GameObject.Find(temp[i + 1]);
			if (dest.GetComponent<MeshRenderer>().material.color != Color.blue)
				dest.GetComponent<MeshRenderer>().material.color = Color.black;
			spline[i].nodes[0].SetPosition(dept.transform.position);
			spline[i].nodes[0].SetDirection(dept.transform.position + Vector3.Normalize(dept.transform.position));
			spline[i].nodes[1].SetPosition(dest.transform.position);
			spline[i].nodes[1].SetDirection(dest.transform.position - Vector3.Normalize(dest.transform.position));
		}
	}
	public int size = 30;
	public List<string> temp;
	public string temp_dept, temp_dest;
	public void initSpline()
	{
		for (int i = 0; i < 30; i++)
			spline[i].gameObject.SetActive(false);
	}
	public void AmazingSuperGodFindPath()
	{
		string set_dept = Departure.captionText.text;
		string set_dest = Arrival.captionText.text;
		string deptStr = long2short[set_dept];
		print(deptStr);
		string destStr = long2short[set_dest];
		var path = AmazingDijkstra(deptStr, destStr, Option.captionText.text == "ShortestTime");
		temp_dept = set_dept; temp_dest = set_dest;
		temp = path;

		for (int i = 0; i < size; i++)
			spline[i].gameObject.SetActive(false);
		Line(temp);
		size = path.Count;
	}
}