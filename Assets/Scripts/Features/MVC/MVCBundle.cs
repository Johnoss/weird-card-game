namespace Assets.Scripts.Features.MVC
{
    public class MVCBundle<TModel, TView, TController>
        where TModel : AbstractModel
        where TView : AbstractView
        where TController : AbstractController
    {
        private readonly TModel model;
        public TModel Model => model;

        private readonly TView view;
        public TView View => view;

        private readonly TController controller;
        public TController Controller=> controller;

        public MVCBundle(TModel model, TView view, TController controller)
        {
            this.model = model;
            this.view = view;
            this.controller = controller;
        }
    }
}