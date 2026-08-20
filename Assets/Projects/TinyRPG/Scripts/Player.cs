using System;
using TinyRPG;
using UnityEngine;

namespace Sandbox2d.TinyRPG {
  public class Player : MonoBehaviour {
    [SerializeField] private float speed = 10f;

    private TinyRPGInput input;
    private Vector2 moveInput;

    private Animator animator;

    private void Awake() {
      input = new TinyRPGInput();
      animator = GetComponentInChildren<Animator>();

      input.Player.Attack.performed += ctx => OnAttack();
    }

    private void Start() {
      Debug.Log(transform.localScale);
    }

    private void Update() {
      moveInput = input.Player.Move.ReadValue<Vector2>();
      HandleRotation();
      HandleMovement();
      HandleAnimation();
    }

    private void HandleRotation() {
      if (moveInput.x < 0) {
        transform.localScale = new Vector3(-1, 1, 1);
      }
      else if (moveInput.x > 0) {
        transform.localScale = new Vector3(1, 1, 1);
      }
    }

    private void HandleMovement() {
      Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * speed * Time.deltaTime;
      transform.Translate(movement);
    }

    private void HandleAnimation() {
      if (moveInput != Vector2.zero) {
        animator.SetFloat("Speed", speed);
      }
      else {
        animator.SetFloat("Speed", 0.0f);
      }
    }

    private void OnAttack() {
      animator.SetTrigger("Attack");
    }

    private void OnEnable() {
      input.Player.Enable();
    }
    private void OnDisable() {
      input.Player.Disable();
    }
  }
}