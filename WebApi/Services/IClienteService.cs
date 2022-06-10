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
        Task SaveAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int clienteid);
        Cliente Get(int clienteid);

    }
}
