using UnityEngine;

namespace Sandbox2d.Pong {
  public class GameManager : MonoBehaviour {
    private enum State {
      Waiting,
      Playing
    }

    [SerializeField] private Ball ball;
    [SerializeField] private float waitDuration;
    [SerializeField] private TMPro.TMP_Text p1Visual;
    [SerializeField] private TMPro.TMP_Text p2Visual;

    private int[] counts;

    private State state;
    private float waitStart;


    private void Start() {
      counts = new int[2] { 0, 0 };
      state = State.Waiting;
      waitStart = Time.time;
    }

    private void Update() {
      if (ShouldStartRound()) {
        NextRound();
      }
    }

    private bool ShouldStartRound() {
      return state == State.Waiting && (Time.time - waitStart > waitDuration);
    }

    public void FinishRound(int winner) {
      if (winner < 0 || winner > counts.Length) {
        Debug.LogWarning($"Invalid round winner value: ${winner}. Must be 0 or 1");
        return;
      }

      counts[winner]++;
      UpdateVisuals();

      waitStart = Time.time;
      state = State.Waiting;

      SetVisualsVisible(true);
    }

    private void NextRound() {
      state = State.Playing;
      Instantiate(ball, Vector3.zero, Quaternion.identity);

      SetVisualsVisible(false);
    }

    private void UpdateVisuals() {
      p1Visual.text = counts[0].ToString();
      p2Visual.text = counts[1].ToString();
    }

    private void SetVisualsVisible(bool isOn) {
      p1Visual.gameObject.SetActive(isOn);
      p2Visual.gameObject.SetActive(isOn);
    }
  }
}