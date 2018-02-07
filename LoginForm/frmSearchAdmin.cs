﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;//NEEDED FOR FILE USE
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace LoginForm
{
    public partial class frmSearchAdmin : Form
    {

        public string connString = @"Data Source=DESKTOP-0V3BOG8\SQLEXPRESS;Initial Catalog=AddressBook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public string subCategory = "";

        int detailedTxtBxYCoordi = 0, detailedTxtBxLbl = 0, cmbErrorCategXCoordi = 0, lblErrorClassificationXCoordi = 0, cmbPathSelcXCoord = 0, lblPointErrorXCoord=0;

        //System.Drawing.Point detailedTxBxCoordi, ;
        

        SqlDataAdapter dataAdapter;
        System.Data.DataTable table;
        SqlCommandBuilder cmdBuilder;
        SqlConnection conn;

        public void EnableProcessGrpBx(bool enable)
        {
            this.grpBoxProcessData.Enabled = enable;
            if (enable)
            {
                this.grpBoxProcessData.Show();
            }
            else
            {
                this.grpBoxProcessData.Hide();
            }
        }

        public void EnableProductionDataGrpBx(bool enable)
        {
            this.grpSerialMatching.Enabled = enable;
            if (enable)
            {
                this.grpSerialMatching.Show();
            }
            else
            {
                this.grpSerialMatching.Hide();
            }
        }

        public void EnableDataSearchGrpBox(bool enable)
        {
            this.grpBoxDataSearch.Enabled = enable;
            if (enable)
            {
                this.grpBoxDataSearch.Show();
            }
            else
            {
                this.grpBoxDataSearch.Hide();
            }
        }

        public void initial_UI_Setting()
        {
            EnableProcessGrpBx(false);
            EnableProductionDataGrpBx(false);
            EnableDataSearchGrpBox(false);
            this.cmbModel.Items.AddRange(new string[] {"TB-3033C", "TB-3093C", "TB-3103C" });
            this.cmbCategory.Items.AddRange(new string[] { "공정 Data", "Serial Matching", "Data Search"});
         
        }

        public void grpBox_ProcessData_Initial_UI()
        {
            this.cmbSubCategory.Enabled = true;
            this.cmbPathSelection.Visible = false;
            this.cmbErrorCategory.Visible = false;
            this.dTPickerBurnStat.Visible = false;
            this.dTPickerBurnStop.Visible = false;
            this.cmbBurningYesNo.Visible = false;
            this.cmbTestItems.Visible = false;
            this.txtBxDetails.Visible = false;
            this.txtBxDCMSerial.Visible = false;
            this.txtBxGiftBxSerial.Visible = false;
            this.txtBxCartonBoxDCMSerial.Visible = false;
            this.dateTimePickerProductionDate.Visible = false;
            this.lblBurningTime.Hide();
            this.grpBoxFixRepairList.Visible = false;

            this.lblPointOfError.Visible = false;
            this.lblErrorClassification.Visible = false;
            this.lblStart.Visible = false;
            this.lblStop.Visible = false;
            this.lblBurningTime.Visible = false;
            this.lblBurningYesNo.Visible = false;
            this.lblTestItem.Visible = false;
            this.lblDetails.Visible = false;
            this.lblCartonBxDCMSerial.Visible = false;
            this.lblGiftBoxSerial.Visible = false;
            this.lblDCMSerial.Visible = false;
            this.lblProductionDate.Visible = false;
            

            //this.txtBxSerialNo.Clear();
            this.cmbSubCategory.Text = "";
            this.cmbPathSelection.Text = "";
            this.cmbErrorCategory.Text = "";
            this.txtBxDetails.Text = "";
            this.cmbBurningYesNo.Text = "";
            this.cmbTestItems.Text = "";
            this.txtBxDCMSerial.Text = "";
            this.txtBxGiftBxSerial.Text = "";
            this.txtBxCartonBoxDCMSerial.Text = "";
            //if (this.cmbPassFail.Text == "Pass"
        }
        public void grpBox_Shipping_Data()
        {
            this.cmbProductionDataSelection.Items.Clear();
            this.cmbProductionDataSelection.Items.AddRange(new string[] {"공정 Data","출하 Data"});
        }
        
        private void btnFail_Click(object sender, EventArgs e)
        {
            if (subCategory == "수삽")
            {
                grpBox_ProcessData_Initial_UI();
                grpBox_Soldering_UI();
            }
            else if (subCategory == "Tuning")
            {
               // subCategory = "Tuning";
                grpBox_ProcessData_Initial_UI();
                grpBox_Tuning_UI();
            }
            else if (subCategory == "반조립")
            {
                //subCategory = "반조립";
                grpBox_ProcessData_Initial_UI();
                partial_integ_UI();
            }
            else if (subCategory == "Table")
            {
                //subCategory = "반조립";
                grpBox_ProcessData_Initial_UI();
                table_fail_UI();
            }
            else if (subCategory == "Burning")
            {
                //subCategory = "Burning";
                grpBox_ProcessData_Initial_UI();
                Burning_UI();
            }
            else if (subCategory == "ATS1")
            {
               // subCategory = "ATS1"
                grpBox_ProcessData_Initial_UI();
                ATS_ICS_UI(1);
            }
            else if (subCategory == "ATS2")
            {
               // subCategory = "ATS2";
                grpBox_ProcessData_Initial_UI();
                ATS_ICS_UI(2);
            }
            else if (subCategory == "ICS1")
            {
               // subCategory = "ICS1";
                grpBox_ProcessData_Initial_UI();
                ATS_ICS_UI(3);
            }
            else if (subCategory == "ICS2")
            {
               // subCategory = "ICS2";
                grpBox_ProcessData_Initial_UI();
                ATS_ICS_UI(4);
            }
            else if (subCategory == "완조립")
            {
                //subCategory = "완조립";
                grpBox_ProcessData_Initial_UI();
                Full_Integ_UI();
            }
            else if (subCategory == "포장")
            {
               // subCategory = "포장";
                grpBox_ProcessData_Initial_UI();
                packaing_UI();
            }
            else if (subCategory == "출하")
            {
               // cmbSubCategory = "출하";
                grpBox_ProcessData_Initial_UI();
                Shipping_UI();
            }
            else if (subCategory == "수리")
            {
                //subCategory = "수리";
                grpBox_ProcessData_Initial_UI();
                grpBoxFix_Repair_UI();
            }
        }

        public void grpBox_Soldering_UI()
        {
            //this.cmbSubCategory.Enabled = true;
            this.txtBxDetails.Visible = true;
            this.lblDetails.Visible = true;
            this.lblDetails.Top = this.lblPointOfError.Location.Y;
            this.txtBxDetails.Top = this.cmbPathSelection.Location.Y;
        }

        public void grpBox_Tuning_UI()
        {
           
            this.txtBxDetails.Visible = true;
            this.lblDetails.Visible = true;

            this.txtBxDetails.Top = detailedTxtBxYCoordi;
            this.lblDetails.Top = detailedTxtBxLbl;

            this.cmbPathSelection.Visible = true;
            this.lblPointOfError.Visible = true;
            this.cmbErrorCategory.Visible = true;
            this.lblErrorClassification.Visible = true;

            this.cmbErrorCategory.Left = cmbErrorCategXCoordi;
            this.lblErrorClassification.Left = lblErrorClassificationXCoordi;

            this.cmbErrorCategory.Items.Clear();
            this.cmbErrorCategory.Items.AddRange(new string[] {"Gain", "VSWR", "etc" });
           // this.cmbErrorCategory.Visible = true;
        }

        public void partial_integ_UI()
        {
            this.cmbErrorCategory.Visible = true;
            this.lblErrorClassification.Visible = true;

            this.cmbErrorCategory.Left = this.cmbPathSelection.Location.X;
            this.lblErrorClassification.Left = this.lblPointOfError.Location.X+20;

            this.cmbErrorCategory.Items.Clear();
            this.cmbErrorCategory.Items.AddRange(new string[] { "etc" });
            this.lblDetails.Visible = true;
            //this.lblBurningYesNo.Visible = true;
            this.txtBxDetails.Visible = true;
            this.lblDetails.Visible = true;
        }

        public void table_fail_UI()
        {
            this.txtBxDetails.Visible = true;
            this.lblDetails.Visible = true;
            this.cmbPathSelection.Visible = true;
            this.lblPointOfError.Visible = true;

            this.cmbErrorCategory.Left = cmbErrorCategXCoordi;
            this.lblErrorClassification.Left = lblErrorClassificationXCoordi;

        }

        public void Burning_UI()
        {
            this.dTPickerBurnStat.Visible = true;
            this.lblStart.Visible = true;
            this.dTPickerBurnStop.Visible = true;
            this.lblStop.Visible = true;
            this.cmbBurningYesNo.Visible = true;
            this.lblBurningYesNo.Visible = true;
            this.lblBurningTime.Visible = true;
        }

        public void ATS_ICS_UI(int ATSICS)
        {
            this.cmbTestItems.Visible = true;
            this.lblTestItem.Visible = true;
            this.txtBxDetails.Visible = true;
            this.lblDetails.Visible = true;

            this.lblTestItem.Top = this.lblBurningYesNo.Location.Y;
            this.cmbTestItems.Top = this.cmbBurningYesNo.Location.Y;
        }
        public void Full_Integ_UI()
        {
            this.cmbErrorCategory.Visible = true;
            this.lblErrorClassification.Visible = true;
            this.txtBxDetails.Visible = true;
            this.lblDetails.Visible = true;

            this.lblErrorClassification.Left = lblPointErrorXCoord+20;
            this.cmbErrorCategory.Left = cmbPathSelcXCoord;
        }

        public void packaing_UI()
        {
            this.txtBxDCMSerial.Visible = true;
            this.lblCartonBxDCMSerial.Visible = true;
            this.txtBxGiftBxSerial.Visible = true;
            this.lblGiftBoxSerial.Visible = true;
            this.txtBxCartonBoxDCMSerial.Visible = true;
            this.lblDCMSerial.Visible = true;

            this.lblDCMSerial.Top = this.lblProductionDate.Location.Y;
            this.lblCartonBxDCMSerial.Top = this.lblProductionDate.Location.Y;
            this.lblGiftBoxSerial.Top = this.lblProductionDate.Location.Y;

            this.txtBxDCMSerial.Top = this.dateTimePickerProductionDate.Location.Y;
            this.txtBxGiftBxSerial.Top = this.dateTimePickerProductionDate.Location.Y;
            this.txtBxCartonBoxDCMSerial.Top = this.dateTimePickerProductionDate.Location.Y;

        }

        public void Shipping_UI()
        {
            this.txtBxCartonBoxDCMSerial.Visible = true;
            this.lblCartonBxDCMSerial.Visible = true;
            this.dateTimePickerProductionDate.Visible = true;
            this.lblProductionDate.Visible = true ;

            this.lblCartonBxDCMSerial.Top = this.lblProductionDate.Location.Y;
            this.txtBxCartonBoxDCMSerial.Top = this.dateTimePickerProductionDate.Location.Y;
        }

        public void grpBoxFix_Repair_UI()
        {
            this.grpBoxFixRepairList.Visible = true;
            this.lblProductionDate.Visible = true;
            this.dateTimePickerProductionDate.Visible = true;
        }

        private void cmbSubCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cmbSubCategory.Text == "수삽")
            {
                subCategory = "수삽";
                grpBox_ProcessData_Initial_UI();
                //grpBox_Soldering_UI();
            }
            else if (this.cmbSubCategory.Text == "Tuning")
            {
                subCategory = "Tuning";
                grpBox_ProcessData_Initial_UI();
                //grpBox_Tuning_UI();
            }
            else if (this.cmbSubCategory.Text == "Table")
            {
                subCategory = "Table";
                grpBox_ProcessData_Initial_UI();
                //grpBox_Tuning_UI();
            }
            else if (this.cmbSubCategory.Text == "반조립")
            {
                subCategory = "반조립";
                grpBox_ProcessData_Initial_UI();
                //partial_integ_UI();
            }
            else if (this.cmbSubCategory.Text == "Burning")
            {
                subCategory = "Burning";
                grpBox_ProcessData_Initial_UI();
                //Burning_UI();
            }
            else if (this.cmbSubCategory.Text == "ATS1")
            {
                subCategory = "ATS1";
                grpBox_ProcessData_Initial_UI();
                //ATS_ICS_UI(1);
            }
            else if (this.cmbSubCategory.Text == "ATS2")
            {
                subCategory = "ATS2";
                grpBox_ProcessData_Initial_UI();
                //ATS_ICS_UI(2);
            }
            else if (this.cmbSubCategory.Text == "ICS1")
            {
                subCategory = "ICS1";
                grpBox_ProcessData_Initial_UI();
                //ATS_ICS_UI(3);
            }
            else if (this.cmbSubCategory.Text == "ICS2")
            {
                subCategory = "ICS2";
                grpBox_ProcessData_Initial_UI();
                //ATS_ICS_UI(4);
            }
            else if (this.cmbSubCategory.Text == "완조립")
            {
                subCategory = "완조립";
                grpBox_ProcessData_Initial_UI();
                //Full_Integ_UI();
            }
            else if (this.cmbSubCategory.Text == "포장")
            {
                subCategory = "포장";
                grpBox_ProcessData_Initial_UI();
                //packaing_UI();
            }
            else if (this.cmbSubCategory.Text == "출하")
            {
                subCategory = "출하";
                grpBox_ProcessData_Initial_UI();
                //Shipping_UI();
            }
            else if (this.cmbSubCategory.Text == "수리")
            {
                subCategory = "수리";
                grpBox_ProcessData_Initial_UI();
                //grpBoxFix_Repair_UI();
            }
        }
        private void cmbCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cmbCategory.Text == "공정 Data")
            {
                EnableProcessGrpBx(true);
                EnableProductionDataGrpBx(false);
                EnableDataSearchGrpBox(false);
                grpBox_ProcessData_Initial_UI();
            }
            else if (this.cmbCategory.Text == "Serial Matching")
            {
                EnableProcessGrpBx(false);
                EnableProductionDataGrpBx(true);
                EnableDataSearchGrpBox(false);
                grpBox_Shipping_Data();
            }
            else if (this.cmbCategory.Text == "Data Search")
            {
                EnableProcessGrpBx(false);
                EnableProductionDataGrpBx(false);
                EnableDataSearchGrpBox(true);
            }
        }

        string selectionStatement = "select * from BizContacts";

        public frmSearchAdmin()
        {
            InitializeComponent();
            
            initial_UI_Setting();
            detailedTxtBxYCoordi = txtBxDetails.Location.Y;
            detailedTxtBxLbl = lblDetails.Location.Y;
            cmbErrorCategXCoordi = cmbErrorCategory.Location.X;
            lblErrorClassificationXCoordi = lblErrorClassification.Location.X;

            cmbPathSelcXCoord = this.cmbPathSelection.Location.X;
            lblPointErrorXCoord = this.lblPointOfError.Location.X;
        }

        private void frmLoggedIn_Load(object sender, EventArgs e)
        {
            cmbBxSrch.SelectedIndex = 0;
            dataGridView1.DataSource = bindingSource1;

            GetData(selectionStatement);    
         }

        private void GetData(string queryCmd)
        {
            try {
                dataAdapter = new SqlDataAdapter(queryCmd, connString);
                table = new System.Data.DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;
                dataGridView1.Columns[0].ReadOnly = true;
            } catch (SqlException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmdObj;
            string insert = @"insert into BizContacts(Date_Added, Company, Website, Title, First_Name, Last_Name, Address, City, State, Postal_Code, Email, Mobile, Notes, Image)
                            values(@Date_Added, @Company, @Website, @Title, @First_Name, @Last_Name, @Address, @City, @State, @Postal_Code, @Email, @Mobile, @Notes, @Image)";

            using (SqlConnection conn = new SqlConnection(connString))
            {

                try
                {
                    conn.Open();
                    cmdObj = new SqlCommand(insert, conn);
                    cmdObj.Parameters.AddWithValue(@"Date_Added", dateTimePicker1.Value.Date);
                    cmdObj.Parameters.AddWithValue(@"Company", txtBxComp.Text);
                    cmdObj.Parameters.AddWithValue(@"Website", txtBxWWW.Text);
                    cmdObj.Parameters.AddWithValue(@"Title", txtBxTitle.Text);
                    cmdObj.Parameters.AddWithValue(@"First_Name", txtBxFstName.Text);
                    cmdObj.Parameters.AddWithValue(@"Last_Name", txtBxLstName.Text);
                    cmdObj.Parameters.AddWithValue(@"Address", txtBxAddr.Text);
                    cmdObj.Parameters.AddWithValue(@"City", txtBxCity.Text);
                    cmdObj.Parameters.AddWithValue(@"State", txtBxState.Text);
                    cmdObj.Parameters.AddWithValue(@"Postal_Code", txtBxZipCod.Text);
                    cmdObj.Parameters.AddWithValue(@"Email", txtBxEmail.Text);
                    cmdObj.Parameters.AddWithValue(@"Mobile", txtBxMPhone.Text);
                    cmdObj.Parameters.AddWithValue(@"Notes", txtBxNot.Text);
                    if (openImageFileDialog.FileName != "")
                        cmdObj.Parameters.AddWithValue(@"Image", File.ReadAllBytes(openImageFileDialog.FileName));//Converts images to bytes for saving. 
                    else
                        cmdObj.Parameters.Add(@"Image", SqlDbType.VarBinary).Value = DBNull.Value;

                    
                    cmdObj.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
                GetData(selectionStatement);
                dataGridView1.Update();
            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            cmdBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
            try
            {
                bindingSource1.EndEdit();
                dataAdapter.Update(table);
                MessageBox.Show("Update Successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentCell.OwningRow;
            string value = row.Cells["ID"].Value.ToString();
            string fname = row.Cells["First_Name"].Value.ToString();
            string lname = row.Cells["Last_Name"].Value.ToString();
            DialogResult result = MessageBox.Show("Are you sure to delete it?\n" + fname + " " + lname + ", record " + value, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            string deleteState = @"Delete from BizContacts where id = '" + value + "'";

            if (result == DialogResult.Yes)
            {
                using (conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand commd = new SqlCommand(deleteState, conn);
                        commd.ExecuteNonQuery();
                        GetData(selectionStatement);
                        dataGridView1.Update();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            switch (cmbBxSrch.SelectedItem.ToString()) {
                case "First Name":
                    GetData("select * from bizcontacts where lower(first_name) like '%" + txtBxSrch.Text.ToLower() + "%'");
                    break;
                case "Last Name":
                    GetData("select * from bizcontacts where lower(last_name) like '%" + txtBxSrch.Text.ToLower() + "%'");
                    break;
                case "Company":
                    GetData("select * from bizcontacts where lower(company) like '%" + txtBxSrch.Text.ToLower() + "%'");
                    break;


            }
        }

        private void btnGetImage_Click(object sender, EventArgs e)
        {
            if(openImageFileDialog.ShowDialog() == DialogResult.OK)
                pictureBox1.Load(openImageFileDialog.FileName);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new Form();
            try
            {
                frm.BackgroundImage = pictureBox1.Image;
                frm.Size = pictureBox1.Image.Size;
                frm.Show();

            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("No image selected");
            }
            
           
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            _Application excel = new Microsoft.Office.Interop.Excel.Application();
            _Workbook workBook = excel.Workbooks.Add(Type.Missing); //Make a work book.
            _Worksheet workSheet = null;
            try
            {
                workSheet = workBook.ActiveSheet;
                workSheet.Name = "Business contacts";

                for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count - 1; rowIndex++) {
                    for (int colIndex = 0; colIndex < dataGridView1.Columns.Count; colIndex++) {
                        if (rowIndex == 0)
                        {
                            workSheet.Cells[rowIndex + 1, colIndex + 1] = dataGridView1.Columns[colIndex].HeaderText;
                        }
                        else
                        {
                            workSheet.Cells[rowIndex + 1, colIndex + 1] = dataGridView1.Rows[rowIndex-1].Cells[colIndex].Value.ToString();
                        }
                    }
                }
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    workBook.SaveAs(saveFileDialog1.FileName);
                    Process.Start("excel.exe", saveFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workSheet = null;
                workBook = null;
            }
             
        }

       
    }
}
