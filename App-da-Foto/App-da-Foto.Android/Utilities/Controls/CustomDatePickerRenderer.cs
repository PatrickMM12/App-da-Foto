using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App_da_Foto.Droid.Utilities.Controls;
using App_da_Foto.Utilities.CustomView;
using Java.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DatePicker = Xamarin.Forms.DatePicker;

[assembly: ExportRenderer(typeof(BindableDatePicker), typeof(CustomDatePickerRenderer))]
namespace App_da_Foto.Droid.Utilities.Controls
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
            public static void Init() { }
        
        public CustomDatePickerRenderer(Context context) : base(context)
        {

        }
        
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                if (Control != null)
                {
                    Control.Text = "Data de Nascimento";
                    Control.SetTextColor(Android.Graphics.Color.Rgb(103, 103, 103));
                    //if (e.NewElement.Date.Year > 1910)
                    //{
                    //    Control.SetTextColor(Android.Graphics.Color.Rgb(14, 14, 14));
                    //}
                }

                Locale locale = new Locale("pt", "BR");
                Control.TextLocale = locale;
                Resources.Configuration.SetLocale(locale);
                Android.Content.Res.Configuration config = new Android.Content.Res.Configuration
                {
                    Locale = locale
                };
                Locale.SetDefault(Locale.Category.Format, locale);
                Resources.Configuration.Locale = locale;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                GradientDrawable gd = new GradientDrawable();
                gd.SetStroke(0, Android.Graphics.Color.LightGray);
                Control.SetBackgroundDrawable(gd);
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0, 0, 0, 0);
            }
        }
    }
}