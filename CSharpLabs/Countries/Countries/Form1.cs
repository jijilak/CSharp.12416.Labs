using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Countries
{
    public partial class Form1 : Form
    {
        private List<Country> _countries;
        private List<string> _continents;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitDgv();
            ReloadData();
        }
        
        private void InitDgv()
        {
            dgv.ColumnCount = 5;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.ReadOnly = true;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Width = 50;
            }
            dgv.Columns[0].Name = "Code";
            dgv.Columns[1].Name = "Name";
            dgv.Columns[1].Width = 200;
            dgv.Columns[2].Name = "Currency";
            dgv.Columns[3].Name = "Capital";
            dgv.Columns[3].Width = 200;
            dgv.Columns[4].Name = "Continent";
            dgv.Columns[4].Width = 100;
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ReloadData()
        {
            dgv.Rows.Clear();
            _countries =  AdoNetHelper.LoadCountries();
            _continents = _countries.Select(c => c.Continent).Distinct().ToList();
            dgv.RowCount = _countries.Count;
            
            for (int i = 0; i < _countries.Count; i++)
            {
                Country c = _countries.ElementAt(i);
                dgv.Rows[i].Cells[0].Value = c.Code;
                dgv.Rows[i].Cells[1].Value = c.Name;
                dgv.Rows[i].Cells[2].Value = c.Currency;
                dgv.Rows[i].Cells[3].Value = c.Capital;
                dgv.Rows[i].Cells[4].Value = c.Continent;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            CountryForm addForm = new CountryForm();
            addForm.IsNew = true;
            addForm.Continents = _continents;
            addForm.Text = "Новая страна";
            addForm.ShowDialog();
            if (addForm.Model == null) return;
            if (string.IsNullOrWhiteSpace(addForm.Model.Code) || string.IsNullOrWhiteSpace(addForm.Model.Name) 
                                                              || string.IsNullOrWhiteSpace(addForm.Model.Continent))
            {
                MessageBox.Show("Не заданы все обязательные значения полей!");
                return;
            }
            AdoNetHelper.AddCountry(addForm.Model);
            ReloadData();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            string code = currentRow?.Cells[0].Value.ToString();
            if (code == null) return;
            int id = _countries.Where(c => c.Code == code).Select(c => c.Id).First();
            string name = currentRow.Cells[1].Value.ToString();
            string currency = currentRow.Cells[2].Value.ToString();
            string capital = currentRow.Cells[3].Value.ToString();
            string continent = currentRow.Cells[4].Value.ToString();
            
            CountryForm editForm = new CountryForm();
            editForm.Continents = _continents;
            editForm.Model = new Country(id, code) {Name = name, Currency = currency, Capital = capital, Continent = continent};
            editForm.Text = "Редакция страны";
            editForm.ShowDialog();
            if (editForm.Model == null) return;
            if (string.IsNullOrWhiteSpace(editForm.Model.Name) || string.IsNullOrWhiteSpace(editForm.Model.Continent))
            {
                MessageBox.Show("Не заданы все обязательные значения полей!");
                return;
            }
            AdoNetHelper.UpdateCountry(editForm.Model);
            ReloadData();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = dgv.CurrentRow;
            string code = currentRow?.Cells[0].Value.ToString();
            string name = currentRow?.Cells[1].Value.ToString();
            if (code == null) return;
            DialogResult dialogResult = MessageBox.Show($"Вы хотите удалить страну {name}?", "Удаление", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                AdoNetHelper.DeleteCountry(code);
            }
            ReloadData();
        }
    }
}
