//	============================================================================
//
//  .,-:::::   :::.    :::::::..   :::::::.      ...   :::.    :::.
//	,;;;'````'   ;;`;;   ;;;;``;;;;   ;;;'';;'  .;;;;;;;.`;;;;,  `;;;
//	[[[         ,[[ '[[,  [[[,/[[['   [[[__[[\.,[[     \[[,[[[[[. '[[
//	$$$        c$$$cc$$$c $$$$$$c     $$""""Y$$$$$,     $$$$$$ "Y$c$$
//	`88bo,__,o, 888   888,888b "88bo,_88o,,od8P"888,_ _,88P888    Y88
//	"YUMMMMMP"YMM   ""` MMMM   "W" ""YUMMMP"   "YMMMMMP" MMM     YM
//
//	============================================================================
//
//	This file is a part of the Carbon Framework.
//
//	Copyright (C) 2006 Mark (Code6) Belles 
//
//	This library is free software; you can redistribute it and/or
//	modify it under the terms of the GNU Lesser General Public
//	License as published by the Free Software Foundation; either
//	version 2.1 of the License, or (at your option) any later version.
//
//	This library is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//	Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public
//	License along with this library; if not, write to the Free Software
//	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
//	============================================================================

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Security.Permissions;

namespace Carbon.Common
{
    /// <summary>
    /// Provides a class for encrypting and decrypting data.
    /// </summary>
    public sealed class EncryptionProvider : DisposableObject
    {        
        private SymmetricAlgorithm _algorithm;

        /// <summary>
        /// Initializes a new instance of the EncryptionProvider class.
        /// </summary>
        /// <param name="algorithm">The symmetric algorithm to use during cryptographic transformations.</param>
        /// <param name="padding">The padding mode to use.</param>
        /// <param name="mode">The cipher mode to use.</param>
        /// <param name="key">The key to use.</param>
        /// <param name="iv">The initialization vector to use.</param>
        public EncryptionProvider(SymmetricAlgorithm algorithm, byte[] key, byte[] iv)
        {
            _algorithm = algorithm;
            //            _algorithm.Padding = PaddingMode.PKCS7;
            //            _algorithm.Mode = CipherMode.CBC;
            _algorithm.Key = key;
            _algorithm.IV = iv;

            foreach (KeySizes keySize in _algorithm.LegalKeySizes)
                Debug.WriteLine(string.Format("Algorithm: {0}, KeyMaxSize: {1}, KeyMinSize: {2}", _algorithm.GetType().Name, keySize.MaxSize, keySize.MinSize));
        }

        protected override void DisposeOfManagedResources()
        {
            base.DisposeOfManagedResources();

            lock (base.SyncRoot)
            {
                _algorithm = null;
            }
        }

        /// <summary>
        /// Transforms the input bytes using the cryptographic transform into some output.
        /// </summary>
        /// <param name="transform">The transform to use.</param>
        /// <param name="input">The bytes to feed in as input to the transformation.</param>
        /// <returns></returns>
        private byte[] Transform(ICryptoTransform transform, byte[] input)
        {
            const int bufferLength = 4096;
            byte[] output = null;

            try
            {
                using (MemoryStream inputStream = new MemoryStream(input))
                {
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(outputStream, transform, CryptoStreamMode.Write))
                        {
                            byte[] buffer = new byte[bufferLength];
                            int bytesRead = 0;

                            do
                            {
                                bytesRead = inputStream.Read(buffer, 0, bufferLength);

                                cryptoStream.Write(buffer, 0, bytesRead);
                            }
                            while (bytesRead != 0);

                            cryptoStream.Close();

                            output = outputStream.ToArray();

                            outputStream.Close();
                            inputStream.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return output;
        }

        /// <summary>
        /// Encrypts the bytes specified.
        /// </summary>
        /// <param name="plainText">The decrypted bytes to encrypt.</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] plainText)
        {
            return Transform(_algorithm.CreateEncryptor(), plainText);
        }

        /// <summary>
        /// Encrypts the text specified, using the specified encoding.
        /// </summary>
        /// <param name="plainText">The decrypted text to encrypt.</param>
        /// <param name="encoding">The encoding to use to use to convert to and from byte arrays.</param>
        /// <returns></returns>
        public string Encrypt(string plainText)
        {
            byte[] input = Encoding.Unicode.GetBytes(plainText);
            byte[] output = this.Encrypt(input);
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// Decrypts the bytes specified.
        /// </summary>
        /// <param name="cipherText">The encrypted bytes to decrypt.</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] cipherText)
        {
            return Transform(_algorithm.CreateDecryptor(), cipherText);
        }

        /// <summary>
        /// Decrypts the text specified, using the specified encoding.
        /// </summary>
        /// <param name="cipherText">The encrypted text to decrypt.</param>
        /// <param name="encoding">The encoding to use to use to convert to and from byte arrays.</param>
        /// <returns></returns>
        public string Decrypt(string cipherText)
        {
            byte[] input = Convert.FromBase64String(cipherText);
            byte[] output = this.Decrypt(input);
            return Encoding.Unicode.GetString(output);
        }

        #region Static Methods

        /// <summary>        
        /// Creates a byte array of non-zero bytes for the purpose of creating a key.
        /// </summary>
        /// <param name="password">The password to base the key upon.</param>
        /// <param name="salt">The salt to initialize the key with.</param>
        /// <param name="strengthInBits">The strength of the key in bytes.</param>
        /// <returns></returns>
        private static byte[] CreateKey(string password, byte[] salt, int sizeOfKeyInBytes)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, salt);
            pdb.IterationCount = 100;
            pdb.HashName = "SHA1";
            return pdb.GetBytes(sizeOfKeyInBytes);
        }

        /// <summary>
        /// Creates a byte array of non-zero bytes for the purpose of creating a key.
        /// </summary>
        /// <param name="password">The password to base the key upon.</param>
        /// <param name="strengthInBits">The strength of the key in bits. Should be divisible by 8.</param>
        /// <returns></returns>
        public static byte[] CreateKey(string password, int strengthInBits)
        {
            return CreateKey(password, CreateSalt(16), strengthInBits / 8);
        }

        /// <summary>
        /// Creates a byte array of non-zero bytes for the purpose of creating an initialization vector.
        /// </summary>
        /// <param name="strengthInBits">The strength of the initialization vector in bits. Should be divisible by 8.</param>
        /// <returns></returns>
        public static byte[] CreateIV(int strengthInBits)
        {
            return CreateSalt(strengthInBits / 8);
        }

        /// <summary>
        /// Creates a byte array of non-zero bytes for the purposes of salting key creation.
        /// </summary>
        /// <param name="sizeInBytes">The number of bytes to create for the salt.</param>
        /// <returns></returns>
        public static byte[] CreateSalt(int sizeInBytes)
        {
            byte[] salt = new byte[sizeInBytes];
            RandomNumberGenerator.Create().GetNonZeroBytes(salt);
            return salt;
        }

        #endregion
    }
}
