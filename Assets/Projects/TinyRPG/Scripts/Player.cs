using TinyRPG;
using UnityEngine;

namespace Sandbox2d.TinyRPG {
  public class Player : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerAnimation anim;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxLife = 1000.0f;
    private float life;

    private TinyRPGInput input;
    private Vector2 moveInput;
    private Vector3 lastDir;

    [Header("Attack")]
    [SerializeField] private float damage = 30.0f;
    [SerializeField] private float attackTime = 0.3f;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayer;
    private bool isAttacking = false;
    private float attackStart = 0.0f;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip attackSfx;
    [SerializeField] private AudioClip moveSfx;
    private float stepInterval = 0.53f;
    private float stepTimer = 0f;
    private AudioSource audioSource;

    private void Awake() {
      rb = GetComponent<Rigidbody2D>();
      audioSource = GetComponent<AudioSource>();
      input = new TinyRPGInput();
      input.Player.Attack.performed += ctx => OnAttack();

      lastDir = Vector3.forward;
      life = maxLife;
    }

    private void FixedUpdate() {
      FinishAttack();
      if (isAttacking) return;

      moveInput = input.Player.Move.ReadValue<Vector2>();
      HandleRotation();
      HandleMovement();
    }

    public void TakeDamage(float value) {
      life -= value;
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
      if (moveInput != Vector2.zero) {
        lastDir = moveInput;
        PlayMoveSfx();
      }

      rb.linearVelocity = moveInput * speed;

      anim.HandleMovement(moveInput);
    }

    private void PlayMoveSfx() {
      stepTimer -= Time.fixedDeltaTime;
      if (stepTimer <= 0) {
        audioSource.PlayOneShot(moveSfx, 0.1f);
        stepTimer = stepInterval;
      }
    }

    private void OnAttack() {
      if (isAttacking) return;

      isAttacking = true;
      rb.linearVelocity = Vector2.zero;
      attackStart = Time.time;
      anim.ShowAttack();
      audioSource.PlayOneShot(attackSfx);
    }

    private void FinishAttack() {
      if (isAttacking && Time.time - attackStart > attackTime) {
        var hits = Physics2D.CircleCastAll(transform.position, attackRadius, Vector3.forward, 0.0f, enemyLayer);
        foreach (var hit in hits) {
          if (IsInFront(hit)) {
            DealDamage(hit);
          }
        }
        isAttacking = false;
      }
    }

    private bool IsInFront(RaycastHit2D hit) {
      Vector3 dir = (hit.transform.position - transform.position).normalized;
      return Vector3.Dot(lastDir, dir) > 0;
    }

    private void DealDamage(RaycastHit2D hit) {
      if (hit.collider == null) return;

      if (hit.collider.TryGetComponent(out Enemy enemy)) {
        enemy.TakeDamage(damage);
      }
    }

    private void OnEnable() {
      input.Player.Enable();
    }
    private void OnDisable() {
      input.Player.Disable();
    }

    public float GetMaxLife() {
      return maxLife;
    }
    public float GetLife() {
      return life;
    }
  }
}