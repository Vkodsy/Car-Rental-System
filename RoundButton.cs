//After adding this , you can use the round button instead of the regular one but I didn't use it yet ... :)

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundButton : Button
{
    public int BorderRadius { get; set; } = 40; // Adjust for roundness

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        GraphicsPath graphicsPath = new GraphicsPath();

        // Create the rounded rectangle path
        graphicsPath.AddArc(0, 0, BorderRadius, BorderRadius, 180, 90);
        graphicsPath.AddArc(Width - BorderRadius, 0, BorderRadius, BorderRadius, 270, 90);
        graphicsPath.AddArc(Width - BorderRadius, Height - BorderRadius, BorderRadius, BorderRadius, 0, 90);
        graphicsPath.AddArc(0, Height - BorderRadius, BorderRadius, BorderRadius, 90, 90);
        graphicsPath.CloseAllFigures();

        this.Region = new Region(graphicsPath);

        // Remove the border look
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (Pen pen = new Pen(this.Parent.BackColor, 2))
        {
            // This ensures the edges look smooth against the background
            pevent.Graphics.DrawPath(pen, graphicsPath);
        }
    }
}
