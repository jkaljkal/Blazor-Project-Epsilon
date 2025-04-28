using BlazorApp.Models.Interfaces;
using BlazorApp.Services.Intefaces;

namespace BlazorApp.Services.Implementations
{
    public class PrintNameService : IPrintNameService
    {
        public ILogger _logger;

        public PrintNameService(ILogger<IPerson> logger)
        {
            _logger = logger;
        }

        public void PrintName(IPerson person)
        {
            if (person != null)
                _logger.LogInformation("\n\n\n>>>>>>>>>>>>>>>>>>>>>>> Print Name:" + person.Name + "<<<<<<<<<<<<<<\n\n");
        }
    }
}
