using System.Drawing.Drawing2D;


namespace Lab5net6
{
    internal class Marker : BaseObjects
    {
        public Action<Target> OnTargetOverlap; // Публичное событие, которое возникает при пересечении маркера с целью.
        public Marker(float x, float y,float angle): base(x,y,angle) { } // Определение конструктора класса Marker с параметрами x, y и angle
        public override void Render(Graphics canvas, bool flag = true) // Переопределение метода Render
        {
            if (flag)
            {
                // Рисование маркера красного цвета.
                canvas.FillEllipse(new SolidBrush(Color.Red), -3, -3, 6, 6);
                canvas.DrawEllipse(new Pen(Color.Red, 2), -6, -6, 12, 12);
                canvas.DrawEllipse(new Pen(Color.Red, 2), -12, -12, 24, 24);
            }
            else
            {
                // Рисование маркера белого цвета.
                canvas.FillEllipse(new SolidBrush(Color.White), -3, -3, 6, 6);
                canvas.DrawEllipse(new Pen(Color.White, 2), -6, -6, 12, 12);
                canvas.DrawEllipse(new Pen(Color.White, 2), -12, -12, 24, 24);
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath(); // Получение пути формы маркера из базового класса.
            path.AddEllipse(-3, -3, 6, 6); // Добавление эллипса в путь, представляющий маркер.
            return path; // Возвращение пути формы маркера.
        }
        public override void Overlap(BaseObjects obj)
        {
            base.Overlap(obj); // Вызов метода Overlap базового класса.
            if (obj is Target) // Проверка, является ли объект целью.
                OnTargetOverlap(obj as Target); // Вызов события пересечения маркера с целью.
        }
    }
}
