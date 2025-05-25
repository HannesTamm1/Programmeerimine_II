using KooliProjekt.PublicAPI;
using KooliProjekt.PublicAPI.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, IProductView
    {
        public IList<Product> Products
        {
            get { return (IList<Product>)TodoListsGrid.DataSource; }
            set { TodoListsGrid.DataSource = value; }
        }

        public Product SelectedItem // Removed nullable annotation to match the interface
        {
            get => TodoListsGrid.SelectedRows.Count > 0
                ? (Product)TodoListsGrid.SelectedRows[0].DataBoundItem
                : new Product(); // Return a default Product instance instead of null
            set
            {
                if (value != null)
                {
                    foreach (DataGridViewRow row in TodoListsGrid.Rows)
                    {
                        if (row.DataBoundItem == value)
                        {
                            row.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        public ProductPresenter Presenter { get; set; } = null!; // Use null-forgiving operator to suppress warning

        public string Title
        {
            get { return TitleField.Text; }
            set { TitleField.Text = value; }
        }

        public new string Name
        {
            get => TitleField.Text;
            set => TitleField.Text = value;
        }

        public int Id
        {
            get { return int.TryParse(IdField.Text, out var id) ? id : 0; }
            set { IdField.Text = value.ToString(); }
        }

        public Form1()
        {
            InitializeComponent();

            TodoListsGrid.AutoGenerateColumns = true;
            TodoListsGrid.SelectionChanged += TodoListsGrid_SelectionChanged;

            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;

            Load += async (s, e) => await Form1_Load(s, e);
        }

        private async void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (Presenter != null && SelectedItem != null)
            {
                await Presenter.Delete(SelectedItem.Id);
            }
        }

        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            if (Presenter != null && SelectedItem != null)
            {
                await Presenter.Save(SelectedItem);
            }
        }

        private void NewButton_Click(object? sender, EventArgs e)
        {
            var newProduct = new Product { Id = 0, Name = "" };
            SelectedItem = newProduct;
            Presenter?.UpdateView(newProduct);
        }

        private void TodoListsGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (TodoListsGrid.SelectedRows.Count == 0)
            {
                // Assign a default Product instance instead of null to fix CS8625
                SelectedItem = new Product { Id = 0, Name = string.Empty };
            }
            else
            {
                SelectedItem = (Product)TodoListsGrid.SelectedRows[0].DataBoundItem;
            }

            if (SelectedItem != null)
            {
                Presenter?.UpdateView(SelectedItem);
            }
        }

        private async Task Form1_Load(object? sender, EventArgs e)
        {
            if (Presenter != null)
                await Presenter.Load();
        }
    }
}