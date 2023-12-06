using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    Rigidbody _rigidbody;
    [SerializeField]private float speed;
    [SerializeField]private Camera camera;
    [SerializeField]private float PowerUpDuration;
    [SerializeField] private int _health;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Transform _respawnPoint;
    private Coroutine _powerupCoroutine;

    private bool _isPowerUpActive = false;

    public void Dead()
    {
        _health -= 1;
        if (_health > 0)
        {
            transform.position = _respawnPoint.position;
        }
        else
        {
            _health = 0;
            Debug.Log("LOSE");
        }
        UpdateUI();
    }

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
        _isPowerUpActive = true;
        if(OnPowerUpStop != null)
        {
            OnPowerUpStart();
            Debug.Log("power up ON");
        }

        yield return new WaitForSeconds(PowerUpDuration);
        _isPowerUpActive = false;
        if(OnPowerUpStop != null)
        {
            OnPowerUpStop();
            Debug.Log("power up OFF");
        }
    }
    private void Awake()
    {
        UpdateUI();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateUI()
    {
        _healthText.text = "Health : " + _health;
    }
}
