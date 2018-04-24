using System;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;

namespace Windows_Insight_App.Model
{
    /// <summary>
    /// Handles the saving of screenshots and getting screen coordinates.
    /// </summary>
    class ScreenshotManager
    {
        /// <summary>
        /// Save a screenshot if there's a screenshot to save.
        /// </summary>
        public static void SaveShot()
        {
            Rect bounds = ProcessManager.UnsafeRect();
            if (bounds.Width == 0 || bounds.Height == 0)
            {
                return;
            }
            SaveScreenPosition(bounds);
        }

        /// <summary>
        /// Attempt to get a process's XY coordinate value.
        /// </summary>
        /// <returns>Two-element integer array of X and Y.</returns>
        public static int[] GetProcessXY()
        {
            int[] xy = { 0, 0 };
            Rect bounds = ProcessManager.UnsafeRect();
            if (bounds.Width == 0 || bounds.Height == 0)
            {
                return xy;
            }
            xy[0] = (int)bounds.X;
            xy[1] = (int)bounds.Y;
            return xy;
        }

        /// <summary>
        /// Gets the screen position to save to a screenshot.
        /// </summary>
        /// <param name="bounds">The parameters of the screen to capture.</param>
        private static void SaveScreenPosition(Rect bounds)
        {
            int w = Convert.ToInt16(bounds.Width);
            int h = Convert.ToInt16(bounds.Height);
            int l = Convert.ToInt16(bounds.Left);
            int t = Convert.ToInt16(bounds.Top);

            System.Drawing.Size convertSize = new System.Drawing.Size(w, h);

            using (Bitmap btmp = new Bitmap(w, h))
            {
                using (Graphics g = Graphics.FromImage(btmp))
                {
                    g.CopyFromScreen(new System.Drawing.Point(
                        l, t), System.Drawing.Point.Empty,
                        convertSize);
                }

                string filepath = SaveShotAt();
                if (filepath != string.Empty)
                {
                    btmp.Save(filepath, ImageFormat.Jpeg);
                }
            }
        }

        // TODO : need to not show this if a default save location has already been chosen.
        /// <summary>
        /// Save the screenshot where and named how the user wishes.
        /// </summary>
        /// <returns>The filepath to save a screenshot.</returns>
        private static string SaveShotAt()
        {
            string filepath = string.Empty;

            Microsoft.Win32.SaveFileDialog dlg =
                new Microsoft.Win32.SaveFileDialog();

            dlg.DefaultExt = "jpeg";
            dlg.Filter = "Jpeg Files|*.jpg";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                filepath = dlg.FileName;
            }

            return filepath;
        }

    }
}
