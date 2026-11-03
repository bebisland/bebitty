using System.Collections;
using System.Text;
using Porta.Pty;

internal static class Pty
{
    public static readonly IPtyConnection? Connection;

    static Pty()
    {
        try
        {
            Terminal.Reply = Write;

            var environment = new Dictionary<string, string>();

            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
                environment[(string)entry.Key] = (string?)entry.Value ?? "";

            environment["TERM"]         = "xterm-256color";
            environment["COLORTERM"]    = "truecolor";
            environment["TERM_PROGRAM"] = "bebitty";

            var options = new PtyOptions
            {
                App         = Environment.GetEnvironmentVariable("BEBITTY_SHELL") ?? "fish",
                CommandLine = [],
                Cwd         = Environment.CurrentDirectory,
                Cols        = Status.Cols,
                Rows        = Status.Rows,
                Environment = environment
            };

            Connection = PtyProvider.SpawnAsync(options, CancellationToken.None).GetAwaiter().GetResult();

            var readerThread = new Thread
            (
                () =>
                {
                    var buffer = new byte[16384];

                    while (true)
                    {
                        var bytesRead = Connection.ReaderStream.Read(buffer, 0, buffer.Length);
                        if (bytesRead <= 0)
                            break;

                        Terminal.XTerm.Write(buffer.AsSpan(0, bytesRead));
                    }

                    if (Interlocked.Exchange(ref Status.ExitSignalled, 1) != 0)
                        return;

                    Status.Exited = true;
                }
            ) { IsBackground = true, Name = "pty-reader" };

            readerThread.Start();
        }
        catch (Exception exception)
        {
            Terminal.XTerm.Write($"\r\n\e[31mShell başlatılamadı: {exception.Message}\e[0m\r\n");
        }
    }

    private static void Write(string text)
    {
        Write(Encoding.UTF8.GetBytes(text));
    }

    private static void Write(byte[] data)
    {
        var connection = Connection;

        if (connection == null)
            return;

        connection.WriterStream.Write(data, 0, data.Length);
        connection.WriterStream.Flush();
    }

    public static void Send(string data)
    {
        Write(data);
    }
}