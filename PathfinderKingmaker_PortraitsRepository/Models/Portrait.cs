using System.Reactive;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace PathfinderKingmaker_PortraitsRepository.Models;

public sealed class Portrait
{
    public const string MediumImageName = "Medium.png";
    public string Name { get; set; }
    public string DirPath { get; set; }
    public Bitmap MediumImage { get; set; }

    public ReactiveCommand<Portrait, Unit> DeleteCommand { get; set; }
}