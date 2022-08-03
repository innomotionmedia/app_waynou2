using System;
namespace Khaled.Views.MainMenu
{
    public class Page_MainMenuMasterMenuItem
    {
        public Page_MainMenuMasterMenuItem()
        {
            TargetType = typeof(Page_MainMenuMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
