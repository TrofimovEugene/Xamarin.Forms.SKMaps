﻿// **********************************************************************
// 
//   LrpFormsDroidPagePresenter.cs
//   
//   This file is subject to the terms and conditions defined in
//   file 'LICENSE.txt', which is part of this source code package.
//   
//   Copyright (c) 2017, Le rond-point
// 
// ***********************************************************************
using FormsSkiaBikeTracker.Forms.UI;
using MvvmCross.Droid.Views;
using Xamarin.Forms;

namespace FormsSkiaBikeTracker.Droid
{
    public class LrpFormsAndroidPagePresenter : LrpFormsPagePresenter, IMvxAndroidViewPresenter
    {
        public LrpFormsAndroidPagePresenter() : base()
        {

        }

        public LrpFormsAndroidPagePresenter(Application mvxFormsApp) : base(mvxFormsApp)
        {
        }

        protected override void PlatformRootViewInitialization(Page rootPage)
        {
        }
    }
}