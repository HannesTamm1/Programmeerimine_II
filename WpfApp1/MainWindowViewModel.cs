using KooliProjekt.WpfApp.Api;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using System.Xml.Linq;
using WpfApp1;
using WpfApp1.Api;

namespace WpfApp1
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public ObservableCollection<Product> Lists { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Predicate<Product> ConfirmDelete { get; set; }

        private readonly IApiClient _apiClient;

        public MainWindowViewModel() : this(new ApiClient())
        {
        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;

            Lists = new ObservableCollection<Product>();

            NewCommand = new RelayCommand<Product>(
                // Execute
                list =>
                {
                    SelectedItem = new Product();
                }
            );

            SaveCommand = new RelayCommand<Product>(
                // Execute
                async list =>
                {
                    await _apiClient.Save(SelectedItem);
                    await Load();
                },
                // CanExecute
                list =>
                {
                    return SelectedItem != null;
                }
            );

            DeleteCommand = new RelayCommand<Product>(
                // Execute
                async list =>
                {
                    if (ConfirmDelete != null)
                    {
                        var result = ConfirmDelete(SelectedItem);
                        if (!result)
                        {
                            return;
                        }
                    }

                    await _apiClient.Delete(SelectedItem.Id);
                    Lists.Remove(SelectedItem);
                    SelectedItem = null;
                },
                // CanExecute
                list =>
                {
                    return SelectedItem != null;
                }
            );
        }

        public async Task Load()
        {
            Lists.Clear();

            var lists = await _apiClient.List();
            foreach (var list in lists)
            {
                Lists.Add(list);
            }
        }

        private Product _selectedItem;
        public Product SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }
    }
}