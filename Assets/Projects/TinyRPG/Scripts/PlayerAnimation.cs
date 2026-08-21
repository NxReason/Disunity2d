using UnityEngine;

namespace Sandbox2d.TinyRPG {
  public class PlayerAnimation : MonoBehaviour {
    public static readonly int Speed = Animator.StringToHash("Speed");
    public static readonly int Attack = Animator.StringToHash("Attack");

    private Animator animator;

    private void Awake() {
      animator = GetComponentInChildren<Animator>();
    }

    public void HandleMovement(Vector2 moveInput) {
      if (moveInput != Vector2.zero) {
        animator.SetFloat(Speed, 1);
      }
      else {
        animator.SetFloat(Speed, 0.0f);
      }
    }

    public void ShowAttack() {
      animator.SetTrigger(Attack);
    }

  }
}