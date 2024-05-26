using Lab5net6.Objects;

namespace Lab5net6
{
    public partial class MainForm : Form
    {
        private int targetCount = 2; // Переменная для хранения количества целей в игре
        Random rnd = new Random(); // Создание объекта Random для генерации случайных чисел
        List<BaseObjects> objects = new(); // Создание списка объектов игры
        /*BlackSpace blackSpace; // Объект пространства, через которое перемещаются объекты*/
        Player player; // Объект игрока
        Marker marker; // Объект маркера
        Target target; // Объект цели
        private int score = 0; // Переменная для хранения текущего количества очков игрока
        public MainForm()
        {
            InitializeComponent();
            // Создание объекта пространства и его настройка
            /*blackSpace = new BlackSpace(PictureBox.Width / 4, PictureBox.Height, 0);
            blackSpace.Width = PictureBox.Width / 4;
            blackSpace.Height = PictureBox.Height;
            objects.Add(blackSpace); // Добавление объекта пространства в список объектов игры*/


            // Создание объекта игрока и его настройка
            player = new Player(PictureBox.Width / 2, PictureBox.Height / 2, 0);
            player.OnOverlap += (p, obj) => // Добавление реакции на пересечение игрока с другим объектом
            {
                Logs.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + Logs.Text;
            };
            // добавил реакцию на пересечение с маркером
            player.OnMarkerOverlap += (m) =>
            {
                // Если достиг, то удаляем маркер из оригинального objects
                objects.Remove(m);
                marker = null; // И обнуляем маркер
            };
            player.OnTargetOverlap += (t) => // Добавление реакции на пересечение игрока с целью
            {
                ScoreLabel.Text = "Очки: " + ++score; // Увеличение счета игрока
                objects.Remove(t); // Удаление цели из списка объектов
                t = null; // Обнуление ссылки на цель
                t = new Target(rnd.Next() % PictureBox.Width, rnd.Next() % PictureBox.Height, 0); // Создание новой цели
                objects.Add(t); // Добавление новой цели в список объектов
            };
            objects.Add(player); // Добавление объекта игрока в список объектов


            // Создание и добавление целей в список объектов игры
            for (int i = 0; i < targetCount; i++)
            {
                target = new Target(rnd.Next() % PictureBox.Width, rnd.Next() % PictureBox.Height, 0);
                objects.Add(target);
            }

            // Создание маркера и добавление его в список объектов игры
            marker = new Marker(PictureBox.Width / 2 + 150, PictureBox.Height / 2, 0);
            objects.Add(marker);
        }

        // Метод для обработки события перерисовки PictureBox
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            var canvas = e.Graphics;
            canvas.Clear(Color.White); // Очистка холста000,,
            /*updateSpace(); // Обновление пространства*/
            updatePlayer(); // Обновление игрока
            // Обновляем позицию игрока
            // Меняем тут objects на objects.ToList()
            // Это будет создавать копию списка
            // И позволит модифицировать оригинальный objects прямо из цикла foreach
            foreach (var curr in objects.ToList()) // Перебор всех объектов игры
            {
                if (curr != player && player.Overlaps(curr, canvas)) // Проверка на пересечение игрока с другим объектом
                {
                    player.Overlap(curr); // Обработка пересечения игрока с объектом
                    curr.Overlap(player); // Обработка пересечения объекта с игроком
                }
                if (curr is Target) // Проверка, является ли текущий объект целью
                {
                    if (curr.Exists()) { curr.ChangeR(); } // Изменение размера цели
                    else // Если цель больше не существует
                    {
                        objects.Remove(curr); // Удаление цели из списка объектов
                        target = null; // Обнуление ссылки на цель
                        target = new Target(rnd.Next() % PictureBox.Width, rnd.Next() % PictureBox.Height, 0); // Создание новой цели
                        objects.Add(target); // Добавление новой цели в список объектов
                    }
                }
            }
            foreach (var curr in objects.ToList()) // Перебор всех объектов игры
            {
                canvas.Transform = curr.GetTransform(); // Установка новой матрицы трансформации для отображения объекта
                /*if (curr != blackSpace && blackSpace.Overlaps(curr, canvas)) curr.Render(canvas, false); // Отрисовка объекта*/
                curr.Render(canvas); // Отрисовка объекта
            }
        }

        // Метод для обработки события таймера
        private void timer1_Tick(object sender, EventArgs e)
        {
            PictureBox.Invalidate(); // Вызов перерисовки PictureBox
        }

        // Метод для обработки события клика мыши на PictureBox
        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            // Тут добавил создание маркера по клику если он еще не создан
            if (marker == null)
            {
                marker = new Marker(0, 0, 0); // Создание нового маркера
                objects.Add(marker); // Добавление маркера в список объектов
            }
            marker.X = e.X; // Установка положения маркера по горизонтали
            marker.Y = e.Y; // Установка положения маркера по вертикали
        }

        // Метод для обновления положения игрока
        private void updatePlayer()
        {
            // Проверка, существует ли маркер
            if (marker != null)
            {
                // Расчет вектора между игроком и маркером
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = (float)Math.Sqrt(dx * dx + dy * dy); // Нахождение длины вектора
                dx /= length; // Нормализация координат вектора
                dy /= length;
                // Пересчитываем координаты игрока
                // Мы используем вектор dx, dy
                // Как вектор ускорения (точнее даже вектор притяжения),
                // Который притягивает игрока к маркеру
                // 0.5 коэффициент, подобранный на глаз, который дает
                // Естественное ощущение движения
                player.vX += dx * 0.5f; // Изменение скорости игрока по горизонтали
                player.vY += dy * 0.5f; // Изменение скорости игрока по вертикали

                // Расчет угла поворота игрока
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }
            // Тормозящий момент,
            // Нужен для того, чтобы когда игрок достигнул маркера, то произошло постепенное замедление
            player.vX += -player.vX * 0.1f; // Применение тормозящего момента к скорости игрока по горизонтали
            player.vY += -player.vY * 0.1f; // Применение тормозящего момента к скорости игрока по вертикали

            // Пересчет позиции игрока с помощью вектора скорости
            player.X += player.vX; // Изменение положения игрока по горизонтали
            player.Y += player.vY; // Изменение положения игрока по вертикали
        }

        // Метод для обновления положения пространства
        /*private void updateSpace()
        {
            // Проверка, если область вышла за правую границу, вернуть ее налево
            if (blackSpace.X > (4 + 3 + 7 + 18) + PictureBox.Width + PictureBox.Width / 4) blackSpace.X = -PictureBox.Width / 4;
            blackSpace.vX = 3f; // Устанавливаем скорость по X
            blackSpace.X += blackSpace.vX; // Обновляем положение по X

        }*/

        private void PictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}