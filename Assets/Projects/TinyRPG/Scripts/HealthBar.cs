using UnityEngine;
using UnityEngine.UI;

namespace Sandbox2d.TinyRPG {
  public class HealthBar : MonoBehaviour {
    [SerializeField] private Image health;

    private float maxWidth;

    private void Awake() {
      maxWidth = health.rectTransform.rect.width;
      Debug.Log(maxWidth);
    }

    // [0, 1]
    public void UpdateHealth(float percentage) {
      health.fillAmount = percentage;
    }
  }
}