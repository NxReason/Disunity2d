using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sandbox2d.Pong {
  public class Player : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private LayerMask collisions;
    [SerializeField] private float collisionSkin;

    private PongControls controls;
    private BoxCollider2D col;

    public float Height { get; private set; }

    private void Awake() {
      col = GetComponent<BoxCollider2D>();
      if (col == null) Debug.LogWarning("BoxCollider2D wasn't found on Player");

      Height = col.size.y / 2;

      controls = new PongControls();
      controls.Enable();
    }

    private void Update() {
      HandleMovement();
    }

    private void HandleMovement() {
      float moveDir = controls.Movement.Slide.ReadValue<float>();
      if (moveDir == 0) return;

      Vector2 moveDelta = new Vector2(0.0f, moveDir * speed * Time.deltaTime);

      if (CanMove(moveDelta)) {
        transform.Translate(moveDelta);
      }
    }

    private bool CanMove(Vector2 direction) {
      float distance = direction.magnitude + collisionSkin;
      RaycastHit2D hit = Physics2D.BoxCast(transform.position, col.size, 0.0f, direction, distance, collisions);
      return hit.collider == null;
    }
  }
}