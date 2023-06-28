using System.Collections;

namespace MaartenSchilt.SodukoSolver.Cli;

public class NumberRange : IEnumerable<int>
{
    private readonly HashSet<int> values = new();

    public IReadOnlyList<int> Values => 
        values.ToList().AsReadOnly();

    public int Count => 
        Values.Count;

    public NumberRange(HashSet<int> values)
    {
        this.values = values;
    }

    public static NumberRange All => 
        new(Enumerable.Range(1, 9).ToHashSet());

    public void Add(int number) =>
        values.Add(number);

    public void Remove(int number) => 
        values.Remove(number);

    public IEnumerator<int> GetEnumerator() => 
        values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();
}
