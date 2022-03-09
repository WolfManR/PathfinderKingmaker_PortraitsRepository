using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace PathfinderKingmaker_PortraitsRepository.Models;

public sealed class PortraitsRepository
{
    private readonly string _baseUrl = @"Owlcat Games\Pathfinder Kingmaker\Portraits";

    public IEnumerable<Portrait> GetImages()
    {
        DirectoryInfo dir = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low", _baseUrl));
        foreach (var portraitDir in dir.EnumerateDirectories())
        {
            Portrait portrait = new()
            {
                Name = portraitDir.Name,
                DirPath = portraitDir.FullName
            };

            var files = portraitDir.GetFiles().ToList();

            if (files.FirstOrDefault(f => f.Name == Portrait.MediumImageName) is { } mediumImage)
            {
                portrait.MediumImage = new(mediumImage.FullName);
                files.Remove(mediumImage);
            }

            yield return portrait;
        }
    }

    public bool DeletePortrait(Portrait portrait)
    {
        if (Directory.Exists(portrait.DirPath))
        {
            try
            {
                Directory.Delete(portrait.DirPath, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        return false;
    }
}