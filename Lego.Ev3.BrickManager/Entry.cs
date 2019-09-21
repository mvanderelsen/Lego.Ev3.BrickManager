using Lego.Ev3.Framework;

namespace Lego.Ev3.BrickManager
{
    public class Entry
    {
        public EntryType Type { get; set; }

        public bool IsPlayable { get; set; }

        public Entry(string path, EntryType type)
        {
            Type = type;
            switch(type)
            {
                case EntryType.FILE:
                    {
                        IsPlayable = path.EndsWith(".rsf");
                        break;
                    }
            }
        }

        public static explicit operator Entry(File file)
        {
            return new Entry(file.Path, EntryType.FILE);
        }

        public static explicit operator Entry(Directory directory)
        {
            return new Entry(directory.Path, EntryType.DIRECTORY);
        }

    }

    public enum EntryType
    {
        FILE,
        DIRECTORY
    }
}
