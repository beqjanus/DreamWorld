using log4net;
using System;
using System.Net;
using System.Reflection;
using OpenMetaverse;
using Nini.Config;

namespace OpenSim.Region.Framework
{
    internal class SmartStart
    {
        //SmartStart
        private static bool m_ALT_Enabled = false;

        private static Int32 m_DiagnosticsPort;
        private static string m_MachineID;
        private static string m_PrivURL;

        //DreamGrid
        public string GetALTRegion(String regionName, UUID agentID)
        {
            // !!!  DreamGrid Smart Start sends requested Region UUID to Dreamgrid.
            // If region is on line, returns same UUID. If Offline, returns UUID for Welcome, brings up the region and teleports you to it.

            string url = m_PrivURL + ":" + m_DiagnosticsPort + "?alt=" + regionName + "&agent=RegionName&agentid=" + agentID + "&password=" + m_MachineID;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Timeout = 15000; //15 Second Timeout

            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                string Result = String.Empty;
                string tempStr = reader.ReadLine();
                while (tempStr != null)
                {
                    Result = Result + tempStr;
                    tempStr = reader.ReadLine();
                }

                regionName = Result;
            }
            catch (WebException ex)
            {
            }

            return regionName;
        }
    }
}
