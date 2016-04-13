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
    //[System.Runtime.InteropServices.Guid("6FE03D8A-15FD-4100-89A9-5BEF81361D24")]
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
        //#08.03.2016|Mock,Acs      |Viele PSP erweitert                           #
        //#08.03.2016|Wolf          |Ausbesserungen                                #
        //#14.03.2016|Mock          |Ausbesserungen und Doku                       #
        //#14.03.2016|Wolf          |Hashfunktion bearbeitet                       #
        //#23.03.2016|Kornfeld,Acs  |Exception Behandlung                          #
        //##########################################################################

        public DbAccess()
        {
            HueAccess.LoadConfig();
            HueAccess.getWebClient();
            HueAccess.getLampList();
        }
        #region PSP 1.1 addLamp(string username, string password, string lampAdress, string lampName)
        /// <summary>
        /// PSP 1.1
        /// Lampe in der DB hinzufügen, Hue-Bridge erkennt eine neue Lampe automatisch
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampAdress"></param>
        /// <param name="lampName"></param>
        public static bool addLamp(string username, string password, string lampAdress, string lampName)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                try
                {
                    cont.sp_add_lamp(username, pwhash, lampAdress, lampName);
                    //Neue Liste lamps von der HUE-Bridge holen
                    HueAccess.getLampList();
                    success = true;
                }
                catch (Exception e)
                {
                    success = false;
                }
            }
            
           
            return success;
        }
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
        internal static bool editLampName(string username, string password, string lampNameOld, string lampNameNew)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            bool success = false;
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                
                List<fn_show_lamp_status_Result> dbLampStatus = null;
                List<fn_show_lamp_control_Result> dbLampsStatus = cont.fn_show_lamp_control(username, pwhash).ToList();
                List<fn_show_lamp_control_Result> dbLampsStatusResult = new List<fn_show_lamp_control_Result>();
                List<fn_show_lampgroups_Result> dbLampGroups = cont.fn_show_lampgroups(username, pwhash).ToList();
                List<fn_show_lampgroup_status_Result> dbLampGroupStatus = null;
                List<fn_show_lamps_Result> dbLampsNew = null;
                //temporäre Variablen
                int? dbLampIdNew = 0;
                int? dblampId = 0;
                string dblampAdr = "";
                string dbLampGroupName = "";

                try
                {
                    foreach (var item in dbLampsStatus)
                    {
                        // Suche des alten Namens zwecks Änderung
                        if (item.lampname == lampNameOld)
                        {
                            dblampId = item.lamp_id;

                            dblampAdr = item.address;

                            dbLampStatus = cont.fn_show_lamp_status(username, pwhash, item.lamp_id).ToList();

                            dbLampsStatusResult.Add(item);

                            dbLampGroupName = item.groupname;
                        }
                    }

                    //Edit gibt es nicht in der DB, Lampe wird gelöscht und wieder neu angelegt
                    cont.sp_delete_lamp(dblampId, username, pwhash);
                    cont.sp_add_lamp(username, pwhash, dblampAdr, lampNameNew);

                    dbLampsNew = cont.fn_show_lamps(username, pwhash).ToList();
                    foreach (var item in dbLampsNew)
                    {
                        if (item.name == lampNameNew)
                        {
                            dbLampIdNew = item.id;
                        }
                    }


                    foreach (var outerItem in dbLampGroups)
                    {
                        dbLampGroupStatus = cont.fn_show_lampgroup_status(username, pwhash, outerItem.id).ToList();
                        foreach (var innerItem in dbLampGroupStatus)
                        {
                            if (innerItem.id == dblampId)
                            {
                                cont.sp_delete_lamp_from_roomgroup(username, pwhash, outerItem.id, dblampId);
                                cont.sp_add_lamp_to_lampgroup(username, pwhash, outerItem.id, dbLampIdNew);
                            }
                        }
                    }

                    cont.sp_lamp_dimm(username, pwhash, dbLampIdNew, dbLampStatus[0].bright);
                    if (dbLampStatus[0].status == true)
                    {
                        cont.sp_lamp_on(username, pwhash, dbLampIdNew);
                    }
                    else
                    {
                        cont.sp_lamp_off(username, pwhash, dbLampIdNew);
                    }
                 //Namen der Lampe in der HUE-Bridge ändern
                 HelperClass.SetLampName(HueAccess.GetLampId(lampNameOld), lampNameNew);
                    success = true;
                }
                catch (Exception e)
                {
                    success = false;
                }
               
                
            }
            return success;
        }
        #endregion

        #region PSP 2.4 editLampName(string username, string password, int lampId, string lampNameNew)
        /// <summary>
        /// PSP 2.4
        /// Editieren eines Lampennamens anhand der DB-LampenId      
        /// Is aber wurscht, da die Hue-Bridge entfernte Lampen automatisch erkennt
        /// Es geht nur um den Db Eintrag
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
            Byte[] pwhash = HelperClass.GetHash(password);

            // Das wichtigste ist, wir brauchen 2 Listen, DB und Hue
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                string dbLampName = "";
                
                List<fn_show_lamps_Result> dbLamps = new List<fn_show_lamps_Result>();


                try
                {
                    foreach (var item in dbLamps)
                    {
                        if (item.id == lampId)
                        {
                            dbLampName = item.name;

                        }
                    }

                    int HueLampId = HelperClass.GetHueLampId(username, pwhash, lampId);
                    HelperClass.SetLampName(HueLampId, lampNameNew);
                    success = true;
                }
                catch (Exception e)
                {
                    success = false;
                }
            }
            return success;
        }
        #endregion

        #region PSP 3.1 deleteLamp(string username, string password, int lampId)
        ///<summary>
        /// PSP 3.1
        /// Löschen der Lampe anhand der LampenId
        /// Is aber wurscht, da die Hue-Bridge entfernte Lampen automatisch erkennt
        /// Es geht nur um den Db Eintrag
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampId">Id der Lampe aus der DB</param>
        /// <returns></returns>
        public static bool deleteLamp(string username, string password, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);

            //Lampe aus der DB löschen
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                try
                {
                    cont.sp_delete_lamp(lampId, username, pwhash);
                    //HUE-Bridge entfernt die Lampe (Da nicht benutzt) automatisch. Liste lamps aktualisieren
                    //Sollte die Lampe, obwohl vorhanden gelöscht werden, wird dies von uns nicht unterstützt!
                    HueAccess.getLampList();
                    success = true;
                }
                catch (Exception e)
                {

                    success = false;
                }
            }

            
            return success;
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
        public static bool deleteLamp(string username, string password, string lampAdress)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                //Nach Lampe in DB über Funktion suchen
                foreach (var item in cont.fn_show_lamps(username,pwhash))
                {
                    //Adress wird geprüft
                    if (item.address == lampAdress)
                    {
                        try
                        {   //Lampe aus DB entfernen                    
                            cont.sp_delete_lamp(item.id, username, pwhash);
                            //HUE-Bridge entfernt die Lampe (Da nicht benutzt) automatisch. Liste lamps aktualisieren
                            //Sollte die Lampe, obwohl vorhanden gelöscht werden, wird dies von uns nicht unterstützt!
                            HueAccess.getLampList();
                            success = true;
                        }
                        catch (Exception e)
                        {
                            success = false;
                        }
                    }
                }
            }

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
        public static bool addLampGroup(string username, string password, string lampGroupName)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {                             
                try
                {
                    //Lampengruppe erstellen                     
                    cont.sp_add_lampgroup(username, pwhash, lampGroupName);                          
                    success = true;
                }
                catch (Exception e)
                {
                    success = false;
                }
            }

            return success;
        }
        #endregion

        #region PSP 5.1 addLampToGroup(string username, string password, int groupId, int lampId
        /// <summary>
        /// PSP 5.1
        /// Lampe einer Gruppe anhand groupId und lampId hinzufügen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupId"></param>
        /// <param name="lampId"></param>
        /// <returns>Bool ob erfolgreich oder nicht</returns>
        public static bool addLampToGroup(string username, string password, int groupId, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                //Durchlauf der Lampengruppen mit Hilfe der DB-Funktion
                foreach (var item in cont.fn_show_lampgroups(username, pwhash))
                {
                    //Überprüfung der GruppenId
                    if (item.id == groupId)
                    {
                        try
                        {
                            //Hinzufügen einer Lampe zu einer Lampengruppe über DB-Funktion
                            cont.sp_add_lamp_to_lampgroup(username, pwhash, item.id, lampId);
                            success = true;
                        }
                        catch (Exception e)
                        {
                            success = false;
                        }
                    }
                }
            }
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
        public static bool addLampToGroup(string username, string password, string groupName, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                //Durchlauf der Lampengruppen mit Hilfe von DB-Funktion
                foreach (var item in cont.fn_show_lampgroups(username, pwhash))
                {
                    //Überprüfung des Gruppennamens
                    if(item.roomgroupname == groupName)
                    {                    
                        try
                        {
                            //Hinzufügen der Lampe zur Lampengruppe
                            //item.id = Id der Lampengruppe
                            cont.sp_add_lamp_to_lampgroup(username, pwhash, item.id, lampId);
                            success = true;
                        }
                        catch (Exception e)
                        {
                            success = false;
                        }
                            
                    }    
                }
            }
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
        public static bool removeLampFromGroup(string username, string password, int groupId, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                //Durchlauf der Lampengruppen über DB-Funktion
                foreach (var item in cont.fn_show_lampgroups(username, pwhash))
                {
                    //GruppenId wird überprüüft
                    if (item.id == groupId)
                    {
                        try
                        {
                            //Lampe wird aus der Lampengruppe entfernt
                            cont.sp_delete_lamp_from_roomgroup(username, pwhash, item.id, lampId);
                            success = true;
                        }
                        catch (Exception e)
                        {
                            success = false;
                        }
                    }
                }

            }
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
        public static bool removeLampGroup(string username, string password, string groupName)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                //Durchlauf der Lampengruppen mittels DB-Funktion
                foreach (var item in cont.fn_show_lampgroups(username,pwhash))
                {
                    //Überprüfung des Gruppennamens
                    if(item.roomgroupname == groupName)
                    {
                        try
                        {
                            //Löschen der Raumgruppe
                            cont.sp_delete_roomgroup(username, pwhash, item.id);
                            success = true;
                        }
                        catch (Exception e)
                        {
                            success = false;                          
                        }
                    }
                }

            }
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
        /// <param name="groupName"></param>
        /// <param name="lampId"></param>
        /// <returns></returns>
        public static bool removeLampFromGroup(string username, string password, string groupName, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                //Durchsuchen mittels DB-Funktion
                foreach (var item in cont.fn_show_lampgroups(username,pwhash))
                {
                    //Überprüfung des Gruppennamens
                    if(item.roomgroupname == groupName)
                    {
                        try
                        {
                            //Löschen der Lampe aus der Gruppe
                            cont.sp_delete_lamp_from_roomgroup(username, pwhash, item.id, lampId);
                            success = true;
                        }
                        catch (Exception e)
                        {

                            success = false;
                        }
                    }
                }
            }
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
        public static bool removeLampGroup(string username, string password, int groupId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                try
                {
                    //Löschen der Raumgruppe
                    cont.sp_delete_roomgroup(username, pwhash, groupId);
                    success = true;
                }
                catch (Exception e)
                {

                    success = false;
            }
            }

            return success;

        }
        #endregion

        #region PSP 7.4 editLampGroup(string username, string password, int groupId)
        /// <summary>
        /// PSP 7.4
        /// Lampengruppen editieren
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static bool editLampGroup(string username, string password, int groupId) {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            return success;

        }
        #endregion

        #region PSP  7.4 editLampGroup (string username, string password, int groupId)
        /// <summary>
        /// PSP 7.4
        /// Lampengruppe umbenennen anhand Id
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static bool editLampGroup(string username, string password, int groupId, string newGroupName)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
           
                return success;   
        }
        #endregion

        #region PSP 8.1 addUser(string username, string password, string usernameNew, string passwordNew, bool admin)
        /// <summary>
        /// PSP 8.1
        /// User hinzufügen. (Angemeldeter User wird anhand Usernamen und Passwort auf Rechte geprüft)
        /// </summary>
        /// <param name="username">den angemeldeten Usernamen übergeben (Überprüfung auf Rechte)</param>
        /// <param name="password">das dazugehörige Passwort übermitteln (Überprüfung auf Rechte)</param>
        /// <param name="usernameNew">Name des neu anzulegenden Users</param>
        /// <param name="passwordNew">Passwort des neu angelegten User</param>
        /// <param name="admin">Ist User Admin?</param>
        public static bool addUser(string username, string password, string usernameNew, string passwordNew, bool admin)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            //Übergebenes neues Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhashNew = HelperClass.GetHash(passwordNew);
            List<int?> userId = new List<int?>();

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {

                try
                {
                    cont.sp_add_user(username, pwhash, usernameNew, pwhashNew);
                   
                    userId = cont.fn_check_user_table(usernameNew, pwhashNew).ToList();
                    
                    if (admin == true)
                        {
                            if (addUserToUsergroup(username, password, userId[0].Value, 1) == true)
                            {
                                 success = true;
                            }
                        }
                    else
                        {
                            if (addUserToUsergroup(username, password, userId[0].Value, 2) == true)
                            {
                                success = true;
                            }
                        }
                }
                catch (Exception e)
                {
                    success = false;
                }
                
            }
            return success;
        }
        #endregion

        #region 8.2 User einer Usergruppe hinzufügen(string username, string password, int userToAdd, int usergroup)
        /// <summary>
        /// PSP 8.2
        /// User einer Usergruppe hinzufügen
        /// </summary>
        /// <param name="username">den angemeldeten Usernamen übergeben (Überprüfung auf Rechte)</param>
        /// <param name="password">das dazugehörige Passwort übermitteln (Überprüfung auf Rechte)</param>
        /// <param name="userToAdd">Name des hinzuzufügenden Users</param>
        /// <param name="usergroup">Name der Gruppe</param>
        /// <returns>Erfolgreich true/false</returns>
        public static bool addUserToUsergroup(string username, string password, int userToAdd, int usergroup)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                try
                {
                    cont.sp_add_user_to_usergroup(username, pwhash, userToAdd, usergroup);
                    success = true;
                }


                catch (Exception e)
                {
                    success = false;
                }
            }
            return success;
        } 
        #endregion

        #region PSP 8.3 removeUser(string username, string password, int usernameId)
        /// <summary>
        /// PSP 8.3
        /// Entfernt User anhand von usernameId
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="usernameId"></param>
        /// <returns></returns>
        public static bool removeUser(string username, string password, int usernameId)
        {
            bool success = false;

            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                try
                {
                    //User wird gelöscht
                    cont.sp_delete_user(username, pwhash, usernameId);
                    success = true;
                }
                catch (Exception e)
                {
                    success = false;
                }
            }

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
        public static bool removeUser(string username, string password, string usernameName)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                //Durchlaufen der User mittels DB-Funktion
                foreach (var item in cont.fn_show_users(username, pwhash))
                {
                    //Prüfung des Usernamens
                    if (item.name == usernameName)
                    {
                        try
                        {
                            //Löschen des Users
                            //item.id = Id des Users
                            cont.sp_delete_user(username, pwhash, item.id);
                            success = true;
                        }
                        catch (Exception e)
                        {
                            success = false;
                        }
                    }
                }

                return success;
            }
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
            Byte[] pwhash = HelperClass.GetHash(password);
            
           
            return success;
        }
        #endregion

        #region PSP 9.2 editUserGroup (string username, int groupId)
        /// <summary>
        /// PSP 9.2
        /// //editUserGroup
        /// </summary>
        /// <param name="username"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        static bool editUserGroup(string username, int groupId)
        {
            bool success = false;
            
            
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                try
                {
                    //cont.sp_add_user_to_usergroup(   (username, groupId);
                    success = true;
                }
                catch (Exception e)
                {

                    success = false;
                }
            }
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
            Byte[] pwhash = HelperClass.GetHash(password);
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
        public static bool switchLamp(string username, string password, bool lampOnOff, int lampId)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                List<fn_show_lamps_Result> dbLamps = cont.fn_show_lamps(username, pwhash).ToList();
                int HueLampId = 0;
                string dbLampName = "";

                try
                {
                    foreach (var item in dbLamps)
                    {
                        if (lampId == item.id)
                        {
                            dbLampName = item.name;
                            break;
                        }
                    }
                    HueLampId = HueAccess.GetLampId(dbLampName);



                    if (lampOnOff == true)
                    {
                        cont.sp_lamp_on(username, pwhash, lampId);
                        // Vereinfachter aufruf über die HelperClass
                        HelperClass.SetLampState(HueLampId, true);
                    }
                    else
                    {
                        cont.sp_lamp_off(username, pwhash, lampId);
                        // Vereinfachter aufruf über die HelperClass
                        HelperClass.SetLampState(HueLampId, false);
                    }
                    success = true;
                    
                }
                catch (Exception e)
                {
                    success = false;
                }
            }
            return success;
        }
        #endregion

        #region PSP 15.1 dimLamp(string username, string password, int lampId, byte brightness,bool lampOnOff)
        /// <summary>
        /// PSP 15.1
        /// Lampen dimmen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampId"></param>
        /// <param name="brightness"></param>
        /// <param name="lampOnOff"></param>
        /// <returns></returns>
        public static bool dimLamp(string username, string password, int lampId, byte brightness, bool lampOnOff)
        {
            bool success = false;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                string dbLampName = "";
                //bool onOff = true;
                int hueId = 1;
                List<fn_show_lamps_Result> db = cont.fn_show_lamps(username, pwhash).ToList();

                try
                {
                    foreach (var item in db)
                    {
                        if (lampId == item.id)
                        {
                            dbLampName = item.name;
                            cont.sp_lamp_dimm(username, pwhash, item.id, brightness);
                       
                               

                                if (lampOnOff == true)
                            {
                                cont.sp_lamp_on(username, pwhash, lampId);
                                //onOff = true;
                            }
                            else
                            {
                                cont.sp_lamp_off(username, pwhash, lampId);
                                //onOff = false;
                            }
                           HelperClass.SetLampState(hueId, lampOnOff);
                        }
                   
                    }
                    success = true;
                }
                catch (Exception e)
                {
                    success = false;
                }                              
            }
            return success;  
        }
        #endregion

        #region PSP 15.2 dimLamp(string username, string password, string lampName, byte brightness)
        /// <summary>
        /// PSP 15.2
        /// Lampen dimmen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lampName"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static void dimLamp(string username, string password, string lampName, byte brightness)
        {
           
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                //ÜbergabeId 
                int hueId = 0;

                //Suche nach Lampe mittels DB-Funktion
                foreach (var item in cont.fn_show_lamps(username, pwhash))
                {
                    //Lampenname überprüfen
                    if (lampName == item.name)
                    {
                        //Holen der LampenId über HueAccess und speichern auf hueId
                        hueId = HueAccess.GetLampId(item.name);
                        cont.sp_lamp_dimm(username, pwhash, item.id, brightness);
                    }

                }
                
                //Setzt die Brightness für die Lampe(Ausführung)
                HelperClass.SetLampBrightness(hueId, brightness);

            }


        }
        #endregion

        #region PSP 16.1 userLogin(string username, string password)
        /// <summary>
        /// PSP 16.1
        /// User Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>true, wenn Anmeldedaten richtig sind ansonsten false</returns>
        public static int userLogin(string username, string password)
        {
            int userIs = 0;
            //userIs Codebelegung: 0 = Fehler, 1= Admin, 2= User, 3= nicht vorhanden

            List<int?> user = new List<int?>();
            List<int?> admin = new List<int?>();
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                try
                {
                    //Von der DB mit den übergebenen Usernamen und PW einen Table mit der UserId/AdminId
                    // anfordern. Wenn kein Eintrag vorhanden ist, ist der User
                    // mit den übergebenen Daten nicht berechtigt
                    user = cont.fn_check_user_table(username, pwhash).ToList();
                    admin = cont.fn_check_admin_table(username, pwhash).ToList();

                    //user vorhanden?
                    if (user[0].Value > 0)
                    {
                        userIs = 2;
                        //ist user admin?
                        if(admin[0].Value > 0)
                        {
                            userIs = 1;
                        }
                    }
                    else
                    {
                        //user nicht vorhanden
                        userIs = 3;
                    }
                 
                }
                catch (Exception e)
                 {
                    //probleme bei überprüfung
                    userIs = 0;
                 }
            }
            return userIs;
        }
        #endregion

        #region PSP 19.1 EditUserPassword(string username, string passwordNew, string passwordOld)
        /// <summary>
        /// PSP 19.1
        /// Edit UserPassword
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordOld"></param>
        /// <param name="passwordNew"></param>
        /// <returns>Bool ob erfolgreich</returns>
        public static bool EditUserPassword(string username, string passwordOld,string passwordNew )
        {
            bool success = false;
            //Übergebene Passwörte hashen und in Var speichern für Übergabe an DB
            Byte[] pwhashOld = HelperClass.GetHash(passwordOld);
            Byte[] pwhashNew = HelperClass.GetHash(passwordNew);
            using(itin18_aktEntities cont = new itin18_aktEntities())
            {
                try
                {
                    cont.sp_change_password(username, pwhashOld, pwhashNew);
                     success = true;
                }
                catch (Exception e)
                {

                     success = false;
                }
               
            }
            return success;
        }
        #endregion

        #region PSP 18.1 GetLogFile(string username, string password, DateTime beginDate, DateTime endDate)
        /// <summary>
        /// PSP 18.1
        /// Logfile von beginDate bis endDate in einer Liste retourgeben
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Liste des Datentyp's "fn_show_lamp_control_history_Result" </returns>
        public static List<fn_show_lamp_control_history_Result> GetLogFile(string username, string password, DateTime beginDate, DateTime endDate)
        {
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                List<fn_show_lamp_control_history_Result> tmp = new List<fn_show_lamp_control_history_Result>();
            
                try
                {

                    List<fn_show_lamp_control_history_Result> db = cont.fn_show_lamp_control_history(username, pwhash).ToList();

                    foreach (var item in db)
                    {
                        if (Convert.ToDateTime((item.date).Value.ToShortDateString()) >= Convert.ToDateTime(beginDate.ToShortDateString()) 
                         && Convert.ToDateTime((item.date).Value.ToShortDateString()) <= Convert.ToDateTime(endDate))
                        {
                            tmp.Add(item);
                        }
                    }
                    if (tmp.Count <= 0)
                    {
                        tmp.Add(new fn_show_lamp_control_history_Result { address = "", lamp_name = "Keine Daten" });
                    }
                    return tmp;
                }
                catch(Exception e)
                {
                    //Fehlermeldung in die leere Liste hinzufügen, die FM wird als name eingetragen
                    tmp.Add(new fn_show_lamp_control_history_Result { address = "", lamp_name = "Keine Datenbankverbindung" });
                    tmp.Add(new fn_show_lamp_control_history_Result { address = "", lamp_name = "No database connection" });

                    return tmp;
                }
            }
        }
        #endregion
        #region PSP 18.2 GetLogFileComplete(string username, string password)
        /// <summary>
        /// PSP 18.2
        /// Logfile in einer Liste retourgeben
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Liste des Datentyp's "fn_show_lamp_control_history_Result" </returns>
        public static List<fn_show_lamp_control_history_Result> GetLogFileComplete(string username, string password)
        {
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                List<fn_show_lamp_control_history_Result> tmp = new List<fn_show_lamp_control_history_Result>();

                try
                {

                    List<fn_show_lamp_control_history_Result> db = cont.fn_show_lamp_control_history(username, pwhash).ToList();

                    foreach (var item in db)
                    {
                            tmp.Add(item);
                    }
                    if (tmp.Count <= 0)
                    {
                        tmp.Add(new fn_show_lamp_control_history_Result { address = "", lamp_name = "Keine Daten" });
                    }
                    return tmp;
                }
                catch (Exception e)
                {
                    //Fehlermeldung in die leere Liste hinzufügen, die FM wird als name eingetragen
                    tmp.Add(new fn_show_lamp_control_history_Result { address = "", lamp_name = "Keine Datenbankverbindung" });
                    tmp.Add(new fn_show_lamp_control_history_Result { address = "", lamp_name = "No database connection" });

                    return tmp;
                }
            }
        }
        #endregion

        #region 19.2 Change Password by Admin(string username, string password, int changeId, string newpassword)
        /// <summary>
        /// 19.2 Change Password by Admin
        /// </summary>
        /// <param name="changeId">Id die geändert werden soll</param>
        /// <param name="newpassword"></param>
        /// <param name="password"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool ChangePasswordByAdmin(string username, string password, int changeId, string newpassword)
        {
            bool success = false;

            Byte[] pwhash = HelperClass.GetHash(password);
            Byte[] pwNewhash = HelperClass.GetHash(newpassword);

            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                try
                {
                    cont.sp_change_password_by_admin(username, pwhash, changeId, pwNewhash);
                    success = true;
                }
                catch (Exception e)
                {

                    success = false;
                }
            }
            return success;

        } 
        #endregion



        /// <summary>
        /// Die in der DB eingetragenen Lampennamen als Liste
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Liste des Datentyp's "fn_show_lamps_Result". D.h. einen Table aller Lampen</returns>
        public static List<fn_show_lamps_Result> GetAllLamps(string username, string password)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                List<fn_show_lamps_Result> tmp = new List<fn_show_lamps_Result>();

                try
                {
                    return cont.fn_show_lamps(username, pwhash).ToList();
                }
                catch (Exception e)
                {
                    //Fehlermeldung in die leere Liste hinzufügen, die FM wird als name eingetragen
                    tmp.Add(new fn_show_lamps_Result {address = "", name = "Keine Datenbankverbindung" });
                    tmp.Add(new fn_show_lamps_Result { address = "", name = "No database connection" });
                    return tmp;
                }
            }
        }

        /// <summary>
        /// Eine Liste welche zurechtgeschnitten ist für den LampControlController
        /// Gibt folgendes zurück: address, brightness, groupname, Lamp_id, lampname, status
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Liste des Datentyp's "fn_show_lamp_control_Result"</returns>
        public static List<fn_show_lamp_control_Result> GetAllLampsStatus(string username, string password)
        {
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                List<fn_show_lamp_control_Result> tmp = new List<fn_show_lamp_control_Result>();

                try
                {
                    return cont.fn_show_lamp_control(username, pwhash).ToList();
                }
                catch (Exception e)
                {
                    //Fehlermeldung in die leere Liste hinzufügen, die FM wird als Lampenname eingetragen
                    tmp.Add( new fn_show_lamp_control_Result { groupname=" ", address=" ", lampname = "Keine Datenbankverbindung" });
                    tmp.Add(new fn_show_lamp_control_Result { groupname = " ", address = " ", lampname = "No database connection" });
                    return tmp;
                }
            }
        }

        /// <summary>
        /// Die in der DB eingetragenen User als Liste
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Liste des Datentyp's "fn_show_users_Result". D.h. einen Table aller User</returns>
        public static List<fn_show_users_Result> GetAllUser(string username, string password)
        {
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                List<fn_show_users_Result> tmp = new List<fn_show_users_Result>();

                try
                {
                    return cont.fn_show_users(username, pwhash).ToList();
                }
                catch (Exception e)
                {
                    //Fehlermeldung in die leere Liste hinzufügen, die FM wird als Username eingetragen
                    tmp.Add(new fn_show_users_Result { group = " ", name = "Keine Datenbankverbindung" });
                    tmp.Add(new fn_show_users_Result { group =" ", name = "No database connection" });
                    return tmp;
                }
            }
        }

        /// <summary>
        /// Die in der DB eingetragenen Lampengruppe als Liste
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Liste des Datentyp's "fn_show_lampgroups_Result". D.h. einen Table aller User</returns>
        public static List<fn_show_lampgroups_Result> GetAllLampGroups(string username, string password)
        {
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                List<fn_show_lampgroups_Result> tmp = new List<fn_show_lampgroups_Result>();

                try
                {
                    return cont.fn_show_lampgroups(username, pwhash).ToList();
                }
                catch (Exception e)
                {
                    //Fehlermeldung in die leere Liste hinzufügen, die FM wird als Gruppenname eingetragen
                    tmp.Add(new fn_show_lampgroups_Result { roomgroupname = "Keine Datenbankverbindung" });
                    tmp.Add(new fn_show_lampgroups_Result { roomgroupname = "No database connection" });
                    return tmp;
                }
            }
        }

        /// <summary>
        /// Special 4 Bastl
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static List<fn_show_lamp_control_Result>GetLampControl(string username, string password)
        {
            Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                List<fn_show_lamp_control_Result> tmp = new List<fn_show_lamp_control_Result>();

                try
                {
                    return cont.fn_show_lamp_control(username, pwhash).ToList();
                }
                catch (Exception e)
                {
                    //Fehlermeldung in die leere Liste hinzufügen, die FM wird als Lampenname eingetragen
                    tmp.Add(new fn_show_lamp_control_Result { address = "", groupname = "", brightness = 0, status = true, lamp_id = 999, lampname = "Keine Datenbankverbindung" });
                    tmp.Add(new fn_show_lamp_control_Result { address = "", groupname = "", brightness = 0, status = true, lamp_id = 999, lampname = "No database connection" });
                    //tmp[0].groupname = "Keine Datenbankverbindung";
                    //tmp[1].groupname = "No database connection";
                    return tmp;
                }
            }
        }

    }
}
 
