using System.Windows;
using BookmarksRaffle.Views;
using BooksRaffle.ViewModels;
using DryIoc;

namespace BooksRaffle
{
    public partial class App : Application
    {
        private static IContainer CreateContainer()
        {
            var container  = new Container();
            container.Register<MainViewModel>();
            container.Register<MainView>(Made.Of(() => new MainView(Arg.Of<MainViewModel>())));
            return container;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = CreateContainer();

            var mainView = container.Resolve<MainView>();

            mainView?.Show();
        }
    }
}