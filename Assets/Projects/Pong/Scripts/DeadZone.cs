using UnityEngine;

namespace Sandbox2d.Pong {
  public class DeadZone : MonoBehaviour {
    [SerializeField] private int winner;
    [SerializeField] private GameManager gm;

    void OnCollisionEnter2D(Collision2D collision) {
      gm.FinishRound(winner);
    }
  }
}