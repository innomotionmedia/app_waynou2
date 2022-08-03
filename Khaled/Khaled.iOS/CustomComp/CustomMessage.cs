using System;
using Foundation;
using Khaled.iOS.CustomComp;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CustomMessage))]
namespace Khaled.iOS.CustomComp
{
	public class CustomMessage : Khaled.CustomComps.IMessage
	{
        const double LONG_DELAY = 8;
        const double MEDIUM_DELAY = 5;

        const double SHORT_DELAY = 3.5;

        NSTimer alertDelay;
        UIAlertController alert;

        //Implementierung der Methoden aus IMessage
        public void LongAlert(string message)
        {
            ShowAlert(message, MEDIUM_DELAY);
        }
        public void ShortAlert(string message)
        {
            ShowAlert(message, SHORT_DELAY);
        }


        public void VeryLongAlert(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }

        //Da es in IOS kein Toast gibt, wird hier eine Komponente von IOS verwendent. Hier könnte natürlich auch eine Custom Component stehen.
        void ShowAlert(string message, double seconds)
        {

            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }


            Device.BeginInvokeOnMainThread(() => {

                //Create Alert
                var okAlertController = UIAlertController.Create("", message, UIAlertControllerStyle.Alert);

                //Add Action
                okAlertController.AddAction(UIAlertAction.Create("okay", UIAlertActionStyle.Default, null));

                // Present Alert
                vc.PresentViewController(okAlertController, true, null);

                // alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
                // {
                //     dismissMessage();
                // });
                // alert = UIAlertController.Create("", message, UIAlertControllerStyle.Alert);
                //
                // var window = UIApplication.SharedApplication.Delegate.GetWindow();
                // window.RootViewController.PresentViewController(alert, true, null);
            });
        }


    }
}

