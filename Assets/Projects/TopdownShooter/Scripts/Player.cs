using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sandbox2d.TopdownShooter {
  public class Player : MonoBehaviour {
    [SerializeField] private InputAction input;
    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotCooldown = 0.2f;
    [SerializeField] private Transform gunPoint;

    private Rigidbody2D rb;
    private Vector2 moveVector;

    private Vector3 mousePos;
    private float lastShot;

    private void Awake() {
      input.Enable();

      rb = GetComponent<Rigidbody2D>();
      if (rb == null) Debug.LogWarning("Rigidbody2D wasn't found on Player");

      lastShot = Time.time - shotCooldown;
    }


    private void Update() {
      moveVector = input.ReadValue<Vector2>();

      HandleRotation();

      HandleShooting();
    }

    private void FixedUpdate() {
      rb.linearVelocity = moveVector * moveSpeed;
    }


    private void HandleRotation() {
      Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
      mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
      mousePos.z = 0;

      Vector2 direction = (mousePos - transform.position).normalized;
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void HandleShooting() {
      if (Mouse.current.leftButton.wasPressedThisFrame && Time.time - lastShot > shotCooldown) {
        lastShot = Time.time;
        // shoot
        Vector3 dir = (mousePos - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Direction = dir;
      }
    }
  }
}