using Carter;
using Carter.ModelBinding;
using Carter.Response;
using PictogramasApi.Model.Requests;
using PictogramasApi.Services;
using System;

namespace PictogramasApi.Modules
{
    public class InterpretacionModule : CarterModule
    {
        private readonly GoogleTranslateService _interpretacionService;

        public InterpretacionModule(GoogleTranslateService interpretacionService) : base("/interpretacion")
        {
            _interpretacionService = interpretacionService;

            GetInterpretacionNatural();
        }

        private void GetInterpretacionNatural()
        {
            Post("/", async (ctx) =>
            {
                var request = await ctx.Request.Bind<InterpretacionRequest>();
                var interpretacion = await _interpretacionService.TraducirTexto(request.Texto);
                await ctx.Response.Negotiate(interpretacion);
            });
        }
    }
}
