using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAsync();
        Task<Cliente> GetAsync(int clienteid);
        Task<ResponseWebApi> SaveAsync(ClienteInsertDTO cliente);
        Task<ResponseWebApi> UpdateAsync(ClienteUpdateDTO cliente);
        Task<ResponseWebApi> DeleteAsync(int clienteid);

    }
}
