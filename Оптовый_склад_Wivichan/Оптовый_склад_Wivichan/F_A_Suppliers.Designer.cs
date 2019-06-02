namespace Оптовый_склад_Wivichan
{
    partial class F_A_Suppliers
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.addSup = new System.Windows.Forms.Button();
            this.editSup = new System.Windows.Forms.Button();
            this.delSup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
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
            this.dataGrid.Size = new System.Drawing.Size(685, 325);
            this.dataGrid.TabIndex = 0;
            // 
            // addSup
            // 
            this.addSup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addSup.Location = new System.Drawing.Point(12, 21);
            this.addSup.Name = "addSup";
            this.addSup.Size = new System.Drawing.Size(97, 47);
            this.addSup.TabIndex = 1;
            this.addSup.Text = "Добавить";
            this.addSup.UseVisualStyleBackColor = true;
            // 
            // editSup
            // 
            this.editSup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editSup.Location = new System.Drawing.Point(115, 21);
            this.editSup.Name = "editSup";
            this.editSup.Size = new System.Drawing.Size(97, 47);
            this.editSup.TabIndex = 2;
            this.editSup.Text = "Изменить";
            this.editSup.UseVisualStyleBackColor = true;
            // 
            // delSup
            // 
            this.delSup.BackColor = System.Drawing.Color.Coral;
            this.delSup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delSup.Location = new System.Drawing.Point(600, 21);
            this.delSup.Name = "delSup";
            this.delSup.Size = new System.Drawing.Size(97, 47);
            this.delSup.TabIndex = 3;
            this.delSup.Text = "Удалить";
            this.delSup.UseVisualStyleBackColor = false;
            // 
            // F_A_Suppliers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 425);
            this.Controls.Add(this.delSup);
            this.Controls.Add(this.editSup);
            this.Controls.Add(this.addSup);
            this.Controls.Add(this.dataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "F_A_Suppliers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поставщики";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button addSup;
        private System.Windows.Forms.Button editSup;
        private System.Windows.Forms.Button delSup;
    }
}