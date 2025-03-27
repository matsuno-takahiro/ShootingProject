using UnityEngine;

namespace MapAndRadarSystem
{
    public class SimpleMoveRotate : MonoBehaviour
    {
        public CharacterController characterController;

        private float rotationX;
        private float rotationY;
        private Vector3 moveDirection;
        private float moveSpeed = 8f;
        private float gravity = 9.81f;
        private float verticalVelocity;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            moveDirection = move * moveSpeed;

            // Yer çekimi
            if (characterController.isGrounded)
            {
                verticalVelocity = -gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }

            moveDirection.y = verticalVelocity;

            // Hareketi uygula
            characterController.Move(moveDirection * Time.deltaTime);

            float mouseX = Input.GetAxis("Mouse X") * 3;
            float mouseY = Input.GetAxis("Mouse Y") * 3;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
            rotationY += mouseX;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
    }
}