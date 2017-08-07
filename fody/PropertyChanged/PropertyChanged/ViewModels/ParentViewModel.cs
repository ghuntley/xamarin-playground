using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;

namespace PropertyChanged
{
    public class ParentViewModel : INotifyPropertyChanged
    {
        public string ParentGivenName { get; set; }
        public string FamilyName { get; set; }

        /// <summary>
        /// https://github.com/Fody/PropertyChanged/wiki/Attributes#dependsonattribute
        /// Injects this property to be notified when a dependent property is set.
        /// </summary>
        [DependsOn(nameof(ParentGivenName), nameof(FamilyName))]
        public string ParentFullName => $"{ParentGivenName} {FamilyName}";
        public string ChildFullName => $"{Child.ChildGivenName} {FamilyName}";

        public ChildViewModel Child { get; set; }

        public ParentViewModel()
        {
            Child = new ChildViewModel();

            Task.Factory.StartNew(async () =>
            {
                while(true)
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                    {
                        FamilyName = DateTime.Now.DayOfWeek.ToString();
                        ParentGivenName = DateTime.Now.ToString();
                        Child.ChildGivenName = DateTime.UtcNow.ToString();
                    });

                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
