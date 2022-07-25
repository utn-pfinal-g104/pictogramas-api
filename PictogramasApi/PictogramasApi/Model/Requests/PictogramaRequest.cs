namespace PictogramasApi.Model.Requests
{
    public class PictogramaRequest
    {
        public string Filtros { get; set; }
        public byte[] File { get; set; }
        public string CategoriasFiltradas {get; set;}
    }

}
