
using AutoMapper;

namespace Sarafi.Application.Common.MappingProfile
{
    public abstract class Mappable<From, To> : IMap
    {
        public void CreateMap(Profile profile) => profile.CreateMap(typeof(From), typeof(To));
    }

}
