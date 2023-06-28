namespace MaartenSchilt.SodukoSolver.Cli;

public static class SudokuSolver
{
    public static void Solve(Sudoku sudoku)
    {
        while (!sudoku.IsSolved)
        {
            var constraints = sudoku.GetConstraintsOrderedBySolvedCount();
            var solvedCount = 0;

            foreach (var constraint in constraints)
            {
                foreach (var missingNumber in constraint.GetMissingNumbers())
                {
                    var solveables = new List<Cell>();

                    foreach (var cell in constraint.GetUnsolvedCells())
                    {
                        if (sudoku.CanSetNumber(cell.Row, cell.Column, missingNumber))
                        {
                            solveables.Add(cell);
                        }
                        else
                        {
                            if (sudoku.RemoveCandidate(cell.Row, cell.Column, missingNumber))
                                solvedCount++;
                        }
                    }

                    if (solveables.Count == 1)
                    {
                        solvedCount++;

                        sudoku.SetNumber(solveables.First().Row, solveables.First().Column, missingNumber);
                    }
                }


            }

            if (solvedCount == 0)
                return;
        }
    }
}
