namespace MaartenSchilt.SodukoSolver.Cli;

public class Cell
{
    private int? solvedNumber = null;

    public int Row { get; }
    public int Column { get; }
    public int Region { get; }
    public NumberRange Candidates { get; }    

    public bool IsSolved =>
        solvedNumber.HasValue;

    public int SolvedNumber =>
        solvedNumber ?? throw new InvalidOperationException("Cell is not solved.");

    public Cell(int row, int column, int region)
    {
        Row = row;
        Column = column;
        Region = region;
        Candidates = NumberRange.All;
    }

    public bool RemoveCandidate(int number)
    {
        if (IsSolved)
            throw new InvalidOperationException("Cell is already solved.");

        Candidates.Remove(number);
        return Candidates.Count == 1;
    }

    public void Solve(int number)
    {
        if (IsSolved)
            throw new InvalidOperationException("Cell is already solved.");

        solvedNumber = number;
    }

    public void Solve()
    {
        if (IsSolved)
            throw new InvalidOperationException("Cell is already solved.");
        if (Candidates.Count != 1)
            throw new InvalidOperationException("Cell is not yet solveable.");

        solvedNumber = Candidates.First();
    }
}
