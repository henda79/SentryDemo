using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Sentry;
using Serilog;
using Serilog.Events;

namespace SentryDemo.Droid
{
    [Activity(Label = "SentryDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SentryXamarin.Init(options =>
            {
                options.AddXamarinFormsIntegration();

                options.Dsn = "";
#if DEBUG
                options.Debug = true;
                options.TracesSampleRate = 1.0;
#endif
            });

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Sentry(o =>
                { 
                    o.MinimumBreadcrumbLevel = LogEventLevel.Debug; 
                    o.MinimumEventLevel = LogEventLevel.Warning;
                })
                .CreateLogger();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}