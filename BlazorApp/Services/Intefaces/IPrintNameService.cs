using BlazorApp.Models.Interfaces;

namespace BlazorApp.Services.Intefaces
{
    public interface IPrintNameService
    {
        public void PrintName(IPerson person) { }
    }
}
