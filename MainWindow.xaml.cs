using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SQLitePCL; 

namespace ProjectManagementApplication
{
    public partial class MainWindow : Window
    {
        private TaskBoardContext _context;
      

        public MainWindow()
        {
            InitializeComponent();
            Batteries.Init(); // Инициализация SQLitePCL.batteries
            _context = new TaskBoardContext();
            _context.Database.EnsureCreated(); // Гарантирует, что база данных создана
            LoadTasks();
        }

        private void ButtonAddTask_Clicked(object sender, RoutedEventArgs e)
        {
            AddNewTask("ToDo");
        }

        private void AddNewTask(string status)
        {
            // Создаем новую задачу в базе данных
            var taskItem = new TaskItem
            {
                Description = "Введите описание задачи",
                Status = status
            };

            _context.TaskItems.Add(taskItem);
            _context.SaveChanges();
            Debug.WriteLine($"Добавлена задача с ID: {taskItem.ID}");

            // Создаем визуальный элемент задачи
            CreateTaskUI(taskItem);
        }

        private void CreateTaskUI(TaskItem taskItem)
        {
            var taskContainer = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(92, 90, 132)),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(5),
                Padding = new Thickness(10),
                Width = 130,
                Height = 70,
                Tag = taskItem.ID // Используем Tag для хранения ID задачи
            };

            var taskIdTextBlock = new TextBlock
            {
                Background = Brushes.Transparent,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 8,
                Text = $"BSN - {taskItem.ID}"
            };

            var taskDescriptionInput = new TextBox
            {
                Background = Brushes.Transparent,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                BorderThickness = new Thickness(0),
                VerticalContentAlignment = VerticalAlignment.Top,
                FontSize = 13,
                Text = taskItem.Description
            };


            taskDescriptionInput.GotFocus += (s, args) =>
            {
                if (taskDescriptionInput.Text == "Введите описание задачи")
                {
                    taskDescriptionInput.Text = string.Empty;
                }
            };

            taskDescriptionInput.LostFocus += (s, args) =>
            {
                if (string.IsNullOrWhiteSpace(taskDescriptionInput.Text))
                {
                    taskDescriptionInput.Text = "Введите описание задачи";
                }
            };

