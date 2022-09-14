using System;
using System.Collections.Generic;
using Khaled.Helpers;
using Khaled.Views.ContentViews;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Khaled.Views.PopUps
{
    public partial class PopUp_Template 
    {
        public static PopUp_Template instance; 
        public PopUp_Template(PopUpTypes PopUpTypes)
        {
            InitializeComponent();
            instance = this; 
            LoadContent(PopUpTypes);

            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;
        }

        private void LoadContent(PopUpTypes PopUpTypes)
        {
            switch (PopUpTypes)
            {
                case PopUpTypes.pickFirstLocation:
                    var content = new CV_PickYourCity(false);
                    layout_mainContent.Children.Add(content);
                    break;
                case PopUpTypes.changeLocation:
                    var content2 = new CV_PickYourCity(true);
                    layout_mainContent.Children.Add(content2);
                    break;

            }
        }


        public void ClickedOnClosed(System.Object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
    }

    public enum PopUpTypes
    {
        pickFirstLocation = 0,
        changeLocation = 1,
    }
}
