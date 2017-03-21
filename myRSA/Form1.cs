using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

/* David Lewis
 * Computer Security 5431
 * Lab 9(Encryption)
 * 3/21/2017
 * The encryption and decryption of a string of any length
 * using public/private keys with RSA.
 */


namespace myRSA
{
    public partial class Form1 : Form
    {
        private readonly string _publicKey; // strings to hold the public/private keys.
        private readonly string _privateKey; // strings to hold the public/private keys.
        private readonly int _maxKeyLength;
        

        /* These keys can be used as real keys. I can hand out my public key to whoever 
         * and encrypt with my private key, and the other program can decrypt with the public key.
         * save the key to a file and just hand it out... */

        readonly UnicodeEncoding _encoder = new UnicodeEncoding();


        public Form1()
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(); // used to create the keys 
            InitializeComponent();
            _privateKey = myRSA.ToXmlString(true); // true indicates private key..
            _publicKey = myRSA.ToXmlString(false); // false indicates public key..
            _maxKeyLength = (myRSA.KeySize - 384) / 8 + 37; // any value under this maxLength works.
        }

        private void btnClearCipher_Click(object sender, EventArgs e)
        {
            txtCipher.Clear();
     
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {

            byte[] originalData = _encoder.GetBytes(txtPlain.Text);
            StringBuilder sbEncrypted = new StringBuilder();
            

            //initialize our list to hold ONLY a limited amount of byte arrays(The amount will change based on how long the message is)
            List<byte[]> fragmentedEncryptedData = new List<byte[]>((int)Math.Ceiling((double)originalData.Length / _maxKeyLength));


            using (RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider())
            {
                int counter = 0; // this will be used to determine how many fragmented arrays the data gets broken up into.
                myRSA.FromXmlString(_publicKey);

                // encrypt the data in increments.
                for (int i = 0; i < fragmentedEncryptedData.Capacity; i++)
                {
                    // _maxKeyLength * counter = 'The position that i'm at within the originalData.
                    byte[] dataToEncrypt = originalData.Skip(_maxKeyLength * counter).Take(_maxKeyLength).ToArray();
                    var encryptedData = myRSA.Encrypt(dataToEncrypt, false);
                    fragmentedEncryptedData.Add(encryptedData);

                    // Harris Code. append the bytes to a stringBuilder seperated by a ','
                    int len = encryptedData.Length;
                    int item1 = 0;
                    foreach (byte b in encryptedData)
                    {
                        item1++;
                        sbEncrypted.Append(b);
                        if (item1 < len)
                            sbEncrypted.Append(',');
                    }

                    // add an extra ',' at the end of each fragment  or else the last byte
                    // of each fragment will combine with the first byte of the next fragment.
                    //. This does add a comma on the very last fragment which is not wanted for decryption when we split.
                    sbEncrypted.Append(',');
                    
                    //increase the counter by 1, only if we can still fill up an entire byteArray.
                    // this is so we can get the straggling bytes at the end that don't fill up an entire array.
                    if (originalData.Length - (_maxKeyLength * counter) > _maxKeyLength)
                        counter++;
                }
            }
            // trim that extra ',' at the end of the string that i mentioned earlier and throw the data in the textbox. 
            txtCipher.Text = sbEncrypted.ToString().TrimEnd(',');
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
         
            var data = txtCipher.Text.Split(',');
            StringBuilder sb = new StringBuilder();
            int maxLength = 128; // Not sure why this HAS TO be 128 for decryption whereas encryption can be any value under the maxKeyLength
            // Conver to bytes.
            byte[] byteData = data.Select(d => Convert.ToByte(d)).ToArray();

            using (RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider())
            {
                myRSA.FromXmlString(_privateKey);
                    
                int totalFragments = (int) Math.Ceiling((double) byteData.Length / maxLength); // number of fragments the data needs to broken up into
                int counter = 0; // counter used to determine which fragment we're on and also the location within the byteData

                List<byte> decryptedBytes = new List<byte>();

                for (int i = 0; i < totalFragments; i++)
                {
                    // maxLength * counter = 'The position location of the last byte decrypted'
                    byte[] dataToDecrypt = byteData.Skip(maxLength * counter).Take(maxLength).ToArray(); // decrypt bytes in chunks of the maxLength.
                    byte[] decryptedData = myRSA.Decrypt(dataToDecrypt, false);
                    decryptedBytes.AddRange(decryptedData);

                    if (byteData.Length - (_maxKeyLength * counter) > _maxKeyLength)
                        counter++;
                }// for loop end.

                txtPlain.Text = _encoder.GetString(decryptedBytes.ToArray());
            }// using statement end.
        }

            private void btnClearPlain_Click(object sender, EventArgs e)
        {
            txtPlain.Clear();
        
        }

    }
}
