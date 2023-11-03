using System;
using System.Collections.Generic;
using System.Drawing;

namespace SnakeGame
{
    public partial class GameWindow : Form
    {
        private const int SnakeSize = 10;
        private const int FoodSize = 10;
        private const int TimerInterval = 100;

        private readonly Random _random = new Random();
        private readonly List<Point> _snake = new List<Point>();
        private Point _food;

        private int _score;
        private bool _gameOver;

        public GameWindow()
        {
            InitializeComponent();
            StartGame();
            Input.Initialize(this);
        }

        private void StartGame()
        {
            _snake.Clear();
            _snake.Add(new Point(0, 0));
            _food = GenerateFoodLocation();
            _score = 0;
            _gameOver = false;
            timer.Start();
        }

        private void EndGame()
        {
            _gameOver = true;
            timer.Stop();
            MessageBox.Show($"Game over! Your score is {_score}.");
        }

        private void UpdateSnake()
        {
            var head = _snake[0];
            var direction = GetDirection();
            var newHead = new Point(head.X + direction.X * SnakeSize, head.Y + direction.Y * SnakeSize);

            if (newHead == _food)
            {
                _snake.Insert(0, newHead);
                _food = GenerateFoodLocation();
                _score++;
            }
            else if (_snake.Contains(newHead) || newHead.X < 0 || newHead.X >= ClientSize.Width || newHead.Y < 0 || newHead.Y >= ClientSize.Height)
            {
                EndGame();
            }
            else
            {
                _snake.Insert(0, newHead);
                _snake.RemoveAt(_snake.Count - 1);
            }
        }

        private Point GetDirection()
        {
            if (Input.LeftPressed)
            {
                return new Point(-1, 0);
            }
            else if (Input.RightPressed)
            {
                return new Point(1, 0);
            }
            else if (Input.UpPressed)
            {
                return new Point(0, -1);
            }
            else if (Input.DownPressed)
            {
                return new Point(0, 1);
            }
            else
            {
                return new Point(0, 0);
            }
        }

        private Point GenerateFoodLocation()
        {
            var x = _random.Next(ClientSize.Width / SnakeSize) * SnakeSize;
            var y = _random.Next(ClientSize.Height / SnakeSize) * SnakeSize;
            return new Point(x, y);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateSnake();
            Invalidate();
        }

        private void GameWindow_Paint(object sender, PaintEventArgs e)
        {
            if (_gameOver)
            {
                return;
            }

            e.Graphics.FillRectangle(Brushes.Red, _food.X, _food.Y, FoodSize, FoodSize);

            for (int i = 0; i < _snake.Count; i++)
            {
                var brush = i == 0 ? Brushes.Green : Brushes.YellowGreen;
                e.Graphics.FillRectangle(brush, _snake[i].X, _snake[i].Y, SnakeSize, SnakeSize);
            }
        }
    }
}