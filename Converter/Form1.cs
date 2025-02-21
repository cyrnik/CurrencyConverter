﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }
    public class CurrencyConverter
    {
        private decimal usdToEur = 0.88m; // Примерный курс валют
        private decimal eurToUsd = 1.12m;
        public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Сумма не может быть отрицательной.");
            }
            if (fromCurrency == "USD" && toCurrency == "EUR")
            {
                return amount * usdToEur;
            }
            else if (fromCurrency == "EUR" && toCurrency == "USD")
            {
                return amount * eurToUsd;
            }
            else
            {
                throw new NotSupportedException("Не поддерживаемая пара валют.");
            }
        }
    }
    public class CurrencyConverterForm : Form
    {
        private CurrencyConverter converter;
        private ComboBox fromCurrencyComboBox;
        private ComboBox toCurrencyComboBox;
        private TextBox amountTextBox;
        private Button convertButton;
        private Label resultLabel;
        public CurrencyConverterForm()
        {
            this.Text = "Конвертер валют";
            this.Width = 300;
            this.Height = 200;
            fromCurrencyComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 100,
                Items = { "USD", "EUR" }
            };
            toCurrencyComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(120, 10),
                Width = 100,
                Items = { "USD", "EUR" }
            };
            amountTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 40),
                Width = 210,
                //PlaceholderText = "Сумма"
            };
            convertButton = new Button
            {
                Location = new System.Drawing.Point(10, 70),
                Text = "Конвертировать",
                Width = 210
            };
            convertButton.Click += ConvertButton_Click;
            resultLabel = new Label
            {
                Location = new System.Drawing.Point(10, 100),
                Width = 210,
                Text = "Результат: "
            };
            this.Controls.Add(fromCurrencyComboBox);
            this.Controls.Add(toCurrencyComboBox);
            this.Controls.Add(amountTextBox);
            this.Controls.Add(convertButton);
            this.Controls.Add(resultLabel);
        }
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(amountTextBox.Text))
            {
                MessageBox.Show("Введите сумму для конвертации!");
                return;
            }
            decimal amount;
            if (!decimal.TryParse(amountTextBox.Text, out amount))
            {
                MessageBox.Show("Неверный формат суммы!");
                return;
            }
            string fromCurrency = fromCurrencyComboBox.SelectedItem.ToString();
            string toCurrency = toCurrencyComboBox.SelectedItem.ToString();
            try
            {
                decimal result = converter.Convert(amount, fromCurrency, toCurrency);
                resultLabel.Text = $"Результат: {amount} {fromCurrency} = {result} {toCurrency}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
