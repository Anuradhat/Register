﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Register.Cls;
using Register.Models;
using Microsoft.Reporting.WebForms;

namespace Register.Controllers
{
    public class ReportsTrnController : Controller
    {
        clsReports reports = new clsReports();
        // GET: Reports

        [Authorize(Roles = "Reports - Reports")]
        public ActionResult Index()
        {
            List<emsReports> lstEmsReportses = reports.SelectAllReportslist();
            return View(lstEmsReportses);
        }

        [HttpPost]
        public ActionResult Details(string reportName, string serverPath, string reportFolder)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Pixel(1100);
            reportViewer.Height = Unit.Pixel(525);
            IReportServerCredentials irsc = new CustomReportCredentials("supertive-001", "Blackice#1977", "ifc");
            reportViewer.ServerReport.ReportServerCredentials = irsc;

            reportViewer.ServerReport.ReportServerUrl = new Uri(serverPath);
            reportViewer.ServerReport.ReportPath = @"/" + reportFolder + @"/" + reportName ;

            ViewBag.ReportViewer = reportViewer;

            return View();
            //Response.Redirect("~/WebForm/WebFormReport.aspx");
            //return new EmptyResult();
        }

        [HttpPost]
        public ActionResult webReport(string reportName, string serverPath, string reportFolder)
        {
            ViewBag.reportName = reportName;
            ViewBag.serverPath = serverPath;
            ViewBag.reportFolder = reportFolder;

            return View();
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reports/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reports/Delete/5
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
