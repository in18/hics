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
        /// <param name="usernameNew"></param>
        /// <param name="passwordNew"></param>
        static void addUser(string username, string password,string usernameNew, string passwordNew)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(passwordNew);
        }

    }
}
