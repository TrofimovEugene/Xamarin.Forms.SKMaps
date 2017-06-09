﻿// **********************************************************************
// 
//   AthleteLoginControlView.xaml.cs
//   
//   This file is subject to the terms and conditions defined in
//   file 'LICENSE.txt', which is part of this source code package.
//   
//   Copyright (c) 2017, Le rond-point
// 
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using FormsSkiaBikeTracker.Models;
using FormsSkiaBikeTracker.Shared.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.WeakSubscription;
using MvvmCross.Plugins.File;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsSkiaBikeTracker.Forms.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class AthleteLoginControlView
    {
        public static readonly BindableProperty AthleteProperty = BindableProperty.Create(nameof(Athlete),
                                                                                       typeof(Athlete),
                                                                                       typeof(AthleteLoginControlView),
                                                                                       null,
                                                                                       BindingMode.OneWay,
                                                                                       null,
                                                                                       AthletePropertyChanged);
        public static readonly BindableProperty ExpandedProperty = BindableProperty.Create(nameof(Expanded),
                                                                                           typeof(bool),
                                                                                           typeof(AthleteLoginControlView),
                                                                                           false,
                                                                                           BindingMode.OneWay,
                                                                                           null,
                                                                                           ExpandedPropertyChanged);

        public static readonly BindableProperty PasswordProperty = BindableProperty.Create(nameof(Password),
                                                                                           typeof(string),
                                                                                           typeof(AthleteLoginControlView),
                                                                                           string.Empty,
                                                                                           BindingMode.OneWayToSource,
                                                                                           null,
                                                                                           PasswordPropertyChanged);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
                                                                                          typeof(ICommand),
                                                                                          typeof(AthleteLoginControlView),
                                                                                          null,
                                                                                          BindingMode.OneWay,
                                                                                          null,
                                                                                          CommandPropertyChanged);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
                                                                                                   typeof(object),
                                                                                                   typeof(AthleteLoginControlView),
                                                                                                   null,
                                                                                                   BindingMode.OneWay,
                                                                                                   null,
                                                                                                   CommandParameterPropertyChanged);

        public bool Expanded
        {
            get { return (bool)GetValue(ExpandedProperty); }
            set { SetValue(ExpandedProperty, value); }
        }

        public Athlete Athlete
        {
            get { return (Athlete)GetValue(AthleteProperty); }
            set { SetValue(AthleteProperty, value); }
        }
        
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        private double _ClosedHeightRequest { get; }

        private AthleteLoginControlViewModel _internalViewModel;
        public AthleteLoginControlViewModel InternalViewModel
        {
            get { return _internalViewModel; }
            set
            {
                if (value != InternalViewModel)
                {
                    _internalViewModel = value;

                    if (_internalViewModel != null)
                    {
                        _internalPropertyChangedSubscription = _internalViewModel.WeakSubscribe(InternalVMPropertyChanged);
                    }
                    else
                    {
                        _internalPropertyChangedSubscription = null;
                    }

                    OnPropertyChanged();
                }
            }
        }

        private MvxNotifyPropertyChangedEventSubscription _internalPropertyChangedSubscription;

        public AthleteLoginControlView()
        {
            InitializeComponent();

            _ClosedHeightRequest = HeightRequest;

            InternalViewModel = Mvx.IocConstruct<AthleteLoginControlViewModel>();
            InternalViewModel.Start();
        }

        private static void AthletePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AthleteLoginControlView view = bindable as AthleteLoginControlView;

            view.InternalViewModel.Athlete = newValue as Athlete;
        }
        
        private static void ExpandedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AthleteLoginControlView view = bindable as AthleteLoginControlView;

            view.SetupPasswordLayout();
        }
        
        private static void PasswordPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AthleteLoginControlView view = bindable as AthleteLoginControlView;

            view.PasswordEntry.Text = newValue as string;
        }

        private static void CommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AthleteLoginControlView view = bindable as AthleteLoginControlView;

            view.SubmitButton.Command = newValue as ICommand;
        }

        private static void CommandParameterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AthleteLoginControlView view = bindable as AthleteLoginControlView;

            view.SubmitButton.CommandParameter = newValue;
        }

        private void SetupPasswordLayout()
        {
            if (Expanded)
            {
                AnimateHeightRequest(PasswordEntry.Height + 1);
            }
            else
            {
                AnimateHeightRequest(-PasswordEntry.Height - 1);
            }
        }

        private void AnimateHeightRequest(double offset)
        {
            double startViewHeight = HeightRequest;
            double startPasswordHeight = HeightRequest;

            this.Animate($"Animation-{offset}",
                         p =>
                         {
                             HeightRequest = startViewHeight + p;
                             PasswordLayout.HeightRequest = startPasswordHeight + p;
                             InvalidateMeasure();
                         },
                         0,
                         offset,
                         16,
                         250,
                         Easing.CubicInOut);
        }

        private void InternalVMPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(AthleteLoginControlViewModel.Athlete))
            {
                Athlete = InternalViewModel.Athlete;
            }
            else if (args.PropertyName == nameof(AthleteLoginControlViewModel.EnteredPassword))
            {
                Password = InternalViewModel.EnteredPassword;
            }
        }
    }
}