using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Khaled.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Khaled.Views.MainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_MainMenuMaster : ContentPage
    {
        public ListView ListView;
        public static Page_MainMenuMaster Instance;

        public Page_MainMenuMaster()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, true);

            BindingContext = new Page_MainMenuMasterViewModel();
            ListView = MenuItemsListView;
            Instance = this;
        }
    }
    class Page_MainMenuMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Page_MainMenuMasterMenuItem> MenuItems { get; set; }

        public Page_MainMenuMasterViewModel()
        {

                MenuItems = new ObservableCollection<Page_MainMenuMasterMenuItem>(new[]
                {                    
                    new Page_MainMenuMasterMenuItem { Id = 0, Title = AppResources.MainMenu},
                    new Page_MainMenuMasterMenuItem { Id = 1, Title = AppResources.DSGVO},
                    new Page_MainMenuMasterMenuItem { Id = 2, Title = AppResources.TermsOfUse},
                    new Page_MainMenuMasterMenuItem { Id = 3, Title = AppResources.Imprint},
                });       
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
