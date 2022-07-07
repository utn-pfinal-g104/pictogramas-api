using AutoMapper;

namespace PictogramasApi.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Actualmente sin uso
            // Add as many of these lines as you need to map your objects
            CreateMap<Model.Responses.Pictograma, Model.Pictograma>();
        }
    }
}
