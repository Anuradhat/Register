using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Register.Cls;
using Register.Models;

namespace Register.Controllers
{
    public class MemberController : Controller
    {
        clsGender _gender = new clsGender();
        clsMaritalStatus _MaritalStatus = new clsMaritalStatus();
        clsReligion _Religion = new clsReligion();

        private clsMember clsMember =  new clsMember();
        // GET: Member
        [Authorize(Roles = "Member - Details")]
        public ActionResult Index()
        {
            List<MemberM> _Member = clsMember.SelectAllActiveMemberlist();
            return View(_Member);
        }

        // GET: Member/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Member/Create
        [Authorize(Roles = "Member - Create")]
        public ActionResult Create()
        {
            List<GenderM> GenderList = _gender.SelectAllGender();
            ViewBag.GenderList = new SelectList(GenderList, "GenderCode", "Gender");

            List<MaritalStatusM> MaritalStatusList = _MaritalStatus.SelectAllMaritalStatus();
            ViewBag.MaritalStatusList = MaritalStatusList;

            List<ReligionM> ReligionList = _Religion.SelectAllReligion();
            ViewBag.ReligionList = new SelectList(ReligionList, "ReligionCode", "ReligionName");

            return View();
        }

        // POST: Member/Create
        [Authorize(Roles = "Member - Create")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var test = Convert.ToBoolean(collection["GCEAL"].Split(',')[0]);

                clsMember.CreateMember(collection["MemberID"], collection["NIC"], collection["FullName"], collection["NameWithInitials"],
                    collection["CallingName"], DateTime.Parse(collection["DateOfBirth"]), collection["Marital"], collection["Gender"],
                    collection["Religion"], collection["Address"], collection["City"], collection["Country"], int.Parse(collection["MobileNo"]),
                    int.Parse(collection["HomeTel"]), collection["Email"], Convert.ToBoolean(collection["GCEOL"].Split(',')[0]), Convert.ToBoolean(collection["GCEAL"].Split(',')[0]),
                    collection["OtherQualification"], Convert.ToBoolean(collection["ActiveMember"].Split(',')[0]), collection["Comments"]);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Member/Edit/5
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

        // GET: Member/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Member/Delete/5
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

        [HttpPost]
        public JsonResult MemberDetailsByID(string MemberID)
        {
            var _MemberDetails = clsMember.SelectAllMemberlistByMemberID(MemberID);

            return Json(_MemberDetails, JsonRequestBehavior.AllowGet);
        }
    }
}
