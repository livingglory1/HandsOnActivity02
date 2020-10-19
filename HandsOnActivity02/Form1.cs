using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;

namespace HandsOnActivity02
{


    public partial class frmAddProduct : Form
    {
        BindingSource showProductList;
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellingPrice;
        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }
        public class StringFormatException : Exception
        {
            public StringFormatException(string name) : base(name)
            {

            }
        }
        public class NumberFormatException : Exception
        {
            public NumberFormatException(string qty) : base(qty)
            {

            }
        }
        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string price) : base(price)
            {

            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellingPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellingPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch(NumberFormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(StringFormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
            {
                "Beverages","Bread/Bakery","Canned/JarredGoods","Dairy","Frozen Goods","Meat","Personal Care","Other"
            };
            foreach(String item in ListOfProductCategory)
            {
                cbCategory.Items.Add(item);
            }

        }
        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new StringFormatException("Enter valid Product Name");
            }  
                return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
            {
                throw new NumberFormatException("Enter valid Quantity");
            }
                return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException("Enter valid Price");
            }
                return Convert.ToDouble(price);
        }
    }
}
