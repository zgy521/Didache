﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Didache.TaskTypes {
	public class TaskTypeManager {
		public static List<TaskTypeInfo> GetTaskTypes() {

			// TODO: Caching

			List<TaskTypeInfo> taskTypes = new List<TaskTypeInfo>();

			Type iTaskType = typeof(ITaskType);

			List<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(t => iTaskType.IsAssignableFrom(t) && t != iTaskType).ToList();

			foreach (Type type in types) {
				TaskTypeInfo info = new TaskTypeInfo() {
					TaskType = type,
					ClassName = type.FullName,
					FriendlyName = type.Name,
					TaskInstance = (ITaskType) Activator.CreateInstance(type)
				};

				taskTypes.Add(info);

			}

			return taskTypes;
		}

		public static object ProcessFormCollection(string taskClassName, int taskID, int userID, FormCollection collection, HttpRequestBase request) {

			List<TaskTypeInfo> taskTypes = GetTaskTypes();
			TaskTypeInfo taskType = taskTypes.SingleOrDefault(i => i.FriendlyName == taskClassName);

			if (taskType != null) {
				return taskType.TaskInstance.ProcessFormCollection(taskID, userID, collection, request);
			} else {
				return null;
			}
		}
	}
}