using System.Drawing.Drawing2D;

namespace Lab5net6
{
    internal class Target : BaseObjects
    {
        private float R=70; // Радиус цели
        public Target(float x, float y, float angle) : base(x, y, angle) { }
        public override void Render(Graphics canvas,bool flag=true)
        {
            if (flag)
            {
                // Рисование цели зеленого цвета.
                canvas.FillEllipse(new SolidBrush(Color.SpringGreen), -1*(R/2), -1 * (R / 2), R, R);
            }
            else
            {
                // Рисование цели белого цвета.
                canvas.FillEllipse(new SolidBrush(Color.White), -1 * (R / 2), -1 * (R / 2), R, R);
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath(); // Получение пути формы цели из базового класса.
            path.AddEllipse(-1 * (R / 2), -1 * (R / 2), R, R); // Добавление эллипса в путь, представляющий цель.
            return path; // Возвращение пути формы цели.
        }

        public override bool Exists() { return (R > 0) ? true : false; } // Возвращает true, если радиус больше 0, иначе false.

        public override void ChangeR() { R -= 0.6f; } // Уменьшает радиус цели на 0.6.
    }
}
