using System;
using System.IO;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

namespace RevitAddin_Test4
{
    public class StartApp : IExternalApplication
    {
        static string thisAssemblyPath = typeof(StartApp).Assembly.Location;
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                createRibbonPanel(application);
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                TaskDialog.Show("IIB1 Demonstrator", e.ToString());
                return Result.Failed;
            }
        }

        private void createRibbonPanel(UIControlledApplication application)
        {
            String panelName = "Meine Revit-Funktionen";
            RibbonPanel ribbonDemoPanel = application.CreateRibbonPanel(panelName);
            PushButton myButton = (PushButton)ribbonDemoPanel
                .AddItem(new PushButtonData("Version 3", "Test 3", thisAssemblyPath, typeof(RevitAddin).FullName));
            string ButtonIconsFolder = Path.GetDirectoryName(thisAssemblyPath);
            myButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ButtonIconsFolder, "play-button.png"), UriKind.Absolute));
            myButton.ToolTip = "Klick mich!!!!";
        }

    }
}
