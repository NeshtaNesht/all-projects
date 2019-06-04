namespace Оптовый_склад_Wivichan
{
    partial class F_A_Contracts
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addCont = new System.Windows.Forms.Button();
            this.editCont = new System.Windows.Forms.Button();
            this.delCont = new System.Windows.Forms.Button();
            this.radioActive = new System.Windows.Forms.RadioButton();
            this.radioDelete = new System.Windows.Forms.RadioButton();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // addCont
            // 
            this.addCont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addCont.Location = new System.Drawing.Point(12, 12);
            this.addCont.Name = "addCont";
            this.addCont.Size = new System.Drawing.Size(121, 60);
            this.addCont.TabIndex = 0;
            this.addCont.Text = "Добавить";
            this.addCont.UseVisualStyleBackColor = true;
            this.addCont.Click += new System.EventHandler(this.addCont_Click);
            // 
            // editCont
            // 
            this.editCont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editCont.Location = new System.Drawing.Point(139, 12);
            this.editCont.Name = "editCont";
            this.editCont.Size = new System.Drawing.Size(121, 60);
            this.editCont.TabIndex = 1;
            this.editCont.Text = "Изменить";
            this.editCont.UseVisualStyleBackColor = true;
            this.editCont.Click += new System.EventHandler(this.editCont_Click);
            // 
            // delCont
            // 
            this.delCont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delCont.Location = new System.Drawing.Point(430, 12);
            this.delCont.Name = "delCont";
            this.delCont.Size = new System.Drawing.Size(121, 60);
            this.delCont.TabIndex = 2;
            this.delCont.Text = "Удалить";
            this.delCont.UseVisualStyleBackColor = true;
            this.delCont.Click += new System.EventHandler(this.delCont_Click);
            // 
            // radioActive
            // 
            this.radioActive.AutoSize = true;
            this.radioActive.Checked = true;
            this.radioActive.Location = new System.Drawing.Point(12, 78);
            this.radioActive.Name = "radioActive";
            this.radioActive.Size = new System.Drawing.Size(125, 17);
            this.radioActive.TabIndex = 3;
            this.radioActive.TabStop = true;
            this.radioActive.Text = "Активные договора";
            this.radioActive.UseVisualStyleBackColor = true;
            // 
            // radioDelete
            // 
            this.radioDelete.AutoSize = true;
            this.radioDelete.Location = new System.Drawing.Point(12, 101);
            this.radioDelete.Name = "radioDelete";
            this.radioDelete.Size = new System.Drawing.Size(133, 17);
            this.radioDelete.TabIndex = 4;
            this.radioDelete.Text = "Удалённые договора";
            this.radioDelete.UseVisualStyleBackColor = true;
            this.radioDelete.CheckedChanged += new System.EventHandler(this.radioDelete_CheckedChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(12, 124);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(539, 236);
            this.dataGrid.TabIndex = 5;
            // 
            // F_A_Contracts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 372);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.radioDelete);
            this.Controls.Add(this.radioActive);
            this.Controls.Add(this.delCont);
            this.Controls.Add(this.editCont);
            this.Controls.Add(this.addCont);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "F_A_Contracts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Договора";
            this.Load += new System.EventHandler(this.F_A_Contracts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addCont;
        private System.Windows.Forms.Button editCont;
        private System.Windows.Forms.Button delCont;
        private System.Windows.Forms.RadioButton radioActive;
        private System.Windows.Forms.RadioButton radioDelete;
        public System.Windows.Forms.DataGridView dataGrid;
    }
}