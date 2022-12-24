using Microsoft.AspNetCore.Authorization;
using Sarafi.Application.Interfaces.Services;
using System.Reflection;

namespace Sarafi.API.Services
{
    public class ControllerActions : IControllerActions
    {
        public List<string> GetAssemblyActionsName()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(AuthorizeAttribute), false).Length > 0)
                .Select(m => m.Name).ToList();
        }
    }
}
