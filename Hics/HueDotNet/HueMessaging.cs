﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;

namespace huedotnet
{
    public class HueMessaging
    {
        private String bridgeIP;
        private String username;
        

        /// <summary>
        /// Speichern der IP und des Usernamens der Bridge
        /// </summary>
        /// <param name="bridgeIP">IP der Bridge</param>
        /// <param name="username">Vorhandener Username in der Bridge</param>
        public HueMessaging(string bridgeIP, string username)
        {
            this.bridgeIP = bridgeIP;
            this.username = username;
        }


        /// <summary>
        /// Das zusammen gefügte Json-Kommando and die Bridge senden
        /// D.h.: on/off,Brightness,.........lampState..)
        /// </summary>
        /// <param name="lampState"></param>
        public void SendMessage(HueLamp lampState)
        {
            WebClient webClient = new WebClient();
            webClient.BaseAddress = "http://" + bridgeIP + "/api/" + username + "/lights/" + lampState.GetLampNumber() + "/state";
            String json = lampState.GetJson();

            Stream writeData = webClient.OpenWrite(webClient.BaseAddress, "PUT");
            writeData.Write(Encoding.ASCII.GetBytes(lampState.GetJson()), 0, lampState.GetJson().Length);
            writeData.Close();
        }

        private void SendMessage(object lampState)
        {
            HueLamp lamp = (HueLamp)lampState;
            SendMessage(lamp);
        }

        public void SendMessage(List<HueLamp> lampStates)
        {
            foreach (HueLamp lamp in lampStates)
            {
                new Thread(new ParameterizedThreadStart(SendMessage)).Start(lamp);
            }
        }

        /// <summary>
        /// Aktuelle Daten der Bridge übermitteln
        /// </summary>
        /// <returns>Url samt IP und Username</returns>
        public String DownloadState()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.BaseAddress = "http://" + bridgeIP + "/api/" + username + "/";
                return webClient.DownloadString(webClient.BaseAddress);
            }
            catch 
            {

                return "Network error"; 
            }
        }

    }
}
