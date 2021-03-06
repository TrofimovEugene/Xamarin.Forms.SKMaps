﻿// **********************************************************************
// 
//   RouteRecorder.cs
//   
//   This file is subject to the terms and conditions defined in
//   file 'LICENSE.txt', which is part of this source code package.
//   
//   Copyright (c) 2018, Sylvain Gravel
// 
// ***********************************************************************

using System;
using Xamarin.Forms.SKMaps.Sample.Models;
using Xamarin.Forms.SKMaps.Sample.Extensions;
using Xamarin.Forms.SKMaps.Sample.Services.Interface;
using MvvmCross.WeakSubscription;

namespace Xamarin.Forms.SKMaps.Sample.Services
{
    public class RouteRecorder : IRouteRecorder
    {
        private ILocationTracker _LocationTracker { get; }

        public bool IsActive { get; private set; }
        public IMutableRoute ActiveRecording { get; private set; }

        private IDisposable _locationChangedSubscription;

        public RouteRecorder(ILocationTracker locationTracker)
        {
            _LocationTracker = locationTracker;

            _locationChangedSubscription = _LocationTracker.WeakSubscribe<ILocationTracker, LocationMovedEventArgs>(nameof(LocationTracker.Moved),
                                                                                                                    UserMoved);
        }

        public IMutableRoute Start()
        {
            if(ActiveRecording != null)
            {
                throw new InvalidOperationException("Cannot start a recorder more than once.");
            }

            ActiveRecording = new MutableRoute();

            if(_LocationTracker.IsTracking &&  _LocationTracker.Location != null)
            {
                ActiveRecording.AddPoint(_LocationTracker.Location.ToPosition());
            }

            IsActive = true;

            return ActiveRecording;
        }

        public void Stop()
        {
            IsActive = false;
            _locationChangedSubscription = null;
        }

        private void UserMoved(object sender, LocationMovedEventArgs e)
        {
            if (IsActive)
            {
                ActiveRecording.AddPoint(e.Location.ToPosition());
            }
        }
    }
}
