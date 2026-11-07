namespace ATMS
{
    partial class FeeCollectionControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            txtSearchAdmNo = new TextBox();
            btnSearch = new Button();
            lblStudentInfo = new Label();
            panel2 = new Panel();
            lblSelectedFee = new Label();
            label2 = new Label();
            txtPaymentAmount = new TextBox();
            btnPay = new Button();
            gridFees = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridFees).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lblStudentInfo);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(txtSearchAdmNo);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1000, 80);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 5);
            label1.Name = "label1";
            label1.Size = new Size(103, 17);
            label1.TabIndex = 0;
            label1.Text = "Search Adm No:";
            // 
            // txtSearchAdmNo
            // 
            txtSearchAdmNo.Location = new Point(109, 2);
            txtSearchAdmNo.Name = "txtSearchAdmNo";
            txtSearchAdmNo.Size = new Size(100, 25);
            txtSearchAdmNo.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(134, 33);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // lblStudentInfo
            // 
            lblStudentInfo.AutoSize = true;
            lblStudentInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblStudentInfo.Location = new Point(252, 10);
            lblStudentInfo.Name = "lblStudentInfo";
            lblStudentInfo.Size = new Size(131, 17);
            lblStudentInfo.TabIndex = 1;
            lblStudentInfo.Text = "No student selected";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnPay);
            panel2.Controls.Add(txtPaymentAmount);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(lblSelectedFee);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 570);
            panel2.Name = "panel2";
            panel2.Size = new Size(1000, 80);
            panel2.TabIndex = 1;
            // 
            // lblSelectedFee
            // 
            lblSelectedFee.AutoSize = true;
            lblSelectedFee.Location = new Point(0, 0);
            lblSelectedFee.Name = "lblSelectedFee";
            lblSelectedFee.Size = new Size(209, 17);
            lblSelectedFee.TabIndex = 0;
            lblSelectedFee.Text = "Select a pending fee from the grid";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 26);
            label2.Name = "label2";
            label2.Size = new Size(75, 17);
            label2.TabIndex = 1;
            label2.Text = "Amount (₹):";
            // 
            // txtPaymentAmount
            // 
            txtPaymentAmount.Location = new Point(81, 26);
            txtPaymentAmount.Name = "txtPaymentAmount";
            txtPaymentAmount.Size = new Size(100, 25);
            txtPaymentAmount.TabIndex = 2;
            // 
            // btnPay
            // 
            btnPay.BackColor = Color.LightGreen;
            btnPay.Location = new Point(187, 23);
            btnPay.Name = "btnPay";
            btnPay.Size = new Size(131, 23);
            btnPay.TabIndex = 3;
            btnPay.Text = "Process Payment";
            btnPay.UseVisualStyleBackColor = false;
            // 
            // gridFees
            // 
            gridFees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridFees.Dock = DockStyle.Fill;
            gridFees.Location = new Point(0, 80);
            gridFees.Name = "gridFees";
            gridFees.Size = new Size(1000, 490);
            gridFees.TabIndex = 4;
            // 
            // FeeCollectionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(gridFees);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FeeCollectionControl";
            Size = new Size(1000, 650);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridFees).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblStudentInfo;
        private Button btnSearch;
        private TextBox txtSearchAdmNo;
        private Label label1;
        private Panel panel2;
        private Button btnPay;
        private TextBox txtPaymentAmount;
        private Label label2;
        private Label lblSelectedFee;
        private DataGridView gridFees;
    }
}
