﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Didache.Web.Areas.Students.Controllers
{
    public class CoursesController : Controller
    {
		DidacheDb db = new DidacheDb();

		public ActionResult Index() {

			List<Course> courses = null;
			User profile = Users.GetLoggedInProfile();

			if (profile != null)
				courses = Didache.Courses.GetUsersCourses(profile.UserID, CourseUserRole.Student);
			else
				courses = new List<Course>();

			return View(courses);
		}


		public ActionResult Dashboard(string slug) {

			Course course = Didache.Courses.GetCourseBySlug(slug);

			return View(course);
		}

		public ActionResult Schedule(string slug, int? id) {

			Course course = Didache.Courses.GetCourseBySlug(slug);
			List<Unit> units = Didache.Units.GetCourseUnits(course.CourseID);
			Unit currentUnit = null;
			List<UserTaskData> userTasks = null;

			if (id.HasValue) {
				currentUnit = units.AsQueryable().SingleOrDefault(u => u.UnitID == id.Value);
			} else {
				currentUnit = units.AsQueryable().SingleOrDefault(u => u.StartDate <= DateTime.Now && u.EndDate >= DateTime.Now);
			}

			if (currentUnit != null) {
				userTasks = Tasks.GetUserTaskDataInUnit(currentUnit.UnitID, Users.GetLoggedInProfile().UserID);
			}
			
			ViewBag.Units = units;
			ViewBag.CurrentUnit = currentUnit;
			ViewBag.UserTasks = userTasks;

			return View();
		}

		public ActionResult Files(string slug) {
			return View();
		}

		public ActionResult Roster(string slug) {

			//List<CourseUser> users = Didache.Courses.GetUsersInCourse(Didache.Courses.GetCourseBySlug(slug).CourseID, CourseUserRole.Student);

			//List<CourseUserGroup> users = Didache.Courses.GetUsersInCourse(Didache.Courses.GetCourseBySlug(slug).CourseID, CourseUserRole.Student);

			Course course = Didache.Courses.GetCourseBySlug(slug);

			List<CourseUserGroup> usergroups = Didache.Courses.GetCourseUserGroups(course.CourseID);

			return View(usergroups);
		}

		public ActionResult Assignments(string slug) {
			return View();
		}

		public ActionResult Forums(string slug) {
			return View();
		}
    }
}
