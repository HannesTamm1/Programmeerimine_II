using KooliProjekt.PublicAPI.Api; // Ensure this namespace is correct and the assembly is referenced
using KooliProjekt.WinFormsApp; // Ensure this namespace is correct and the assembly is referenced

namespace KooliProjekt.WinFormsApp
{
    internal static class Program
    {
        /// <summary>  
        ///  The main entry point for the application.  
        /// </summary>  
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,  
            // see https://aka.ms/applicationconfiguration.  
            ApplicationConfiguration.Initialize();
            var apiClient = new ApiClient(); // Ensure the class is in the correct namespace  
            var productView = new ProductView(); // Ensure the class is in the correct namespace and is a concrete implementation of IProductView  
            var presenter = new ProductPresenter(productView, apiClient);
            Application.Run(new Form1(presenter));
        }
    }
}