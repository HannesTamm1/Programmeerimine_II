using KooliProjekt.PublicAPI.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.WinFormsApp
{
    public interface IProductView
    {
        IList<Product> Products { get; set; }
        Product SelectedItem { get; set; }
        string Title { get; set; }
        string Name { get; set; }
        int Id { get; set; }
        ProductPresenter Presenter { get; set; }    
    }
}
