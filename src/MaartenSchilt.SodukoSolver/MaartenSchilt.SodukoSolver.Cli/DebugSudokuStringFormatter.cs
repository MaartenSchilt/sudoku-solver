using System.Text;

namespace MaartenSchilt.SodukoSolver.Cli;

internal class DebugSudokuStringFormatter : ISudokuStringFormatter
{
    public string Format(Sudoku sudoku)
    {
        var builder = new StringBuilder();

        builder.AppendLine("┌─────────┬─────────┬─────────┬─────────┬─────────┬─────────┬─────────┬─────────┬─────────┐");

        for (var row = 0; row < 9; row++)
        {
            builder.Append("│");

            for (var col = 0; col < 9; col++)
            {
                var cell = sudoku[row, col];

                if (cell.IsSolved)
                {
                    builder.Append($"****{cell.SolvedNumber}****");
                }
                else
                {
                    for (var n = 1; n <= 9; n++)
                    {
                        if (sudoku[row, col].Candidates.Contains(n))
                            builder.Append($"{n}");
                        else
                            builder.Append(" ");
                    }
                }

                

                builder.Append("│");
            }
            builder.AppendLine();

            if (row == 2 || row == 5)
                builder.AppendLine("├─────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┤");

        }
        builder.AppendLine("└─────────┴─────────┴─────────┴─────────┴─────────┴─────────┴─────────┴─────────┴─────────┘");

        return builder.ToString();
    }
}
