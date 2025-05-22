namespace KooliProjekt.WinFormsApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView TodoListsGrid;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.TextBox IdField;
        private System.Windows.Forms.TextBox TitleField;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TodoListsGrid = new System.Windows.Forms.DataGridView();
            this.NewButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.IdField = new System.Windows.Forms.TextBox();
            this.TitleField = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.TodoListsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TodoListsGrid
            // 
            this.TodoListsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TodoListsGrid.Location = new System.Drawing.Point(12, 12);
            this.TodoListsGrid.Name = "TodoListsGrid";
            this.TodoListsGrid.Size = new System.Drawing.Size(776, 150);
            this.TodoListsGrid.TabIndex = 0;
            // 
            // NewButton
            // 
            this.NewButton.Location = new System.Drawing.Point(12, 168);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(75, 23);
            this.NewButton.TabIndex = 1;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(93, 168);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(174, 168);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 3;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // IdField
            // 
            this.IdField.Location = new System.Drawing.Point(12, 197);
            this.IdField.Name = "IdField";
            this.IdField.Size = new System.Drawing.Size(100, 20);
            this.IdField.TabIndex = 4;
            // 
            // TitleField
            // 
            this.TitleField.Location = new System.Drawing.Point(118, 197);
            this.TitleField.Name = "TitleField";
            this.TitleField.Size = new System.Drawing.Size(100, 20);
            this.TitleField.TabIndex = 5;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TitleField);
            this.Controls.Add(this.IdField);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.TodoListsGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TodoListsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
