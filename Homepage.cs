using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turbo.az
{
    public partial class frmHomePage : Form
    {
        public frmHomePage()
        {
            InitializeComponent();
        }

        ClssCommonMethods clssCommonMethods = new ClssCommonMethods();

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }

        private void TubroazEsassehife_Load(object sender, EventArgs e)
        {
            
            ClssInfoAdapter clssInfoAdapter = new ClssInfoAdapter();


            #region SetDataToComponents
            clssCommonMethods.SetBrand(lookeditMarka);
            clssCommonMethods.SetLookUpEditTypeId(lookUpValute, "7"); 
            clssCommonMethods.SetLookUpEditTypeId(lookUpMakeDateMin, "8");
            clssCommonMethods.SetLookUpEditTypeId(lookUpMakeDateMax, "8");
            clssCommonMethods.SetLookUpEditTypeId(lookeditCities, "6");

            #endregion
            

        }







        private void simpleButton12_Click(object sender, EventArgs e)
        {
          
            RegisterNumber form2 = new RegisterNumber();
            form2.Show();
            this.Hide();
           
        }

        private void simpleButton5_MouseHover(object sender, EventArgs e)
        {
            btnBinaAz.BackColor = Color.AliceBlue;
        }

        

        private void simpleButton5_MouseLeave(object sender, EventArgs e)
        {
            btnBinaAz.BackColor = Color.FromArgb(97, 97, 97);
        }

        private void btnAddAdvertise_Click(object sender, EventArgs e)
        {
            RegisterNumber form2 = new RegisterNumber();
            form2.Show();
            this.Hide();


        }

        private void btnTapZ_Click(object sender, EventArgs e)
        {
           
           
        }

        private void lookeditMarka_EditValueChanged(object sender, EventArgs e)
        {
            clssCommonMethods.SetModel(lookeditModel,lookeditMarka);
        }
        
        private void GetCars()
        {
            

          

            string query = $@"Select ADS.ID,(SELECT top(1) IMG.CAR_IMAGE FROM ADS_IMAGES IMG WHERE IMG.ADS_ID = ID) CAR_IMAGE, ADS.PRICE,(BR.NAME+' '+MD.NAME) CAR_NAME,
     GN1.NAME MAKE_DATE,ADS.MILAGE,GN.NAME CITY_NAME,
     ADS.CREDIT,ADS.BARTER

           FROM ADVERTISE ADS
           INNER JOIN MODELS MD ON MD.ID = ADS.MODEL_ID
           INNER JOIN BRANDS BR ON BR.ID=MD.BRAND_ID
           INNER JOIN GENERAL_INFO GN ON GN.ID = ADS.CITIES_ID
           INNER JOIN GENERAL_INFO GN1 ON GN1.ID = ADS.MAKE_DATE
           WHERE ADS.VALUTE={lookUpValute.EditValue}" ;
                     
            if (lookeditMarka.EditValue != null)
            {
                query = query + $" AND BR.ID ={lookeditMarka.EditValue}";
            }

            if (lookeditModel.EditValue != null)
            {
                query = query + $" AND ADS.MODEL_ID={lookeditModel.EditValue}";
            }

            if (txtPriceMin.EditValue != null)
            {
                query = query + $" AND ADS.PRICE >={txtPriceMin.EditValue}";
            }

            if (txtPriceMax.EditValue != null)
            {
                query = query + $" AND ADS.PRICE <={txtPriceMax.EditValue}";
            }

            if (lookUpMakeDateMin.EditValue != null)
            {
                query = query + $" AND GN1.NAME >={lookUpMakeDateMin.EditValue}";
            }

            if (lookUpMakeDateMax.EditValue != null)
            {
                query = query + $" AND GN1.NAME <={lookUpMakeDateMax.EditValue}";
            }

            if (lookeditCities.EditValue != null)
            {
                query = query + $" AND ADS.CITIES_ID ={lookeditCities.EditValue}";
            }


            if (checkKredit.Checked)
            {
                query = query + $" AND ADS.CREDIT= 1";
            }


            if (checkBarter.Checked)
            {
                query = query + $" AND ADS.BARTER= 1";
            }


            SqlConnection sqlConnection = new SqlConnection(SqlUtils.GetInstance().conString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            DataTable dataTableCars = new DataTable();
            sqlDataAdapter.Fill(dataTableCars);
            grdCntrlCarShop.DataSource = dataTableCars;
         
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetCars();
            lblAdCount.Text = crdVwImage.RowCount.ToString();
            MessageBox.Show("Axtaris bitdi", "Xəbərdarlıq bitdi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

       
    }
}
