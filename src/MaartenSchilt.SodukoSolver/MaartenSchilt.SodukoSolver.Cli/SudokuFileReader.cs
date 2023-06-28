namespace MaartenSchilt.SodukoSolver.Cli;
internal static class SudokuFileReader
{
    public static async Task<Sudoku> ReadAsync(string path)
    {
        var lines = await File.ReadAllLinesAsync(path);

        var sudoku = new Sudoku();

        for (var row = 0; row < lines.Length; row++)
        {
            for (var col = 0; col < lines[row].Length; col++)
            {
                var entry = lines[row][col];
                if (entry == '.')
                    continue;

                var number = entry - '0';
                sudoku.SetNumber(row, col, number);
            }
        }

        return sudoku;
    }

}
