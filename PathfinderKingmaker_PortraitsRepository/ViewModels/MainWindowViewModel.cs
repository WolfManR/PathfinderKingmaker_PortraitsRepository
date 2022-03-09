using Avalonia.Threading;

using PathfinderKingmaker_PortraitsRepository.Models;

using ReactiveUI;

using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;

namespace PathfinderKingmaker_PortraitsRepository.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly PortraitsRepository _repository;
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            _repository = new();
            Portraits = new();

            LoadImagesCommand = ReactiveCommand.CreateFromTask(LoadImages);
            DeletePortraitCommand = ReactiveCommand.Create<Portrait>(portrait =>
            {
                if (_repository.DeletePortrait(portrait))
                {
                    Selected = Portraits[Portraits.IndexOf(portrait)];
                    Portraits.Remove(portrait);
                }
            });
        }

        private async Task<Unit> LoadImages()
        {
            await Task.Run(() =>
            {
                foreach (var portrait in _repository.GetImages())
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        portrait.DeleteCommand = DeletePortraitCommand;
                        Portraits.Add(portrait);
                    });
                }
            });

            return Unit.Default;
        }

        private Portrait _selected;
        public Portrait Selected { get => _selected; set => this.RaiseAndSetIfChanged(ref _selected, value); }

        public ObservableCollection<Portrait> Portraits { get; }

        public ReactiveCommand<Unit, Unit> LoadImagesCommand { get; set; }
        public ReactiveCommand<Portrait, Unit> DeletePortraitCommand { get; set; }
    }
}
