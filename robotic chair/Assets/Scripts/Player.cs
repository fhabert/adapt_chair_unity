using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace UnityTemplateProjects {
    public class Player: MonoBehaviour
    {
        public Rigidbody player;
        public Animator anim;
        public bool is_waiting;
        public GameManager gm;
        public float playerSpeed;
        public string[] peopleObstacles;

        void Start()
        {
            player = GetComponent<Rigidbody>();
            anim.GetComponent<Animator>();
            playerSpeed = 3f;
            is_waiting = true;
            gm = GameManager.instance;
            peopleObstacles = new string[] {"people_talking1", "people_talking2","people_talking3",
            "people_talking4","people_talking5","people_talking6"};
        }

        void Update()
        {
            if (Input.GetKey("q"))
            {
                player.transform.position += new Vector3(playerSpeed * Time.deltaTime, 0f,  0f);
            }

            if (Input.GetKey(("d")))
            {
                player.transform.position += new Vector3(-playerSpeed * Time.deltaTime, 0f, 0f);
            }
            if (Input.GetKey(("s")))
            {
                player.transform.position += new Vector3(0f, 0f, playerSpeed * Time.deltaTime);
            }
            if (Input.GetKey(("z")))
            {
                is_waiting = false;
                // anim.SetBool("0PlayerWaiting", is_waiting);
                anim.Play("Walking");
                player.transform.position += new Vector3(0f, 0f, -playerSpeed * Time.deltaTime);
            } else {
                is_waiting = true;
                anim.Play("0PlayerWaiting");
                // anim.SetBool("Walking", false);
            }
        }
    }
}