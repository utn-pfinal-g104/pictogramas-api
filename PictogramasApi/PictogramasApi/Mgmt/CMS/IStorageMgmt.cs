using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.CMS
{
    public interface IStorageMgmt
    {
        void Guardar(Stream file, string fileName);
        Stream Obtener(string filename);
        void Borrar(string filename);
        void GuardarImagenCategoria(Stream file, string fileName);
        Stream ObtenerImagenCategoria(string filename);
        void GuardarImagenUsuario(Stream file, string fileName);
        Stream ObtenerImagenUsuario(string filename);
        Task<List<string>> ObtenerTotalImagenesPictogramas();
        Task<List<string>> ObtenerTotalImagenesCategorias();
        void BorrarTodasLasImagenesPictogramas(List<string> archivos);
        void BorrarTodasLasImagenesCategorias(List<string> archivos);
    }
}
