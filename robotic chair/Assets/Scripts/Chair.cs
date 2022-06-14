using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTemplateProjects {
    public class Chair : MonoBehaviour
    {
        public Rigidbody chair;
        public GameManager gm;
        public float inputX;
        public float inputZ;
        private Animator anim;
        public float stepBack;

        void Start()
        {
            gm = GameManager.instance;
            chair = GetComponent<Rigidbody>();
            stepBack = 30f;
        }

        void Update()
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");
        }

        void FixedUpdate() {
            if (Input.GetKey("s")) {
                transform.position -= transform.forward * Time.deltaTime * gm.chairSpeed;
            }
            if (Input.GetKey("z")) {
                transform.position += transform.forward * Time.deltaTime * gm.chairSpeed;
            }
            if (Input.GetKey("q")) {
                Quaternion deltaRotation = Quaternion.Euler(gm.angle_right * Time.deltaTime);
                chair.MoveRotation(chair.rotation * deltaRotation);
            }
            if (Input.GetKey("d")) {
                Quaternion deltaRotation = Quaternion.Euler(gm.angle_left * Time.deltaTime);
                chair.MoveRotation(chair.rotation * deltaRotation);
            }
        }
        void OnCollisionEnter(Collision collision) {
            string name = collision.gameObject.name;
            foreach (string person in gm.peopleObstacles) {
                if (name == person) {
                    anim = collision.gameObject.GetComponent<Animator>();
                    anim.Play("Obstacle1");
                }
            }
            if (name == "LeftWall" | name == "RightWall" | name == "FrontWall" | name == "BackWall") {
                transform.position -= transform.forward * Time.deltaTime * gm.chairSpeed * stepBack;
            }
        }
    }
}
