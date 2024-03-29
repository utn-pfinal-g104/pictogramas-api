﻿using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IPictogramaMgmt
    {
        Task AgregarPictogramas(List<Pictograma> pictogramas);
        Task<List<Pictograma>> ObtenerPictogramas(int? usuarioId);
        Task<List<Pictograma>> ObtenerPictogramasPorIds(List<int> pictogramasIds);
        Task<int> ObtenerTotalPictogramas();
        Task<List<Pictograma>> ObtenerInformacionPictogramas(int? usuarioId);
        Task<Pictograma> AgregarPictograma(Pictograma pictograma);
        Task<int> ObtenerTotalPictogramas(int usuarioId);
        Task EliminarPictogramas();
        Task EliminarPictogramaDeUsuario(int pictogramaDeUsuarioId);
        Task AgregarFavorito(int idUsuario, int idPictograma);
        Task EliminarFavorito(int idUsuario, int idPictograma);
        Pictograma ObtenerPictogramaPropio(int usuario, string identificador);
        Task<List<FavoritoPorUsuario>> ObtenerFavoritos(int idUsuario);
    }
}
