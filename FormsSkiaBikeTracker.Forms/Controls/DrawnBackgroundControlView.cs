﻿// **********************************************************************
// 
//   DrawnBackgroundControlView.xaml.cs
//   
//   This file is subject to the terms and conditions defined in
//   file 'LICENSE.txt', which is part of this source code package.
//   
//   Copyright (c) 2017, Le rond-point
// 
// ***********************************************************************

using LRPFramework.Views.Forms;
using Xamarin.Forms;

namespace FormsSkiaBikeTracker.Forms.Controls
{
    public class DrawnBackgroundControlView : AbsoluteLayout
    {
        public static readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background),
                                                                                             typeof(DrawableView),
                                                                                             typeof(DrawnBackgroundControlView),
                                                                                             null,
                                                                                             BindingMode.OneWay,
                                                                                             null,
                                                                                             BackgroundPropertyChanged);

        public static readonly BindableProperty ForegroundProperty = BindableProperty.Create(nameof(Foreground),
                                                                                             typeof(View),
                                                                                             typeof(DrawnBackgroundControlView),
                                                                                             null,
                                                                                             BindingMode.OneWay,
                                                                                             null,
                                                                                             ForegroundPropertyChanged);

        public View Foreground
        {
            get => (View)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }


        public DrawableView Background
        {
            get => (DrawableView)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public DrawnBackgroundControlView()
        {
        }

        private static void BackgroundPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            DrawnBackgroundControlView view = bindable as DrawnBackgroundControlView;

            view.RefreshLayout();
        }

        private static void ForegroundPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            DrawnBackgroundControlView view = bindable as DrawnBackgroundControlView;

            view.RefreshLayout();
        }

        private void RefreshLayout()
        {
            SetFullSize(Background);
            SetFullSize(Foreground);
            
            Children.Clear();

            if (Background != null)
            {
                Children.Add(Background);
            }

            if (Foreground != null)
            {
                Foreground.BackgroundColor = Color.Transparent;
                Children.Add(Foreground);
            }
        }

        private void SetFullSize(Element uiElement)
        {
            if (uiElement != null)
            {
                uiElement.SetValue(LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));
                uiElement.SetValue(LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            }
        }
    }
}