using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTest : MonoBehaviour {
    public Spline[] spline = new Spline[10];

    public Transform incheon;
    public Transform jeju;
	// Use this for initialization
	void Start () {
        spline[0].gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        spline[0].nodes[0].SetPosition(incheon.position); //공항 출발지점
        spline[0].nodes[0].SetDirection(incheon.position + Vector3.Normalize(incheon.position)); //공항 출발지점 + 공항 출발지점 표준화 값
        spline[0].nodes[1].SetPosition(jeju.position); //공항 도착지점
        spline[0].nodes[1].SetDirection(jeju.position - Vector3.Normalize(jeju.position)); //공항 도착지점 - 공항 도착지점 표준화 값
    }
}
