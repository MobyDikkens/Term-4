using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTree;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace BTreeVisualization.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        #region BTree Stuff

        //BTree instance
        private BTree<string, string> _tree;

        private const int _degree = 50;

        #endregion

        #region



        #endregion

        #region Binding Property Names

        private const string BTreeProperty = "BTree";


        #endregion


        #region Binding Properties

        public IEnumerable<object> BTree
        {
            get
            {
                TreeViewItem item = new TreeViewItem();

                var _root = _tree.Root;

                foreach(var entries in _root.Entries)
                {
                    yield return new Test {
                        Key = entries.Key,
                        Value = entries.Pointer,
                        Children = _root.Children
                    };
                }

            }
        }

        public class Test
        {
            public List<Node<string, string>> Children { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }

            public ObservableCollection<Test> Ch
            {
                get
                {
                    ObservableCollection<Test> test = new ObservableCollection<Test>();

                    Test t = new Test();

                    foreach (var tmp in Children)
                    {
                        foreach(var curr in tmp.Entries)
                        {
                            t.Key = curr.Key;
                            t.Value = curr.Pointer;

                            t.Children = tmp.Children;

                            test.Add(t);
                        }

                    }

                    return test;
                }
            }
        }


        #endregion


        //Constructor
        public MainWindowVM()
        {
            _tree = new BTree<string, string>(_degree);

            for(int i = 0; i < 1000; i++)
            {
                _tree.Insert(Convert.ToString(i), Convert.ToString(i));
            }

        }





    }
}
