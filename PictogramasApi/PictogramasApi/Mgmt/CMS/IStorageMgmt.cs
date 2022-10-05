using System.IO;

namespace PictogramasApi.Mgmt.CMS
{
    public interface IStorageMgmt
    {
        void Guardar(Stream file, string fileName);
        Stream Obtener(string filename);
        void Borrar(string filename);
        void GuardarImagenCategoria(Stream file, string fileName);
        Stream ObtenerImagenCategoria(string filename);
    }
}
