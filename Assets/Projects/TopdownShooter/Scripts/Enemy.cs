using UnityEngine;

namespace Sandbox2d.TopdownShooter {
  public class Enemy : MonoBehaviour {
    [SerializeField] private float health;

    private void Update() {

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