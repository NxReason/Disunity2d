using UnityEngine;

namespace Sandbox2d.Pong {
  public class Ball : MonoBehaviour {
    [SerializeField] private float speed;

    // speed & acceleration
    private float accSpeed;
    private int hitCount = 0;
    private const float wallHitMult = 1.05f;

    // direction
    private int xDir;
    private Vector2 moveDir;
    private const float maxAngle = 70;

    private void Start() {

      xDir = Random.Range(0, 2) * 2 - 1;
      moveDir = new Vector2(1, 0) * xDir;
      accSpeed = speed;
    }

    private void Update() {
      Vector2 moveDelta = moveDir * accSpeed * Time.deltaTime;
      transform.Translate(moveDelta);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
      if (collision.collider.TryGetComponent(out Player player)) {
        HandlePlayerCollision(player, collision.GetContact(0).point);
      }
      else if (collision.collider.TryGetComponent(out DeadZone dz)) {
        Destroy(gameObject);
      }
      else {
        HandleWallCollision();
      }
    }

    private void HandlePlayerCollision(Player player, Vector2 contactPoint) {
      float relativeAngle = (contactPoint.y - player.transform.position.y) / player.Height;
      float angle = relativeAngle * maxAngle;

      xDir *= -1;
      hitCount++;
      moveDir = new Vector2((90 - angle) * xDir, angle).normalized;
      accSpeed = speed + hitCount;
    }

    private void HandleWallCollision() {
      moveDir.y = -moveDir.y;
      accSpeed *= wallHitMult;
    }
  }
}