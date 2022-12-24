using AutoMapper;
using Sarafi.Domain.Common;
using System.Reflection;

namespace Sarafi.Application.Common.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            new List<Type[]> {
                Assembly.GetExecutingAssembly().GetExportedTypes(),
                typeof(AuditableEntity).Assembly.GetExportedTypes()
            }
            .SelectMany(a => a.ToList())
            .Where(t => t.IsAssignableTo(typeof(IMap)) && t.IsClass && !t.IsGenericType)
            .ToList().ForEach(t =>
            {
                var instance = Activator.CreateInstance(t);
                var method = t.GetMethod("CreateMap");
                method?.Invoke(instance, new object[] { this });
            });
        }
    }
}
