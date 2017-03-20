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

namespace myRSA
{
    public partial class Form1 : Form
    {
        private string publicKey, privateKey; // strings to hold the public/private keys.
        /* These keys can be used as real keys. I can hand out my public key to whoever 
         * and encrypt with my private key, and the other program can decrypt with the public key.
         * save the key to a file and just hand it out... */

        UnicodeEncoding encoder = new UnicodeEncoding();


        public Form1()
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(); // used to create the keys 
            InitializeComponent();
            privateKey = myRSA.ToXmlString(true); // true indicates private key..
            publicKey = myRSA.ToXmlString(false); // false indicates public key..
        }

        private void btnClearCipher_Click(object sender, EventArgs e)
        {
            txtCipher.Clear();
     
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            var myRSA = new RSACryptoServiceProvider();

            // give the crypto Service provider a key to encrypt.
            myRSA.FromXmlString(publicKey);

            // encrypting the data.
            //var dataToEncrypt = encoder.GetBytes(txtPlain.Text);
            byte[] data = encoder.GetBytes(txtPlain.Text);

            // need a byte array in order to encrypt.
            var encryptedData = myRSA.Encrypt(data, false).ToArray();
            
            // build a string of the encrypted bytes.
            StringBuilder sb = new StringBuilder();
            int item = 0;
            int length = encryptedData.Length;
            foreach (var i in encryptedData)
            {
                sb.Append(i);
                if (item < length)
                    sb.Append(",");
                item++;
            }

            txtCipher.Text = sb.ToString();
        }

        private void btnClearPlain_Click(object sender, EventArgs e)
        {
            txtPlain.Clear();
        
        }
    }
}
