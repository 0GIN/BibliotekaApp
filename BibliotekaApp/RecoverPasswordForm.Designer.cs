namespace BibliotekaApp
{
    partial class RecoverPasswordForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecoverPasswordForm));
            label1 = new Label();
            txtEmail = new TextBox();
            btnSend = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(30, 30);
            label1.Name = "label1";
            label1.Size = new Size(174, 19);
            label1.TabIndex = 0;
            label1.Text = "Podaj swój adres e-mail:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(30, 60);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 25);
            txtEmail.TabIndex = 1;
            txtEmail.Text = "exaple@exaple.pl";
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.FromArgb(100, 149, 237);
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSend.ForeColor = Color.White;
            btnSend.Location = new Point(144, 113);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(90, 30);
            btnSend.TabIndex = 2;
            btnSend.Text = "Wyślij";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Gray;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(240, 113);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Anuluj";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click_1;
            // 
            // RecoverPasswordForm
            // 
            AcceptButton = btnSend;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(370, 170);
            Controls.Add(btnCancel);
            Controls.Add(btnSend);
            Controls.Add(txtEmail);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RecoverPasswordForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Odzyskiwanie hasła";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtEmail;
        private Button btnSend;
        private Button btnCancel;
    }
}
