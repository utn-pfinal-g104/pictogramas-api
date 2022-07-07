﻿using PictogramasApi.Mgmt.CMS;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using PictogramasApi.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Jobs
{
    public class ActualizacionStorageJob : IJob
    {
        private readonly IPictogramaMgmt _pictogramaMgmt;
        private readonly IStorageMgmt _storageMgmt;
        private readonly ICategoriaMgmt _categoriaMgmt;
        private readonly IPalabraClaveMgmt _palabraClaveMgmt;
        private readonly ITagMgmt _tagMgmt;
        private readonly IPictogramaPorTagMgmt _pictogramaPorTagMgmt;
        private readonly IPictogramaPorCategoriaMgmt _pictogramaPorCategoriaMgmt;

        private readonly ArasaacService _arasaacService;

        public ActualizacionStorageJob(IPictogramaMgmt pictogramaMgmt, ITagMgmt tagMgmt,
            IPalabraClaveMgmt palabraClaveMgmt, IPictogramaPorTagMgmt pictogramaPorTagMgmt,
            IPictogramaPorCategoriaMgmt pictogramaPorCategoriaMgmt, ArasaacService arasaacService,
            IStorageMgmt storageMgmt, ICategoriaMgmt categoriaMgmt)
        {
            _pictogramaMgmt = pictogramaMgmt;
            _storageMgmt = storageMgmt;
            _categoriaMgmt = categoriaMgmt;
            _palabraClaveMgmt = palabraClaveMgmt;
            _tagMgmt = tagMgmt;
            _pictogramaPorTagMgmt = pictogramaPorTagMgmt;
            _pictogramaPorCategoriaMgmt = pictogramaPorCategoriaMgmt;

            _arasaacService = arasaacService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
            });
        }

        internal async void ActualizarPictogramas()
        {
            // Dejo esto hasta que el metodo este finalizado
            throw new NotImplementedException();

            List<Model.Responses.Pictograma> pictogramasArasaac = await _arasaacService.ObtenerPictogramasDeArasaac();

            List<Pictograma> pictogramas = MapearPictogramas(pictogramasArasaac);
            List<Categoria> categorias = ObtenerCategorias(pictogramasArasaac);
            List<Tag> tags = ObtenerTags(pictogramasArasaac);
            List<PalabraClave> palabrasClaves = ObtenerPalabrasClaves(pictogramasArasaac);

            await _categoriaMgmt.AgregarCategorias(categorias);
            await _tagMgmt.AgregarTags(tags);
            await _palabraClaveMgmt.AgregarPalabrasClaves(palabrasClaves);
            await _pictogramaMgmt.AgregarPictogramas(pictogramas);

            List<PictogramaPorCategoria> picsXcats = ObtenerPictogramasPorCategorias(categorias, pictogramas, pictogramasArasaac);
            List<PictogramaPorTag> picsXtags = ObtenerPictogramasPorTags(tags, pictogramas, pictogramasArasaac);

            await _pictogramaPorCategoriaMgmt.AgregarRelaciones(picsXcats);
            await _pictogramaPorTagMgmt.AgregarRelaciones(picsXtags);

            List<Stream> pictogramasAsStreams = new List<Stream>();
            foreach (var pictograma in pictogramasArasaac)
            {
                var pictogramaAsStream = await _arasaacService.ObtenerPictogramaDeArasaac(pictograma._id);
                _storageMgmt.Guardar(pictogramaAsStream, $"Arasaac-{pictograma._id}"); // Como lo guardamos?
                //pictogramasAsStreams.Add(pictogramaAsStream);
            }
        }

        private static List<PictogramaPorCategoria> ObtenerPictogramasPorCategorias(List<Categoria> categorias, List<Pictograma> pictogramas, List<Model.Responses.Pictograma> pictogramasArasaac)
        {
            List<PictogramaPorCategoria> picsXcats = new List<PictogramaPorCategoria>();
            foreach (var pictograma in pictogramasArasaac)
            {
                foreach(var categoria in pictograma.categories)
                {
                    picsXcats.Add(new PictogramaPorCategoria
                    {
                        IdCategoria = categorias.FirstOrDefault(c => c.Nombre == categoria).Id,
                        IdPictograma = pictogramas.FirstOrDefault(p => p.IdArasaac == pictograma._id).Id
                    });
                }
            }
            return picsXcats;
        }

        private static List<PictogramaPorTag> ObtenerPictogramasPorTags(List<Tag> tags, List<Pictograma> pictogramas, List<Model.Responses.Pictograma> pictogramasArasaac)
        {
            List<PictogramaPorTag> picsXtags = new List<PictogramaPorTag>();
            foreach (var pictograma in pictogramasArasaac)
            {
                foreach (var tag in pictograma.tags)
                {
                    picsXtags.Add(new PictogramaPorTag
                    {
                        IdTag = tags.FirstOrDefault(t => t.Nombre == tag).Id,
                        IdPictograma = pictogramas.FirstOrDefault(p => p.IdArasaac == pictograma._id).Id
                    });
                }
            }
            return picsXtags;
        }

        private List<Pictograma> MapearPictogramas(List<Model.Responses.Pictograma> pictogramasArasaac)
        {
            List<Pictograma> pictogramas = new List<Pictograma>();

            foreach(var pictograma in pictogramasArasaac){
                pictogramas.Add(new Pictograma
                {
                    Aac = pictograma.aac,
                    AacColor = pictograma.aacColor,
                    Hair = pictograma.hair,
                    IdArasaac = pictograma._id,
                    Schematic = pictograma.schematic,
                    Sex = pictograma.sex,
                    Skin = pictograma.skin,
                    Violence = pictograma.violence
                });
            }
            return pictogramas;
        }

        private List<PalabraClave> ObtenerPalabrasClaves(List<Model.Responses.Pictograma> pictogramas)
        {
            List<PalabraClave> palabrasClaves = new List<PalabraClave>();
            foreach (var pictograma in pictogramas)
            {
                foreach (var palabraClave in pictograma.keywords)
                    palabrasClaves.Add(new PalabraClave
                    { 
                        HasLocution = palabraClave.hasLocution,
                        IdPictograma = pictograma._id,
                        Keyword = palabraClave.keyword,
                        Meaning = palabraClave.meaning,
                        Plural = palabraClave.plural,
                        Tipo = palabraClave.type
                    });
            }
            return palabrasClaves;
        }

        private List<Tag> ObtenerTags(List<Model.Responses.Pictograma> pictogramas)
        {
            List<Tag> tags = new List<Tag>();
            foreach (var pictograma in pictogramas)
            {
                foreach (var tag in pictograma.tags)
                {
                    if(!tags.Any(t => t.Nombre == tag))
                        tags.Add(new Tag { Nombre = tag });
                }
            }
            return tags;
        }

        private static List<Categoria> ObtenerCategorias(List<Model.Responses.Pictograma> pictogramas)
        {
            List<Categoria> categorias = new List<Categoria>();
            foreach (var pictograma in pictogramas)
            {
                foreach (var categoria in pictograma.categories)
                {
                    if(!categorias.Any(c => categoria == c.Nombre))
                        categorias.Add(new Categoria { Nombre = categoria });
                }
            }
            return categorias;
        }
    }
}
