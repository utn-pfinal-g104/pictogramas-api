using PictogramasApi.Model;
using System.Collections.Generic;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IEstadisticaMgmt
    {
        void InsertarEstadistica(Estadistica estadistica);
        List<Estadistica> ObtenerEstadisticasDeUsuario(int usuario);
    }
}
