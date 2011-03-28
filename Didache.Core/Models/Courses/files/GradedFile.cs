﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Didache {

	public class GradedFile : BaseFile {

		public string FileUrl {
			get {
				return "/courses/gradedfile/" + FileID + "/" + Filename;
			}
		}
	}
}