using System;
using System.Security.Cryptography;
using System.Text;
using DarkRift;

namespace RSAInterface
{
    public class RSAInterface : Plugin
    {
        //Basic puglin data
        public override string name
        {
            get { return "RSAInterface"; }
        }

        public override string version
        {
            get { return "0.1"; }
        }

        public override Command[] commands
        {
            get { return new Command[0]; }
        }

        public override string author
        {
            get { return "Luiz G. A. Roland"; }
        }

        public override string supportEmail
        {
            get { return "luizherege@gmail.coma"; }
        }

        //RSA Provider once it is initialized on the constructor the public and private key will be acessible
        static private RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider();

        //RSA PublicKey
        static public RSAParameters PublicKey = RSAProvider.ExportParameters(false);

        //RSA PrivateKey
        static private RSAParameters PrivateKey = RSAProvider.ExportParameters(true);

        //Unicode converter, for making strings into byte strings to encrypt and decrypt
        static private UnicodeEncoding ByteConverter = new UnicodeEncoding();

        //RSA Encrypt method
        static public byte[] RSAEncrypt(byte[] DataToEncrypt, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;

                //Import the RSA Key information. This only needs 
                //to include the public key information.
                RSAProvider.ImportParameters(PublicKey);

                //Encrypt the passed byte array and specify OAEP padding.   
                //OAEP padding is only available on Microsoft Windows XP or 
                //later.  
                encryptedData = RSAProvider.Encrypt(DataToEncrypt, DoOAEPPadding);

                return encryptedData;
            }

            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Interface.Log(e.Message);
                return null;
            }
        }

        //RSA Decrypt method
        static public byte[] RSADecrypt(byte[] DataToDecrypt, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;

                //Import the RSA Key information. This needs 
                //to include the private key information.
                RSAProvider.ImportParameters(PrivateKey);

                //Decrypt the passed byte array and specify OAEP padding.   
                //OAEP padding is only available on Microsoft Windows XP or 
                //later.  
                decryptedData = RSAProvider.Decrypt(DataToDecrypt, DoOAEPPadding);

                return decryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Interface.Log(e.ToString());

                return null;
            }

        }

        //String to byte convertion
        static public byte[] StrToByte(string str)
        {
            return ByteConverter.GetBytes(str);
        }

        //byte to string conversion
        static public string ByteToStr(byte[] bte)
        {
            return ByteConverter.GetString(bte);
        }
    }
}
