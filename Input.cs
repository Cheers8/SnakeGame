using System.Windows.Forms;

public static class Input
{
    public static bool LeftPressed { get; private set; }
    public static bool RightPressed { get; private set; }
    public static bool UpPressed { get; private set; }
    public static bool DownPressed { get; private set; }

    public static void Initialize(Form form)
    {
        form.KeyDown += Form_KeyDown;
        form.KeyUp += Form_KeyUp;
    }

    private static void Form_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:
                LeftPressed = true;
                break;
            case Keys.Right:
                RightPressed = true;
                break;
            case Keys.Up:
                UpPressed = true;
                break;
            case Keys.Down:
                DownPressed = true;
                break;
        }
    }

    private static void Form_KeyUp(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:
                LeftPressed = false;
                break;
            case Keys.Right:
                RightPressed = false;
                break;
            case Keys.Up:
                UpPressed = false;
                break;
            case Keys.Down:
                DownPressed = false;
                break;
        }
    }
}
