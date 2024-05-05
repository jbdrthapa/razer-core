using System.Diagnostics;

using Microsoft.Extensions.Logging;

namespace RazerCore.Services;

public class ShellService : IShellService
{
    private readonly ILogger<IShellService> _logger;
    private bool _disposed = false;

    public ShellService(ILogger<IShellService> logger)
    {
        _logger = logger;
    }

    public string? ExecuteCommand(string command, string[]? arguments = null)
    {
        var startInfo = new ProcessStartInfo()
        {
            FileName = command,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            WindowStyle = ProcessWindowStyle.Hidden

        };

        if (arguments != null)
        {
            startInfo.Arguments = string.Join(" ", arguments);
        }

        var process = Process.Start(startInfo);
        process?.WaitForExit();

        var output = process?.StandardOutput.ReadToEnd();
        var error = process?.StandardError.ReadToEnd();

        if (!string.IsNullOrEmpty(error))
        {
            return error;
        }

        return output;

    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {

            // Free any other managed objects here.
            //
        }

        _disposed = true;
    }

}