using UnityEngine;

namespace Sandbox2d.TinyRPG {
  public class GameManager : MonoBehaviour {
    [SerializeField] HealthBar hpBar;
    [SerializeField] Player player;

    private void Update() {
      float lifePercentage = player.GetLife() / player.GetMaxLife();
      hpBar.UpdateHealth(lifePercentage);
    }
  }
}