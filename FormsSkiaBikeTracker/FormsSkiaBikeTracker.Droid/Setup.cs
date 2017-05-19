﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.Content;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Presenter.Droid;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform.IoC;
using FormsSkiaBikeTracker.Forms.UI.Views;
using LRPLib.Mvx.Droid;

namespace FormsSkiaBikeTracker.Droid
{
    public class Setup : LrpDroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            bool alwaysOutput = false;

#if DEBUG
            alwaysOutput = true;
#endif

            return new LrpDroidDebugTrace(alwaysOutput);
        }

        protected override void InitializePlatformServices()
        {
            base.InitializePlatformServices();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            MvxFormsDroidPagePresenter presenter = new MvxFormsDroidPagePresenter();
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

            return presenter;
        }

        protected override IEnumerable<Assembly> GetViewAssemblies()
        {
            var result = base.GetViewAssemblies();

            return result.Append(typeof(LoadingPage).Assembly).ToList();
        }

        protected override IMvxNameMapping CreateViewToViewModelNaming()
        {
            return new MvxPostfixAwareViewToViewModelNameMapping("View", "Page");
        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
            };
        }
    }
}