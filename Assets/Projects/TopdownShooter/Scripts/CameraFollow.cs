using UnityEngine;

namespace Sandbox2d.TopdownShooter {
  public class CameraFollow : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float idleX;
    [SerializeField] private float idleY;
    [SerializeField] private float followSpeed = 5f;

    private float minX = 0.1f;
    private float minY = 0.1f;
    private bool isFollowing = false;

    private void LateUpdate() {
      if (ShouldFollow()) {
        isFollowing = true;
        Vector3 moveDelta = Vector2.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);
        transform.position = new Vector3(moveDelta.x, moveDelta.y, -10);

        ShouldStop();
      }
    }

    private void ShouldStop() {
      if (Mathf.Abs(target.position.x - transform.position.x) < minX) isFollowing = false;
      if (Mathf.Abs(target.position.y - transform.position.y) < minY) isFollowing = false;
    }


    private bool ShouldFollow() {
      if (isFollowing) return true;
      if (Mathf.Abs(target.position.x - transform.position.x) > idleX) return true;
      if (Mathf.Abs(target.position.y - transform.position.y) > idleY) return true;
      return false;
    }
  }
}