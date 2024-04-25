using AgendamentoBackend.DTOs.Cliente;
using AgendamentoBackend.Model;
using AutoMapper;

namespace AgendamentoBackend.Mapping
{
    public class EntitesToDTO : Profile
    {
        public EntitesToDTO()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
        }
    }
}
