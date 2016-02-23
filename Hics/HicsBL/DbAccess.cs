﻿using System;
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
        /// <param name="lampAdress"></param>
        /// <param name="lampName"></param>
        static void addLamp(string username, string password, string lampAdress, string lampName)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

        }

        /// <summary>
        /// PSP 1.3
        /// Lampe hinzufügen
        /// </summary>
        /// <param name="address"></param>
        /// <param name="lampNameId"></param>
        /// <returns></returns>
        static int addLamp(string username, string password, string address, int lampNameId)
        {
            int lampId = 0;
            return lampId;
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
            string pwhash = HelperClass.GetHash(password);
            //Übergebenes neues Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhashNew = HelperClass.GetHash(passwordNew);
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
        /// <summary>
        /// PSP 4.1
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampGroupName"></param>
        /// <returns></returns>
        static int addLampGroup (string username, string password, string lampGroupName)
        {
            int lampGroupId = -1;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return lampGroupId;
        }
        /// <summary>
        /// PSP 2.1
        /// Editieren eines Lampennamens anhand des alten Lampennamens
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampNameOld"></param>
        /// <param name="lampNameNew"></param>
        /// <returns></returns>
        static bool editLampName(string username, string password, string lampNameOld, string lampNameNew)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        }
        /// <summary>
        /// PSP 2.4
        /// Editieren eines Lampennamens anhand der LampenId
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampId"></param>
        /// <param name="lampNameNew"></param>
        /// <returns></returns>
        static bool editLampName(string username, string password, int lampId, string lampNameNew)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        }
        /// <summary>
        /// PSP 5.1
        /// Lampe einer Gruppe anhand groupId und lampId hinzufügen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupId"></param>
        /// <param name="lampId"></param>
        static bool addLampToGroup(string username, string password, int groupId, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        }
        /// <summary>
        /// PSP 5.3
        /// Lampe einer Gruppe anhand groupName und lampId hinzufügen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupId"></param>
        /// <param name="lampId"></param>
        static bool addLampToGroup(string username, string password, string groupName, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        }

        /// <summary>
        /// PSP 6.1
        /// Entfernt eine Lampe von einer Gruppe mittels group_id und lamp_id
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupId"></param>
        /// <param name="lampId"></param>
        /// <returns></returns>
        static bool removeLampFromGroup(string username, string password, int groupId, int lampId)
        {
            bool success = false;

            return success;
        }

        /// <summary>
        /// PSP 7.1
        /// Lampengruppe entfernen mittels Gruppennamen
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        static bool removeLampGroup(string username, string password, string groupName)
        {
            bool success = false;

            return success;
        }

        /// <summary>
        /// PSP 6.3
        /// Lampe einer Gruppe anhand groupName und lampId entfernen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupId"></param>
        /// <param name="lampId"></param>
        /// <returns></returns>
        static bool removeLampFromGroup(string username, string password, string groupName, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        }

        /// <summary>
        /// PSP 7.3
        /// Lampengruppe anhand id entfernen
        /// </summary>
        /// <param name="group_id"></param>
        /// <returns></returns>
        static bool removeLampGroup(string username, string password, int groupId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;

        }
        /// <summary>
        /// PSP 8.3
        /// entfernt user anhand von usernameId
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupName"></param>
        /// <param name="lampId"></param>
        /// <returns></returns>
        static bool removeUser(string username, string password, int usernameId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        }
        /// <summary>
        /// PSP 8.5
        /// entfernt user anhand von usernameName
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupName"></param>
        /// <param name="lampId"></param>
        /// <returns></returns>
        static bool removeUser(string username, string password, string usernameName)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        }

        /// <summary>
        /// PSP 8.8
        /// User hinzufügen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="usernameId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        static bool AddUser(string username, string password, string userNew, string passwordNew)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(passwordNew);
            return success;
        }

        /// <summary>
        /// PSP 9.1
        /// UserGroup editieren
        /// </summary>
        /// <param name="usernameId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        static bool EditUserGroup(string username, string password, int usernameId, int groupId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);
            return success;
        }

        /// <summary>
        /// PSP 9.2
        /// UserGroup editieren
        /// </summary>
        /// <param name="username"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
       static bool EditUserGroup(string username, string password, string usernameName, int groupId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);
            return success;
        }

        /// <summary>
        /// PSP 13.1
        /// Lampe wechseln
        /// </summary>
        /// <param name="lampOnOff"></param>
        /// <param name="lampId"></param>
        /// <returns></returns>
        static bool switchLamp(string username, string password, bool lampOnOff, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);
            return success;
        }

        /// <summary>
        /// PSP 15.1
        /// Lampen dimmen
        /// </summary>
        /// <param name="lampId"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        static bool dimLamp(string username, string password, int lampId, byte brightness)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);
            return success;
        }

        /// <summary>
        /// PSP 15.2
        /// Lampen dimmen
        /// </summary>
        /// <param name="lampName"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        static bool dimLamp(string username, string password, string lampName, byte brightness)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);
            return success;
        }

        /// <summary>
        /// PSP 19.1
        /// Edit UserPassword
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordOld"></param>
        /// <param name="passwordNew"></param>
        /// <returns></returns>
        static bool EditUserPassword(string username, string passwordOld, string passwordNew)
        {
            bool success = false;
            //Übergebene Passwörte hashen und in Var speichern für Übergabe an DB
            string pwhashOld = HelperClass.GetHash(passwordOld);
            string pwhashNew = HelperClass.GetHash(passwordNew);
            return success;
        }
        
    }
}
