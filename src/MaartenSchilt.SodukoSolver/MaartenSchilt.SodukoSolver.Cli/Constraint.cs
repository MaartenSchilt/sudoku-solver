using System.Data;

namespace MaartenSchilt.SodukoSolver.Cli;

public class Constraint
{
    public int Index { get; }
    public Cell[] Cells { get; }

    public int SolvedCount => GetSolvedCells().Count();

    public bool IsSolved => SolvedCount == 9;

    public Constraint(int index, Cell[] cells)
    {
        Index = index;
        Cells = cells;
    }

    public IEnumerable<Cell> GetSolvedCells() =>
        Cells.Where(c => c.IsSolved);

    public IEnumerable<Cell> GetUnsolvedCells() =>
        Cells.Where(c => !c.IsSolved);

    public IEnumerable<int> GetNumbers() =>
        GetSolvedCells().Select(c => c.SolvedNumber);

    public IEnumerable<int> GetMissingNumbers() => 
        Enumerable.Range(1, 9).Except(GetNumbers());    

    public static Constraint ForRow(int row, Sudoku sudoku) => 
        new(row, Enumerable.Range(0, 9).Select(column => sudoku[row, column]).ToArray());

    public static Constraint ForColumn(int column, Sudoku sudoku) =>
        new(column, Enumerable.Range(0, 9).Select(row => sudoku[row, column]).ToArray());

    public static Constraint ForRegion(int region, Sudoku sudoku)
    {
        var cells = new List<Cell>();
        var rowStart = (region / 3) * 3;
        var colStart = (region % 3) * 3;

        for (var row = rowStart; row < rowStart + 3; row++)
            for (var column = colStart; column < colStart + 3; column++)
                cells.Add(sudoku[row, column]);

        return new Constraint(region, cells.ToArray());
    }
}
