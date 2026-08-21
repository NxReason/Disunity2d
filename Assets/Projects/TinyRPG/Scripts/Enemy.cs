using UnityEngine;

namespace Sandbox2d.TinyRPG {
  public class Enemy : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float aggroRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private LayerMask playerLayer;
    private float attackStart;
    private bool isAttacking = false;
    private Vector3 attackDir;

    [Header("Life")]
    [SerializeField] private HealthBar hpBar;
    [SerializeField] private float maxLife = 100.0f;
    private float life;

    [SerializeField] private float speed;

    [SerializeField] private SpriteRenderer visual;
    private Animator anim;

    private Vector3 initialPos;
    private Vector3 initialScale;

    private void Awake() {
      anim = GetComponentInChildren<Animator>();
      initialPos = transform.position;
      initialScale = visual.transform.localScale;
      life = maxLife;
    }

    private void Update() {
      if (isAttacking) {
        FinishAttack();
        return;
      }

      if (IsInAggroRange() && IsInAttackRange()) {
        Attack();
      }
      else if (IsInAggroRange()) {
        Follow();
      }
      else {
        Return();
      }
    }

    public void TakeDamage(float value) {
      life -= value;
      hpBar.UpdateHealth(life / maxLife);
      if (life <= 0) {
        Destroy(gameObject);
      }
    }

    private void Follow() {
      float step = speed * Time.deltaTime;
      transform.position = Vector2.MoveTowards(transform.position, target.position, step);
      Rotate(target.position);
    }

    private void Attack() {
      isAttacking = true;
      attackStart = Time.time;
      attackDir = target.position - transform.position;
      anim.SetTrigger("Attack");
    }

    private void FinishAttack() {
      if (!ShouldFinishAttack()) return;

      var dir = (transform.position - target.position).normalized;
      var hit = Physics2D.CircleCast(transform.position, attackRange, dir, 0.0f, playerLayer);

      if (hit && IsInFront(hit.transform)) {
        if (hit.collider.TryGetComponent(out Player player)) {
          player.TakeDamage(damage);
        }
      }

      isAttacking = false;
    }

    private bool IsInFront(Transform playerPos) {
      var playerDir = playerPos.position - transform.position;
      return Vector3.Dot(playerDir, attackDir) > 0;
    }

    private void Return() {
      float step = speed * Time.deltaTime;
      transform.position = Vector2.MoveTowards(transform.position, initialPos, step);
      Rotate(initialPos);

      // rotate to initial direction when reached initial position
      if (transform.position == initialPos) {
        visual.transform.localScale = initialScale;
      }
    }

    private void Rotate(Vector3 dest) {
      float x = 1.0f;
      if (transform.position.x > dest.x) {
        x = -1.0f;
      }
      visual.transform.localScale = new Vector3(x, 1.0f, 1.0f);
    }

    private float TargetDistance() {
      return (transform.position - target.position).magnitude;
    }

    private bool IsInAttackRange() {
      return TargetDistance() < attackRange;
    }

    private bool IsInAggroRange() {
      return (initialPos - target.position).magnitude < aggroRange;
    }

    private bool ShouldFinishAttack() {
      return (Time.time - attackStart) > attackSpeed;
    }
  }
}