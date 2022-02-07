namespace DolBlazor.Utilities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dol_sdk.Controllers;
using Area = Models.Area;

interface IAreaAnalyzer
{
    public IDictionary<(int, int), Area> areas { get; }
    public int progress { get; }
    public string status { get; }
    public Task Analyze();
}

public class AreaAnalyzer : IAreaAnalyzer
{
    private readonly IAreaController _areaController;

    public AreaAnalyzer(IAreaController areaController)
    {
        _areaController = areaController;
        this.areas = new Dictionary<(int, int), Area>();
        this.progress = 0;
        this.status = "Idle";
    }

    public IDictionary<(int, int), Area> areas { get; }
    public int progress { get; internal set; }
    public string status { get; internal set; }


    public async Task Analyze()
    {
        this.areas.Clear();
        this.progress = 1;
        this.status = "Getting data";


        var data = (await _areaController.GetAllAreas()).ToArray();

        this.progress = 5;
        this.status = "Analyzing Areas";

        var count = data.Length;
        for (var i = 0; i < count; i++)
        {
            this.progress = ((i / count) * 95) + 5;
            var analyzedArea = new Area(data[i]);
            areas.Add((analyzedArea.X, analyzedArea.Y), analyzedArea);
        }

        this.progress = 100;
        this.status = "Analysis Complete";
    }
}
