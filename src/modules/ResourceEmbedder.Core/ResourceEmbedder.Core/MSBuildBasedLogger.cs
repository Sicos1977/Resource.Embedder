﻿using System;
using Microsoft.Build.Framework;

namespace ResourceEmbedder.Core
{
	public class MSBuildBasedLogger : ILogger
	{
		#region Fields

		private readonly IBuildEngine _buildEngine;
		private readonly string _sender;

		#endregion Fields

		#region Constructors

		/// <summary>
		/// Creates a logger that uses the MS build engine to issue log statements.
		/// </summary>
		/// <param name="buildEngine"></param>
		/// <param name="sender">The sender name that will be used in the MS build log.</param>
		public MSBuildBasedLogger(IBuildEngine buildEngine, string sender)
		{
			if (buildEngine == null)
				throw new ArgumentNullException("buildEngine");
			if (sender == null)
				throw new ArgumentNullException("sender");

			_buildEngine = buildEngine;
			_sender = sender;
		}

		#endregion Constructors

		#region Methods

		public void LogError(string message, params object[] args)
		{
			_buildEngine.LogErrorEvent(new BuildErrorEventArgs("", "", "", 0, 0, 0, 0, string.Format(message, args), "", _sender));
		}

		public void LogInfo(string message, params object[] args)
		{
			_buildEngine.LogMessageEvent(new BuildMessageEventArgs(string.Format(message, args), "", _sender, MessageImportance.Normal));
		}

		public void LogWarning(string message, params object[] args)
		{
			_buildEngine.LogWarningEvent(new BuildWarningEventArgs("", "", "", 0, 0, 0, 0, string.Format(message, args), "", _sender));
		}

		#endregion Methods
	}
}