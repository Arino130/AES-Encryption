using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AES_ENC;
namespace AES_Encryption
{
    public partial class GUI : Form
    {
        private AES aes = new AES();
        public GUI()
        {
            InitializeComponent();
        }
        //Ma hoa
        private void button1_Click(object sender, EventArgs e)
        {
            txtOutput.Text = String.Join(",", aes.EncryptAes(txtInput.Text, txtKeys.Text));
        }
        //Giai ma
        private void btnDecry_Click(object sender, EventArgs e)
        {
            if (!txtInput.Text.Contains(','))
            {
                MessageBox.Show("Input không hợp lệ");
                return;
            }
            string[] Phay = txtInput.Text.Split(new Char[] { ',' });
            byte[] data = new byte[Phay.Length];
            for(int i=0;i< data.Length; i++)
            {
                data[i] =Byte.Parse(Phay[i]);
            }
            string Decr= aes.DecryptAes(data, txtKeys.Text);
            txtOutput.Text = Decr == null ? "Đầu vào hoặc khóa không đúng" : Decr;
        }
    }
}
