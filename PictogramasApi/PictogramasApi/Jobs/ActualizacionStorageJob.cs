using PictogramasApi.Mgmt.CMS;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using PictogramasApi.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PictogramasApi.Jobs
{
    public class ActualizacionStorageJob : IJob
    {
        private readonly IPictogramaMgmt _pictogramaMgmt;
        private readonly IStorageMgmt _storageMgmt;
        private readonly ICategoriaMgmt _categoriaMgmt;
        private readonly IPalabraClaveMgmt _palabraClaveMgmt;
        private readonly IPictogramaPorCategoriaMgmt _pictogramaPorCategoriaMgmt;

        private readonly ArasaacService _arasaacService;

        public ActualizacionStorageJob(IPictogramaMgmt pictogramaMgmt, 
            IPalabraClaveMgmt palabraClaveMgmt,
            IPictogramaPorCategoriaMgmt pictogramaPorCategoriaMgmt, ArasaacService arasaacService,
            IStorageMgmt storageMgmt, ICategoriaMgmt categoriaMgmt)
        {
            _pictogramaMgmt = pictogramaMgmt;
            _storageMgmt = storageMgmt;
            _categoriaMgmt = categoriaMgmt;
            _palabraClaveMgmt = palabraClaveMgmt;
            _pictogramaPorCategoriaMgmt = pictogramaPorCategoriaMgmt;

            _arasaacService = arasaacService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
            });
        }

        internal async Task ActualizarPictogramas()
        {
            // Dejo esto hasta que el metodo este finalizado
            throw new NotImplementedException();

            List<Model.Responses.Pictograma> pictogramasArasaac = await _arasaacService.ObtenerPictogramasDeArasaac();

            List<Pictograma> pictogramas = MapearPictogramas(pictogramasArasaac);
            List<Categoria> categorias = ObtenerCategorias(pictogramasArasaac);
            List<Tag> tags = ObtenerTags(pictogramasArasaac);
            List<PalabraClave> palabrasClaves = ObtenerPalabrasClaves(pictogramasArasaac);

            //// ELIMINAR REGISTROS ACTUALES
            await _categoriaMgmt.EliminarCategorias();
            await _pictogramaMgmt.EliminarPictogramas();
            await _pictogramaPorCategoriaMgmt.EliminarRelaciones();
            await _palabraClaveMgmt.EliminarPalabrasClaves();

            // TODO: Las categorias se deben insertar manualmente con ids definidos
            //// INSERT CATEGORIAS
            await _categoriaMgmt.AgregarCategorias(categorias);

            //// INSERT PICTOGRAMAS
            await _pictogramaMgmt.AgregarPictogramas(pictogramas);

            var pictogramasNuestros = await _pictogramaMgmt.ObtenerPictogramas(null);
            foreach (var keyword in palabrasClaves)
                keyword.IdPictograma = pictogramasNuestros.FirstOrDefault(p => p.IdArasaac == keyword.IdPictograma).Id;

            //// INSERT KEYWORDS
            await _palabraClaveMgmt.AgregarPalabrasClaves(palabrasClaves);

            // TODO: No se deben unificar mas las categorias con los tags, y solo se debe tener asociado al pictograma las categorias y no los tags
            List<Categoria> categoriasNuestras = await _categoriaMgmt.ObtenerCategorias();
            // Tambien agrega tags como categorias
            List<PictogramaPorCategoria> picsXcats = ObtenerPictogramasPorCategorias(categoriasNuestras, pictogramasNuestros, pictogramasArasaac);
            

            //// INSERT PICTOGRAMAS X CATEGORIAS
            await _pictogramaPorCategoriaMgmt.AgregarRelaciones(picsXcats);

            //Guardar imagenes en Storage
            List<Stream> pictogramasAsStreams = new List<Stream>();
            try
            {
                foreach (var pictograma in pictogramasArasaac)
                {
                    var pictogramaAsStream = await _arasaacService.ObtenerPictogramaDeArasaac(pictograma._id);
                    _storageMgmt.Guardar(pictogramaAsStream, $"{pictogramasNuestros.FirstOrDefault(p => p.IdArasaac == pictograma._id).Id}"); // TODO: Con que nombre lo guardamos? por ahora lo estamos guardando con el id
                    Console.WriteLine($"Se inserto pictograma: {pictograma._id}");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Tambien agrega tags como categorias
        private static List<PictogramaPorCategoria> ObtenerPictogramasPorCategorias(List<Categoria> categorias, List<Pictograma> pictogramas, List<Model.Responses.Pictograma> pictogramasArasaac)
        {
            try
            {
                List<PictogramaPorCategoria> picsXcats = new List<PictogramaPorCategoria>();
                foreach (var pictograma in pictogramasArasaac)
                {
                    foreach (var categoria in pictograma.categories)
                    {
                        picsXcats.Add(new PictogramaPorCategoria
                        {
                            //TODO: Revisar - debe utilizarse el nombre en ingles para comparar
                            IdCategoria = categorias.FirstOrDefault(c => c.NombreOriginal == categoria).Id,
                            IdPictograma = pictogramas.FirstOrDefault(p => p.IdArasaac == pictograma._id).Id
                        });
                    }
                    try
                    {
                        // TODO: No se deben unificar mas las categorias con los tags, y solo se debe tener asociado al pictograma las categorias y no los tags
                        //if (pictograma.tags != null)
                        //{
                        //    foreach (var tag in pictograma.tags)
                        //    {
                        //        if (tag != null && !pictograma.categories.Any(c => c == tag))
                        //            try
                        //            {
                        //                picsXcats.Add(new PictogramaPorCategoria
                        //                {
                        //                    IdCategoria = categorias.FirstOrDefault(c => c.Nombre == tag).Id,
                        //                    IdPictograma = pictogramas.FirstOrDefault(p => p.IdArasaac == pictograma._id).Id
                        //                });
                        //            }
                        //            catch (Exception ex)
                        //            {

                        //            }
                        //    }
                        //}
                    }
                    catch (Exception)
                    {
                    }
                }
                return picsXcats;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                        Keyword = palabraClave.keyword != null ? String.Join("", palabraClave.keyword.Split(',', '\'', '"','@')) : "",
                        Meaning = palabraClave.meaning != null ? String.Join("", Regex.Replace(palabraClave.meaning, @"\t|\n|\r", "").Split(',', '\'', '"', '@')) : "",
                        Plural = palabraClave.plural != null ? String.Join("", palabraClave.plural.Split(',', '\'', '"', '@')) : "",
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
