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
                        Text  = "Ödev yapýlacak. Pazar son gün. Unutma!",
                        IsDone = false},
            new ToDo() {Image = "dotnet_bot.png",
                        Text = "Sýnava çalýþmalýsýn. Collection view mutlaka sorulur.",
                        IsDone = true},
        };

    private async void AddTodo_Click(object sender, EventArgs e)
    {
		 var res = await DisplayPromptAsync("Görev Ekle", "Yapýlacak:", "OK", placeholder: "yaplacaklarý buraya yaz");

		if (res != "")
		{
			ToDo todo = new ToDo(){ Image = "dotnet_bot.png", Text = res, IsDone=false};
		    ListTodo.Add(todo);
        }
    }

    private async void DeleteTodo_Click(object sender, EventArgs e)
    {
        var todo = ListTodo.First(o => o.ID == (sender as MenuItem).CommandParameter.ToString());

        var res = await DisplayAlert("Silmeyi onayla", "Silinsin mi?", "Evet", "Hayýr");

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
            var res = await DisplayPromptAsync("Düzenle", "Yapýlacak:", initialValue: todo.Text, placeholder: "yapýlacaklarý buraya yaz");     
            
            if(res != "")
            {
                todo.Text = res;
            }
        }
    }

    private async void ImageTodo_Click(object sender, EventArgs e)
    {
        var todo = ListTodo.First(o => o.ID == (sender as MenuItem).CommandParameter.ToString());
        var res = await DisplayActionSheet("Resim Seç", null, null, "Galeri", "Kamera" );
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