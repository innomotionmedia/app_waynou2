using Khaled.Helpers;
using Khaled.Resources;
using Khaled.Views.ContentViews.Categories;
using Khaled.Views.ContentViews.MainMenuTabs;
using Khaled.Views.PopUps;
using TodoApp.Data;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Khaled.Views.MainMenu
{
    public partial class Page_MainMenuDetail : Xamarin.Forms.TabbedPage
    {
        public static Page_MainMenuDetail instance;

        bool showMidPage = true;
        bool favAdded = false;
        bool offersAdded = false;

        public Page_MainMenuDetail()
        {
            InitializeComponent();

            instance = this;


            layout_container_page3.Children.Add(new CV_CategoryList());
            SetStyling();

        }

        private void SetStyling()
        {
            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;

            if (Device.RuntimePlatform == Device.iOS)
            {
                BarBackground = new SolidColorBrush(Converters.ColorFromResourceKey("MainTheme"));//Converters.ColorFromResourceKey("MainTheme");
                BarBackgroundColor = (Converters.ColorFromResourceKey("MainTheme"));
            }


            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetEnableAccessibilityScalingForNamedFontSizes(false);

            
        }

        public void ShowNavBar(bool value)
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, value);
        }

        protected async override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            //if (CurrentPage.Equals(page_news))
            //{
            //    Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, true);
            //}
            if (CurrentPage.Equals(page_cats))
            {
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, true);
                if (!offersAdded)
                    layout_container_page2.Children.Add(new CV_AllAdsRes(null));
                offersAdded = true;
            } 
            if (CurrentPage.Equals(page_main))
            {
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            }
            if (CurrentPage.Equals(page_favs))
            {
                if (!favAdded)
                    layout_container_page5.Children.Add(new CV_AllAdsRes(AdsListType.favorites, ""));
                favAdded = true;
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, true);

            }




        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // go to page 3 as home page
            if (showMidPage)
            {
                CurrentPage = Children[1];
                showMidPage = false; 
            }

        }

        public async void GetClickFromDrawer(string title)
        {
            //Click comes from Page_MainMenu via instance reference
            if (title == AppResources.MainMenu)
                Xamarin.Forms.Application.Current.MainPage = new MainPage(true, true);
            if (title == AppResources.DSGVO)
                await Browser.OpenAsync(Constants.urlDataPrivacy);
            if (title == AppResources.TermsOfUse)
                await Browser.OpenAsync(Constants.urlToS);
            if (title == AppResources.Imprint)
                await Browser.OpenAsync(Constants.urlImprint);

        }

        public void ClosePopUpTemplate()
        {
            try
            {
                
                PopUp_Template.instance.ClickedOnClosed(null, null);
            }
            catch { }
        }

    }

    public enum filterEnum
    {
        city = 0,
    }

}



