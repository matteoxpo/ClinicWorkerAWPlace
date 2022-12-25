using System;
using System.Threading.Tasks;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using ReactiveUI;

namespace Presentation.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public async Task ShowUncatchedExceptionMessageBox(Exception ex)
    {
        var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
            new MessageBoxStandardParams
            {
                ContentTitle = "Ошибка",
                ContentMessage = "Не обработанная ошибка, обратитесь, пожалуйста в поддержку" + "\ndoc.crm.help@awp.com" + $"\n{ex.Message}"
            });
        await messageBoxStandardWindow.Show();
    }
    
    public async Task ShowExceptionMessageBox(string contet)
    {
        var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
            new MessageBoxStandardParams
            {
                ContentTitle = "Ошибка",
                ContentMessage = contet
            });
        await messageBoxStandardWindow.Show();
    }
    
    public async Task ShowExceptionMessageBox(Exception ex)
    {
        var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
            new MessageBoxStandardParams
            {
                ContentTitle = "Ошибка",
                ContentMessage = ex.Message
            });
        await messageBoxStandardWindow.Show();
    }
    
    
}