using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace rsa
{
    public partial class Form1 : Form
    {
        private static RSAParameters publicKey;
        private static RSAParameters privateKey;
        public static byte[] encrypted;
        public static byte[] decrypted;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text;
            generateKeys();
            encrypted = Encrypt(Encoding.UTF8.GetBytes(message));
            textBox2.Text = BitConverter.ToString(encrypted).Replace("-", "") + "\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decrypted = Decrypt(encrypted);
            textBox1.Text = Encoding.UTF8.GetString(decrypted);
        }

        static byte[] Decrypt(byte[] input)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);
                decrypted = rsa.Decrypt(input, true);
            }
            return decrypted;
        }

        private byte[] Encrypt(byte[] input)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(publicKey);
                encrypted = rsa.Encrypt(input, true);
            }
            return encrypted;
        }

        static void generateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);

            }
        }

    }
}
