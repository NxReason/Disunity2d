namespace Sandbox2d.Twenty2048_CLI {

  public class Board {
    public event EventHandler<int?>? CellsMerged;

    public enum ShiftDirection {
      UP,
      DOWN,
      LEFT,
      RIGHT
    };

    private int rowCount;
    private int colCount;
    private List<int?> cells;

    private bool shiftedOnCurrentMove = false;

    public Board(int rows = 4, int cols = 4) {
      rowCount = rows;
      colCount = cols;
      int size = rows * cols;
      cells = new(size);
      for (int i = 0; i < size; i++) {
        cells.Add(null);
      }

      CreateInitialCells();
    }

    public void Reset() {
      int size = rowCount * colCount;
      cells = new(size);
      for (int i = 0; i < size; i++) {
        cells.Add(null);
      }

      CreateInitialCells();
    }

    public int? At(int row, int col) {
      return cells[row * rowCount + col];
    }

    public void SetCell(int row, int col, int? value) {
      cells[row * 4 + col] = value;
    }

    public void Shift(ShiftDirection dir) {
      shiftedOnCurrentMove = false;
      switch (dir) {
        case ShiftDirection.UP:
          for (int col = 0; col < colCount; col++) {
            for (int row = 1; row < rowCount; row++) {
              if (At(row, col) == null) continue;

              int newRow = row;
              while (newRow > 0 && At(newRow - 1, col) == null) newRow--;

              TryShiftCell(row, col, newRow, col);
              TryMerge(newRow - 1, col, newRow, col);
            }
          }
          break;
        case ShiftDirection.DOWN:
          for (int col = 0; col < colCount; col++) {
            for (int row = rowCount - 2; row >= 0; row--) { // -2 = -1 array pad -1 skip last element
              if (At(row, col) == null) continue;

              int newRow = row;
              while ((newRow < rowCount - 1) && At(newRow + 1, col) == null) newRow++;

              TryShiftCell(row, col, newRow, col);
              TryMerge(newRow + 1, col, newRow, col);
            }
          }
          break;
        case ShiftDirection.LEFT:
          for (int row = 0; row < rowCount; row++) {
            for (int col = 1; col < colCount; col++) {
              if (At(row, col) == null) continue;

              int newCol = col;
              while (newCol > 0 && At(row, newCol - 1) == null) newCol--;

              TryShiftCell(row, col, row, newCol);
              TryMerge(row, newCol - 1, row, newCol);
            }
          }
          break;

        case ShiftDirection.RIGHT:
          for (int row = 0; row < rowCount; row++) {
            for (int col = colCount - 2; col >= 0; col--) { // -2 = -1 array pad -1 skip last element
              if (At(row, col) == null) continue;

              int newCol = col;
              while ((newCol < colCount - 1) && At(row, newCol + 1) == null) newCol++;

              TryShiftCell(row, col, row, newCol);
              TryMerge(row, newCol + 1, row, newCol);
            }
          }
          break;
      }

      if (shiftedOnCurrentMove) CreateCell();
    }


    private void TryMerge(int targetRow, int targetCol, int fromRow, int fromCol) {
      // check bounds
      if (targetRow < 0 || targetRow >= rowCount) return;
      if (targetCol < 0 || targetCol >= colCount) return;

      // merge if same value and clear old cell
      if (At(targetRow, targetCol) == At(fromRow, fromCol)) {
        cells[targetRow * rowCount + targetCol] *= 2;
        cells[fromRow * rowCount + fromCol] = null;

        CellsMerged?.Invoke(this, At(targetRow, targetCol));
        shiftedOnCurrentMove = true;
      }
    }

    private void TryShiftCell(int fromRow, int fromCol, int toRow, int toCol) {
      if (fromRow == toRow && fromCol == toCol) return;

      SetCell(toRow, toCol, At(fromRow, fromCol));
      SetCell(fromRow, fromCol, null);
      shiftedOnCurrentMove = true;
    }

    private void CreateInitialCells(int count = 3) {
      for (int i = 0; i < count; i++) {
        CreateCell();
      }
    }

    private void CreateCell() {
      List<int> candidatePos = cells
        .Select((value, index) => (value, index))
        .Where(x => x.value == null)
        .Select(x => x.index)
        .ToList();
      int candidateIndex = new Random().Next(candidatePos.Count());
      int newCellPos = candidatePos[candidateIndex];
      cells[newCellPos] = 2;
    }
  }
}