using AlgorithmGUILib;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AlgorithmGUIApp;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>

public sealed partial class MainWindow : Window
{
    private AppWindow _appWindow;
    private ObservableCollection<Algorithm> _algorithms;
    
    public MainWindow()
    {
        this.InitializeComponent();
        _appWindow = GetAppWindowForCurrentWindow();
        _appWindow.Title = "Algorithm GUI";
        _appWindow.Resize(new SizeInt32(1200, 800));
        _appWindow.Closing += OnMainWindowClosing;
        CreateAlgorithms();
    }
    
    private AppWindow GetAppWindowForCurrentWindow()
    {
        IntPtr hWnd = WindowNative.GetWindowHandle(this);
        WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        return AppWindow.GetFromWindowId(wndId);
    }
    
    private async void OnMainWindowClosing(object sender, AppWindowClosingEventArgs e)
    {
        ContentDialog confirmDialog = new()
        {
            XamlRoot = MainPanel.XamlRoot,
            Title = "Are you sure you want to exit the application?",
            Content = "Confirm that you want to exit the application",
            CloseButtonText = "Cancel",
            PrimaryButtonText = "Exit"
        };
        e.Cancel = true;
        try
        {
            ContentDialogResult result = await confirmDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                this.Close();
            }
        }
        catch (Exception)
        {
            //Continue
        }
    }
    
    private void CreateAlgorithms()
    {
        _algorithms = new ObservableCollection<Algorithm>()
        {
            new Algorithm() {AlgoName = "Bubble Sort", AverageComplexity = "O(n2)"},
            new Algorithm() {AlgoName = "Quick Sort", AverageComplexity = "O(n × log n)"},
            new Algorithm() {AlgoName = "Merge Sort", AverageComplexity = "O(n × log n)"},
            new Algorithm() {AlgoName = "Insertion Sort", AverageComplexity = "O(n2)"},
        };
        AlgorithmsListView.ItemsSource = _algorithms;
    }

    private async void OnFileNewAlgorithm(object sender, RoutedEventArgs e)
    {
        ContentDialog createNewAlgorithmDialog = new()
        {
            XamlRoot = MainPanel.XamlRoot,
            Title = "Creating a new algorithm",
            Content = new CreateAlgorithmDialog(),
            PrimaryButtonText = "Create",
            SecondaryButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        };

        ContentDialogResult result = await createNewAlgorithmDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            CreateAlgorithmDialog content = (CreateAlgorithmDialog)createNewAlgorithmDialog.Content;
            Algorithm algorithm = new()
            {
                AlgoName = content.AlgoName,
                AverageComplexity = content.AverageComplexity
            };
            _algorithms.Add(algorithm);
        }

    }

    private async void OnFileExit(object sender, RoutedEventArgs e)
    {
        ContentDialog confDialog = new()
        {
            XamlRoot = MainPanel.XamlRoot,
            Title = "Are you sure you want to exit the application?",
            Content = "Confirm that you want to exit the application",
            CloseButtonText = "Cancel",
            PrimaryButtonText = "Exit"
        };

        ContentDialogResult result = await confDialog.ShowAsync();
        if(result == ContentDialogResult.Primary)
        {
            this.Close();
        }
    }

    private async void OnAboutClick(object sender, RoutedEventArgs e)
    {
        ContentDialog confDialog = new()
        {
            XamlRoot = MainPanel.XamlRoot,
            Title = "Project 2 - Sorting Algorithm App",
            Content = "by Brycen Dunn for CSCI 1260 Summer 2022",
            PrimaryButtonText = "Exit"
        };

        ContentDialogResult result = await confDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
        }
    }

    private async void OnAlgorithmClicked(object sender, ItemClickEventArgs e)
    {
        Algorithm algorithm = (Algorithm)e.ClickedItem;
        ContentDialog confDialog = new()
        {
            XamlRoot = MainPanel.XamlRoot,
            Title = $"Algorithm Name: {algorithm.AlgoName}",
            Content = $"Average Complexity: {algorithm.AverageComplexity}",
            PrimaryButtonText = "Delete Algorithm",
            SecondaryButtonText = "Update Algorithm",
            CloseButtonText = "Close"
        };
        ContentDialogResult result = await confDialog.ShowAsync();
        if(result == ContentDialogResult.Primary)
        {
            _algorithms.Remove(algorithm);
        }    
        else if (result == ContentDialogResult.Secondary)
        {
            ContentDialog createNewAlgorithmDialog = new()
            {
                XamlRoot = MainPanel.XamlRoot,
                Title = "Creating a new algorithm",
                Content = new CreateAlgorithmDialog(),
                PrimaryButtonText = "Create",
                SecondaryButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };

            ContentDialogResult result2 = await createNewAlgorithmDialog.ShowAsync();
            if (result2 == ContentDialogResult.Primary)
            {
                CreateAlgorithmDialog content = (CreateAlgorithmDialog)createNewAlgorithmDialog.Content;
                Algorithm algorithm2 = new()
                {
                    AlgoName = content.AlgoName,
                    AverageComplexity = content.AverageComplexity
                };
                _algorithms.Remove(algorithm);
                _algorithms.Add(algorithm2);
            }
        }
    }
    
}
