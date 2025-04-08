namespace TraineeTrack.Client

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Data.Core
open Avalonia.Data.Core.Plugins
open Avalonia.Markup.Xaml
open TraineeTrack.Client.ViewModels
open TraineeTrack.Client.Views

type App() =
    inherit Application()

    override this.Initialize() =
            AvaloniaXamlLoader.Load(this)

    override this.OnFrameworkInitializationCompleted() =

        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0)

        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow(DataContext = MainViewModel())
        | :? ISingleViewApplicationLifetime as singleViewLifetime ->
            singleViewLifetime.MainView <- MainView(DataContext = MainViewModel())
        | _ -> ()

        base.OnFrameworkInitializationCompleted()