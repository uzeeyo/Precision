using Precision.Model;
using Precision.Services;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Threading;
using System;

namespace Precision.ViewModel
{
    public class OrderDetailsViewModel : ObservableObject, IPageViewModel
    {
        public OrderDetailsViewModel(int id)
        {
            PageName = "Orders / Order Details";
            currentOrderID = id;
            LoadOrderDetails(currentOrderID);
        }

        private int currentOrderID;
        private bool _editMode;
        private ICommand _editOrderCommand;
        private ICommand _savePriceCommand;
        private ICommand _changeTaxableCommand;
        private ICommand _addProductCommand;
        private ICommand _removeProductCommand;
        private Product _selectedProduct;
        private ObservableCollection<Product> _fiilteredProducts;
        private string _productSearchBox;
        private readonly DispatcherTimer timer = new DispatcherTimer();

        #region Public Properties
        public string PageName { get; set; }
        public Order OrderDetails { get; set; }

        //Calls search method
        public string ProductSearchBox
        {
            get => _productSearchBox;
            set
            {
                if (_productSearchBox != value)
                {
                    _productSearchBox = value;
                    UpdateFilteredProducts();
                }
            }
        }
        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                if (_editMode != value) { _editMode = value; }
                OnPropertyChanged(nameof(EditMode));
            }
        }

        public Customer Customer
        {
            get { return CustomerDataAccess.GetCustomerByOrderID(OrderDetails.OrderID); }
        }

        public ObservableCollection<Product> FilteredProducts
        {
            get => _fiilteredProducts;
            set
            {
                if (_fiilteredProducts != value)
                {
                    _fiilteredProducts = value;
                    OnPropertyChanged(nameof(FilteredProducts));
                }
            }
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand EditOrderCommand
        {
            get
            {
                if (_editOrderCommand == null)
                {
                    _editOrderCommand = new RelayCommand(
                        p => ChangeEditMode()
                        );
                }
                return _editOrderCommand;
            }
        }

        public ICommand SavePriceCommand
        {
            get
            {
                _savePriceCommand ??= new RelayCommand(
                    p => EditProductPrice((Product)p),
                    p => p is Order
                    );
                return _savePriceCommand;
            }
        }

        public ICommand ChangeTaxableCommand
        {
            get
            {
                if (_changeTaxableCommand == null)
                {
                    _changeTaxableCommand = new RelayCommand(
                        p => ChangeProductTaxable((Product)p)
                        );
                }
                return _changeTaxableCommand;
            }
        }

        public ICommand AddProductCommand
        {
            get
            {
                if (_addProductCommand == null)
                {
                    _addProductCommand = new RelayCommand(
                        p => AddItemToOrder((Product)p),
                        p => p is Product && p != null
                        );

                }

                return _addProductCommand;
            }
        }

        public ICommand RemoveProductCommand
        {
            get
            {
                if (_removeProductCommand == null)
                {
                    _removeProductCommand = new RelayCommand(
                        p => RemoveItemFromOrder((Product)p),
                        p => p is Product
                        );
                }
                return _removeProductCommand;
            }
        }
        #endregion

        #region Methods
        private void LoadOrderDetails(int orderID)
        {
            OrderDetails = OrderDataAccess.GetOrderDetailsByID(currentOrderID);
        }
        private void ChangeEditMode()
        {
            if (EditMode == false)
            {
                EditMode = true;
            }
            else
            {
                EditMode= false;
            }
        }

        //Called by Add Product dialog
        private void AddItemToOrder(Product product)
        {
            OrderDataAccess.AddProductToOrderID(currentOrderID, product.ProductID);
            LoadOrderDetails(currentOrderID);
            OnPropertyChanged(nameof(OrderDetails));
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);

        }

        private void EditProductPrice(Product product)
        {
            OrderDataAccess.EditProductPrice(product.EntryID, product.FinalPrice);
        }

        private void ChangeProductTaxable(Product product)
        {
            OrderDataAccess.ChangeProductTaxable(product.EntryID, product.Taxable);
        }

        private void RemoveItemFromOrder(Product product)
        {
            OrderDataAccess.RemoveProductFromOrderID(product.EntryID);
            LoadOrderDetails(currentOrderID);
            OnPropertyChanged(nameof(OrderDetails));
        }

        private void UpdateFilteredProducts()
        {
            timer.Tick -= TimeElapsed;
            timer.Interval = TimeSpan.FromMilliseconds(600);
            timer.Stop();
            timer.Start();
            timer.Tick += TimeElapsed;
        }

        private void TimeElapsed(object sender, EventArgs e)
        {
            timer.Tick -= TimeElapsed;
            timer.Stop();
            FilteredProducts = ProductDataAccess.GetSearchedProducts(ProductSearchBox);
        }

        #endregion

    }
}
