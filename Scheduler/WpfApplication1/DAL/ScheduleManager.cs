using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApplication1.BL;
using WpfApplication1.ViewModels;

namespace WpfApplication1
{
    public class ScheduleManager
    {
         const string connectionString = "Server=85.92.146.196;port=5432;Database=bodyview3;UserID=postgres;Password=Banek12";
        public static void AddScriptSchedule(ScriptSchedule scriptSchedule)
        {
            var res = Serializer.Serialize(scriptSchedule.Schedule);

            
            string insertQuery = "INSERT INTO scriptsschedule(scriptid, schedule, schedulename) VALUES (@scriptid, @schedule, @schedulename)";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {

                NpgsqlCommand commandInsert = new NpgsqlCommand(insertQuery, connection);
                // ScriptID
                commandInsert.Parameters.Add("@scriptid", scriptSchedule.ScriptId);

                //// serialized schedule
                commandInsert.Parameters.Add("@schedule", res);

                //// schecudleName
                commandInsert.Parameters.Add("@schedulename", scriptSchedule.ScheduleName);

                try
                {
                    connection.Open();
                    commandInsert.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void UpdateScriptSchedule(ScriptSchedule scriptSchedule)
        {
            // TODO: Update existing script schedule in database
             var res = Serializer.Serialize(scriptSchedule.Schedule);


             string updateQuery = @"update  scriptsschedule set scriptid= @scriptid , schedule= @schedule , schedulename= @schedulename where scriptsscheduleid= @scriptsscheduleid";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                
                NpgsqlCommand commandUpdate= new NpgsqlCommand(updateQuery, connection);
                commandUpdate.Parameters.Add("@scriptid", scriptSchedule.ScriptId);
                commandUpdate.Parameters.Add("@schedule", res);
                commandUpdate.Parameters.Add("@schedulename", scriptSchedule.ScheduleName);
                commandUpdate.Parameters.Add("@scriptsscheduleid", scriptSchedule.ScriptScheduleId);
            
                try
                {
                    connection.Open();
                    commandUpdate.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        public static void DeleteScriptSchedule(int scriptscheduleId)
        {
            // TODO: Delete existing script schedule

            string deleteQuery = "delete from scriptsschedule where scriptsscheduleid  = @scriptsscheduleid";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {

                NpgsqlCommand commandDelete = new NpgsqlCommand(deleteQuery, connection);
                commandDelete.Parameters.Add("@scriptsscheduleid", scriptscheduleId);
                try
                {
                    connection.Open();
                    commandDelete.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        public static List<ScriptSchedule> GetScriptschedules()
        {
          
            string selectQuery = "select * from scriptsschedule";
            var listOfschedules = new List<ScriptSchedule>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {

                connection.Open();
                NpgsqlCommand commandSelect = new NpgsqlCommand(selectQuery, connection);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = commandSelect.ExecuteReader();

                // Output rows
                while (dr.Read())
                {

                    listOfschedules.Add(new ScriptSchedule()
                    {
                        ScriptScheduleId = (int)dr["scriptsscheduleid"],
                        ScheduleName = (string)dr["schedulename"],
                        ScriptId = (int)dr["scriptid"],
                        Schedule = Serializer.Deserialize<Schedule>((string)dr["schedule"])
                    });
                }
            }
            return listOfschedules;
        }
        public  static void AddScriptsToModel(ScriptSchedulerViewModel context)
        {
            
            string selectQuery = "select scriptid ,scriptname from script";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                NpgsqlCommand commandSelect = new NpgsqlCommand(selectQuery, connection);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = commandSelect.ExecuteReader();

                // Output rows
                while (dr.Read())
                {
                    context.Scripts.Add(new ScriptItem((int)dr["scriptid"], dr["scriptname"].ToString()));
                }
            }
        }
       
    }
}
