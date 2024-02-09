using static System.Net.Mime.MediaTypeNames;

namespace DESProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string inputFilePath;
        private void OpenSourceFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFilePath = openFileDialog.FileName;
            }
        }

        private void ApplyDESBtn_Click(object sender, EventArgs e)
        {
            bool success = true;
            if (inputFilePath == null)
            {
                MessageBox.Show("File is not selected!");
                success = false;
            }
            if (!ulong.TryParse(EnterKeyTextbox.Text, out ulong key))
            {
                MessageBox.Show("Key is not specified!");
                success = false;
            }
            if (!EncryptionModeEncryptRadioBtn.Checked && !EncryptionModeDecryptRadioBtn.Checked)
            {
                MessageBox.Show("Encoding mode is not specified!");
                success = false;
            }
            if (!success)
                return;
            DESEncryptor encryptor = new DESEncryptor();
            DESEncryptor.EncryptionMode mode = EncryptionModeEncryptRadioBtn.Checked ? DESEncryptor.EncryptionMode.Encrypt : DESEncryptor.EncryptionMode.Decrypt;
            byte[] bytes = File.ReadAllBytes(inputFilePath);
            byte[] result = encryptor.Apply(bytes, key, mode);
            File.WriteAllBytes("DESOutput" + Path.GetExtension(inputFilePath), result);
        }
    }
}