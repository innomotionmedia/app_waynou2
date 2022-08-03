using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Khaled.Views.MainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_MainMenu : MasterDetailPage
    {
        public static Page_MainMenu instance;
        public Page_MainMenu()
        {
            InitializeComponent();
            instance = this;


            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

        }


        private void Page_MainMenu_IsPresentedChanged(object sender, EventArgs e)
        {
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Page_MainMenuMasterMenuItem;
            if (item == null)
                return;


            IsPresented = false;

            Page_MainMenuDetail.instance.GetClickFromDrawer(item.Title);

            MasterPage.ListView.SelectedItem = null;
        }
    }
}
