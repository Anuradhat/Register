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

        private clsMember clsMember = new clsMember();
        // GET: Member
        [Authorize(Roles = "Member - Details")]
        public ActionResult Index()
        {
            List<MemberM> _Member = clsMember.SelectAllActiveMemberlist();
            return View(_Member);
        }

        // GET: Member/Details/5
        public ActionResult Details(string MemberID)
        {
            MemberM _MemberDetails = clsMember.SelectAllMemberByMemberID(MemberID);

            List<GenderM> GenderList = _gender.SelectAllGender();
            ViewBag.GenderList = new SelectList(GenderList, "GenderCode", "Gender", _MemberDetails.GenderCode);

            List<MaritalStatusM> MaritalStatusList = _MaritalStatus.SelectAllMaritalStatus();
            ViewBag.MaritalStatusList = new SelectList(MaritalStatusList, "MaritalStatusCode", "MaritalStatus", _MemberDetails.MaritalStatusCode);

            List<ReligionM> ReligionList = _Religion.SelectAllReligion();
            ViewBag.ReligionList = new SelectList(ReligionList, "ReligionCode", "ReligionName", _MemberDetails.ReligionCode);

            return View(_MemberDetails);
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
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Member/Edit/
        [Authorize(Roles = "Member - Edit")]
        public ActionResult Edit(string MemberID)
        {
            MemberM _MemberDetails = clsMember.SelectAllMemberByMemberID(MemberID);

            List<GenderM> GenderList = _gender.SelectAllGender();
            ViewBag.GenderList = new SelectList(GenderList, "GenderCode", "Gender", _MemberDetails.GenderCode);

            List<MaritalStatusM> MaritalStatusList = _MaritalStatus.SelectAllMaritalStatus();
            ViewBag.MaritalStatusList = new SelectList(MaritalStatusList, "MaritalStatusCode", "MaritalStatus", _MemberDetails.MaritalStatusCode);

            List<ReligionM> ReligionList = _Religion.SelectAllReligion();
            ViewBag.ReligionList = new SelectList(ReligionList, "ReligionCode", "ReligionName", _MemberDetails.ReligionCode);

            return View(_MemberDetails);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [Authorize(Roles = "Member - Edit")]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                var test = Convert.ToBoolean(collection["GCEAL"].Split(',')[0]);

                clsMember.EditMember(collection["MemberID"], collection["NIC"], collection["FullName"], collection["NameWithInitials"],
                    collection["CallingName"], DateTime.Parse(collection["DateOfBirth"]), collection["Marital"], collection["Gender"],
                    collection["Religion"], collection["Address"], collection["City"], collection["Country"], int.Parse(collection["MobileNo"]),
                    int.Parse(collection["HomeTel"]), collection["Email"], Convert.ToBoolean(collection["GCEOL"].Split(',')[0]), Convert.ToBoolean(collection["GCEAL"].Split(',')[0]),
                    collection["OtherQualification"], Convert.ToBoolean(collection["ActiveMember"].Split(',')[0]), collection["Comments"]);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Member/Delete/5
        [Authorize(Roles = "Member - Delete")]
        public ActionResult Delete(string MemberID)
        {
            MemberM _MemberDetails = clsMember.SelectAllMemberByMemberID(MemberID);

            List<GenderM> GenderList = _gender.SelectAllGender();
            ViewBag.GenderList = new SelectList(GenderList, "GenderCode", "Gender", _MemberDetails.GenderCode);

            List<MaritalStatusM> MaritalStatusList = _MaritalStatus.SelectAllMaritalStatus();
            ViewBag.MaritalStatusList = new SelectList(MaritalStatusList, "MaritalStatusCode", "MaritalStatus", _MemberDetails.MaritalStatusCode);

            List<ReligionM> ReligionList = _Religion.SelectAllReligion();
            ViewBag.ReligionList = new SelectList(ReligionList, "ReligionCode", "ReligionName", _MemberDetails.ReligionCode);

            return View(_MemberDetails);
        }

        // POST: Member/Delete/5
        [HttpPost]
        [Authorize(Roles = "Member - Delete")]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                clsMember.DeleteMember(collection["MemberID"]);

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
