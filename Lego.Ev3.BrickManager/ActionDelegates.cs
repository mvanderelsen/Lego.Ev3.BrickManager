using Lego.Ev3.Framework;

namespace Lego.Ev3.BrickManager
{
    public delegate void OnDirectoryAction(object sender, Directory directory, ActionType type);

    public delegate void OnFileAction(object sender, File file, ActionType type);

}
