using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgCifraBlocos
{
    public partial class Form1 : Form
    {
        private string inputPath;
        private string outputPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectInput_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputPath = openFileDialog.FileName;
                lblInputFile.Text = inputPath;
            }
        }

        private void btnSelectOutput_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputPath = saveFileDialog.FileName;
                lblOutputFile.Text = outputPath;
            }
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKey.Text))
            {
                MessageBox.Show("Informe uma chave (string).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(inputPath) || !File.Exists(inputPath))
            {
                MessageBox.Show("Selecione um arquivo de entrada válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(outputPath))
            {
                MessageBox.Show("Selecione um arquivo de saída.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnProcess.Enabled = false;
            progressBar.Value = 0;

            try
            {
                bool encrypt = rbtnEncrypt.Checked;
                await Task.Run(() => ProcessFile(inputPath, outputPath, txtKey.Text, encrypt));
                MessageBox.Show("Operação concluída com sucesso.", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnProcess.Enabled = true;
                progressBar.Value = 0;
            }
        }

        private void ProcessFile(string inputFile, string outputFile, string keyString, bool encrypt)
        {
            const int blockSize = 4; // 32 bits
            int rounds = 3; // requisito mínimo de 3 rodadas

            var cipher = new SimpleBlockCipher(keyString, rounds);

            byte[] inputBytes = File.ReadAllBytes(inputFile);

            if (encrypt)
            {
                // Padding simples: PKCS#7-like para 4 bytes
                int pad = blockSize - (inputBytes.Length % blockSize);
                if (pad == 0) pad = blockSize;
                byte[] padded = new byte[inputBytes.Length + pad];
                Buffer.BlockCopy(inputBytes, 0, padded, 0, inputBytes.Length);
                for (int i = inputBytes.Length; i < padded.Length; i++) padded[i] = (byte)pad;

                using (var outFs = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    int totalBlocks = padded.Length / blockSize;
                    for (int i = 0; i < totalBlocks; i++)
                    {
                        uint block = BitConverter.ToUInt32(padded, i * blockSize);
                        uint enc = cipher.EncryptBlock(block);
                        byte[] outb = BitConverter.GetBytes(enc);
                        outFs.Write(outb, 0, outb.Length);
                        UpdateProgress(i + 1, totalBlocks);
                    }
                }
            }
            else
            {
                if (inputBytes.Length % blockSize != 0) throw new InvalidOperationException("Arquivo de entrada corrompido (tamanho inválido).");

                byte[] outputBuffer = new byte[inputBytes.Length];
                int totalBlocks = inputBytes.Length / blockSize;
                for (int i = 0; i < totalBlocks; i++)
                {
                    uint block = BitConverter.ToUInt32(inputBytes, i * blockSize);
                    uint dec = cipher.DecryptBlock(block);
                    byte[] outb = BitConverter.GetBytes(dec);
                    Buffer.BlockCopy(outb, 0, outputBuffer, i * blockSize, blockSize);
                    UpdateProgress(i + 1, totalBlocks);
                }

                // Remove padding
                int pad = outputBuffer[outputBuffer.Length - 1];
                if (pad < 1 || pad > blockSize) throw new InvalidOperationException("Padding inválido ao decriptar.");
                for (int i = outputBuffer.Length - pad; i < outputBuffer.Length; i++) if (outputBuffer[i] != (byte)pad) throw new InvalidOperationException("Padding inválido ao decriptar.");

                byte[] finalOut = new byte[outputBuffer.Length - pad];
                Buffer.BlockCopy(outputBuffer, 0, finalOut, 0, finalOut.Length);
                File.WriteAllBytes(outputFile, finalOut);
            }
        }

        private void UpdateProgress(int completed, int total)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action(() =>
                {
                    progressBar.Value = Math.Min(progressBar.Maximum, (int)((completed / (double)total) * 100));
                }));
            }
            else
            {
                progressBar.Value = Math.Min(progressBar.Maximum, (int)((completed / (double)total) * 100));
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string msg = "Como usar:\n\n" +
                         "1) Informe uma chave (string) no campo 'Chave'.\n" +
                         "2) Clique em 'Selecionar arquivo' para escolher o arquivo de entrada.\n" +
                         "3) Clique em 'Salvar como...' para escolher o arquivo de saída.\n" +
                         "4) Selecione 'Encriptar' ou 'Decriptar'.\n" +
                         "5) Clique em 'Iniciar' para processar o arquivo.\n\n" +
                         "Observações:\n" +
                         "- O algoritmo processa blocos de 32 bits (4 bytes).\n" +
                         "- Ao encriptar é aplicado padding; ao decriptar o padding é removido automaticamente.";

            MessageBox.Show(msg, "Ajuda - Como usar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
