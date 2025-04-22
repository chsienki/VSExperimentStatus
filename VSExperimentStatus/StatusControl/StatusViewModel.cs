using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace VSExperimentStatus
{
    internal class StatusViewModel : INotifyPropertyChanged
    {
        private readonly FeatureFlagsService flagsService;

        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Flag> flags = [];

        public ObservableCollection<Flag> Flags { get => flags; set => flags = value; }

        public StatusViewModel(FeatureFlagsService flagsService)
        {
            this.flagsService = flagsService;
            flagsService.OnFlagsChanged += (o, e) => Dispatcher.CurrentDispatcher.Invoke(UpdateFlags);
            UpdateFlags();
        }

        private void UpdateFlags()
        {
            Flags.Clear();
            foreach (var flag in flagsService.Flags)
            {
                Flags.Add(new(flag.FriendlyName, flag.Value ? "On" : "Off"));
            }
            PropertyChanged?.Invoke(this, new (nameof(Flags)));
        }

        public record Flag(string Name, string Value);
    }
}
