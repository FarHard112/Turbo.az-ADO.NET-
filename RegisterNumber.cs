using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turbo.az
{
    public partial class RegisterNumber : Form
    {
        public RegisterNumber()
        {
            InitializeComponent();
        }

        private void btnDavamEt_Click(object sender, EventArgs e)
        {
            if (textEditChange)
            {
                AddAdvertise addAdvertise = new AddAdvertise(txtPhoneNumber.Text);
                addAdvertise.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Nömrənizi daxil edin,zəhmət olmasa");
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RegisterNumber_Load(object sender, EventArgs e)
        {
       
        }
        bool textEditChange = false;
        private void txtPhoneNumber_EditValueChanged(object sender, EventArgs e)
        {
            textEditChange = true;
        }

        private void panel6_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
