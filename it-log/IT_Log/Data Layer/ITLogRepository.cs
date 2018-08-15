using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using IT_Log.Model;
using MySql.Data.MySqlClient;

namespace IT_Log.Data_Layer
{
    class ITLogRepository : IITLogRepository
    {
        string connStr = "server=192.168.77.163;user=mrwd;database=ittransactionlog;port=3308;password=Hydrogen1";

        public List<it_log_all> GetAll()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT l.it_log_id, l.name, l.office, l.date, l.time, l.service_request, p.it_personnel_name " +
                    "FROM it_log AS l INNER JOIN it_personnel AS p ON l.it_personnel_id = p.it_personnel_id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);
                conn.Close();

                List<it_log_all> list = new List<it_log_all>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    it_log_all log = new it_log_all();

                    log.id = Convert.ToInt32(dt.Rows[i]["it_log_id"]);
                    log.Name = dt.Rows[i]["name"].ToString();
                    log.Office = dt.Rows[i]["office"].ToString();
                    log.Date = Convert.ToDateTime(dt.Rows[i]["date"]);
                    log.Time = TimeSpan.Parse(dt.Rows[i]["time"].ToString());
                    log.Service_Request = dt.Rows[i]["service_request"].ToString();
                    log.IT_Personnel = dt.Rows[i]["it_personnel_name"].ToString();

                    list.Add(log);
                }
                return list;
            }
        }

        public it_log GetById(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT * FROM it_log WHERE it_log_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);
                conn.Close();

                it_log log = new it_log();
                log.id = Convert.ToInt32(dt.Rows[0]["it_log_id"]);
                log.Name = dt.Rows[0]["name"].ToString();
                log.Office = dt.Rows[0]["office"].ToString();
                log.Date = Convert.ToDateTime(dt.Rows[0]["date"]);
                log.Time = TimeSpan.Parse(dt.Rows[0]["time"].ToString());
                log.Service_Request = dt.Rows[0]["service_request"].ToString();
                log.IT_Personnel_id = Convert.ToInt32(dt.Rows[0]["it_personnel_id"]);

                return log;
            }
        }

        //public it_log Insert(it_log obj)
        //{
        //    using (ittransactionlogEntities db = new ittransactionlogEntities())
        //    {
        //        db.it_log.Add(obj);
        //        db.SaveChanges();
        //        return obj;
        //    }
        //}

        //public void Update(it_log obj)
        //{
        //    using (ittransactionlogEntities db = new ittransactionlogEntities())
        //    {

        //        db.it_log.Attach(obj);
        //        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //}

        //public void Delete(int id)
        //{
        //    using (ittransactionlogEntities db = new ittransactionlogEntities())
        //    {
        //        //using (dbemployeeEntities db = new dbemployeeEntities())
        //        //{
        //        //    db.Database.ExecuteSqlCommand("UPDATE employee_info SET lastname = 'SQLTEST' WHERE empID = 19");
        //        //}

        //        var delete = (from l in db.it_log
        //                      where l.it_log_id.Equals(id)
        //                      select l).FirstOrDefault();

        //        db.it_log.Attach(delete);
        //        db.it_log.Remove(delete);
        //        db.SaveChanges();
        //    }
        //}

        //public List<it_log_all> Search(string text)
        //{
        //    using (ittransactionlogEntities db = new ittransactionlogEntities())
        //    {
        //        var result = (from l in db.it_log
        //                      join p in db.it_personnel
        //                      on l.it_personnel_id equals p.it_personnel_id
        //                      where(l.name.Contains(text) || l.office.Contains(text) || 
        //                      l.service_request.Contains(text) || p.it_personnel_name.Contains(text))
        //                      select new it_log_all
        //                      {
        //                          id = l.it_log_id,
        //                          Name = l.name,
        //                          Office = l.office,
        //                          Date = l.date,
        //                          Time = l.time,
        //                          Service_Request = l.service_request,
        //                          IT_Personnel = p.it_personnel_name

        //                      }).ToList();

        //        return result;
        //    }
        //}

        //public List<it_log_all> SearchByDate(DateTime from, DateTime to)
        //{
        //    using (ittransactionlogEntities db = new ittransactionlogEntities())
        //    {
        //        /*var result = db.Database.SqlQuery<IList>("SELECT l.it_log_id, l.name, " +
        //            "l.office, l.date, l.time, l.service_request, p.it_personnel_name " +
        //            "FROM it_log AS l INNER JOIN it_personnel AS p ON l.it_personnel_id = p.it_personnel_id" +
        //            "WHERE l.date BETWEEN DATE(@from) AND DATE(@to)", 
        //            new SqlParameter("@from", from), 
        //            new SqlParameter("@to", to)).ToList();
        //            */

        //        var result = (from l in db.it_log
        //                      join p in db.it_personnel
        //                      on l.it_personnel_id equals p.it_personnel_id
        //                      where (l.date >= @from.Date && l.date <= @to.Date)
        //                      select new it_log_all
        //                      {
        //                          id = l.it_log_id,
        //                          Name = l.name,
        //                          Office = l.office,
        //                          Date = l.date,
        //                          Time = l.time,
        //                          Service_Request = l.service_request,
        //                          IT_Personnel = p.it_personnel_name

        //                      }).ToList();

        //        return result;
        //    }
        //}

        //public List<it_log_all> SearchByPersonnel(int id)
        //{
        //    using (ittransactionlogEntities db = new ittransactionlogEntities())
        //    {
        //        /*var result = db.Database.SqlQuery<IList>("SELECT l.it_log_id, l.name, " +
        //            "l.office, l.date, l.time, l.service_request, p.it_personnel_name " +
        //            "FROM it_log AS l INNER JOIN it_personnel AS p ON l.it_personnel_id = p.it_personnel_id" +
        //            "WHERE l.date BETWEEN DATE(@from) AND DATE(@to)", 
        //            new SqlParameter("@from", from), 
        //            new SqlParameter("@to", to)).ToList();
        //            */

        //        var result = (from l in db.it_log
        //                      join p in db.it_personnel
        //                      on l.it_personnel_id equals p.it_personnel_id
        //                      where l.it_personnel_id == id
        //                      select new it_log_all
        //                      {
        //                          id = l.it_log_id,
        //                          Name = l.name,
        //                          Office = l.office,
        //                          Date = l.date,
        //                          Time = l.time,
        //                          Service_Request = l.service_request,
        //                          IT_Personnel = p.it_personnel_name

        //                      }).ToList();

        //        return result;
        //    }
        //}
    }
}
