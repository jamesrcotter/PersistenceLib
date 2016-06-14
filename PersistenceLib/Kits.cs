using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLib
{
    public static class Kits
    {
        public static String SaveKit(String arguments)
        {
            DBConnect db = new DBConnect();

            String[] values = prepareArguments(arguments);
            String playerId = values[0];
            String kitName = values[1];
            String gearList = values[2];

            long? result;
            if (kitExists(db, playerId, kitName))
            {
                result = updateKit(db, playerId, kitName, gearList);
            }
            else
            {
                result = insertKit(db, playerId, kitName, gearList);
            }

            if (result != null)
            {
                return string.Format("Saved {0} for {1} (id {2}))", kitName, playerId, result);
            }
            else
            {
                return string.Format("Failed to save {0} for {1}.", kitName, playerId);
            }
        }

        public static String LoadKit(String arguments)
        {
            DBConnect db = new DBConnect();

            String[] values = prepareArguments(arguments);
            String playerId = values[0];
            String kitName = values[1];

            String query = string.Format("SELECT gearList FROM players WHERE playerId = '{0}' AND kitName = '{1}';", playerId, kitName);

            List<List<String>> kits = db.Select(query);
            if (kits != null)
            {
                String kit = kits.First().First();
                Trace.TraceInformation("Loading kit for " + playerId + " / " + kitName + " (" + kit + ").");
                return kit;
            }
            return null;
        }

        private static String[] prepareArguments(String arguments)
        {
            return arguments.Replace("\"", "").Split('~');
        }

        private static bool kitExists(DBConnect db, String playerId, String kitName)
        {
            string query = string.Format("SELECT * FROM players WHERE playerId = '{0}' AND kitName = '{1}';", playerId, kitName);
            List<List<String>> result = db.Select(query);
            if (result != null && result.Count > 0)
            {
                Trace.TraceInformation("Kit found for " + playerId + " / " + kitName + ".");
                return true;
            }
            return false;
        }

        private static long? insertKit(DBConnect db, String playerId, String kitName, String gearList)
        {
            Trace.TraceInformation("Inserting kit for " + playerId + " / " + kitName + ".");
            String query = string.Format("INSERT INTO players (playerId, kitName, gearList) VALUES ('{0}', '{1}', '{2}');", playerId, kitName, gearList);
            return db.Insert(query);
        }

        private static long? updateKit(DBConnect db, String playerId, String kitName, String gearList)
        {
            Trace.TraceInformation("Updating kit for " + playerId + " / " + kitName + ".");
            String query = string.Format("UPDATE players SET kitName = '{0}', gearList = '{1}' WHERE playerId = '{2}';", kitName, gearList, playerId);
            return db.Update(query);
        }
    }
}
