using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLib
{
    class Containers
    {
        public static String Save(String arguments)
        {
            DBConnect db = new DBConnect();

            String[] values = Utils.PrepareArguments(arguments);
            String playerId = values[0];
            String containerName = values[1];
            String gearList = values[2];

            long? result;
            if (exists(db, playerId, containerName))
            {
                result = update(db, playerId, containerName, gearList);
            }
            else
            {
                result = insert(db, playerId, containerName, gearList);
            }

            if (result != null)
            {
                return string.Format("Saved {0} for {1} (id {2}))", containerName, playerId, result);
            }
            else
            {
                return string.Format("Failed to save {0} for {1}.", containerName, playerId);
            }
        }

        public static String Load(String arguments)
        {
            DBConnect db = new DBConnect();

            String[] values = Utils.PrepareArguments(arguments);
            String playerId = values[0];
            String containerName = values[1];

            String query = string.Format("SELECT gearList FROM containers WHERE playerId = '{0}' AND containerName = '{1}';", playerId, containerName);

            List<List<String>> containers = db.Select(query);
            if (containers != null)
            {
                String container = containers.First().First();
                Trace.TraceInformation("Loading container for " + playerId + " / " + containerName + " (" + container + ").");
                return container;
            }
            return null;
        }

        private static bool exists(DBConnect db, String playerId, String containerName)
        {
            string query = string.Format("SELECT * FROM containers WHERE playerId = '{0}' AND containerName = '{1}';", playerId, containerName);
            List<List<String>> result = db.Select(query);
            if (result != null && result.Count > 0)
            {
                Trace.TraceInformation("Container found for " + playerId + " / " + containerName + ".");
                return true;
            }
            return false;
        }

        private static long? insert(DBConnect db, String playerId, String containerName, String gearList)
        {
            Trace.TraceInformation("Inserting container for " + playerId + " / " + containerName + ".");
            String query = string.Format("INSERT INTO containers (playerId, containerName, gearList) VALUES ('{0}', '{1}', '{2}');", playerId, containerName, gearList);
            return db.Insert(query);
        }

        private static long? update(DBConnect db, String playerId, String containerName, String gearList)
        {
            Trace.TraceInformation("Updating container for " + playerId + " / " + containerName + ".");
            String query = string.Format("UPDATE containers SET containerName = '{0}', gearList = '{1}' WHERE playerId = '{2}';", containerName, gearList, playerId);
            return db.Update(query);
        }
    }
}
