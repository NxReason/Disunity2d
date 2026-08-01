using UnityEngine;


namespace Sandbox2d.Sandbox {
  public class Enemy : MonoBehaviour {
    [SerializeField] private Vector2 bounds;
    [SerializeField] private float speed;
    private int direction = 1;
    void Update() {
      float xDelta = direction * speed * Time.deltaTime;
      transform.position = new Vector3(transform.position.x + xDelta, transform.position.y, 0.0f);

      if (transform.position.x <= bounds.x) {
        direction = 1;
      }
      else if (transform.position.x >= bounds.y) {
        direction = -1;
      }
    }
  }
}