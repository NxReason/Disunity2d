namespace Sandbox2d.Twenty2048_CLI {
  public class Renderer {
    private char paddingSymbol = ' ';
    private int cellWidth = 5;

    private char horBorder = '-';
    private char verBorder = '|';

    public void Update(GameState state) {
      Console.Clear();
      Console.SetCursorPosition(0, 0);

      PrintControls();
      PrintScore(state.Score);
      PrintBoard(state.Board);
      PrintInput();
    }

    private void PrintControls() {
      Console.WriteLine("Controls: ");
      Console.WriteLine(new string(paddingSymbol, 3) + "W" + new string(paddingSymbol, 3));
      Console.WriteLine(" A" + new string(paddingSymbol, 3) + "D ");
      Console.WriteLine(new string(paddingSymbol, 3) + "S" + new string(paddingSymbol, 3));
      Console.WriteLine("R - restart");
      Console.WriteLine("T - quit\n");
    }

    private void PrintScore(int score) {
      Console.WriteLine($"Score: {score}\n");
    }

    private void PrintBoard(Board board) {
      Console.WriteLine(new string(horBorder, 25));
      for (int i = 0; i < 4; i++) {
        for (int j = 0; j < 4; j++) {
          Console.Write(verBorder);
          if (board.At(i, j) is not null) {
            string value = board.At(i, j)?.ToString() ?? "";
            string leftPad = new string(paddingSymbol, (cellWidth - value.Length) / 2);
            string rightPad = new string(paddingSymbol, cellWidth - value.Length - leftPad.Length);

            Console.Write(leftPad + value + rightPad);
          }
          else {
            Console.Write(new string(paddingSymbol, 5));
          }
        }
        Console.WriteLine(verBorder);
        // row delimeter
        Console.WriteLine(new string(horBorder, 25));
      }
      Console.WriteLine();
    }

    private void PrintInput() {
      Console.Write("λ > ");
    }
  }
}