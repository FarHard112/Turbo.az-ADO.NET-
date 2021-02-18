using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turbo.az
{
    public partial class AddAdvertise : Form
    {
        string phoneNumber = "";
        
        SqlUtils SqlUtils = SqlUtils.GetInstance();

        ClssInfoAdapter clssInfoAdapter = new ClssInfoAdapter();
        ClssCommonMethods clssCommonMethods = new ClssCommonMethods();
        public AddAdvertise()
        {
            InitializeComponent();
        }
        public AddAdvertise(string phoneNumber)
        {
            InitializeComponent();
            this.phoneNumber = phoneNumber;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
            
            lblPhoneNumber.Text = $"Siz +{phoneNumber} nömrəsindən elan yerləşdirirsiniz ";

            #region SetDataToComponents
            clssCommonMethods.SetBrand(lookeditMarka);
            clssCommonMethods.SetLookUpEditTypeId(lookUpCarBody, "1");
            clssCommonMethods.SetLookUpEditTypeId(lookUpColor, "2");
            clssCommonMethods.SetLookUpEditTypeId(lookUpFuel, "3");
            clssCommonMethods.SetLookUpEditTypeId(lookUpTransmission, "5");
            clssCommonMethods.SetLookUpEditTypeId(lookeditCities, "6");
            clssCommonMethods.SetLookUpEditTypeId(lookUpValute, "7");
            clssCommonMethods.SetLookUpEditTypeId(lookUpGear, "4");
            clssCommonMethods.SetLookUpEditTypeId(lookUpMakeDate, "8");
            clssCommonMethods.SetLookUpEditTypeId(lookUpEngineVolume, "9");
            gridCntrPictures.DataSource = clssInfoAdapter.GetImage("-1");
            #endregion
   

    }
        protected override Point ScrollToControl(Control activeControl)
        {
            return this.AutoScrollPosition;
        }
        private void lookeditMarka_EditValueChanged(object sender, EventArgs e)
        {
            clssCommonMethods.SetModel(lookeditModel, lookeditMarka);         
        }

        void InsertAdsImages(SqlTransaction sqlTransaction, string adsId)
        {
            DataTable dtTableImages = (DataTable)gridCntrPictures.DataSource;

            for (int i = 0; i < dtTableImages.Rows.Count; i++)
            {
                DataRow dataRowImage = dtTableImages.Rows[i];
                string query = @"INSERT INTO [dbo].[ADS_IMAGES] 
            ([CAR_IMAGE],[ADS_ID])
              VALUES
            (@CAR_IMAGE,@ADS_ID)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlTransaction.Connection);
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Add("CAR_IMAGE", SqlDbType.VarBinary).Value = dataRowImage["CAR_IMAGE"];
                sqlCommand.Parameters.Add("ADS_ID", SqlDbType.Int).Value = adsId;
                sqlCommand.ExecuteNonQuery();
            }     
        }


        bool ComponentEmpty()
        {
            bool control = true;
            
            if (lookeditMarka.EditValue == null)
            {
                lookeditMarka.ErrorText = "Markanı daxil edin";
                control = false;
            }
            if (lookeditModel.EditValue == null)
            {
                lookeditModel.ErrorText = "Modeli daxil edin";
                control = false;
            }
            if (lookUpCarBody.EditValue == null)
            {
                lookUpCarBody.ErrorText = "Ban növünü daxil edin";
                control = false;
            }
            if (lookUpColor.EditValue == null)
            {
                lookUpColor.ErrorText = "Rəngi daxil edin";
                control = false;
            }
            if (lookUpFuel.EditValue == null)
            {
                lookUpFuel.ErrorText = "Yanacaq növünü daxil edin";
                control = false;
            }
            if (lookUpTransmission.EditValue == null)
            {
                lookUpTransmission.ErrorText = "Ötürücünü daxil edin";
                control = false;
            }
            if (lookUpGear.EditValue == null)
            {
                lookUpGear.ErrorText = "Sürətlər qutusunu daxil edin";
                control = false;
            }
            if (lookUpMakeDate.EditValue == null)
            {
                lookUpMakeDate.ErrorText = "Buraxılış ilini  daxil edin";
                control = false;
            }
            if (lookUpEngineVolume.EditValue == null)
            {
                lookUpEngineVolume.ErrorText = "Mühərrikin həcmini daxil edin";
                control = false;
            }
            if (lookeditCities.EditValue == null)
            {
                lookeditCities.ErrorText = "Şəhəri daxil edin";
                control = false;
            }
            if (txtMail.Text == "")
            {
                txtMail.ErrorText = "Mail daxil edin";
                control = false;
            }
            if (txtName.Text == "")
            {
                txtName.ErrorText = "Adınızı daxil edin";
                control = false;
            }
            if (txtPrice.Text == "")
            {
                txtPrice.ErrorText = "Qiymət daxil edin";
                control = false;
            }
            if (crdViewImage.DataRowCount < 3)
            {
                MessageBox.Show("Ən azı 3 şəkil daxil edin");
                control = false;
            }
            
            return control;


        }


        private void btn_Placeing_Click(object sender, EventArgs e)
        {

            if (ComponentEmpty())
            {
                if(MessageBox.Show("Elanı yerləşdirmək istədiyinizdən əminsinizmi ?", "Sual", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    InsertAllInfo();
                }
              
            }
            
        }  

        private void InsertAllInfo()
        {
            SqlTransaction sqlTransaction = null;
     try { 

            SqlConnection sqlConnection = new SqlConnection(SqlUtils.GetInstance().conString);
            sqlConnection.Open();
             sqlTransaction = sqlConnection.BeginTransaction();
            string insertedID = InsertAds(sqlTransaction);
            InsertAdsImages(sqlTransaction, insertedID);
                sqlTransaction.Commit();
            sqlConnection.Close();


            MessageBox.Show("Melumat yadda saxlanildi");
            frmHomePage frmHomePage = new frmHomePage();
            frmHomePage.Show();
            this.Close();
         }
            catch(Exception ex)
            {
                sqlTransaction.Rollback();
                MessageBox.Show(ex.Message);

            }
           

        }
        
        string InsertAds(SqlTransaction sqlTransaction) {

           
                string query = @"INSERT INTO [dbo].[ADVERTISE]
           ([MODEL_ID]
           ,[BODY_ID]
           ,[COLOR_ID]
           ,[MILAGE]
           ,[PRICE]
           ,[CREDIT]
           ,[BARTER]
           ,[FUEL_ID]
           ,[TRANSMISSION_ID]
           ,[GEAR_ID]
           ,[MAKE_DATE]
           ,[ENGINE_VOLUME]
           ,[ENGINE_POWER]
           ,[MORE_INFO]
           ,[ALLOY_WHEELS]
           ,[CENTRAL_LOCKING]
           ,[LEATHER_SALON]
           ,[VENTILATION_SEATS]
           ,[ABS]
           ,[PARK_RADAR]
           ,[XENON_LAMPS]
           ,[HATCH]
           ,[CONDISIONER]
           ,[REAR_VIEW_CAMERA]
           ,[NAME]
           ,[CITIES_ID]
           ,[VALUTE]
           ,[MAIL]
          )
     VALUES
           (@MODEL_ID
           ,@BODY_ID 
           ,@COLOR_ID
           ,@MILAGE
           ,@PRICE 
           ,@CREDIT
           ,@BARTER
           ,@FUEL_ID
           ,@TRANSMISSION_ID
           ,@GEAR_ID
           ,@MAKE_DATE
           ,@ENGINE_VOLUME
           ,@ENGINE_POWER
           ,@MORE_INFO
           ,@ALLOY_WHEELS
           ,@CENTRAL_LOCKING
           ,@LEATHER_SALON
           ,@VENTILATION_SEATS
           ,@ABS
           ,@PARK_RADAR
           ,@XENON_LAMPS 
           ,@HATCH
           ,@CONDISIONER
           ,@REAR_VIEW_CAMERA
           ,@NAME
           ,@CITIES_ID
           ,@VALUTE
           ,@MAIL ) ; SELECT SCOPE_IDENTITY();";

              
                SqlCommand sqlCommand = new SqlCommand(query, sqlTransaction.Connection);
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Add("MODEL_ID", SqlDbType.Int).Value = lookeditModel.EditValue;
                sqlCommand.Parameters.Add("BODY_ID", SqlDbType.Int).Value = lookUpCarBody.EditValue;
                sqlCommand.Parameters.Add("COLOR_ID", SqlDbType.Int).Value = lookUpColor.EditValue;
                sqlCommand.Parameters.Add("MILAGE", SqlDbType.Int).Value = numericMileage.Value;
                sqlCommand.Parameters.Add("PRICE", SqlDbType.Int).Value = txtPrice.EditValue;
                sqlCommand.Parameters.Add("CREDIT", SqlDbType.Bit).Value = checkCredit.Checked;
                sqlCommand.Parameters.Add("BARTER", SqlDbType.Bit).Value = checkBarter.Checked;
                sqlCommand.Parameters.Add("FUEL_ID", SqlDbType.Int).Value = lookUpFuel.EditValue;
                sqlCommand.Parameters.Add("TRANSMISSION_ID", SqlDbType.Int).Value = lookUpTransmission.EditValue;
                sqlCommand.Parameters.Add("GEAR_ID", SqlDbType.Int).Value = lookUpGear.EditValue;
                sqlCommand.Parameters.Add("MAKE_DATE", SqlDbType.Int).Value = lookUpMakeDate.EditValue;
                sqlCommand.Parameters.Add("ENGINE_VOLUME", SqlDbType.Int).Value = lookUpEngineVolume.EditValue;
                sqlCommand.Parameters.Add("ENGINE_POWER", SqlDbType.Int).Value = numericEnginePower.Value;
                sqlCommand.Parameters.Add("MORE_INFO", SqlDbType.NVarChar).Value = richTxtMoreİnfo.Text;
                sqlCommand.Parameters.Add("ALLOY_WHEELS", SqlDbType.Bit).Value = checkAlloyWheels.Checked;
                sqlCommand.Parameters.Add("CENTRAL_LOCKING", SqlDbType.Bit).Value = checkCentralLocking.Checked;
                sqlCommand.Parameters.Add("LEATHER_SALON", SqlDbType.Bit).Value = checkLeatherSalon.Checked;
                sqlCommand.Parameters.Add("VENTILATION_SEATS", SqlDbType.Bit).Value = checkVentilationSeats.Checked;
                sqlCommand.Parameters.Add("ABS", SqlDbType.Bit).Value = checkABS.Checked;
                sqlCommand.Parameters.Add("PARK_RADAR", SqlDbType.Bit).Value = checkParkRadar.Checked;
                sqlCommand.Parameters.Add("XENON_LAMPS", SqlDbType.Bit).Value = checkXenonLamps.Checked;
                sqlCommand.Parameters.Add("HATCH", SqlDbType.Bit).Value = checkHatch.Checked;
                sqlCommand.Parameters.Add("CONDISIONER", SqlDbType.Bit).Value = checkCondisioner.Checked;
                sqlCommand.Parameters.Add("REAR_VIEW_CAMERA", SqlDbType.Bit).Value = checkRearViewCamera.Checked;
                sqlCommand.Parameters.Add("NAME", SqlDbType.NVarChar).Value = txtName.Text;
                sqlCommand.Parameters.Add("CITIES_ID", SqlDbType.Int).Value = lookeditCities.EditValue;
                 sqlCommand.Parameters.Add("VALUTE", SqlDbType.Int).Value = lookUpValute.EditValue;
                sqlCommand.Parameters.Add("MAIL", SqlDbType.NVarChar).Value = txtMail.Text;
            
            
               
                return sqlCommand.ExecuteScalar().ToString();           
           
        }

        private void groupCntrPictures_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button == groupCntrPictures.CustomHeaderButtons[0])
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                DataTable dataTable = (DataTable)gridCntrPictures.DataSource;
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach(string fileName in openFileDialog.FileNames)
                    {
                        dataTable.Rows.Add(0, GetByteImage(fileName));
                        GetByteImage(fileName);
                    }
                }
            }
        }

        private Byte[] GetByteImage(string fileName)
        {
            byte[] imageByteArray = null;
            FileStream fileStream = new FileStream(fileName,FileMode.Open,FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            imageByteArray = binaryReader.ReadBytes((int)fileStream.Length);
            binaryReader.Close();
            fileStream.Close();
            return imageByteArray;
        }

       

        
    }
}
