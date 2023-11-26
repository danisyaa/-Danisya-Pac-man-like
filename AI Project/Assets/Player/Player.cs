using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField]private float speed;
    [SerializeField]private Camera camera;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    private void HideAndLockCursor(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * camera.transform.right;
        Vector3 verticalDirection = vertical * camera.transform.forward;
        verticalDirection.y = 0;
        horizontalDirection.y = 0;



        Vector3 movementDirection = horizontalDirection + verticalDirection;
        _rigidbody.velocity = movementDirection * speed * Time.fixedDeltaTime;
    }
}
