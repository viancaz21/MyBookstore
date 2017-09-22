﻿using MyBookstore.App_Code;
using MyBookstore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookstore.Controllers
{
    public class PublishersController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<PublishersModels> list = new List<PublishersModels>();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT pubID, pubName FROM publishers";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                var pub = new PublishersModels();
                                pub.ID = Convert.ToInt32(row["pubID"].ToString());
                                pub.PublishersName = row["pubName"].ToString();
                                list.Add(pub);
                            }
                        }
                        return View(list);
                    }
                }
            }
        }
                      

        // GET: Publishers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Publishers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        [HttpPost]
        public ActionResult Create(PublishersModels publishers)
        {
            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO publishers VALUES (@pubName)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    
                    cmd.Parameters.AddWithValue("@pubName", publishers.PublishersName);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
            }
        }

        // GET: Publishers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) // record not selected
            {
                return RedirectToAction("Index");
            }

            PublishersModels publishers = new PublishersModels();

            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();

                string query = @"SELECT pubID, pubName FROM publishers WHERE pubID=@pubID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pubID", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                publishers.PublishersName = dr["pubName"].ToString();
                            }
                            return View(publishers);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
        }

        // POST: Publishers/Edit/5
        [HttpPost]
        public ActionResult Edit(PublishersModels publishers)
        { 
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE publishers SET pubName=@pubName
                WHERE pubID=@pubID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pubID", publishers.ID);
                    cmd.Parameters.AddWithValue("@pubName", publishers.PublishersName);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
}
            }

        }

        // GET: Publishers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"DELETE FROM publishers
                WHERE pubID=@pubID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pubID", id);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
            }


        }


        // POST: Publishers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
