using System;
using System.Collections.Generic;
using Khaled.Helpers;
using Khaled.Views.ContentViews;
using Rg.Plugins.Popup.Services;
using TodoApp.Data;
using Xamarin.Forms;

namespace Khaled.Views.PopUps
{
    public partial class PopUp_Template_DropDown 
    {
        public static PopUp_Template_DropDown instance; 
        public PopUp_Template_DropDown(PopUpTypesDropDown PopUpTypes)
        {
            InitializeComponent();
            instance = this; 
            LoadContent(PopUpTypes);

            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;
        }

        private void LoadContent(PopUpTypesDropDown PopUpTypes)
        {
            switch (PopUpTypes)
            {
                case PopUpTypesDropDown.dropRadius:
                    break;
            }
        }

        public void ClickedOnClosed(System.Object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
    }

    public enum PopUpTypesDropDown
    {
        dropRadius = 0
    }
}
