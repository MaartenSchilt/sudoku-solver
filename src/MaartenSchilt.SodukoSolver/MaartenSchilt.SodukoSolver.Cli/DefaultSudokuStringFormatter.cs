using System.Text;

namespace MaartenSchilt.SodukoSolver.Cli;
internal class DefaultSudokuStringFormatter : ISudokuStringFormatter
{
    public string Format(Sudoku sudoku)
    {
        var builder = new StringBuilder();

        builder.AppendLine("┌───────┬───────┬───────┐");

        for (var row = 0; row < 9; row++)
        {
            builder.Append("│ ");

            for (var col = 0; col < 9; col++)
            {
                if (sudoku[row, col].IsSolved)
                    builder.Append(sudoku[row, col].SolvedNumber + " ");
                else
                    builder.Append("  ");

                if (col % 3 == 2)
                    builder.Append("│ ");
            }
            builder.AppendLine();

            if (row == 2 || row == 5)
                builder.AppendLine("├───────┼───────┼───────┤");

        }

        builder.AppendLine("└───────┴───────┴───────┘");

        return builder.ToString();
    }
}
