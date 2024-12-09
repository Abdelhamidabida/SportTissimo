using AutoMapper;
using SportissimoProject.Models;
using SportissimoProject.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateClientDto, Client>();
        // Ajoute d'autres mappages si nécessaire
    }
}
