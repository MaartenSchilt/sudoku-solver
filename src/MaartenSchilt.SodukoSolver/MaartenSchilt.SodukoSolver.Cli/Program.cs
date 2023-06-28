using MaartenSchilt.SodukoSolver.Cli;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var sudoku = await SudokuFileReader.ReadAsync("./Puzzles/Sudoku-Hard.txt");

SudokuSolver.Solve(sudoku);

Console.WriteLine(new DefaultSudokuStringFormatter().Format(sudoku));
