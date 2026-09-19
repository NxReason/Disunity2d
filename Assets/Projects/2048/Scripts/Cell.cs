using TMPro;
using UnityEngine;

namespace Sandbox2d.Twenty48 {
  public class Cell : MonoBehaviour {
    [SerializeField] private TMP_Text numValue;

    private static readonly float BOARD_SIZE = 8f;
    private static readonly float CELL_SIZE = 1.5f;
    private static readonly float GAP_SIZE = 0.4f;

    private int val;
    public int Value {
      get { return val; }
      set {
        val = value;
        numValue.text = $"{value}";
      }
    }

    // position in grid (4x4)
    private int gridX;
    public int GridX {
      get {
        return gridX;
      }
      set {
        gridX = value;
        transform.localPosition = new Vector3(GetXScreenPos(value), transform.position.y, 0.0f);
      }
    }

    private int gridY;
    public int GridY {
      get {
        return gridY;
      }
      set {
        gridY = value;
        transform.localPosition = new Vector3(transform.position.x, GetYScreenPos(value), 0.0f);
      }
    }


    // render positions
    private float GetXScreenPos(int gridPos) {
      return GetMargin(gridPos) + CELL_SIZE / 2;
    }
    private float GetYScreenPos(int gridPos) {
      return -(GetMargin(gridPos) + CELL_SIZE / 2);
    }
    private float GetMargin(int gridPos) {
      // initial grid offset + border gap
      float result = -BOARD_SIZE / 2 + GAP_SIZE;
      // full cells padding
      result += (CELL_SIZE + GAP_SIZE) * gridPos;
      return result;
    }
  }
}