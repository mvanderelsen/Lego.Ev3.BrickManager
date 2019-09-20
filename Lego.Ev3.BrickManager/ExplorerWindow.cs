using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class ExplorerWindow : UserControl
    {

        private Directory CURRENT_DIRECTORY;

        public ExplorerWindow()
        {
            InitializeComponent();
            folderTree.DirectoryAction += Execute;
            directoryContentPane.DirectoryAction += Execute;
            navigationBar.DirectoryAction += Execute;
            directoryContentPane.FileAction += Execute;
        }

        #region initialize
        public async Task Initialize()
        {
            Directory root = await FileExplorer.GetDirectory(FileExplorer.ROOT_PATH);
            CURRENT_DIRECTORY = root;
            await folderTree.Initialize(root);
            await previewPane.Initialize(root);
            navigationBar.Initialize();
            DirectoryContent content = await DirectoryContent.Get(root);
            directoryContentPane.Set(content);
            statusBar.Set(content.Info);
        }

        public void InitView()
        {
            folderTree.InitView();
        }
        #endregion


        private async void Execute(object sender, Directory directory, ActionType type)
        {
            switch (type)
            {
                case ActionType.OPEN:
                    {
                        if (CURRENT_DIRECTORY.Path == directory.Path) return; // do not open again is already open
                        CURRENT_DIRECTORY = directory;
                        statusBar.SetLoading();
                        ((Manager)Parent).UseWaitCursor = true;
                        DirectoryContent content = await DirectoryContent.Get(directory);
                        previewPane.Set(directory, content.Info);
                        directoryContentPane.Set(content);
                        statusBar.Set(content.Info);
                        navigationBar.Set(directory);
                        ((Manager)Parent).UseWaitCursor = false;
                        break;
                    }
                case ActionType.SELECT:
                    {
                        previewPane.Set(directory, null);
                        break;
                    }
            }
        }


        private async void Execute(object sender, File file, ActionType type)
        {
            switch (type)
            {
                case ActionType.SELECT:
                    {
                        await previewPane.Set(file);
                        break;
                    }
            }
        }
    }
}
