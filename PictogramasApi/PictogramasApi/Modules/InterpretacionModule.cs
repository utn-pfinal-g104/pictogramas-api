using Carter;

namespace PictogramasApi.Modules
{
    public class InterpretacionModule : CarterModule
    {
        private readonly InterpretacionService _interpretacionService;

        public InterpretacionModule(InterpretacionService interpretacionService) : base("/interpretacion")
        {
            _interpretacionService = interpretacionService;
        }
    }
}
