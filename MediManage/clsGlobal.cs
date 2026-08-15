using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Cryptography;
using MediManage_Buisness;



namespace MediManage
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;
        public static string ComputeHash(string input)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static bool RememberUsernameAndPasswordInRegistry(string Username, string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\MediManage";
            string valueName = "MediManageLogin";

            if (Password != "")
                Password = ComputeHash(Password);

            string dataToSave = Username + "#//#" + Password;
            try
            {
                //Write the value to the Regitry
                Registry.SetValue(KeyPath, valueName, dataToSave, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occcurred {ex.Message}" , "Regitry Error" ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool GetStoredCredentialFromRegistry(ref string Username, ref string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\MediManage";
            string valueName = "MediManageLogin";
            try
            {
                //Read the value from the Registry 
                string value = Registry.GetValue(KeyPath, valueName, null) as string;
                if (value != null)
                {
                    Console.WriteLine(value); // Output each line of data to the console
                    string[] result = value.Split(new string[] { "#//#" }, StringSplitOptions.None);
                    Username = result[0];
                    Password = result[1];
                    if (Username != "" && Password != "")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occcurred {ex.Message}", "Regitry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

    }
}
