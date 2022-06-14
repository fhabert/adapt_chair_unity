using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionL : MonoBehaviour
{
    public Transform transform;
    public ParticleSystem particlesL;
    public GameManager gm;

    void Start()
    {
        particlesL = GetComponent<ParticleSystem>();
        gm = GameManager.instance;
        transform.Rotate(0f, 0.0f, gm.sensorRotation, Space.World);
    }

    void OnParticleCollision(GameObject other) {
        foreach (string person in gm.allPeople) {
            if (person == other.name) {
                gm.elementDetected = "person";
                break;
            }
        }
        if (other.name == "SquareTableHigh" | other.name == "SquareTableLow" | other.name == "RoundTableHigh" | other.name == "RoundTableLow") {
            gm.elementDetected = "table";
        }
        gm.distanceL = Mathf.Round(getDistance(transform.position, other.transform.position) * 100);
        gm.heightTable = other.transform.localScale[2];
        gm.straightDistance = Mathf.Sqrt(Mathf.Pow(gm.distanceL, 2) - Mathf.Pow(gm.heightTable, 2));
        // Debug.Log("Height L: " + gm.heightTable.ToString());
        Debug.Log("Distance L: " + gm.straightDistance.ToString());
    }
    
    float getDistance(Vector3 pos1, Vector3 pos2) {
        float val = Mathf.Sqrt(Mathf.Pow(pos1[0] - pos2[0], 2) + Mathf.Pow(pos1[1] - pos2[1], 2) + Mathf.Pow(pos1[2] - pos2[2], 2));
        return val;
    }
}
