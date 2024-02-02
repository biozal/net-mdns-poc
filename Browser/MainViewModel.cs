using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Makaretu.Dns;

namespace Browser;

public partial class MainViewModel 
    : ObservableObject
{
    [ObservableProperty] 
    private ObservableCollection<string> _messages;
    
    public MainViewModel()
    {
        Messages = new();
        Messages.Add("Starting Browser...");
        LoadDebuggingBrowser();
        LoadBrowser();
    }

    private void LoadDebuggingBrowser()
    {
        ServiceDiscovery sd = new ();
        sd.ServiceDiscovered += (s, serviceName) =>
        {
            MainThread.InvokeOnMainThreadAsync(() => Messages.Add($"{DateTime.Now}::DEBUG: found serviceName {serviceName}"));    
        };
        
        sd.ServiceInstanceDiscovered += (s, instance) =>
        {
            MainThread.InvokeOnMainThreadAsync(() => Messages.Add($"{DateTime.Now}::DEBUG: found InstanceName {instance.ServiceInstanceName} has {instance.Message.Answers.Count} DNS Answers and {instance.Message.AdditionalRecords.Count} Additional DNS Records"));    
        };
    }

    private void LoadBrowser()
    {
        string serviceRecordName = "_cbliteptp._tcp.local";
        var mdns = new MulticastService();
        mdns.NetworkInterfaceDiscovered += (s, e) => mdns.SendQuery(serviceRecordName);
        mdns.AnswerReceived += (s, e) =>
        {
            var msg = e.Message;
            
            if (msg.Answers.OfType<PTRRecord>().Any(x => x.Name.ToString().Contains(serviceRecordName)))
            {
                var srv = msg.AdditionalRecords.OfType<SRVRecord>().FirstOrDefault();
                var aRecord = msg.AdditionalRecords.OfType<ARecord>().FirstOrDefault();
                
                if (srv != null && aRecord != null)
                {
                    var txt = msg.Answers.OfType<TXTRecord>().FirstOrDefault();
                    var ipAddress = aRecord.Address.ToString();
                    var host = srv.Target.ToString();
                    var port = srv.Port;
                    var name = txt?.Name; 
                    var url = $"http://{ipAddress}:{port}/";
                    MainThread.InvokeOnMainThreadAsync(() => Messages.Add($"{DateTime.Now}::Log:Found {name} on host {host} - can connect on @ {url}"));
                }
            }
        };
        mdns.Start();
    }
}