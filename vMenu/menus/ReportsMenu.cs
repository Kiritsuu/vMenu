using CitizenFX.Core;

using MenuAPI;

namespace vMenuClient.menus
{
    public class ReportsMenu
    {
        private Menu menu;
        private void CreateMenu()
        {
            Player player = Game.Player;
            menu = new Menu(vMenuClient.CommonFunctions.GetSafePlayerName(player.Name), "Report Options");
        }
        public Menu GetMenu()
        {
            if (menu == null)
            {
                CreateMenu();
            }
            return menu;
        }
    }
}