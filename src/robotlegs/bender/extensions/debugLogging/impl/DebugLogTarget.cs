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

namespace robotlegs.bender.extensions.debugLogging.impl
{
	public class DebugLogTarget : ILogTarget
	{
		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/
		
		private IContext _context;
		
		/*============================================================================*/
		/* Constructor                                                                */
		/*============================================================================*/
		
		/**
		 * Creates a Debug Log Target
		 * @param context Context
		 */
		
		public DebugLogTarget (IContext context)
		{
			_context = context;
		}
		
		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/
		
		public void Log (object source, robotlegs.bender.framework.impl.LogLevel level, DateTime timestamp, object message, params object[] messageParameters)
		{
			UnityEngine.Debug.Log(string.Format(timestamp.ToLongTimeString()
			                      + " " + level.ToString()
			                      + " " + _context
			                      + " " + source
			                      + " " + message, messageParameters));
		}
	}
}
