using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float distanceL;
    public float distanceR;
    public float heightTable;
    private float minHeightTable;
    public float chairSpeed;
    public float moveBack;
    public bool tableDetected;
    public string elementDetected;
    public List<string> peopleObstacles;
    public List<string> peopleWalking;
    public List<string> allPeople;
    public int numWalking;
    public int numObstacles;
    public GameObject body;
    private Rigidbody chair;
    public Vector3 angle_left;
    public Vector3 angle_right;
    public float sensorRotation;
    public float straightDistance;
    
    void Awake() {
        body = GameObject.Find("chair");
        chair = body.GetComponent<Rigidbody>();
        if (instance != null & instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(chair);
        }
        numObstacles = 9;
        numWalking = 6;
        peopleWalking = new List<string>();
        peopleObstacles = new List<string>();
        allPeople = new List<string>();
        for (int i = 0; i < numObstacles; i++) {
            string name = $"person_talking{i+1}";
            peopleObstacles.Add(name);
        }
        for (int i = 0; i < numWalking; i++) {
            string name = $"person_walking{i+1}";
            peopleWalking.Add(name);
        }
        allPeople.AddRange(peopleObstacles);
        allPeople.AddRange(peopleWalking);
    }

    void Start() {
        tableDetected = true;
        elementDetected = "";
        moveBack = 20f;
        chairSpeed = 4f;
        minHeightTable = 3f;
        distanceL = 200;
        distanceR = 200;
        heightTable = 5f;
        numWalking = 6;
        numObstacles = 9;
        straightDistance = 0f;
        angle_right = new Vector3(0, -100, 0);
        angle_left = new Vector3(0, 100, 0);
        sensorRotation = 45f;
    }

    void FixedUpdate() {
        if (distanceL < 170 | distanceR < 170) {
            if (chair != null & elementDetected == "table") {
                if (heightTable < minHeightTable) {
                    try {
                        StartCoroutine(MoveBackwards());
                        heightTable = 5f;
                    } catch (Exception e) {
                        Debug.Log("Error: " + e.ToString());
                    }
                } else {
                    bool diff = closeEnough(distanceL, distanceR);
                    if (!diff) {
                        if (distanceL < distanceR) {
                            Quaternion deltaRotation = Quaternion.Euler(angle_right * Time.deltaTime);
                            chair.MoveRotation(chair.rotation * deltaRotation);
                        } else if (distanceR < distanceL) {
                            Quaternion deltaRotation = Quaternion.Euler(angle_left * Time.deltaTime);
                            chair.MoveRotation(chair.rotation * deltaRotation);
                        }
                    }
                }
            }
        }
    }

    IEnumerator MoveBackwards() {
        Vector3 startPos = chair.transform.position;
        Vector3 arrivalPoint = chair.transform.position - chair.transform.forward * Time.deltaTime * moveBack;
        while (chair.transform.position != arrivalPoint) {
            chair.transform.position = Vector3.Lerp(startPos, arrivalPoint, chairSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
    
    bool closeEnough(float dis1, float dis2) {
        float dif = dis1 - dis2;
        float val = Mathf.Sqrt(Mathf.Pow(dif, 2f));
        return val <= 3;
    }
}
