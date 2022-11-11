using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ActualizacionStorageJob> _logger;

        public ActualizacionStorageJob(IPictogramaMgmt pictogramaMgmt,
            IPalabraClaveMgmt palabraClaveMgmt,
            IPictogramaPorCategoriaMgmt pictogramaPorCategoriaMgmt, ArasaacService arasaacService,
            IStorageMgmt storageMgmt, ICategoriaMgmt categoriaMgmt, ILogger<ActualizacionStorageJob> logger)
        {
            _pictogramaMgmt = pictogramaMgmt;
            _storageMgmt = storageMgmt;
            _categoriaMgmt = categoriaMgmt;
            _palabraClaveMgmt = palabraClaveMgmt;
            _pictogramaPorCategoriaMgmt = pictogramaPorCategoriaMgmt;

            _arasaacService = arasaacService;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
            });
        }

        internal async Task ActualizarPictogramas()
        {
            try
            {
                // Se comenta y descomenta lo que se desea utilizar
                // Dejo esto hasta que el metodo este finalizado
                //throw new NotImplementedException();

                _logger.LogInformation($"Se inicia la actualizacion de pictogramas - {DateTime.Now}");
                List<Model.Responses.Pictograma> pictogramasArasaac = await _arasaacService.ObtenerPictogramasDeArasaac();
                _logger.LogInformation($"Se obtuvieron los pictogramas de arasaac, total pictogramas: {pictogramasArasaac.Count} - {DateTime.Now}");

                List<Pictograma> pictogramas = MapearPictogramas(pictogramasArasaac);
                List<Categoria> categorias = ObtenerCategorias(pictogramasArasaac);
                List<Tag> tags = ObtenerTags(pictogramasArasaac);
                List<PalabraClave> palabrasClaves = ObtenerPalabrasClaves(pictogramasArasaac);

                // INSERT PICTOGRAMAS - Solo inserta los pictogramas faltantes de agregar
                var pictogramasNuestros = await _pictogramaMgmt.ObtenerPictogramas(null);
                _logger.LogInformation($"Total pictogramas actuales: {pictogramasNuestros.Count} - {DateTime.Now}");
                if (pictogramasNuestros.Count < pictogramas.Count)
                {
                    var pictogramasAAgregar = pictogramas.Where(p => !pictogramasNuestros.Any(pic => pic.IdArasaac == p.IdArasaac)).ToList();

                    await _pictogramaMgmt.AgregarPictogramas(pictogramasAAgregar);
                    _logger.LogInformation($"Se insertaron los pictogramas, total pictogramas agregados: {pictogramasAAgregar.Count} - {DateTime.Now}");

                    pictogramasNuestros = await _pictogramaMgmt.ObtenerPictogramas(null);
                    _logger.LogInformation($"Total pictogramas: {pictogramasNuestros.Count} - {DateTime.Now}");
                }
                else
                    _logger.LogInformation($"No hizo falta agregar nuevos pictogramas - {DateTime.Now}");

                foreach (var keyword in palabrasClaves)
                {
                    try
                    {
                        var pic = pictogramasNuestros.FirstOrDefault(p => p.IdArasaac == keyword.IdPictograma);
                        keyword.IdPictograma = pic != null ? pic.Id : 0;
                    }
                    catch (Exception ex)
                    {

                    }
                }

                // INSERT KEYWORDS
                var keywordsNuestras = await _palabraClaveMgmt.ObtenerKeywords();
                if (keywordsNuestras.Count < palabrasClaves.Count)
                {
                    var keywordsAAgregar = palabrasClaves.Where(p => !keywordsNuestras.Any(pc => (pc.Keyword == p.Keyword && pc.IdPictograma == p.IdPictograma))).ToList();

                    await _palabraClaveMgmt.AgregarPalabrasClaves(keywordsAAgregar);
                    _logger.LogInformation($"Se insertaron las palabras claves, total palabras claves agregadas: {keywordsAAgregar.Count} - {DateTime.Now}");
                }
                else
                    _logger.LogInformation($"No hizo falta agregar nuevas palabras claves - {DateTime.Now}");

                // TODO: No se deben unificar mas las categorias con los tags, y solo se debe tener asociado al pictograma las categorias y no los tags
                List<Categoria> categoriasNuestras = await _categoriaMgmt.ObtenerCategorias();


                //// INSERT PICTOGRAMAS X CATEGORIAS
                // Tambien agrega tags como categorias
                var relacionesNuestras = await _pictogramaPorCategoriaMgmt.ObtenerTotalPictogramasPorCategoria();
                List<PictogramaPorCategoria> picsXcats = ObtenerPictogramasPorCategorias(categoriasNuestras, pictogramasNuestros, pictogramasArasaac);

                if (relacionesNuestras.Count < picsXcats.Count)
                {
                    var pxcAAgregar = picsXcats.Where(p => !relacionesNuestras.Any(pc => (pc.IdCategoria == p.IdCategoria && pc.IdPictograma == p.IdPictograma))).ToList();

                    await _pictogramaPorCategoriaMgmt.AgregarRelaciones(pxcAAgregar);
                    _logger.LogInformation($"Se insertaron las relaciones de pictogramas por categorias, total relaciones: {picsXcats.Count} - {DateTime.Now}");
                }
                else
                    _logger.LogInformation($"No hizo falta agregar nuevas relaciones de pictogramas por categorias - {DateTime.Now}");

                //Guardar imagenes en Storage
                var pictogramasGuardados = await _storageMgmt.ObtenerTotalImagenesPictogramas();
                _logger.LogInformation($"Total imagenes de pictogramas locales actualmente {pictogramasGuardados.Count} - {DateTime.Now}");
                List<Stream> pictogramasAsStreams = new List<Stream>();
                if (pictogramasGuardados.Count < pictogramasNuestros.Count)
                {
                    var pictogramasAAgregar = pictogramasNuestros.Where(p => !pictogramasGuardados.Contains(p.Id.ToString())).ToList();

                    foreach(var pictograma in pictogramasAAgregar)
                    {
                        try
                        {
                            var pictogramaAsStream = await _arasaacService.ObtenerPictogramaDeArasaac((int)pictograma.IdArasaac);
                            _storageMgmt.Guardar(pictogramaAsStream, $"{pictogramasNuestros.FirstOrDefault(p => p.IdArasaac == pictograma.IdArasaac).Id}"); // TODO: Con que nombre lo guardamos? por ahora lo estamos guardando con el id
                            _logger.LogInformation($"Se descargo y guardo el pictograma de arasaac {pictograma.IdArasaac} - {DateTime.Now}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation($"Error al guardar el pictograma {pictograma.IdArasaac} en el storage - {ex.Message} - {DateTime.Now}");
                        }
                    }

                    // Para probar asincronismo
                    //Parallel.ForEach(pictogramasAAgregar,
                    //    //new ParallelOptions { MaxDegreeOfParallelism = 10 },
                    //    async (pictograma) =>
                    //{
                    //    try
                    //    {
                    //        var pictogramaAsStream = await _arasaacService.ObtenerPictogramaDeArasaac((int)pictograma.IdArasaac);
                    //        _storageMgmt.Guardar(pictogramaAsStream, $"{pictogramasNuestros.FirstOrDefault(p => p.IdArasaac == pictograma.IdArasaac).Id}"); // TODO: Con que nombre lo guardamos? por ahora lo estamos guardando con el id
                    //        _logger.LogInformation($"Se descargo y guardo el pictograma de arasaac {pictograma.IdArasaac} - {DateTime.Now}");
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        _logger.LogInformation($"Error al guardar el pictograma {pictograma.IdArasaac} en el storage - {ex.Message} - {DateTime.Now}");
                    //    }
                    //});
                    _logger.LogInformation($"Total imagenes de pictogramas guardadas en el storage {pictogramasAAgregar.Count} - {DateTime.Now}");
                }

                // Generar imagenes de categorias
                await GenerarImagenesDeCategorias(categoriasNuestras, pictogramasNuestros, picsXcats);
                _logger.LogInformation($"Se generaron las imagenes de categorias - {DateTime.Now}");
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Ocurrio un error en la actualizacion de pictogramas: {e.Message} - {DateTime.Now}");
            }
        }

        private async Task GenerarImagenesDeCategorias(List<Categoria> categoriasNuestras, List<Pictograma> pictogramasNuestros, List<PictogramaPorCategoria> picsXcats)
        {
            var imagenesCategoriasGuardadas = await _storageMgmt.ObtenerTotalImagenesCategorias();
            _logger.LogInformation($"Total imagenes de categorias locales actualmente {imagenesCategoriasGuardadas.Count}");
            if (imagenesCategoriasGuardadas.Count < categoriasNuestras.Count)
            {
                var categoriasAAgregar = categoriasNuestras.Where(c => !imagenesCategoriasGuardadas.Contains(c.Id.ToString())).ToList();
                _logger.LogInformation($"Total imagenes de categorias a guardar {categoriasAAgregar.Count}");
                foreach (var categoria in categoriasAAgregar)
                {
                    try
                    {
                        var pictogramasFiltrados = pictogramasNuestros.Where(p => p.Sex == false && p.Violence == false);
                        var pictogramasDeCategoria = picsXcats.Where(p => p.IdCategoria == categoria.Id && pictogramasFiltrados.Any(pf => pf.Id == p.IdPictograma)).ToList();
                        var pictograma1 = pictogramasDeCategoria[0] != null ? pictogramasDeCategoria[0].IdPictograma : 0;
                        var pictograma2 = pictogramasDeCategoria.Count > 1 && pictogramasDeCategoria[1] != null ? pictogramasDeCategoria[1].IdPictograma : pictograma1;
                        var pictograma3 = pictogramasDeCategoria.Count > 2 && pictogramasDeCategoria[2] != null ? pictogramasDeCategoria[2].IdPictograma : pictograma2;
                        var pictograma4 = pictogramasDeCategoria.Count > 3 && pictogramasDeCategoria[3] != null ? pictogramasDeCategoria[3].IdPictograma : pictograma3;
                        var imagen1 = _storageMgmt.Obtener(pictograma1.ToString());
                        var imagen2 = _storageMgmt.Obtener(pictograma2.ToString());
                        var imagen3 = _storageMgmt.Obtener(pictograma3.ToString());
                        var imagen4 = _storageMgmt.Obtener(pictograma4.ToString());
                        try
                        {
                            using (Image image1 = Image.FromStream(imagen1))
                            {
                                try
                                {
                                    using (Image image2 = Image.FromStream(imagen2))
                                        ObtenerImagenesDeCategoriaConImagenes3Y4(categoria, imagen2, imagen3, imagen4, image1, image2);
                                }
                                catch (Exception exce)
                                {
                                    ObtenerImagenesDeCategoriaConImagenes3Y4(categoria, imagen1, imagen3, imagen4, image1, image1);
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            try
                            {
                                using (Image image2 = Image.FromStream(imagen2))
                                    ObtenerImagenesDeCategoriaConImagenes3Y4(categoria, imagen2, imagen3, imagen4, image2, image2);
                            }
                            catch (Exception exce)
                            {
                                try
                                {
                                    using (Image image3 = Image.FromStream(imagen3))
                                        ObtenerImagenesDeCategoriaConImagenes3Y4(categoria, imagen3, imagen3, imagen4, image3, image3);
                                }
                                catch (Exception ex2)
                                {
                                    using (Image image4 = Image.FromStream(imagen4))
                                        ObtenerImagenesDeCategoriaConImagenes3Y4(categoria, imagen4, imagen3, imagen3, image4, image4);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Fallo guardar imagen de categoria: {categoria.Id} - {ex.Message} - {DateTime.Now}");
                    }
                }
                _logger.LogInformation($"Se finalizo el guardado de imagenes de categorias - {DateTime.Now}");
            }
        }

        private void ObtenerImagenesDeCategoriaConImagenes3Y4(Categoria categoria, Stream imagen2, Stream imagen3, Stream imagen4, Image image1, Image image2)
        {
            try
            {
                using (Image image3 = Image.FromStream(imagen3))
                {
                    try
                    {
                        ObtenerImagenCategoria(categoria, imagen4, image1, image2, image3);
                    }
                    catch (Exception ex)
                    {
                        ObtenerImagenCategoria(categoria, imagen3, image1, image2, image3);
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    ObtenerImagenCategoria(categoria, imagen4, image1, image2, image2);

                }
                catch (Exception ex2)
                {
                    ObtenerImagenCategoria(categoria, imagen2, image1, image2, image2);
                }
            }
        }

        private void ObtenerImagenCategoria(Categoria categoria, Stream ImagenEnStream, Image image1, Image image2, Image image3)
        {
            using (Image image4 = Image.FromStream(ImagenEnStream))
            {
                GenerarYGuardarImagen(categoria, image1, image2, image3, image4);
            }
        }

        private void GenerarYGuardarImagen(Categoria categoria, Image image1, Image image2, Image image3, Image image4)
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
                    _logger.LogInformation($"Se guardo la imagen de la categoria: {categoria.Id} - {DateTime.Now}");
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
                        catch (Exception ex)
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

            foreach (var pictograma in pictogramasArasaac)
            {
                pictogramas.Add(new Pictograma
                {
                    Aac = pictograma.aac,
                    AacColor = pictograma.aacColor,
                    Hair = pictograma.hair,
                    IdArasaac = pictograma._id,
                    Schematic = pictograma.schematic,
                    Sex = pictograma.sex,
                    Skin = pictograma.skin,
                    Violence = pictograma.violence,
                    UltimaActualizacion = DateTime.Now
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
                        Keyword = palabraClave.keyword != null ? String.Join("", palabraClave.keyword.Split(',', '\'', '"', '@')) : "",
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
                    if (!tags.Any(t => t.Nombre == tag))
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
                    if (!categorias.Any(c => categoria == c.Nombre))
                        categorias.Add(new Categoria { Nombre = categoria });
                }
            }
            return categorias;
        }
    }
}
