using appliPandora.Forms;

namespace appliPandora
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormDashboard());
        }
    }
}
