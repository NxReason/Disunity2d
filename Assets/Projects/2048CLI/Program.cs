using Sandbox2d.Twenty2048_CLI;

App app = new();
app.Run();


public class App {
  private Board board;
  private GameState state;
  private Renderer renderer;

  public App() {
    board = new();
    state = new() { Score = 0, Board = board };
    renderer = new();

    board.CellsMerged += UpdateScore;
  }

  private void UpdateScore(object? _, int? newCellValue) {
    state.Score += newCellValue ?? 0;
  }

  public void Run() {
    string? input = "";
    while (true) {
      switch (input) {
        case "q":
          return;
        case "w":
          board.Shift(Board.ShiftDirection.UP);
          break;
        case "a":
          board.Shift(Board.ShiftDirection.LEFT);
          break;
        case "s":
          board.Shift(Board.ShiftDirection.DOWN);
          break;
        case "d":
          board.Shift(Board.ShiftDirection.RIGHT);
          break;
        case "r":
          board.Reset();
          state.Score = 0;
          break;
      }
      renderer.Update(state);
      input = Console.ReadKey().KeyChar.ToString();
    }
  }
}