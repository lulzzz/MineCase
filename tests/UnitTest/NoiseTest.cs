﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using ImageSharp;
using MineCase.Algorithm.Noise;
using Xunit;

namespace MineCase.UnitTest
{
    public class NoiseTest
    {
        public readonly string RootDir;

        public NoiseTest()
        {
            RootDir = SetRootDir();
        }

        private static string SetRootDir([CallerFilePath]string fileName = null) =>
            Path.Combine(Path.GetDirectoryName(fileName), "bin");

        [Fact]
        public void TestPerlinNoise3D()
        {
            const int xExtent = 100;
            const int yExtent = 100;

            using (var file = File.OpenWrite(Path.Combine(RootDir, "PerlinNoise3D.bmp")))
            using (var image = new Image<ImageSharp.PixelFormats.Rgb24>(xExtent, yExtent))
            {
                var noise = new PerlinNoise(100);
                for (int x = 0; x < xExtent; x++)
                {
                    for (int y = 0; y < yExtent; y++)
                    {
                        var color = (byte)(noise.Noise(x / 10.0, 0, y / 10.0) * 255);
                        image[x, y] = new ImageSharp.PixelFormats.Rgb24(color, color, color);
                    }
                }

                image.SaveAsBmp(file);
            }
        }

        [Fact]
        public void TestOctavedPerlinNoise3D()
        {
            const int xExtent = 100;
            const int yExtent = 100;

            using (var file = File.OpenWrite(Path.Combine(RootDir, "OctavedPerlinNoise3D.bmp")))
            using (var image = new Image<ImageSharp.PixelFormats.Rgb24>(xExtent, yExtent))
            {
                var noise = new OctavedNoise<PerlinNoise>(new PerlinNoise(100), 8, 1);
                for (int x = 0; x < xExtent; x++)
                {
                    for (int y = 0; y < yExtent; y++)
                    {
                        var color = (byte)(noise.Noise(x / 10.0, 0, y / 10.0) * 255);
                        image[x, y] = new ImageSharp.PixelFormats.Rgb24(color, color, color);
                    }
                }

                image.SaveAsBmp(file);
            }
        }
    }
}