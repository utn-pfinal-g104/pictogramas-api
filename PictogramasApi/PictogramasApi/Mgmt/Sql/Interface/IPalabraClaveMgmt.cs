
using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IPalabraClaveMgmt
    {
        Task AgregarPalabrasClaves(List<PalabraClave> palabrasClaves);
        Task<List<PalabraClave>> ObtenerKeywords();
        Task<PalabraClave> ObtenerKeyword(string palabra);
        Task AgregarPalabraClave(Pictograma pictograma, string keyword);
        Task EliminarPalabrasClaves();
        void EliminarPalabraClave(int palabraId);
    }
}
