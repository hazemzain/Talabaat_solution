using AutoMapper;
using Talabaat.APIs.Dtos;
using Talabaat.Core.Entity;

namespace Talabaat.APIs.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.Brand,o=>o.MapFrom(s=>s.Brand.Name))
                .ForMember(d => d.Catogry, o => o.MapFrom(s => s.Catogry.Name))

                ;
        }
    }
}
