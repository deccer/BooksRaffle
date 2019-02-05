using System.Windows;
using BooksRaffle.Data;
using BooksRaffle.ViewModels;
using BooksRaffle.Views;
using DryIoc;

namespace BooksRaffle
{
    public partial class App : Application
    {
        private static IContainer CreateContainer()
        {
            var container  = new Container();
            container.Register<IBookmarksContextFactory, BookmarksContextFactory>();
            container.Register<MainViewModel>(Made.Of(() => new MainViewModel(Arg.Of<IBookmarksContextFactory>())));
            container.Register<MainView>(Made.Of(() => new MainView(Arg.Of<MainViewModel>())));
            return container;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

            var container = CreateContainer();

            var mainView = container.Resolve<MainView>();

            mainView?.Show();
        }
    }
}