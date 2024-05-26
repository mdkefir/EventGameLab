using System.Drawing.Drawing2D;

namespace Lab5net6
{
    internal class Player : BaseObjects
    {
        public Action<Marker> OnMarkerOverlap;
        public Action<Target> OnTargetOverlap;
        public float vX, vY;

        public Player(float x,float y, float angle) : base(x,y,angle) { }
        public override void Render(Graphics canvas, bool flag = true)
        {
            if(flag) {
                // Рисование игрока синего цвета.
                canvas.FillEllipse(new SolidBrush(Color.DeepSkyBlue),-15,-15,30,30);
                canvas.DrawEllipse(new Pen(Color.Black,2), -15,-15,30,30);
                canvas.DrawLine(new Pen(Color.Black,2),0,0,25,0);
            }
            else
            {
                // Рисование игрока белого цвета.
                canvas.FillEllipse(new SolidBrush(Color.White), -15, -15, 30, 30);
                canvas.DrawEllipse(new Pen(Color.White, 2), -15, -15, 30, 30);
                canvas.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0);
            }
        } 
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath(); // Получение пути формы игрока из базового класса.
            path.AddEllipse(-15, -15, 30, 30); // Добавление эллипса в путь, представляющий игрока.
            return path; // Возвращение пути формы игрока.
        }
        public override void Overlap(BaseObjects obj)
        {
            base.Overlap(obj); // Вызов метода базового класса для обработки пересечения
            if (obj is Marker) OnMarkerOverlap(obj as Marker); // Проверка, является ли объект маркером, и вызов соответствующего делегата.
            if (obj is Target) OnTargetOverlap(obj as Target); // Проверка, является ли объект целью, и вызов соответствующего делегата.
        }
        
    }
}
