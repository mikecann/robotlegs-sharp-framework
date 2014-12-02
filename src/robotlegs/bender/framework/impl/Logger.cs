//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using robotlegs.bender.framework.api;


namespace robotlegs.bender.framework.impl
{
	/// <summary>
	/// Default Robotlegs logger
	/// </summary>
	public class Logger : ILogger
	{
		
		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/
		
		private object _source;
		
		private ILogTarget _target;
		
		/*============================================================================*/
		/* Constructor                                                                */
		/*============================================================================*/

		/// <summary>
		/// Creates a new logger
		/// </summary>
		/// <param name="source">The log source object</param>
		/// <param name="target">The log target</param>
		public Logger (object source, ILogTarget target)
		{
			_source = source;
			_target = target;
		}
		
		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/

		public void Debug (object message, params object[] messageParameters)
		{
			_target.Log(_source, LogLevel.DEBUG, DateTime.Now, message, messageParameters);
		}

		public void Info (object message, params object[] messageParameters)
		{
			_target.Log(_source, LogLevel.INFO, DateTime.Now, message, messageParameters);
		}

		public void Warn (object message, params object[] messageParameters)
		{
			_target.Log(_source, LogLevel.WARN, DateTime.Now, message, messageParameters);
		}

		public void Error (object message, params object[] messageParameters)
		{
			_target.Log(_source, LogLevel.ERROR, DateTime.Now, message, messageParameters);
		}

		public void Fatal (object message, params object[] messageParameters)
		{
			_target.Log(_source, LogLevel.FATAL, DateTime.Now, message, messageParameters);
		}
	}
}
