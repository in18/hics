using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HicsBL
{
    public class DbAccess
    {
        /// <summary>
        /// PSP 1.1
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampadress"></param>
        /// <param name="lampname"></param>
        static void addLamp(string username, string password, string lampadress, string lampname)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

        }
        /// <summary>
        /// PSP 8.1
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
         static void addUser(string username, string password)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);
        }
        /// <summary>
        /// PSP 3.1
        /// Löschen der Lampe anhand der LampenId
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampId"></param>
        /// <returns></returns>
        static bool deleteLamp(string username, string password, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        
        }
        /// <summary>
        /// PSP 3.2
        /// Löschen einer Lampe anhand der Lampenadresse
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampAdress"></param>
        /// <returns></returns>
        static bool deleteLamp(string username, string password, string lampAdress)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;

        }
    }
}