            // Событие для сохранения изменений описания
            taskDescriptionInput.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(taskDescriptionInput.Text) || taskDescriptionInput.Text == "Введите описание задачи")
                {
                    taskDescriptionInput.Text = "Введите описание задачи";
                }
                else
                {
                    // Обновляем описание задачи в базе данных
                    var id = (int)taskContainer.Tag;
                    var taskToUpdate = _context.TaskItems.Find(id);
                    if (taskToUpdate != null)
                    {
                        taskToUpdate.Description = taskDescriptionInput.Text;
                        _context.SaveChanges();
                    }
                }
            };

            // Переменные для отслеживания состояния перетаскивания
            Point mouseStartPoint = new Point();
            bool isDragging = false;

            // Обработка событий мыши для перетаскивания
            taskContainer.MouseLeftButtonDown += (s, e) =>
            {
                mouseStartPoint = e.GetPosition(null);
                isDragging = false;
                taskContainer.CaptureMouse();
            };

            taskContainer.PreviewMouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point currentPosition = e.GetPosition(null);
                    Vector diff = currentPosition - mouseStartPoint;

                    if (!isDragging)
                    {
                        // Проверяем, переместилась ли мышь достаточно, чтобы начать перетаскивание
                        if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                            Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                        {
                            isDragging = true;

                            // Инициируем операцию перетаскивания
                            try
                            {
                                DragDrop.DoDragDrop(taskContainer, new DataObject(typeof(Border), taskContainer), DragDropEffects.Move);
                            }
                            catch (InvalidOperationException ex)
                            {
                                Debug.WriteLine($"DragDrop exception: {ex.Message}");
                                // Опционально: уведомить пользователя или обработать ошибку иначе
                            }
                        }
                    }
                }
            };

            taskContainer.PreviewMouseLeftButtonUp += (s, e) =>
            {
                taskContainer.ReleaseMouseCapture();
            };

            taskContainer.Child = new StackPanel
            {
                Children = { taskIdTextBlock, taskDescriptionInput }
            };

            // Добавляем контекстное меню для удаления задачи
            var contextMenu = new ContextMenu();
            var deleteMenuItem = new MenuItem
            {
                Header = "Delete",
            };

            deleteMenuItem.Click += (s, e) =>
            {
                // Удаляем задачу из родительского контейнера
                var parentPanel = taskContainer.Parent as Panel;
                if (parentPanel != null)
                {
                    parentPanel.Children.Remove(taskContainer);
                }

                // Удаляем задачу из базы данных
                var id = (int)taskContainer.Tag;
                var taskToRemove = _context.TaskItems.Find(id);
                if (taskToRemove != null)
                {
                    _context.TaskItems.Remove(taskToRemove);
                    _context.SaveChanges();
                    Debug.WriteLine($"Удалена задача с ID: {id}");
                }
            };

            contextMenu.Items.Add(deleteMenuItem);
            taskContainer.ContextMenu = contextMenu;

            // Добавляем задачу в соответствующую колонку
            switch (taskItem.Status)
            {
                case "ToDo":
                    ToDo_Panel.Children.Add(taskContainer);
                    break;
                case "InProgress":
                    InProgressPanel.Children.Add(taskContainer);
                    break;
                case "CodeReview":
                    CodeReviewPanel.Children.Add(taskContainer);
                    break;
                case "QA":
                    QAPanel.Children.Add(taskContainer);
                    break;
                case "Done":
                    DonePanel.Children.Add(taskContainer);
                    break;
                default:
                    ToDo_Panel.Children.Add(taskContainer);
                    break;
            }
        }

        private void LoadTasks()
        {
            var tasks = _context.TaskItems.ToList();
            foreach (var task in tasks)
            {
                CreateTaskUI(task);
            }
        }

        private void Column_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Border)))
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                e.Effects = DragDropEffects.Move;
            }
            e.Handled = true;
        }

        private void Column_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Border)))
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                e.Effects = DragDropEffects.Move;
            }
            e.Handled = true;
        }

        private void Column_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Border)))
            {
                Border taskContainer = e.Data.GetData(typeof(Border)) as Border;
                if (taskContainer != null)
                {
                    StackPanel sourcePanel = VisualTreeHelper.GetParent(taskContainer) as StackPanel;
                    Grid targetGrid = sender as Grid;

                    if (sourcePanel != null && targetGrid != null)
                    {
                        // Находим StackPanel (панель задач) внутри targetGrid
                        StackPanel targetPanel = targetGrid.Children
                            .OfType<StackPanel>()
                            .FirstOrDefault();

                        if (targetPanel != null && sourcePanel != targetPanel)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                sourcePanel.Children.Remove(taskContainer);
                                targetPanel.Children.Add(taskContainer);
                            });

                            // Обновляем статус задачи в базе данных
                            var taskId = (int)taskContainer.Tag;
                            var taskToUpdate = _context.TaskItems.Find(taskId);
                            if (taskToUpdate != null)
                            {
                                switch (targetPanel.Name)
                                {
                                    case "ToDo_Panel":
                                        taskToUpdate.Status = "ToDo";
                                        break;
                                    case "InProgressPanel":
                                        taskToUpdate.Status = "InProgress";
                                        break;
                                    case "CodeReviewPanel":
                                        taskToUpdate.Status = "CodeReview";
                                        break;
                                    case "QAPanel":
                                        taskToUpdate.Status = "QA";
                                        break;
                                    case "DonePanel":
                                        taskToUpdate.Status = "Done";
                                        break;
                                    default:
                                        taskToUpdate.Status = "ToDo";
                                        break;
                                }
                                _context.SaveChanges();
                            }
                        }
                    }
                }
            }
            e.Handled = true;
        }
    }
}
