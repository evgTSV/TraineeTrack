﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace TraineeTrack.Client.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia!";
}