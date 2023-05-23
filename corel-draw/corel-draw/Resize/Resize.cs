using corel_draw.Figures;
using System.Drawing;

namespace corel_draw.Resize
{
    public class Resize
    {
        private System.Drawing.Rectangle boundingBox;
        
        public System.Drawing.Rectangle BoundingBox
        {
            get => boundingBox;
            private set => boundingBox = value;
        }
        
        public Resize(System.Drawing.Rectangle boundingBox) => this.boundingBox = boundingBox;

        public void ResizeTo(Point cursorPosition, Figure figure)
        {
            int left = boundingBox.Left - 20;
            int right = boundingBox.Right + 20;
            int top = boundingBox.Top - 20;
            int bottom = boundingBox.Bottom + 20;

            if (left <= cursorPosition.X && cursorPosition.X <= right &&
                top <= cursorPosition.Y && cursorPosition.Y <= bottom)
            {
                int newWidth = boundingBox.Width;
                int newHeight = boundingBox.Height;
                int newX = boundingBox.X;
                int newY = boundingBox.Y;

                if (cursorPosition.X < boundingBox.Left + 20)
                {
                    newWidth = boundingBox.Width + boundingBox.Left - cursorPosition.X;
                    newX = cursorPosition.X;
                    figure.Location = new Point(newX,figure.Location.Y);
                }
                else if (cursorPosition.X > boundingBox.Right - 20) 
                    newWidth = cursorPosition.X - boundingBox.Left;

                if (cursorPosition.Y < boundingBox.Top + 20) 
                {
                    newHeight = boundingBox.Height + boundingBox.Top - cursorPosition.Y;
                    newY = cursorPosition.Y;
                    figure.Location = new Point(figure.Location.X, newY);
                }
                else if (cursorPosition.Y > boundingBox.Bottom - 20) 
                    newHeight = cursorPosition.Y - boundingBox.Top;

                figure.Width = newWidth;
                figure.Height = newHeight;
                figure.Location = new Point(newX,newY);
                boundingBox = new System.Drawing.Rectangle(newX, newY, newWidth, newHeight);
            }
        }
    }
}
