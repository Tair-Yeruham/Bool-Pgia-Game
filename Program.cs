using System.Windows.Forms;

namespace BoolPgiaGameUI
{    
    public static class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            GameConfiguration gameConfiguration = new GameConfiguration();
            gameConfiguration.ShowDialog();
        }
    }
}
