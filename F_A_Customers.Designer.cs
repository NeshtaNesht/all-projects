namespace Оптовый_склад_Wivichan
{
    partial class F_A_Customers
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
            this.addCust = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.editCust = new System.Windows.Forms.Button();
            this.delCust = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // addCust
            // 
            this.addCust.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addCust.Location = new System.Drawing.Point(12, 35);
            this.addCust.Name = "addCust";
            this.addCust.Size = new System.Drawing.Size(97, 47);
            this.addCust.TabIndex = 2;
            this.addCust.Text = "Добавить";
            this.addCust.UseVisualStyleBackColor = true;
            this.addCust.Click += new System.EventHandler(this.addCust_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(12, 88);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.Size = new System.Drawing.Size(685, 326);
            this.dataGrid.TabIndex = 3;
            // 
            // editCust
            // 
            this.editCust.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editCust.Location = new System.Drawing.Point(115, 35);
            this.editCust.Name = "editCust";
            this.editCust.Size = new System.Drawing.Size(97, 47);
            this.editCust.TabIndex = 4;
            this.editCust.Text = "Изменить";
            this.editCust.UseVisualStyleBackColor = true;
            this.editCust.Click += new System.EventHandler(this.editCust_Click);
            // 
            // delCust
            // 
            this.delCust.BackColor = System.Drawing.Color.Coral;
            this.delCust.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delCust.Location = new System.Drawing.Point(600, 35);
            this.delCust.Name = "delCust";
            this.delCust.Size = new System.Drawing.Size(97, 47);
            this.delCust.TabIndex = 5;
            this.delCust.Text = "Удалить";
            this.delCust.UseVisualStyleBackColor = false;
            this.delCust.Click += new System.EventHandler(this.delCust_Click);
            // 
            // F_A_Customers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 426);
            this.Controls.Add(this.delCust);
            this.Controls.Add(this.editCust);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.addCust);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "F_A_Customers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Покупатели";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addCust;
        public System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button editCust;
        private System.Windows.Forms.Button delCust;
    }
}