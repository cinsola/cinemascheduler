using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaScheduler.Models
{
    public static class NavigationFactory
    {
        public static Components.BreadcrumbComponent.Step CurrentStep { get; set; }
        private static Dictionary<Components.BreadcrumbComponent.Step, ProjectPage> PagesMapping { get; set; }
        public static void Build(MainWindow mainPage)
        {
            CurrentStep = Components.BreadcrumbComponent.Step.Start;
            PagesMapping = new Dictionary<Components.BreadcrumbComponent.Step, ProjectPage>();
            PagesMapping.Add(Components.BreadcrumbComponent.Step.Start, new View.InitialView());
            PagesMapping.Add(Components.BreadcrumbComponent.Step.LoadFiles, new View.FilePickerView());
            PagesMapping.Add(Components.BreadcrumbComponent.Step.SetParameters, new View.ConfigurationManager());
            foreach(var pageKV in PagesMapping)
            {
                pageKV.Value.NextButtonStatusChanged += (s, e) =>
                {
                    mainPage.IsNextButtonEnabled = e;
                };
            }
        }

        public static System.Windows.Controls.Page GetDisplayPage(Components.BreadcrumbComponent.Step step)
        {
            return PagesMapping[step];
        }

        internal static System.Windows.Controls.Page Next()
        {
            try
            {
                int nextStep = ((int)CurrentStep) + 1;
                CurrentStep = (Components.BreadcrumbComponent.Step)nextStep;
            } catch { CurrentStep = Components.BreadcrumbComponent.Step.Start; }
            return GetDisplayPage(CurrentStep);
        }

        internal static System.Windows.Controls.Page Prev()
        {
            try
            {
                int prevStep = ((int)CurrentStep) - 1;
                if (prevStep < 0) prevStep = 0;
                CurrentStep = (Components.BreadcrumbComponent.Step)prevStep;
            }
            catch { CurrentStep = Components.BreadcrumbComponent.Step.Start; }
            return GetDisplayPage(CurrentStep);
        }
    }
}
