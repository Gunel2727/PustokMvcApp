using PustokMvcApp.Data;

namespace PustokMvcApp.Services
{
    public class LayoutService(PustokAppDbContext pustokAppDbContext)
    {
            public Dictionary<string,string> GetSettings()
            {
                return pustokAppDbContext.Settings.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
