//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Reflection;
using robotlegs.bender.extensions.mediatorMap.api;

namespace robotlegs.bender.extensions.mediatorMap.impl
{
	public class MediatorManager
	{
		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/
		
		private MediatorFactory _factory;

		/*============================================================================*/
		/* Constructor                                                                */
		/*============================================================================*/
		
		public MediatorManager (MediatorFactory factory)
		{
			_factory = factory;
		}
		
		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/
		
		public void AddMediator(object mediator, object item, IMediatorMapping mapping)
		{
			//TODO: DO I want ot handle lifecycle items to be initialiazed first?

			if (item is IView && mapping.AutoRemoveEnabled)
				(item as IView).RemoveView += HandleRemoveView;

			InitializeMediator(mediator, item);
		}

		public void RemoveMediator(object mediator, object item, IMediatorMapping mapping)
		{
			if (item is IView)
				(item as IView).RemoveView -= HandleRemoveView;;

			DestroyMediator(mediator);
		}

		/*============================================================================*/
		/* Private Functions                                                          */
		/*============================================================================*/
		
		private void HandleRemoveView (IView view)
		{
			_factory.RemoveMediators(view);
		}

		private void InitializeMediator(object mediator, object mediatedItem)
		{
			Type mediatorType = mediator.GetType();

			CallMethod("PreInitialize", mediatorType, mediator);
			SetField("viewComponent", mediatorType, mediator, mediatedItem);
			CallMethod("Initialize", mediatorType, mediator);
			CallMethod("PostInitialize", mediatorType, mediator);
		}

		private void DestroyMediator(object mediator)
		{
			Type mediatorType = mediator.GetType();

			CallMethod("PreDestroy", mediatorType, mediator);
			CallMethod("Destroy", mediatorType, mediator);
			SetField("viewComponent", mediatorType, mediator, null);
			CallMethod("PostDestroy", mediatorType, mediator);
		}
		
		private bool CallMethod(string methodName, Type mediatorType, object instance)
		{
			MethodInfo methodInfo = mediatorType.GetMethod(methodName);
			if (methodInfo == null)
				return false;

			methodInfo.Invoke(instance, null);
			return true;
		}
		
		private object SetField(string fieldName, Type mediatorType, object instance, object fieldValue)
		{
			FieldInfo fieldInfo = mediatorType.GetField(fieldName);
			if (fieldInfo == null)
				return false;

			fieldInfo.SetValue(instance, fieldValue);
			return true;
		}
	}
}
