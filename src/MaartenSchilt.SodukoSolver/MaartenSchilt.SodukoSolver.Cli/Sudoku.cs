namespace MaartenSchilt.SodukoSolver.Cli;

public class Sudoku
{
    private readonly Cell[][] grid = new Cell[9][];

    private readonly Constraint[] rows = new Constraint[9];
    private readonly Constraint[] columns = new Constraint[9];
    private readonly Constraint[] regions = new Constraint[9];

    public bool IsSolved => rows.All(r => r.IsSolved);

    public Sudoku()
    {
        InitializeGrid();
        InitializeConstraints();
    }

    private void InitializeGrid()
    {
        for (var row = 0; row < 9; row++)
        {
            grid[row] = new Cell[9];

            for (var column = 0; column < 9; column++)
            {
                var region = (row / 3) * 3 + column / 3;

                grid[row][column] = new Cell(row, column, region);
            }
        }
    }

    private void InitializeConstraints()
    {
        for (var i = 0; i < 9; i++)
        {
            rows[i] = Constraint.ForRow(i, this);
            columns[i] = Constraint.ForColumn(i, this);
            regions[i] = Constraint.ForRegion(i, this);
        }
    }

    public Cell this[int row, int column] => grid[row][column];

    public bool CanSetNumber(int row, int column, int number)
    {
        var cell = grid[row][column];
        if (cell.IsSolved)
            return false;

        var relatedCells = GetRelatedCells(cell);
        if (relatedCells.Any(c => c.IsSolved && c.SolvedNumber == number))
            return false;

        return true;
    }

    public void SetNumber(int row, int column, int number)
    {
        var cell = grid[row][column];
        cell.Solve(number);

        UpdateRelatedCells(cell);
    }

    private void UpdateRelatedCells(Cell cell)
    {
        foreach (var related in GetRelatedCells(cell))
        {
            if (!related.IsSolved)
            {
                var isSolveable = related.RemoveCandidate(cell.SolvedNumber);
                if (isSolveable)
                {
                    related.Solve();
                    UpdateRelatedCells(related);
                }                    
            }
        }
    }

    public bool RemoveCandidate(int row, int column, int number)
    {
        var isSolvable = grid[row][column].RemoveCandidate(number);
        if (isSolvable)
            grid[row][column].Solve();

        return isSolvable;
    }

    public IEnumerable<Constraint> GetConstraintsOrderedBySolvedCount()
    {
        return rows.Concat(columns).Concat(regions).OrderByDescending(c => c.SolvedCount);
    }

    private IEnumerable<Cell> GetRelatedCells(Cell cell) => 
        rows[cell.Row].Cells.Union(
            columns[cell.Column].Cells.Union(
                regions[cell.Region].Cells));
}
