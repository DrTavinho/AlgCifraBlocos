namespace AlgCifraBlocos
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtKey = new TextBox();
            lblKey = new Label();
            btnSelectInput = new Button();
            lblInputFile = new Label();
            btnSelectOutput = new Button();
            lblOutputFile = new Label();
            rbtnEncrypt = new RadioButton();
            rbtnDecrypt = new RadioButton();
            btnProcess = new Button();
            progressBar = new ProgressBar();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            btnHelp = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtKey
            // 
            txtKey.Location = new Point(57, 39);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(440, 23);
            txtKey.TabIndex = 1;
            // 
            // lblKey
            // 
            lblKey.AutoSize = true;
            lblKey.Location = new Point(9, 42);
            lblKey.Name = "lblKey";
            lblKey.Size = new Size(43, 15);
            lblKey.TabIndex = 0;
            lblKey.Text = "Chave:";
            // 
            // btnSelectInput
            // 
            btnSelectInput.Location = new Point(9, 77);
            btnSelectInput.Name = "btnSelectInput";
            btnSelectInput.Size = new Size(120, 27);
            btnSelectInput.TabIndex = 2;
            btnSelectInput.Text = "Selecionar arquivo";
            btnSelectInput.UseVisualStyleBackColor = true;
            btnSelectInput.Click += btnSelectInput_Click;
            // 
            // lblInputFile
            // 
            lblInputFile.AutoEllipsis = true;
            lblInputFile.Location = new Point(137, 77);
            lblInputFile.Name = "lblInputFile";
            lblInputFile.Size = new Size(440, 27);
            lblInputFile.TabIndex = 3;
            lblInputFile.Text = "Nenhum arquivo selecionado";
            lblInputFile.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSelectOutput
            // 
            btnSelectOutput.Location = new Point(9, 117);
            btnSelectOutput.Name = "btnSelectOutput";
            btnSelectOutput.Size = new Size(120, 27);
            btnSelectOutput.TabIndex = 4;
            btnSelectOutput.Text = "Salvar como...";
            btnSelectOutput.UseVisualStyleBackColor = true;
            btnSelectOutput.Click += btnSelectOutput_Click;
            // 
            // lblOutputFile
            // 
            lblOutputFile.AutoEllipsis = true;
            lblOutputFile.Location = new Point(137, 117);
            lblOutputFile.Name = "lblOutputFile";
            lblOutputFile.Size = new Size(440, 27);
            lblOutputFile.TabIndex = 5;
            lblOutputFile.Text = "Nenhum arquivo de saída";
            lblOutputFile.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // rbtnEncrypt
            // 
            rbtnEncrypt.AutoSize = true;
            rbtnEncrypt.Checked = true;
            rbtnEncrypt.Location = new Point(9, 157);
            rbtnEncrypt.Name = "rbtnEncrypt";
            rbtnEncrypt.Size = new Size(72, 19);
            rbtnEncrypt.TabIndex = 6;
            rbtnEncrypt.TabStop = true;
            rbtnEncrypt.Text = "Encriptar";
            rbtnEncrypt.UseVisualStyleBackColor = true;
            // 
            // rbtnDecrypt
            // 
            rbtnDecrypt.AutoSize = true;
            rbtnDecrypt.Location = new Point(97, 157);
            rbtnDecrypt.Name = "rbtnDecrypt";
            rbtnDecrypt.Size = new Size(73, 19);
            rbtnDecrypt.TabIndex = 7;
            rbtnDecrypt.Text = "Decriptar";
            rbtnDecrypt.UseVisualStyleBackColor = true;
            // 
            // btnProcess
            // 
            btnProcess.Location = new Point(9, 182);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(120, 30);
            btnProcess.TabIndex = 8;
            btnProcess.Text = "Iniciar";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(137, 182);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(440, 30);
            progressBar.TabIndex = 9;
            // 
            // btnHelp
            // 
            btnHelp.Location = new Point(505, 39);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(80, 24);
            btnHelp.TabIndex = 10;
            btnHelp.Text = "Ajuda";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 9);
            label1.Name = "label1";
            label1.Size = new Size(206, 15);
            label1.TabIndex = 11;
            label1.Text = "Por: Gustavo Rodrigues Muti Pacheco";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 227);
            Controls.Add(label1);
            Controls.Add(progressBar);
            Controls.Add(btnHelp);
            Controls.Add(btnProcess);
            Controls.Add(rbtnDecrypt);
            Controls.Add(rbtnEncrypt);
            Controls.Add(lblOutputFile);
            Controls.Add(btnSelectOutput);
            Controls.Add(lblInputFile);
            Controls.Add(btnSelectInput);
            Controls.Add(txtKey);
            Controls.Add(lblKey);
            Name = "Form1";
            Text = "Algoritmo de Cifra de Blocos - Inn Seguros";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Button btnSelectInput;
        private System.Windows.Forms.Label lblInputFile;
        private System.Windows.Forms.Button btnSelectOutput;
        private System.Windows.Forms.Label lblOutputFile;
        private System.Windows.Forms.RadioButton rbtnEncrypt;
        private System.Windows.Forms.RadioButton rbtnDecrypt;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btnHelp;
        private Label label1;
    }
}
