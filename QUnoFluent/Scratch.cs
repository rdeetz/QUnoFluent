                    < ListView x: Name = "listSource" Margin = "16" Width = "128" Height = "128" BorderThickness = "1" BorderBrush = "White" AllowDrop = "True" CanDragItems = "True" CanReorderItems = "True" >
                        < ListView.ItemTemplate >
                            < DataTemplate >
                                < Border BorderBrush = "Gray" BorderThickness = "1" Padding = "5" Margin = "2" >
                                    < TextBlock Text = "{Binding}" FontSize = "16" Foreground = "DarkRed" />
                                </ Border >
                            </ DataTemplate >
                        </ ListView.ItemTemplate >
                    </ ListView >
                    < ListView x: Name = "listTarget" Margin = "16" Width = "128" Height = "128" BorderThickness = "1" BorderBrush = "White" AllowDrop = "True" CanDragItems = "True" CanReorderItems = "True" >
                        < ListView.ItemTemplate >
                            < DataTemplate >
                                < Border BorderBrush = "Gray" BorderThickness = "1" Padding = "5" Margin = "2" >
                                    < TextBlock Text = "{Binding}" FontSize = "16" Foreground = "DarkGreen" />
                                </ Border >
                            </ DataTemplate >
                        </ ListView.ItemTemplate >
                    </ ListView >

            this.listSource.ItemsSource = this.sourceItems;
this.listTarget.ItemsSource = this.targetItems;

this.sourceItems.Add("Item 1");
this.sourceItems.Add("Item 2");
this.sourceItems.Add("Item 3");

this.listSource.DragItemsStarting += this.ListSource_DragItemsStarting;
this.listTarget.DragOver += this.ListTarget_DragOver;
this.listTarget.Drop += this.ListTarget_Drop;

private ObservableCollection<string> sourceItems = new ObservableCollection<string>();
private ObservableCollection<string> targetItems = new ObservableCollection<string>();

private async void ListTarget_Drop(object sender, DragEventArgs e)
{
    // This test is in theory not needed as we returned DataPackageOperation.None if
    // the DataPackage did not contained text. However, it is always better if each
    // method is robust by itself
    if (e.DataView.Contains(StandardDataFormats.Text))
    {
        // We need to take a Deferral as we won't be able to confirm the end
        // of the operation synchronously
        var def = e.GetDeferral();
        var s = await e.DataView.GetTextAsync();
        var items = s.Split('\n');
        foreach (var item in items)
        {
            this.targetItems.Add(item);
            this.sourceItems.Remove(item);
        }
        e.AcceptedOperation = DataPackageOperation.Move;
        def.Complete();
    }
}

private void ListTarget_DragOver(object sender, DragEventArgs e)
{
    e.DragUIOverride.Caption = "Play card";
    e.DragUIOverride.IsGlyphVisible = false;
    e.AcceptedOperation = (e.DataView.Contains(StandardDataFormats.Text)) ? DataPackageOperation.Move : DataPackageOperation.None;
}

private void ListSource_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
{
    // Prepare a string with one dragged item per line
    var items = new StringBuilder();
    foreach (var item in e.Items)
    {
        if (items.Length > 0) items.AppendLine();
        items.Append(item as string);
    }
    // Set the content of the DataPackage
    e.Data.SetText(items.ToString());
    // As we want our Reference list to say intact, we only allow Copy
    e.Data.RequestedOperation = DataPackageOperation.Move;
}

    using Microsoft.UI.Xaml.Controls;
    using System.Text;
    using Windows.ApplicationModel.DataTransfer;
    using System.Collections.ObjectModel;
