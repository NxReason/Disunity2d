using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Rand = UnityEngine.Random;

namespace Sandbox2d.Twenty48 {
  public enum ShiftDirection {
    UP,
    DOWN,
    LEFT,
    RIGHT
  }

  enum State {
    WAIT,
    SHIFTING
  }

  public class CellManager : MonoBehaviour {
    [SerializeField] private Cell cellPrefab;

    private const int INITIAL_CELL_COUNT = 3;
    private const int MAX_CELL_COUNT = 16;

    private List<int> emptyCells;
    private List<Cell> cells;

    private State boardState;

    void Start() {
      boardState = State.WAIT;

      cells = new List<Cell>();
      for (int i = 0; i < MAX_CELL_COUNT; i++) {
        cells.Add(null);
      }

      emptyCells = new List<int>();
      for (int i = 0; i < MAX_CELL_COUNT; i++) {
        emptyCells.Add(i);
      }

      InitBoard();
    }

    void Update() {
      if (boardState == State.SHIFTING) return;

      if (Keyboard.current.wKey.wasPressedThisFrame) {
        Shift(ShiftDirection.UP);
      }
      if (Keyboard.current.sKey.wasPressedThisFrame) {
        Shift(ShiftDirection.DOWN);
      }
      if (Keyboard.current.aKey.wasPressedThisFrame) {
        Shift(ShiftDirection.LEFT);
      }
      if (Keyboard.current.dKey.wasPressedThisFrame) {
        Shift(ShiftDirection.RIGHT);
      }
    }

    private void Shift(ShiftDirection dir) {
      if (dir == ShiftDirection.UP) {
        for (int row = 0; row < 4; row++) {
          for (int col = 1; col < 4; col++) {
            Cell testCell = cells[row * 4 + col];
            if (testCell is not null) {
              // check empty cell(s)
              int tempCol = col - 1;
              while (tempCol >= 1) {
                if (!emptyCells.Contains(row * 4 + (tempCol - 1))) {
                  break;
                }
                tempCol--;
              }
              if (tempCol != col) {
                testCell.GridY = tempCol;
                cells[row * 4 + tempCol] = testCell;
                cells[row * 4 + col] = null;
                emptyCells.Remove(row * 4 + tempCol);
                emptyCells.Add(row * 4 + col);
              }

              // check pair
            }
          }
        }
      }
    }

    private void InitBoard() {
      for (int i = 0; i < INITIAL_CELL_COUNT; i++) {
        int cellGridPos = Rand.Range(0, emptyCells.Count);
        var newCell = CreateNewCell(cellGridPos);
        cells[cellGridPos] = newCell;
        emptyCells.RemoveAt(cellGridPos);
      }
    }

    private Cell CreateNewCell(int pos) {
      var cell = Instantiate(cellPrefab, transform.position, Quaternion.identity, transform);
      var (x, y) = GetCell2DPos(pos);
      cell.GridX = x;
      cell.GridY = y;
      cell.Value = 2;
      return cell;
    }

    private (int, int) GetCell2DPos(int pos) {
      return (pos / 4, pos % 4);
    }
  }
}