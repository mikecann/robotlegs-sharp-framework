//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using robotlegs.bender.extensions.matching;
using robotlegs.bender.extensions.mediatorMap.api;
using robotlegs.bender.extensions.mediatorMap.dsl;
using robotlegs.bender.extensions.viewManager.api;
using robotlegs.bender.framework.api;

namespace robotlegs.bender.extensions.mediatorMap.impl
{
	public class MediatorMap : IMediatorMap, IViewHandler
	{
		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private Dictionary<string, MediatorMapper> _mappers = new Dictionary<string, MediatorMapper>();

		private ILogger _logger;

		private IMediatorFactory _factory;

		private MediatorViewHandler _viewHandler;

		private static readonly IMediatorUnmapper NULL_UNMAPPER = new NullMediatorUnmapper();
		
		/*============================================================================*/
		/* Constructor                                                                */
		/*============================================================================*/

		public MediatorMap (IContext context)
		{
			_logger = context.GetLogger(this);
			_factory = new MediatorFactory(context.injector);
			_viewHandler = new MediatorViewHandler(_factory);
		}

		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/
		
		public IMediatorMapper MapMatcher(ITypeMatcher matcher)
		{
			string descriptor = matcher.CreateTypeFilter().Descriptor;
			if (!_mappers.ContainsKey (descriptor))
			{
				_mappers [descriptor] = CreateMapper (matcher);
			}

			return _mappers[descriptor];
		}

		public IMediatorMapper Map<T>()
		{
			return Map (typeof(T));
		}

		public IMediatorMapper Map(Type type)
		{
			return MapMatcher(new TypeMatcher().AllOf(type));
		}

		public IMediatorUnmapper UnmapMatcher(ITypeMatcher matcher)
		{
			string descriptor = matcher.CreateTypeFilter().Descriptor;
			return _mappers.ContainsKey(descriptor) ? _mappers[descriptor] : NULL_UNMAPPER;
		}

		public IMediatorUnmapper Unmap<T>()
		{
			return Unmap (typeof(T));
		}

		public IMediatorUnmapper Unmap(Type type)
		{
			return UnmapMatcher(new TypeMatcher().AllOf(type));
		}

		public void HandleView(object view, Type type)
		{
			_viewHandler.HandleView(view, type);
		}

		public void Mediate(object item)
		{
			_viewHandler.HandleView(item, item.GetType());
		}

		public void Unmediate(object item)
		{
			_factory.RemoveMediators(item);
		}

		public void UnmediateAll()
		{
			_factory.RemoveAllMediators();
		}

		/*============================================================================*/
		/* Private Functions                                                          */
		/*============================================================================*/

		private MediatorMapper CreateMapper(ITypeMatcher matcher)
		{
			return new MediatorMapper(matcher.CreateTypeFilter(), _viewHandler, _logger);
		}
	}
}

