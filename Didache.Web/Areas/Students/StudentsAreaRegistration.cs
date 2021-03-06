﻿using System.Web.Mvc;

namespace Didache.Web.Areas.Students {
	public class StudentsAreaRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "Students";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			// special one for just /courses/
			context.MapRoute(
				"Courses_home",
				"courses",
				new { controller = "Courses", action = "Index" }
			);

			// API
			context.MapRoute(
				"Courses_API",
				"courses/api/{action}/{taskid}",
				new { controller = "Api", action = "taskstatus", taskid = UrlParameter.Optional }
			);


			// discussion
			context.MapRoute(
				"Courses_Discussion",
				"courses/{slug}/discussion/{action}/{id}",
				new { controller = "Discussion", action = "Index", id = UrlParameter.Optional }
			);

			// files
			context.MapRoute(
				"Coursefiles",
				"courses/coursefile/{id}/{*filename}",
				new { controller = "Courses", action = "CourseFile" }
			);
			context.MapRoute(
				"studentfiles",
				"courses/studentfile/{id}/{*filename}",
				new { controller = "Courses", action = "StudentFile" }
			);
			context.MapRoute(
				"gradedfiles",
				"courses/gradedfile/{id}/{*filename}",
				new { controller = "Courses", action = "GradedFile" }
			);
			context.MapRoute(
				"downloadall",
				"courses/downloadall/{slug}.zip",
				new { controller = "Courses", action = "DownloadAll" }
			);

			context.MapRoute(
				"rss",
				"courses/feed/{userID}/{slug}-{type}.{extension}",
				new { controller = "Courses", action = "feed" }
			);

			// ical
			context.MapRoute(
				"ics",
				"courses/ical/{userID}/{slug}-{type}.{extension}",
				new { controller = "Courses", action = "ical" }
			);

			// main six tabs
			context.MapRoute(
				"Courses",
				"courses/{slug}/{action}/{id}",
				new { controller = "Courses", action = "Schedule", id = UrlParameter.Optional }
			);


		}
	}
}
