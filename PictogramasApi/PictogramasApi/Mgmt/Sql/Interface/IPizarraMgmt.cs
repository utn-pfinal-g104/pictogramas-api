﻿using PictogramasApi.Model;
using System.Collections.Generic;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IPizarraMgmt
    {
        public Pizarra GuardarPizarra(Pizarra pizarra);
        public Pizarra ObtenerPizarra(int pizarraId);
        public List<Pizarra> ObtenerPizarras(int usuarioId);
        public List<CeldaPizarra> ObtenerCeldasDePizarra(int pizarraId, int usuarioId);
        public Pizarra GuardarCeldasDePizarra(Pizarra pizarra);
        public void ActualizarCeldasDePizarra(Pizarra pizarra);
        public void ActualizarPizarra(Pizarra pizarra);
        public void BorrarPizarra(int pizarraId);
    }
}
