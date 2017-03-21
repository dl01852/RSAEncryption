using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace myRSA
{
    public partial class Form1 : Form
    {
        private readonly string _publicKey; // strings to hold the public/private keys.
        private readonly string _privateKey; // strings to hold the public/private keys.
        private readonly int _maxKeyLength;
        private readonly int _keySize;

        /* These keys can be used as real keys. I can hand out my public key to whoever 
         * and encrypt with my private key, and the other program can decrypt with the public key.
         * save the key to a file and just hand it out... */

        UnicodeEncoding encoder = new UnicodeEncoding();


        public Form1()
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(); // used to create the keys 
            InitializeComponent();
            _privateKey = myRSA.ToXmlString(true); // true indicates private key..
            _publicKey = myRSA.ToXmlString(false); // false indicates public key..
            _keySize = myRSA.KeySize;
            _maxKeyLength = ((myRSA.KeySize - 384) / 8) + 37; // determines the maximum length(usually 117)
        }

        private void btnClearCipher_Click(object sender, EventArgs e)
        {
            txtCipher.Clear();
     
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            var myRSA = new RSACryptoServiceProvider();
            List<byte[]> fragmentedEncryptedData; // this will hold a list of fragmented byte arrays of our data.
            // give the crypto Service provider a key to encrypt.
            myRSA.FromXmlString(_publicKey);

            
            byte[] original_data = encoder.GetBytes(txtPlain.Text);
            

            //initialize our list to hold ONLY a limited amount of byte arrays(The amount will change based on how long the message is)
            fragmentedEncryptedData = new List<byte[]>((int)Math.Ceiling((double)original_data.Length / _maxKeyLength));

          
            // encrypt the data in increments.
            int counter = 0; // value will increment as we fill up each byte array with the maximum amount of encrypted bytes.
            for (int i = 0; i < fragmentedEncryptedData.Capacity; i++)
            {
                // _maxKeyLength * counter = 'The position within the original_data that have been encrypted already.'

                byte[] dataToEncrypt = original_data.Skip(_maxKeyLength * counter).Take(_maxKeyLength).ToArray();
                var dataEncrypted = myRSA.Encrypt(dataToEncrypt,false).ToArray();
                fragmentedEncryptedData.Add(dataEncrypted);
               
                // increment the counter only if we can max out an entire byteArray(_maxKeyLength)
                // need this to grab the remaining bytes, that won't fill up an entire byteArray.
                if (original_data.Length - (_maxKeyLength * counter) > _maxKeyLength)
                    counter++;
            }

            //once we have the fragmented Encrypted data. add the ',' and then output it to the textbox.
            StringBuilder sb = new StringBuilder();
            int item = 0;
            int length = fragmentedEncryptedData.Sum(d => d.Length); // length of the fragmented encrypted data summed together. 

            foreach (byte[] dataPieces in fragmentedEncryptedData)
            {
                foreach (byte b in dataPieces)
                {
                    item++;
                    sb.Append(b);
                    if (item < length)
                        sb.Append(",");
                }
            }

            txtCipher.Text = sb.ToString();
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            var myRSA = new RSACryptoServiceProvider();
            var data = txtCipher.Text.Split(',');
            StringBuilder sb = new StringBuilder(); 

            // Conver to bytes.
            byte[] byteData = data.Select(d => Convert.ToByte(d)).ToArray();

            using (var provider = new RSACryptoServiceProvider(_keySize))
            {
                provider.FromXmlString(_privateKey);
                var decrypt = provider.Decrypt(byteData, false);
                txtPlain.Text = encoder.GetString(decrypt);
            }
            //    myRSA.FromXmlString(_privateKey);

            //int total_fragments = (int)Math.Ceiling((double)byteData.Length / _maxKeyLength); // number of fragments the bytes need to be broken into.
            //int counter = 0;
            //for (int i = 0; i < total_fragments; i++)
            //{
            //    byte[] dataToDecrypt = byteData.Skip(_maxKeyLength * counter).Take(_maxKeyLength).ToArray();
            //    var decryptedData = myRSA.Decrypt(dataToDecrypt, false);
            //    sb.Append(encoder.GetString(decryptedData));

            //    if (byteData.Length - (_maxKeyLength * counter) > _maxKeyLength)
            //        counter++;
            //}

            //txtPlain.Text = sb.ToString();

        }

        private void btnClearPlain_Click(object sender, EventArgs e)
        {
            txtPlain.Clear();
        
        }
    }
}
