using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppHafta5.Model
{
    public class ToDo : INotifyPropertyChanged
    {
        string id;
        string image, text;
        bool isdone;

        public string ID
        {
            get
            {
                if (id == null)
                    id = Guid.NewGuid().ToString();
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Image { get { return image; } 
            set{ image = value;
                NotifyPropertyChanged();
            } }
        public string Text
        {
            get { return text; }
            set { text = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsDone
        {
            get { return isdone; }
            set
            {
                isdone = value;
                NotifyPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName="") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
