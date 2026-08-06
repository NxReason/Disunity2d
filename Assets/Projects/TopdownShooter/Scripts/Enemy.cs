using UnityEngine;

namespace Sandbox2d.TopdownShooter {
  public class Enemy : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private float attackCooldown;

    private float lastAttack;

    private void Start() {
      lastAttack = Time.time - attackCooldown;
    }

    private void Update() {
      Rotate();
      if (GetTargetDistance() < attackRange) {
        Attack();
      }
      else {
        Follow();
      }
    }

    private void Rotate() {
      Vector2 direction = (target.position - transform.position).normalized;
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Attack() {
      if (Time.time - lastAttack < attackCooldown) return;

      lastAttack = Time.time;
      var bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
      var dir = target.position - transform.position;
      bullet.GetComponent<Bullet>().Direction = dir.normalized;
    }

    private void Follow() {
      transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private float GetTargetDistance() {
      return (target.position - transform.position).magnitude;
    }

    public void TakeDamage(float damage) {
      health -= damage;
      if (health <= 0) Die();
    }

    private void Die() {
      Destroy(gameObject);
    }
  }
}