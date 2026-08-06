using UnityEngine;
using UnityEngine.InputSystem;

namespace Sandbox2d.TopdownShooter {
  public class Player : MonoBehaviour {
    [SerializeField] private InputAction input;
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 moveVector;

    private void Awake() {
      input.Enable();

      rb = GetComponent<Rigidbody2D>();
      if (rb == null) Debug.LogWarning("Rigidbody2D wasn't found on Player");
    }


    private void Update() {
      moveVector = input.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
      rb.linearVelocity = moveVector * moveSpeed;
    }
  }
}