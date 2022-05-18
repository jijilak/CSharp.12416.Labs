using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Countries
{
    public partial class CountryForm : Form
    {
        public bool IsNew { get; set; } = false;
        public Country Model { get; set; }

        private List<string> _continents;

        public List<string> Continents
        {
            get { return _continents; }
            set
            {
                _continents = value;
                comboContinent.DataSource = _continents;
            }
        }

        public CountryForm()
        {
            InitializeComponent();
        }

        private void CountryForm_Load(object sender, EventArgs e)
        {
            if (IsNew) return;
            string code = Model.Code;
            tbCode.Text = code;
            tbCode.Enabled = false;
            tbName.Text = Model.Name;
            tbCurrency.Text = Model.Currency;
            tbCapital.Text = Model.Capital;
            comboContinent.Text = Model.Continent;
            
            string flag = $"Flags/{code.ToLower()}.png";
            Stream imgStream = GetResource(flag);
            if (imgStream == null) return;
            var image = new Bitmap(imgStream);
            picFlag.Image = image;
            picFlag.Height = image.Height;
            picFlag.Width = image.Width;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Model = null;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int id = Model?.Id ?? -1;
            Model = new Country(id, tbCode.Text)
            {
                Name = tbName.Text, Currency = tbCurrency.Text,
                Capital = tbCapital.Text, Continent = comboContinent.Text
            };
            this.Close();
        }

        private Stream GetResource(string resourcePath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> resourceNames = assembly.GetManifestResourceNames().ToList();
            string path = resourcePath.Replace(@"/", ".").Replace(@"\", ".");
            string assemblyPath = assembly.GetName().Name + "." + path;
            if (!resourceNames.Contains(assemblyPath)) return null;

            var resource = assembly.GetManifestResourceStream(assemblyPath);
            return resource;
        }
    }
}
