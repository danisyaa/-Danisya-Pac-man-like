using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    Rigidbody _rigidbody;
    [SerializeField]private float speed;
    [SerializeField]private Camera camera;
    [SerializeField]private float PowerUpDuration;
    private Coroutine _powerupCoroutine;

    public void PickPowerUp()
    {
        if(_powerupCoroutine != null)
        {
            StopCoroutine(_powerupCoroutine);
        }
        _powerupCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        if(OnPowerUpStop != null)
        {
            OnPowerUpStart();
            Debug.Log("power up ON");
        }

        yield return new WaitForSeconds(PowerUpDuration);

        if(OnPowerUpStop != null)
        {
            OnPowerUpStop();
            Debug.Log("power up OFF");
        }
    }
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
