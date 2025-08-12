using System.Text.Json;

namespace Waterloo.Repository.Line;
public class LineRepository
{
    public Line[] Lines { get; init; }

    public LineRepository()
    {
        using StreamReader r = new("line.json");
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

}
