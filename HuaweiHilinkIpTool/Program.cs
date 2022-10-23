namespace HuaweiHilinkIpTool
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            foreach (var item in args)
            {
                Console.WriteLine(item);
                Application.Exit();
            }
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmMain());
        }
    }
}