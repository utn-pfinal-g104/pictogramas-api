using PictogramasApi.Mgmt.CMS;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using PictogramasApi.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            // Se comenta y descomenta lo que se desea utilizar
            // Dejo esto hasta que el metodo este finalizado
            //throw new NotImplementedException();

            List<Model.Responses.Pictograma> pictogramasArasaac = await _arasaacService.ObtenerPictogramasDeArasaac();

            List<Pictograma> pictogramas = MapearPictogramas(pictogramasArasaac);
            List<Categoria> categorias = ObtenerCategorias(pictogramasArasaac);
            List<Tag> tags = ObtenerTags(pictogramasArasaac);
            List<PalabraClave> palabrasClaves = ObtenerPalabrasClaves(pictogramasArasaac);

            // INSERT PICTOGRAMAS
            await _pictogramaMgmt.AgregarPictogramas(pictogramas);

            var pictogramasNuestros = await _pictogramaMgmt.ObtenerPictogramas(null);
            foreach (var keyword in palabrasClaves)
            {
                try
                {
                    var pic = pictogramasNuestros.FirstOrDefault(p => p.IdArasaac == keyword.IdPictograma);
                    keyword.IdPictograma = pic != null ? pic.Id : 0;
                }
                catch(Exception ex)
                {
                     
                }
            }

            // INSERT KEYWORDS
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
                    //TODO: Aca podriamos tirar grupos de tasks que corran en simultaneo para acelerar el proceso
                    var pictogramaAsStream = await _arasaacService.ObtenerPictogramaDeArasaac(pictograma._id);
                    _storageMgmt.Guardar(pictogramaAsStream, $"{pictogramasNuestros.FirstOrDefault(p => p.IdArasaac == pictograma._id).Id}"); // TODO: Con que nombre lo guardamos? por ahora lo estamos guardando con el id
                    Console.WriteLine($"Se inserto pictograma: {pictograma._id}");
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            // Generar imagenes de categorias
            GenerarImagenesDeCategorias(categoriasNuestras, pictogramasNuestros, picsXcats);
        }

        private void GenerarImagenesDeCategorias(List<Categoria> categoriasNuestras, List<Pictograma> pictogramasNuestros, List<PictogramaPorCategoria> picsXcats)
        {
            foreach (var categoria in categoriasNuestras)
            {
                try
                {
                    var pictogramasFiltrados = pictogramasNuestros.Where(p => p.Sex == false && p.Violence == false);
                    var pictogramasDeCategoria = picsXcats.Where(p => p.IdCategoria == categoria.Id && pictogramasFiltrados.Any(pf => pf.Id == p.IdPictograma)).ToList();
                    var pictograma1 = pictogramasDeCategoria[0] != null ? pictogramasDeCategoria[0].IdPictograma : 0;
                    var pictograma2 = pictogramasDeCategoria[1] != null ? pictogramasDeCategoria[1].IdPictograma : 0;
                    var pictograma3 = pictogramasDeCategoria[2] != null ? pictogramasDeCategoria[2].IdPictograma : 0;
                    var pictograma4 = pictogramasDeCategoria[3] != null ? pictogramasDeCategoria[3].IdPictograma : 0;
                    var imagen1 = _storageMgmt.Obtener(pictograma1.ToString());
                    var imagen2 = _storageMgmt.Obtener(pictograma2.ToString());
                    var imagen3 = _storageMgmt.Obtener(pictograma3.ToString());
                    var imagen4 = _storageMgmt.Obtener(pictograma4.ToString());
                    using (Image image1 = Image.FromStream(imagen1))
                    {
                        using (Image image2 = Image.FromStream(imagen2))
                        {
                            using (Image image3 = Image.FromStream(imagen3))
                            {
                                using (Image image4 = Image.FromStream(imagen4))
                                {
                                    using (Bitmap bmp = new Bitmap(
                                        image1.Width + image2.Width >= image3.Width + image4.Width ?
                                            image1.Width + image2.Width + 80 : image3.Width + image4.Width + 80,
                                        image1.Height + image3.Height >= image2.Height + image4.Height ?
                                            image1.Height + image3.Height + 80 : image2.Height + image4.Height + 80))
                                    {
                                        using (Graphics g = Graphics.FromImage(bmp))
                                        {
                                            using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255)))
                                            {
                                                g.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
                                            }
                                            g.DrawImage(image1, 5, 5, image1.Width + 10, image1.Height + 10);
                                            g.DrawImage(image2, image1.Width + 45, 5, image2.Width + 10, image2.Height + 10);
                                            g.DrawImage(image3, 5, image1.Height + 15, image3.Width + 30, image3.Height + 30);
                                            g.DrawImage(image4, image3.Width + 45, image2.Height + 15, image4.Width + 30, image4.Height + 30);

                                            var stream = new System.IO.MemoryStream();
                                            bmp.Save(stream, ImageFormat.Jpeg);
                                            stream.Position = 0;
                                            _storageMgmt.GuardarImagenCategoria(stream, categoria.Id.ToString());
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                { 
                }
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
                        try
                        {
                            picsXcats.Add(new PictogramaPorCategoria
                            {
                                //TODO: Revisar - debe utilizarse el nombre en ingles para comparar
                                IdCategoria = categorias.FirstOrDefault(c => c.NombreOriginal == categoria).Id,
                                IdPictograma = pictogramas.FirstOrDefault(p => p.IdArasaac == pictograma._id).Id
                            });
                        }
                        catch(Exception ex)
                        {

                        }
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
