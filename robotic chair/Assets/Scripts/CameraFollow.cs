using UnityEngine;

namespace UnityTemplateProjects
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed = 0.135f;
        public float turnSpeed = 0.3f;
        public Vector3 offset;

        void Start() {
            offset = new Vector3(target.position.x - 12.6f, target.position.y + 2.5f, target.position.z - 0.2f);
        }

        void LateUpdate() {
            // Vector3 desiredPosition = target.position + offset;
            // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.Rotate(5, Input.GetAxis("Horizontal") + 4f, 0);
            transform.position = target.position + offset - new Vector3(0f, 0.6f, -0.3f);
        }
    }
}