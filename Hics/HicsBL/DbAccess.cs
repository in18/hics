using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using huedotnet;

namespace HicsBL
{
    /// <summary>
    /// Diese Klasse ist die Schnittstelle zwischen GUI,DB,HUE
    /// </summary>
    public class DbAccess
    {
        //##########################################################################
        //#                            Changelog                                   #
        //##########################################################################
        //# Datum    |  Name        |Was geändert                                  #
        //#----------+--------------+----------------------------------------------#
        //#04.03.2016|Wolf          |Changelog integriert                          #
        //#05.03.2016|Wolf          |Region für die Arbeitspakete erstellt         #
        //#06.03.2016|Wolf          |XML Ausgabe für die tech. Dokumentation       #
        //#          |              |in HicsBl eingeschaltet                       #
        //#07.03.2016|Wolf          |Tech. Dok. erweitert                          #
        //##########################################################################


        #region PSP 1.1 addLamp(string username, string password, string lampAdress, string lampName)
        /// <summary>
        /// PSP 1.1
        /// Lampe hinzufügen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampAdress"></param>
        /// <param name="lampName"></param>
        public static void addLamp(string username, string password, string lampAdress, string lampName)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                cont.sp_add_lamp(username, pwhash, lampAdress, lampName);
            }

        }
        #endregion

        #region PSP 1.3 addLamp(string username, string password, string lampAdress, int lampNameId)
        ///// <summary>
        ///// PSP 1.3
        ///// Lampe hinzufügen
        ///// </summary>
        ///// <param name="lampAdress"></param>
        ///// <param name="lampNameId"></param>
        ///// <returns></returns>
        ////static void addLamp(string username, string password, string lampAdress, int lampNameId)
        ////{
        ////    //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
        ////    string pwhash = HelperClass.GetHash(password);
        ////    using (itin18_aktEntities cont = new itin18_aktEntities())
        ////    {
        ////        cont.sp_add_lamp(username, pwhash, lampAdress, lampNameId);
        ////    }

        ////} 
        #endregion

        #region PSP 2.1 editLampName(string username, string password, string lampNameOld, string lampNameNew
        /// <summary>
        /// PSP 2.1
        /// Editieren eines Lampennamens anhand des alten Lampennamens
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampNameOld"></param>
        /// <param name="lampNameNew"></param>
        /// <returns></returns>
        static void editLampName(string username, string password, string lampNameOld, string lampNameNew)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                //Table der Db-Fn holen
                List<fn_show_lamps_Result> dblamps = cont.fn_show_lamps().ToList();

                //temporäre Variablen
                int? dblampId = 0; //Nullable da in der Db Nullable
                string dblampAdr = "";

                // Lambda Bsp. statt foreach
                // dblampId= cont.fn_show_lamps().Where(i => i.name == lampNameOld);
                // Lamda ende

                foreach (var item in dblamps)
                {
                    if (item.name == lampNameOld)
                    {
                        //Für das Wiederanlegen der Lampe die ID temp. speichern
                        dblampId = item.id;
                        //Für das Wiederanlegen der Lampe die Adresse temp. speichern
                        dblampAdr = item.address;
                        //Wenn gefunden muss nicht die ganze Liste durchlaufen werden
                        break;
                    }
                }
                //Edit gibt es nicht in der DB, Lampe wird gelöscht und wieder neu angelegt
                cont.sp_delete_lamp(dblampId, username, pwhash);
                cont.sp_add_lamp(username, pwhash, dblampAdr, lampNameNew);
            }
            //Namen der Lampe in der HUE-Bridge ändern
            HelperClass.SetLampName(HueAccess.GetLampId(lampNameOld), lampNameNew);
        }
        #endregion

        #region PSP 2.4 editLampName(string username, string password, int lampId, string lampNameNew)
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
        #endregion

        #region PSP 3.1 deleteLamp(string username, string password, int lampId)
        ///<summary>
        /// PSP 3.1
        /// Löschen der Lampe anhand der LampenId
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampId"></param>
        /// <returns></returns>
        public static void deleteLamp(string username, string password, int lampId)
        {

            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            //Lampe aus der DB löschen
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                cont.sp_delete_lamp(lampId, username, pwhash);
            }

            //HUE-Bridge entfernt die Lampe (Da nicht benutzt) automatisch. Liste lamps aktualisieren
            HueAccess.getLampList();

        }
        #endregion

        #region PSP 3.2 deleteLamp(string username, string password, string lampAdress)
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
        #endregion

        #region PSP 4.1 addLampGroup(string username, string password, string lampGroupName)
        /// <summary>
        /// PSP 4.1
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampGroupName"></param>
        /// <returns></returns>
        static int addLampGroup(string username, string password, string lampGroupName)
        {
            int lampGroupId = -1;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return lampGroupId;
        }
        #endregion

        #region PSP 5.1 addLampToGroup(string username, string password, int groupId, int lampId)
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
        #endregion

        #region PSP 5.3 addLampToGroup(string username, string password, string groupName, int lampId
        /// <summary>
        /// PSP 5.3
        /// Lampe einer Gruppe anhand groupName und lampId hinzufügen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupName"></param>
        /// <param name="lampId"></param>
        static bool addLampToGroup(string username, string password, string groupName, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        }
        #endregion

        #region PSP 6.1 removeLampFromGroup(string username, string password, int groupId, int lampId)
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
        #endregion

        #region PSP 7.1 removeLampGroup(string username, string password, string groupName)
        /// <summary>
        /// PSP 7.1
        /// Lampengruppe entfernen mittels Gruppennamen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        static bool removeLampGroup(string username, string password, string groupName)
        {
            bool success = false;

            return success;
        }

        #endregion

        #region PSP 6.3 removeLampFromGroup(string username, string password, string groupName, int lampId)
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
        #endregion

        #region PSP 7.3 removeLampGroup(string username, string password, int groupId)
        /// <summary>
        /// PSP 7.3
        /// Lampengruppe anhand id entfernen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        static bool removeLampGroup(string username, string password, int groupId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;

        }
        #endregion

        #region PSP 8.1 addUser(string username, string password, string usernameNew, string passwordNew)
        /// <summary>
        /// PSP 8.1
        /// User hinzufügen. (Angemeldeter User wird anhand Usernamen und Passwort auf Rechte geprüft)
        /// </summary>
        /// <param name="username">den angemeldeten Usernamen übergeben (Überprüfung auf Rechte)</param>
        /// <param name="password">das dazugehörige Passwort übermitteln (Überprüfung auf Rechte)</param>
        /// <param name="usernameNew">Name des neu anzulegenden Users</param>
        /// <param name="passwordNew">Passwort des neu angelegten User</param>
        public static void addUser(string username, string password, string usernameNew, string passwordNew)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);
            //Übergebenes neues Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhashNew = HelperClass.GetHash(passwordNew);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                cont.sp_add_user(username, pwhash, usernameNew, pwhashNew);
            }
        }
        #endregion

        #region PSP 8.3 removeUser(string username, string password, int usernameId)
        /// <summary>
        /// PSP 8.3
        /// entfernt user anhand von usernameId
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="usernameId"></param>
        /// <returns></returns>
        static bool removeUser(string username, string password, int usernameId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        }
        #endregion

        #region PSP 8.5 removeUser(string username, string password, string usernameName)
        /// <summary>
        /// PSP 8.5
        /// entfernt user anhand von usernameName
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="usernameName"></param>
        /// <returns></returns>
        static bool removeUser(string username, string password, string usernameName)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);

            return success;
        }
        #endregion

        #region PSP 9.1 EditUserGroup(string username, string password, int usernameId, int groupId)
        /// <summary>
        /// PSP 9.1
        /// UserGroup editieren
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
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
        #endregion

        #region PSP 9.2 EditUserGroup(string username, string password, string usernameName, int groupId)
        /// <summary>
        /// PSP 9.2
        /// UserGroup editieren
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="usernameName"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        static bool EditUserGroup(string username, string password, string usernameName, int groupId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);
            return success;
        }
        #endregion

        #region PSP 13.1 switchLamp(string username, string password, bool lampOnOff, int lampId)
        /// <summary>
        /// PSP 13.1
        /// Lampe Ein/Aus
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampOnOff"></param>
        /// <param name="lampId"></param>
        /// <returns></returns>
        static void switchLamp(string username, string password, bool lampOnOff, int lampId)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);


            //Ab hier wird die HUE-Bridge angesprochen
            if (lampOnOff == true)
            {
                // Vereinfachter aufruf über die HelperClass
                HelperClass.SetLampState(lampId, true);
            }
            else
            {
                // Vereinfachter aufruf über die HelperClass
                HelperClass.SetLampState(lampId, false);
            }
        } 
        #endregion

        #region PSP 16.1 dimLamp(string username, string password, int lampId, byte brightness)
        /// <summary>
        /// PSP 15.1
        /// Lampen dimmen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
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
        #endregion

        #region PSP 16.2 dimLamp(string username, string password, string lampName, byte brightness)
        /// <summary>
        /// PSP 15.2
        /// Lampen dimmen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
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
        #endregion

        #region PSP 16.1 userLogin(string username, string password)
        /// <summary>
        /// PSP 16.1
        /// User Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool userLogin(string username, string password)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);
            return success;
        }
        #endregion

        #region PSP 19.1 EditUserPassword(string username, string passwordOld, string passwordNew)
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
        #endregion

        #region PSP 16.1 GetLogFile(string username, string password, DateTime beginDate, DateTime endDate)
        /// <summary>
        /// PSP 16.1
        /// Logfile von beginDate bis endDate in einer Liste returgeben
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        static List<Object> GetLogFile(string username, string password, DateTime beginDate, DateTime endDate)
        {
            List<Object> tmp = new List<object>();
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            string pwhash = HelperClass.GetHash(password);
            return tmp;
        } 
        #endregion
    }
}
