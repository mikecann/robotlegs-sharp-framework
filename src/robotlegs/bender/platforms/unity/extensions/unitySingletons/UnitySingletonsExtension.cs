//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using robotlegs.bender.extensions.contextview.api;
using robotlegs.bender.framework.api;
using robotlegs.bender.platforms.unity.extensions.unitySingletons.impl;
using UnityEngine;

namespace robotlegs.bender.platforms.unity.extensions.unitySingletons
{
	public class UnitySingletonsExtension : IExtension
	{
		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private IInjector _injector;
		
		private SingletonFactory _singletonFactory;

		private UnitySingletons unitySingletons;
		
		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/

		public void Extend (IContext context)
		{
			_injector = context.injector;

			_singletonFactory = new SingletonFactory (_injector);

			context.BeforeInitializing (BeforeInitializing);
			context.BeforeDestroying(BeforeDestroying);
		}
		
		/*============================================================================*/
		/* Private Functions                                                          */
		/*============================================================================*/

		private void BeforeInitializing()
		{
			if (_injector.HasDirectMapping(typeof(IContextView)))
			{
				IContextView contextView = _injector.GetInstance(typeof(IContextView)) as IContextView;
				unitySingletons = (contextView.view as Transform).gameObject.AddComponent<UnitySingletons>();
				unitySingletons.SetFactory(_singletonFactory);
			}
		}

		private void BeforeDestroying()
		{
			if (unitySingletons != null) 
			{
				GameObject.Destroy(unitySingletons);
			}
			_singletonFactory.Destroy();
		}
	}
}