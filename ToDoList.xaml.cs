using MauiAppHafta5.Model;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.Collections.ObjectModel;

namespace MauiAppHafta5;

public partial class ToDoList : ContentPage
{
	public ToDoList()
	{
		InitializeComponent();
		ctrlTodoList.ItemsSource = ListTodo;
	}

	public ObservableCollection<ToDo> ListTodo { get; set; } =
		new ObservableCollection<ToDo> {
            new ToDo() {Image = "dotnet_bot.png" ,
                        Text  = "�dev yap�lacak. Pazar son g�n. Unutma!",
                        IsDone = false},
            new ToDo() {Image = "dotnet_bot.png",
                        Text = "S�nava �al��mal�s�n. Collection view mutlaka sorulur.",
                        IsDone = true},
        };

    private async void AddTodo_Click(object sender, EventArgs e)
    {
		 var res = await DisplayPromptAsync("G�rev Ekle", "Yap�lacak:", "OK", placeholder: "yaplacaklar� buraya yaz");

		if (res != "")
		{
			ToDo todo = new ToDo(){ Image = "dotnet_bot.png", Text = res, IsDone=false};
		    ListTodo.Add(todo);
        }
    }

    private async void DeleteTodo_Click(object sender, EventArgs e)
    {
        var todo = ListTodo.First(o => o.ID == (sender as MenuItem).CommandParameter.ToString());

        var res = await DisplayAlert("Silmeyi onayla", "Silinsin mi?", "Evet", "Hay�r");

        if (res == true)
        {
            ListTodo.Remove(todo);
        }

    }

    private async void EditTodo_Click(object sender, EventArgs e)
    {
        var todo = ListTodo.First(o => o.ID == (sender as MenuItem).CommandParameter.ToString());

        if (todo != null)
        {
            var res = await DisplayPromptAsync("D�zenle", "Yap�lacak:", initialValue: todo.Text, placeholder: "yap�lacaklar� buraya yaz");     
            
            if(res != "")
            {
                todo.Text = res;
            }
        }
    }

    private async void ImageTodo_Click(object sender, EventArgs e)
    {
        var todo = ListTodo.First(o => o.ID == (sender as MenuItem).CommandParameter.ToString());
        var res = await DisplayActionSheet("Resim Se�", null, null, "Galeri", "Kamera" );
        if (res == "Galeri")
        {

            var resim = await MediaPicker.Default.PickPhotoAsync();
            todo.Image = resim.FullPath;

        }
        else if (res == "Kamera")
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var resim = await  MediaPicker.Default.CapturePhotoAsync();
                todo.Image = resim.FullPath;
            }

        }


    }
}