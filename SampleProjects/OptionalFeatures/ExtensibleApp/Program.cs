using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace ExtensibleApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            List<IFeature> _installedFeatures = loadPlugins();

            Application.Run(new Form1(_installedFeatures));
        }

        private static List<IFeature> loadPlugins()
        {
            string[] _pluginFiles = Directory.GetFiles(Application.StartupPath, "*Plugin.dll");

            List<IFeature> _featureList = new List<IFeature>(_pluginFiles.Length);

            foreach (string pluginFile in _pluginFiles)
            {
                IFeature _feature = loadPluginModule(pluginFile);

                if(_feature != null)
                    _featureList.Add(_feature);
            }

            return _featureList;
        }

        private static IFeature loadPluginModule(string filePath)
        {
            Assembly _asm = Assembly.LoadFile(filePath);

            Type _featureType = findInterface(typeof(IFeature), _asm);

            if(_featureType != null)
                return (IFeature)Activator.CreateInstance(_featureType);

            return null;
        }

        private static Type findInterface(Type typToFind, Assembly asm)
        {
            Type _foundType = null;
            Type[] _types = asm.GetExportedTypes();

            foreach (Type typ in _types)
            {
                if (!typ.IsClass)
                    continue;

                Type[] _interfaces = typ.GetInterfaces();
                if (_interfaces.Length == 0)
                    continue;

                for (int i = 0; i < _interfaces.Length; i++)
                    if (_interfaces[i] == typToFind)
                    {
                        _foundType = typ;
                        break;
                    }

                if (_foundType != null)
                    break;
            }

            return _foundType;
        }
    }
}
