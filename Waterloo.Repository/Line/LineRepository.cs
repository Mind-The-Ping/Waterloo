using CSharpFunctionalExtensions;
using System.Text.Json;

namespace Waterloo.Repository.Line;
public class LineRepository
{
    public Line[] Lines { get; init; }

    public LineRepository()
    {
        using StreamReader r = new(Path.Combine(AppContext.BaseDirectory, "Data", "line.json"));
        string json = r.ReadToEnd();
        Lines = JsonSerializer.Deserialize<Line[]>(json)
            ?? throw new Exception($" line.json file wasn't found");
    }

    public IEnumerable<Model.Line> GetAll() =>
       Lines.Select(x => new Model.Line(x.Id, x.Name));

    public Model.Line? GetLineById(Guid id) 
    {
        var line = Lines.SingleOrDefault(x => x.Id == id);
        return line == null ? null : new Model.Line(line.Id, line.Name);
    }

    public Result<IEnumerable<Model.Line>> GetLinesById(IEnumerable<Guid> lineIds)
    {
        var lines = new HashSet<Model.Line>();

        foreach (var lineId in lineIds)
        {
            var line = GetLineById(lineId);
            if (line == null)
            {

                return Result.Failure<IEnumerable<Model.Line>>
                    ($"Could not find line with id: {lineId}");
            }

            lines.Add(line);
        }

        return Result.Success<IEnumerable<Model.Line>>(lines);
    }

    public Model.Line? GetLineByName(string name)
    {
        var line = Lines.FirstOrDefault(x => AreStringEqual(x.Name, name));
        return line == null ?
            null : new Model.Line(line.Id, line.Name);
    }

    private static bool AreStringEqual(string a, string b)
    {
        a = a.ToLowerInvariant();
        b = b.ToLowerInvariant();

        a = string.Concat(a.Where(c => !char.IsWhiteSpace(c)));
        b = string.Concat(b.Where(c => !char.IsWhiteSpace(c)));

        return string.Equals(a, b, StringComparison.Ordinal);
    }
}
