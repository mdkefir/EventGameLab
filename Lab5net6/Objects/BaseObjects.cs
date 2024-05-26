using System.Drawing.Drawing2D;


namespace Lab5net6
{
    internal class BaseObjects
    {
        // Поля для хранения координат и угла объекта
        public float X;
        public float Y;
        public float Angle;

        // Поле делегата, к которому можно привязать реакцию на пересечение объектов
        public Action<BaseObjects, BaseObjects> OnOverlap;

        // Конструктор объекта, принимающий начальные координаты и угол
        public BaseObjects(float x, float y, float angle)
        {
            X = x;
            Y = y;
            this.Angle = angle;
        }

        // Метод для получения матрицы преобразования объекта
        public Matrix GetTransform()
        {
            // Вытаскиваем матрицу преобразования Graphics
            var matrix = new Matrix();
            matrix.Translate(X, Y); // Смещение в указанные координаты
            matrix.Rotate(Angle); // Поворот на указанный угол
            return matrix; // Возвращение готовой матрицы
        }
        // Виртуальный метод для отрисовки объекта на Graphics
        public virtual void Render(Graphics canvas,bool flag=true){}

        // Виртуальный метод для отрисовки объекта белым цветом
        public virtual void WhiteRender(Graphics canvas) { }

        // Виртуальный метод для получения графического пути объекта
        public virtual GraphicsPath GetGraphicsPath() { 
            return new GraphicsPath(); // Создание пустого графического пути
        }
        // Так как пересечение учитывает толщину линий и матрицы трансформацией
        // То для того чтобы определить пересечение объекта с другим объектом
        // Надо передать туда объект Graphics
        public virtual bool Overlaps( BaseObjects obj, Graphics canvas)
        {
            // Получение графических путей текущего и переданного объектов
            var path1 =this.GetGraphicsPath();
            var path2=obj.GetGraphicsPath();
            // Применение матрицы преобразования к графическим путям
            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());
            // Используем класс Region, который позволяет определить 
            // Пересечение объектов в данном графическом контексте
            var region = new Region(path1);
            region.Intersect(path2); // Пересекаем формы
            return !region.IsEmpty(canvas); // Возвращение true, если регион не пуст (произошло пересечение), иначе false
        }

        // Виртуальный метод для обработки пересечения с другим объектом
        public virtual void Overlap(BaseObjects obj)
        {
            // Проверка, что у объекта есть привязанные функции к событию пересечения
            if (this.OnOverlap != null)
            { // Если к полю есть привязанные функции
                this.OnOverlap(this, obj); // То вызываем их
            }
        }
        // Виртуальный метод для проверки существования объекта
        public virtual bool Exists() { return true; }

        // Виртуальный метод для изменения размера объекта (пустой по умолчанию)
        public virtual void ChangeR() { }
    }
}
