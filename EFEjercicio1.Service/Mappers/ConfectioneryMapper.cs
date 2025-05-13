using EFEjercicio1.Service.DTOs.Confectionery;
using EFEjercicio1Entities;

namespace EFEjercicio1.Service.Mappers
{
    public static class ConfectioneryMapper
    {
        public static ConfectioneryDto ToDto(Confectionery confectionery) => new()
        {
            Id = confectionery.Id,
            Name = confectionery.Name
        };

        public static Confectionery ToEntity(ConfectioneryCreateDto confectioneryDto) => new()
        {
            Name = confectioneryDto.Name
        };

        public static Confectionery ToEntity(ConfectioneryUpdateDto confectioneryDto) => new()
        {
            Id = confectioneryDto.Id,
            Name = confectioneryDto.Name
        };
    }
}
