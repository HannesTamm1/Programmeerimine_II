using KooliProjekt.PublicAPI;
using KooliProjekt.PublicAPI.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, IProductView
    {
        public IList<Product> Products
        {
            get
            {
                return (IList<Product>)ProductsGrid.DataSource;
            }
            set
            {
                ProductsGrid.DataSource = value;
            }
        }

        public Product SelectedItem { get; set; }

        public ProductPresenter Presenter { get; set; }

        public string Title
        {
            get
            {
                return TitleField.Text; ;
            }
            set
            {
                TitleField.Text = value;
            }
        }

        public int Id
        {
            get
            {
                return int.Parse(IdField.Text);
            }
            set
            {
                IdField.Text = value.ToString();
            }
        }

        public Form1()
        {
            InitializeComponent();

            ProductsGrid.AutoGenerateColumns = true;
            ProductsGrid.SelectionChanged += ProductsGrid_SelectionChanged;

            AddButton.Click += AddButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;

            Load += Form1_Load;
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            // Kutsu presenteri Delete meetodi
            // Lae andmed uuesti
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            // Kutsu presenteri Save meetodi
            // Lae andmed uuesti
        }

        private void AddButton_Click(object? sender, EventArgs e)
        {
            // Kutsu presenteri UpdateView meetodi
        }

        private void ProductsGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (ProductsGrid.SelectedRows.Count == 0)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = (Product)ProductsGrid.SelectedRows[0].DataBoundItem;
            }

            Presenter.UpdateView(SelectedItem);
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            await Presenter.Load();
        }
    }
}
