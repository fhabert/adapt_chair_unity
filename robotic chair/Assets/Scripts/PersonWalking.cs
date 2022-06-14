using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonWalking : MonoBehaviour
{
    public GameManager gm;
    public float personSpeed;
    public float goBack;
    public Animator anim;
    public bool hasEncounterChair;

    void Start()
    {
        gm = GameManager.instance;
        personSpeed = 0.8f;
        goBack = 5f;
        hasEncounterChair = false;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking")) {
            transform.position += transform.forward * Time.deltaTime * personSpeed;
        }
    }

    void OnCollisionEnter(Collision collision) {
        string name = collision.gameObject.name;
        if (name == "LeftWall" | name == "RightWall" | name == "FrontWall" | name == "BackWall") {
            transform.position -= transform.forward * Time.deltaTime * personSpeed * goBack;
            transform.Rotate(0f, 110f, 0f);
        }
        if (name == "chair") {
            hasEncounterChair = true;
            anim.Play("0PlayerWaiting");
        }
        foreach (string person in gm.peopleObstacles) {
            if (name == person) {
                transform.Rotate(0f, 90f, 0f);
            }
        }
        foreach (string person in gm.peopleWalking) {
            if (name == person) {
                transform.Rotate(0f, 30f, 0f);
            }
        }
        if (name == "SquareTableHigh" | name == "SquareTableLow" | name == "RoundTableLow" | name == "RoundTableHigh") {
            transform.Rotate(0f, 90f, 0f);
        }
    }
}
