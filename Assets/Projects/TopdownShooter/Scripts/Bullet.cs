using UnityEngine;

namespace Sandbox2d.TopdownShooter {
  public class Bullet : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private Rigidbody2D rb;

    public Vector3 Direction { get; set; }

    private void Awake() {
      rb = GetComponent<Rigidbody2D>();
      if (rb == null) Debug.LogWarning("Rigidbody2D wasn't found on Bullet");
    }

    private void FixedUpdate() {
      rb.linearVelocity = Direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision) {
      if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) {
        enemy.TakeDamage(damage);
      }
      Destroy(gameObject);
    }
  }
}