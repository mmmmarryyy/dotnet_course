using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AnimatedSortingCubes;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private class CubeItem
    {
        public int Value { get; set; }
        public Border Cube { get; set; } = default!;
        public double Left { get; set; }
    }

    private List<CubeItem> cubes = new List<CubeItem>();
    private const double CubeWidth = 50;
    private const double CubeHeight = 50;
    private const double CubeMargin = 10;
    private const double BaseTop = 70;

    private int minValue = 0;
    private int maxValue = 1;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        string filePath = "numbers.txt";
        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            var numbers = content
                .Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(str => int.Parse(str))
                .ToList();

            if (numbers.Count > 0)
            {
                minValue = numbers.Min();
                maxValue = numbers.Max();
                if (minValue == maxValue)
                {
                    maxValue = minValue + 1;
                }
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                int val = numbers[i];
                var cube = CreateCube(val);
                double leftPos = CubeMargin + i * (CubeWidth + CubeMargin);
                Canvas.SetLeft(cube, leftPos);
                Canvas.SetTop(cube, BaseTop);
                CubeCanvas.Children.Add(cube);

                cubes.Add(new CubeItem { Value = val, Cube = cube, Left = leftPos });
            }
        }
        else
        {
            MessageBox.Show("Файл numbers.txt не найден!");
        }
    }

    private Border CreateCube(int value)
    {
        Border border = new Border
        {
            Width = CubeWidth,
            Height = CubeHeight,
            Background = new SolidColorBrush(GetColorForValue(value)),
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(5)
        };

        TextBlock text = new TextBlock
        {
            Text = value.ToString(),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontWeight = FontWeights.Bold
        };

        border.Child = text;
        return border;
    }

    private Color GetColorForValue(int value)
    {
        byte intensity = (byte)Math.Min(255, value * 20);
        return Color.FromRgb(intensity, (byte)(255 - intensity), 255);
    }

    private async void SortButton_Click(object sender, RoutedEventArgs e)
    {
        await AnimateBubbleSort();
    }

    private async Task AnimateBubbleSort()
    {
        int n = cubes.Count;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (cubes[j].Value > cubes[j + 1].Value)
                {
                    await AnimateSwapAsync(cubes[j], cubes[j + 1]);

                    var temp = cubes[j];
                    cubes[j] = cubes[j + 1];
                    cubes[j + 1] = temp;
                }
            }
        }
    }

    private async Task AnimateSwapAsync(CubeItem cubeA, CubeItem cubeB)
    {
        double posA = cubeA.Left;
        double posB = cubeB.Left;
        double durationVertical = 0.2;
        double durationHorizontal = 0.2;

        var t1 = AnimateProperty(cubeA.Cube, "(Canvas.Top)", BaseTop, BaseTop - 50, durationVertical);
        var t2 = AnimateProperty(cubeB.Cube, "(Canvas.Top)", BaseTop, BaseTop + 50, durationVertical);
        await Task.WhenAll(t1, t2);

        var t3 = AnimateProperty(cubeA.Cube, "(Canvas.Left)", posA, posB, durationHorizontal);
        var t4 = AnimateProperty(cubeB.Cube, "(Canvas.Left)", posB, posA, durationHorizontal);
        await Task.WhenAll(t3, t4);

        var t5 = AnimateProperty(cubeA.Cube, "(Canvas.Top)", BaseTop - 50, BaseTop, durationVertical);
        var t6 = AnimateProperty(cubeB.Cube, "(Canvas.Top)", BaseTop + 50, BaseTop, durationVertical);
        await Task.WhenAll(t5, t6);

        cubeA.Left = posB;
        cubeB.Left = posA;
    }

    private Task AnimateProperty(UIElement element, string propertyPath, double from, double to, double durationSeconds)
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        DoubleAnimation anim = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = TimeSpan.FromSeconds(durationSeconds)
        };

        Storyboard sb = new Storyboard();
        sb.Children.Add(anim);
        Storyboard.SetTarget(anim, element);
        Storyboard.SetTargetProperty(anim, new PropertyPath(propertyPath));

        sb.Completed += (s, e) => tcs.SetResult(true);
        sb.Begin();

        return tcs.Task;
    }
}