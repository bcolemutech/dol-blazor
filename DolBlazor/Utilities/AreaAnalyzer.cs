namespace DolBlazor.Utilities;

using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

interface IAreaAnalyzer
{
    public IDictionary<(int, int), Area> areas { get; }
    public int progress { get; }
    public string status { get; }
    public Task Analyze();
}

public class AreaAnalyzer : IAreaAnalyzer
{
    public AreaAnalyzer()
    {
        this.areas = new Dictionary<(int, int), Area>();
        this.progress = 0;
        this.status = "Idle";
    }

    public IDictionary<(int, int), Area> areas { get; }
    public int progress { get; internal set; }
    public string status { get; internal set; }


    public Task Analyze()
    {
        this.progress = 1;
        this.status = "Getting data";
        throw new System.NotImplementedException();
    }
}
